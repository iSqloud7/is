using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Web.Data;
using LibraryApp.Domain.Models.Domain;
using System.Security.Claims;
using LibraryApp.Domain.Models.Relationship;
using LibraryApp.Domain.Models.Domain.DTO;
using LibraryApp.Service.Interface;

namespace LibraryApp.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IShoppingCartService shopCartService;

        public BooksController(IBookService bookService, IShoppingCartService shopCartService)
        {
            this.bookService = bookService;
            this.shopCartService = shopCartService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(this.bookService.GetAll);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.bookService.GetByID(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,ID,CreatedOn, Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.ID = Guid.NewGuid();

                this.bookService.Create(book);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.bookService.GetByID(id.Value);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Author,ID,CreatedOn, Image")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.bookService.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.bookService.GetByID(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            this.bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _context.Books.Any(e => e.ID == id);
        }

        // GET: Books/AddBookToCart/5
        [HttpGet]
        public async Task<IActionResult> AddBookToCart(Guid? ID)
        {
            if (ID == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var book = this.bookService.GetByID(ID.Value);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // POST: Books/AddBookToCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBookToCart(AddBookToCartDTO DTO)
        {
            if (DTO == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var book = this.bookService.GetByID(DTO.BookID);

            if(book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userCart = this.shopCartService.GetByOwner(userID);

            if(userCart == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.BooksInShoppingCarts.Add(new BooksInShoppingCart
            {
                BookID = DTO.BookID,
                ShoppingCartID = userCart.ID,
                Quantity = DTO.Quantity
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

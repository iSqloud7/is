using LibraryApp.Repository.Interface;
using LibraryApp.Service.Interface;
using LibraryApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCart? GetByOwner(string ownerID)
        {
            return this.shoppingCartRepository.Get(selector: cart => cart, filter: cart => cart.OwnerID != null && ownerID.Equals(ownerID));
        }
    }
}

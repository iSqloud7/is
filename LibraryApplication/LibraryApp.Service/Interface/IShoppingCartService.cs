using LibraryApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByOwner(string ownerID);
    }
}

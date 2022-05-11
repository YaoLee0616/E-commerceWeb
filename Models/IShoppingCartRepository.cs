using System.Collections.Generic;

namespace E_commerceWeb.Models
{
    public interface IShoppingCartRepository
    {
        void AddOrUpdate(int id,string name);
        int Sum();
        List<ShoppingCart> SelectAll();
        void Clear();
    }
}

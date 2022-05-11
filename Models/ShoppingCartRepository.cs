using System.Collections.Generic;

namespace E_commerceWeb.Models
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        List<ShoppingCart> list = new List<ShoppingCart> { };

        public void AddOrUpdate(int id,string name)
        {
            bool flag = false;
            foreach (var item in list)
            {
                if(item.GoodsId== id)
                {
                    item.Count++;
                    flag = true;
                }
            }
            if(!flag)
            {
                ShoppingCart cart = new ShoppingCart();
                cart.GoodsId = id;
                cart.GoodsName = name;
                cart.Count = 1;
                list.Add(cart);
            }
        }

        public int Sum()
        {
            int sum = 0;
            foreach (var item in list)
            {
                    sum += item.Count;
            }
            return sum;
        }

        public List<ShoppingCart> SelectAll()
        {
            return list;
        }

        public void Clear()
        {
            list.Clear();
        }

    }
}
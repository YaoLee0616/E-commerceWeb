using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public interface IOrderRepository
    {
        int Add(Order order);
        IQueryable<Order> SelectAll();
        void Clear();
        void DeleteOrderById(int id);
        void UpdateOrderById(Order order);
        Order SelectOrderById(int id);
    }
}

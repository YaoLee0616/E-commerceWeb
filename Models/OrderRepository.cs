using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext context;

        public OrderRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public int Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order.OrderId;
        }

        public IQueryable<Order> SelectAll()
        {
            return context.Orders.Include(e => e.GoodsLists);
        }

        public void Clear()
        {
            context.Orders.RemoveRange(context.Orders);
        }

        public void DeleteOrderById(int id)
        {
            Order order = context.Orders.Find(id);
            context.Orders.Remove(order);
            context.SaveChanges();
        }
        public Order SelectOrderById(int id)
        {
            return context.Orders.Find(id);
        }

        public void UpdateOrderById(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }
    }
}

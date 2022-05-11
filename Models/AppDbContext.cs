using Microsoft.EntityFrameworkCore;
namespace E_commerceWeb.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<GoodsList> GoodsLists { get; set; }
    }
}

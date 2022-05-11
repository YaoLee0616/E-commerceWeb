namespace E_commerceWeb.Models
{
    public class GoodsListRepository : IGoodsListRepository
    {
            
        private AppDbContext context;

        public GoodsListRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public void Add(GoodsList goodsList)
        {
            context.GoodsLists.Add(goodsList);
            context.SaveChanges();
        }

    }
}
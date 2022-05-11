using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public class GoodsRepository : IGoodsRepository
    {
        private AppDbContext context;

        public GoodsRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Goods> SelectAll()
        {
            return context.Goods.Include(e=>e.GoodsType);
        }

        public void CreateTestData()
        {
            context.Goods.RemoveRange(context.Goods);
            Random rd = new Random();
            for(int i = 1; i <= 100; i++)
            {
                Goods goods = new Goods();
                goods.GoodsName = "商品" + i;
                goods.GoodsType = context.Types.OrderBy(x => Guid.NewGuid()).First();
                goods.GoodsPurchasePrice = rd.Next(1, 100);
                goods.GoodsRetailPrice = rd.Next(1, 200);
                context.Goods.Add(goods);
            }
            context.SaveChanges();
        }

        public void UpdateGoodsById(Goods goods)
        {
            context.Goods.Update(goods);
            context.SaveChanges();
        }

        public void DeleteGoodsById(int id)
        {
            Goods goods = context.Goods.Find(id);
            context.Goods.Remove(goods);
            context.SaveChanges();
        }

        public void AddGoods(Goods goods)
        {
            context.Goods.Add(goods);
            context.SaveChanges();
        }

        public Goods SelectGoodsById(int id)
        {
            return context.Goods.Find(id);
        }
    }
}

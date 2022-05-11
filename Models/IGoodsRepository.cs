using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public interface IGoodsRepository
    {
        IQueryable<Goods> SelectAll();
        void CreateTestData();
        Goods SelectGoodsById(int id);
        void UpdateGoodsById(Goods goods);
        void DeleteGoodsById(int id);
        void AddGoods(Goods goods);
    }
}

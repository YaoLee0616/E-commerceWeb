namespace E_commerceWeb.Models
{
    public class Goods
    {
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public int TypeId { get; set; }
        public Type GoodsType { get; set; }
        public int GoodsPurchasePrice { get; set; }
        public int GoodsRetailPrice { get; set; }
    }
}

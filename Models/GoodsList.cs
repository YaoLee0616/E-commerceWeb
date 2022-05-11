using System.ComponentModel.DataAnnotations;

namespace E_commerceWeb.Models
{
    public class GoodsList
    {
        [Key]
        public int ListId { get; set; }
        public int GoodsId { get; set; }
        public int GoodsCount { get; set; }
        public int OrderId { get; set; }
    }
}
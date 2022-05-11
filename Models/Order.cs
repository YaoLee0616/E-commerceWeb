using System.Collections.Generic;

namespace E_commerceWeb.Models
{
    public class Order
    { 
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int OrderStatus { get; set; }
        public int ShipmentsStatus { get; set; }
        public IEnumerable<GoodsList> GoodsLists { get; set; }
    }
}

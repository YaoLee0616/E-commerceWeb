using System.Collections.Generic;

namespace E_commerceWeb.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public IEnumerable<Goods> Goods { get; set; }
    }
}

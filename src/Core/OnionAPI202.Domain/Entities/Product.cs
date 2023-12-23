using OnionAPI202.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Domain.Entities
{
    public class Product : NamedBaseEntity
    {
        public decimal Price { get; set; }
        public string SKU { get; set; } = null!;
        public string Description { get; set; } = null!;



        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductColor>? ProductColors { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; } = null!;




    }
}

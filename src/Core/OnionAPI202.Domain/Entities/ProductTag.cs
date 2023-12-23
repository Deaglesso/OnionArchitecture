using OnionAPI202.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Domain.Entities
{
    public class ProductTag:BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int TagId { get; set; }

        public Tag Tag { get; set; } = null!;
    }
}

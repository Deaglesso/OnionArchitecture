using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.DTOs.Product
{

    public record UpdateProductDTO(string Name, decimal Price, string? Description, string SKU, int CategoryId, ICollection<int>? ColorIds, ICollection<int> TagIds);

}

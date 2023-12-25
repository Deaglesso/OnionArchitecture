using OnionAPI202.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.DTOs.Product
{
    public record DetailedProductDTO(int Id, string Name, decimal Price, string SKU,string? Description,int CategoryId ,IncludeCategoryDTO Category);
    
}

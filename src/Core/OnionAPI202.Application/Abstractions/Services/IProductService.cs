using OnionAPI202.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDTO>> GetAllAsync(int page, int limit);
    }
}

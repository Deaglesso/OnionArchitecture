using OnionAPI202.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDTO>> GetAllAsync(int page, int limit);
        Task<GetCategoryDTO> GetAsync(int id);
        Task CreateAsync(CreateCategoryDTO categoryDTO);
        Task UpdateAsync(int id, CreateCategoryDTO categoryDTO);
        Task DeleteAsync(int id);
    }
}

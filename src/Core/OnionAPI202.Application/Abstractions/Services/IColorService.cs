using OnionAPI202.Application.DTOs.Colors;
using OnionAPI202.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<GetColorDTO>> GetAllAsync(int page, int take);
        Task<GetColorDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDTO categoryDto);
        Task UpdateAsync(int id, UpdateColorDTO categoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task RecoverAsync(int id);

    }
}

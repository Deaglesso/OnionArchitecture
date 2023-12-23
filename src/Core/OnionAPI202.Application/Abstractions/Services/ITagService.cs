using OnionAPI202.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<GetTagDTO>> GetAllAsync(int page, int take);
        Task CreateAsync(CreateTagDTO categoryDto);
        Task UpdateAsync(int id, UpdateTagDTO categoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}

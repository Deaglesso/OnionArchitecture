using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Colors;
using OnionAPI202.Application.DTOs.Colors;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    public class ColorService:IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetColorDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Color> Colors = await _repository.GetAllWhereAsync(skip: (page - 1) * limit, limit: limit, isTracked: false).ToListAsync();
            var ColorDTOs = _mapper.Map<ICollection<GetColorDTO>>(Colors);
            return ColorDTOs;
        }

        public async Task CreateAsync(CreateColorDTO ColorDTO)
        {
            await _repository.AddAsync(_mapper.Map<Color>(ColorDTO));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Color Color = await _repository.GetByIdAsync(id);
            if (Color is null) throw new Exception("Not found");
            _repository.Delete(Color);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateColorDTO ColorDTO)
        {
            Color Color = await _repository.GetByIdAsync(id);
            if (Color is null) throw new Exception("Not found");
            _mapper.Map(ColorDTO, Color);
            _repository.Update(Color);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Color Color = await _repository.GetByIdAsync(id, true);
            if (Color is null) throw new Exception();
            _repository.SoftDelete(Color);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetColorDTO> GetByIdAsync(int id)
        {
            Color Color = await _repository.GetByIdAsync(id);
            GetColorDTO dto = _mapper.Map<GetColorDTO>(Color);
            return dto;
        }
        public async Task RecoverAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id, ignoreQuery: true);
            if (color is null) throw new Exception();
            _repository.Recover(color);
            await _repository.SaveChangesAsync();
        }
    }
}

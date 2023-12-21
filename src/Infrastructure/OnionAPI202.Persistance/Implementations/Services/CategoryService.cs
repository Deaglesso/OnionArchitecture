using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Categories;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCategoryDTO> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("not found");

            return new GetCategoryDTO(category.Id, category.Name);
            
        }

        public async Task<ICollection<GetCategoryDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1) * limit, limit: limit).ToListAsync();
            ICollection<GetCategoryDTO> dtos = _mapper.Map<ICollection<GetCategoryDTO>>(categories);
            return dtos;
        }

        public async Task CreateAsync(CreateCategoryDTO categoryDTO)
        {
            
            await _repository.AddAsync(_mapper.Map<Category>(categoryDTO));
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CreateCategoryDTO categoryDTO)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("not found");

            category.Name = categoryDTO.Name;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}


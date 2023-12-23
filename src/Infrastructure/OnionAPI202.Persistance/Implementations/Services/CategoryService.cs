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
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetCategoryDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1) * limit, limit: limit,isTracked: false).ToListAsync();
            var categoryDTOs = _mapper.Map<ICollection<GetCategoryDTO>>(categories);
            return categoryDTOs;
        }

        public async Task CreateAsync(CreateCategoryDTO categoryDTO)
        {
            await _repository.AddAsync(_mapper.Map<Category>(categoryDTO));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _mapper.Map(categoryDTO, category);
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception();
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();
        }
    }
}


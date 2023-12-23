using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Tags;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetTagDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * limit, limit: limit, isTracked: false).ToListAsync();
            var tagDTOs = _mapper.Map<ICollection<GetTagDTO>>(tags);
            return tagDTOs;
        }

        public async Task CreateAsync(CreateTagDTO tagDTO)
        {
            await _repository.AddAsync(_mapper.Map<Tag>(tagDTO));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTagDTO tagDTO)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            _mapper.Map(tagDTO, tag);
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public Task SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

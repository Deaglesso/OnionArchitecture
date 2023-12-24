using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    internal class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetProductDTO>> GetAllAsync(int page,int limit)
        {
            return _mapper.Map<IEnumerable<GetProductDTO>>(await _repository.GetAllWhereAsync(skip: (page - 1) * limit, limit: limit).ToListAsync());
        }
    }
}

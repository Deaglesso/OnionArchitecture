using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Product;
using OnionAPI202.Domain.Entities;
using OnionAPI202.Persistance.Implementations.Repositories;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;

        public ProductService(IProductRepository repository, IMapper mapper,ICategoryRepository categoryRepository,IColorRepository colorRepository,ITagRepository tagRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IEnumerable<GetProductDTO>> GetAllAsync(int page,int limit)
        {
            return _mapper.Map<IEnumerable<GetProductDTO>>(await _repository.GetAllWhereAsync(skip: (page - 1) * limit, limit: limit).ToListAsync());
        }
        public async Task<DetailedProductDTO> GetByIdAsync(int id)
        {
            Product product = await _repository.GetByIdAsync(id,includes:nameof(Product.Category));
            DetailedProductDTO dto = _mapper.Map<DetailedProductDTO>(product);
            return dto;
        }
        public async Task CreateAsync(CreateProductDTO dto)
        {
            if (await _repository.IsExistAsync(p => p.Name == dto.Name)) throw new Exception("Exist");

            if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId)) throw new Exception("Doesnt exist");

            Product product = _mapper.Map<Product>(dto);

            product.ProductColors = new List<ProductColor>();

            if(dto.ColorIds is not null)
            {
                foreach (var cId in dto.ColorIds)
                {
                    if (!await _colorRepository.IsExistAsync(c => c.Id == cId)) throw new Exception();
                    product.ProductColors.Add(new ProductColor
                    {
                        ColorId = cId,
                    });
                }
            }
            product.ProductTags = new List<ProductTag>();

            if (dto.TagIds is not null)
            {
                foreach (var tId in dto.TagIds)
                {
                    if (!await _tagRepository.IsExistAsync(t => t.Id == tId)) throw new Exception();
                    product.ProductTags.Add(new ProductTag
                    {
                        TagId = tId,
                    });
                }
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            
        }
        public async Task SoftDeleteAsync(int id)
        {
            Product product = await _repository.GetByIdAsync(id, true);
            if (product is null) throw new Exception();
            _repository.SoftDelete(product);
            await _repository.SaveChangesAsync();
        }
        public async Task RecoverAsync(int id)
        {
            Product product = await _repository.GetByIdAsync(id, ignoreQuery: true);
            if (product is null) throw new Exception();
            _repository.Recover(product);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id,UpdateProductDTO dto)
        {
            string[] include = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}" };
            Product existed = await _repository.GetByIdAsync(id, includes: include,isTracked:true);
            if (existed is null) throw new Exception("Not found");


            if (dto.Name != existed.Name)
                if (await _repository.IsExistAsync(p => p.Name == dto.Name))
                    throw new Exception("Name already used");
            
            
            if (dto.CategoryId != existed.CategoryId)
                if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId))
                    throw new Exception("Category Not Found");
            //123
            //234

            existed = _mapper.Map(dto, existed);

            existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colid => pc.ColorId == colid)).ToList();
            foreach (var colorId in dto.ColorIds)
            {
                if (!await _colorRepository.IsExistAsync(x => x.Id == colorId)) throw new Exception("Color not found.");
                if (!existed.ProductColors.Any(pc => pc.ColorId == colorId))
                {
                    existed.ProductColors.Add(new ProductColor
                    {
                        ColorId = colorId
                    });
                }
            }

            existed.ProductTags = existed.ProductTags.Where(pt => dto.TagIds.Any(tagid => pt.TagId == tagid)).ToList();
            foreach (var tagid in dto.TagIds)
            {
                if (!await _tagRepository.IsExistAsync(x => x.Id == tagid)) throw new Exception("Tag not found.");
                if (!existed.ProductTags.Any(pt => pt.TagId == tagid))
                {
                    existed.ProductTags.Add(new ProductTag
                    {
                        TagId = tagid
                    });
                }
            }

            _repository.Update(existed);
            await _repository.SaveChangesAsync();

        }
    }
}

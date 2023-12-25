using AutoMapper;
using OnionAPI202.Application.DTOs.Product;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Mapping
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductDTO,Product>().ReverseMap();
            CreateMap<Product, DetailedProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

        }
    }
}

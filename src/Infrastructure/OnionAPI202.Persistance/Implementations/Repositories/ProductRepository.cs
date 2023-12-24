using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Domain.Entities;
using OnionAPI202.Persistance.DAL;
using OnionAPI202.Persistance.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db) : base(db)
        {
        }
    }
}

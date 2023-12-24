using Microsoft.EntityFrameworkCore;
using OnionAPI202.Domain.Entities;
using OnionAPI202.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder builder) where T : BaseEntity, new()
        {
            builder.Entity<T>().HasQueryFilter(x => x.DeletedAt == null);
        }
        public static void ApplyQueryFilters(this ModelBuilder builder)
        {
            builder.ApplyFilter<Product>();
            builder.ApplyFilter<Category>();
            builder.ApplyFilter<Color>();
            builder.ApplyFilter<Tag>();
            builder.ApplyFilter<ProductColor>();
            builder.ApplyFilter<ProductTag>();


        }
    }
}

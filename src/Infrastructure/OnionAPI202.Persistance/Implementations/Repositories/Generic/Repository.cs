using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Repositories.Generic;
using OnionAPI202.Domain.Entities.Common;
using OnionAPI202.Persistance.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity,new()
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _table;


        public Repository(AppDbContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        

        public IQueryable<T> GetAllAsync(bool isTracked = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (isTracked) query = query.AsNoTracking();
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;

        }

        public IQueryable<T> GetAllWhereAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int limit = 0, bool isTracked = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;



            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (orderExpression is not null)
            {
                if (isDescending == false)
                {
                    query = query.OrderBy(orderExpression);
                }
                else
                {
                    query = query.OrderByDescending(orderExpression);
                }
            }

            if (skip != 0) query = query.Skip(skip);
            if (limit != 0) query = query.Take(limit);

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return isTracked ? query : query.AsNoTracking();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracked = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracked) query = query.AsNoTracking();
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        

        public async Task<T> GetByIdAsync(int id, bool isTracked = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x => x.Id == id);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracked) query = query.AsNoTracking();
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return  await query.FirstOrDefaultAsync();

        }

        public void Recover(T entity)
        {
            entity.DeletedAt = null;
            Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void SoftDelete(T entity)
        {
            entity.DeletedAt = DateTime.Now;
            Update(entity); 
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}

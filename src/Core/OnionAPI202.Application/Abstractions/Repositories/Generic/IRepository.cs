using OnionAPI202.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAllAsync(bool isTracked = false, bool ignoreQuery = false, params string[] includes);
        IQueryable<T> GetAllWhereAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int limit = 0, bool isTracked = false, bool ignoreQuery = false, params string[] includes);

        Task<T> GetByIdAsync(int id, bool isTracked = false, bool ignoreQuery = false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracked = false, bool ignoreQuery = false, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T,bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void Recover(T entity);

        Task SaveChangesAsync();

    }
}

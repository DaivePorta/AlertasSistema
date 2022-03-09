using AlertasUnicorn.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlertasUnicorn.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        //Task DeleteBy(Expression<Func<T, bool>> predicate);
        //IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        Task AddRange(IEnumerable<T> entities);
        Task RemoveRange(IEnumerable<T> entities);
    }
}

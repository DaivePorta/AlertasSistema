using AlertasUnicorn.Domain.Entities;
using AlertasUnicorn.Domain.Repositories;
using AlertasUnicorn.Infrastructure.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlertasUnicorn.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ConnectionProvider _connection;

        public BaseRepository(ConnectionProvider connection)
        {
            _connection = connection;
        }

        public abstract Task<IEnumerable<T>> GetAll();

        public abstract Task<T> GetById(int id);

        public abstract Task Insert(T entity);

        public abstract Task Update(T entity);

        public abstract Task Delete(int id);

        //public async Task DeleteBy(Expression<Func<T, bool>> predicate)
        //{
        //    T entity = await GetBy(predicate).FirstOrDefaultAsync().ConfigureAwait(false);
        //    _entities.Remove(entity);
        //}

        //public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        //{
        //    return _entities.Where(predicate);
        //}

        public abstract Task AddRange(IEnumerable<T> entities);

        public abstract Task RemoveRange(IEnumerable<T> entities);
    }
}

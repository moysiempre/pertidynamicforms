using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FormsAdminGP.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> FindEntityBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> FindEntityAsNoTrackingBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        Task<int> SaveChanges();

    }
}

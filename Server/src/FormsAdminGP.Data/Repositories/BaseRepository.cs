using FormsAdminGP.Domain;
using FormsAdminGP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FormsAdminGP.Core.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _entities;
        protected readonly DbSet<T> _idbset;
        public string UserName { get; set; }

        public BaseRepository(DbContext context)
        {
            _entities = context;
            _idbset = context.Set<T>();
        }


        public virtual async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var set = _idbset.AsQueryable();
            if (includes != null)
                set = includes.Aggregate(set, (current, include) => current.Include(include));

            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var set = _idbset.Where(predicate);
            if (includes != null)
                set = includes.Aggregate(set, (current, include) => current.Include(include));
            return await set.AsNoTracking().ToListAsync();
        }

        public async Task<T> FindEntityBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var set = _idbset.Where(predicate);
            if (includes != null)
                set = includes.Aggregate(set, (current, include) => current.Include(include));

            return await set.FirstOrDefaultAsync();
        }

        public async Task<T> FindEntityAsNoTrackingBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var set = _idbset.AsNoTracking().Where(predicate);
            if (includes != null)
                set = includes.Aggregate(set, (current, include) => current.Include(include));

            return await set.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> SQLQuery(string sql, params object[] parameters)
        {
            var list = await _idbset.FromSql(sql, parameters).ToListAsync();
            return list;
        }

        public virtual T Add(T entity)
        {
            var entry = _entities.Entry(entity);
            _idbset.Add(entity);
            SetPropertiesLog();
            return entity;

        }

        public virtual void Delete(T entity)
        {
            _idbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            var entry = _entities.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            entry.State = EntityState.Modified;
            SetPropertiesLog();
        }

        public async virtual Task<int> SaveChanges()
        {
            int result = 0;
            using (var transaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    result = await _entities.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.InnerException == null) throw ex;
                    throw ex.InnerException.GetBaseException();
                }
            }

            return result;
        }

        private void SetPropertiesLog()
        {
 

            var modifiedEntries = _entities.ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity
                   && (x.State == (EntityState)EntityState.Added || x.State == (EntityState)EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.IsActive = true;
                        entity.CreatedBy = UserName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        _entities.Entry(entity).Property("CreatedBy").IsModified = false;
                        _entities.Entry(entity).Property("CreatedDate").IsModified = false;

                        entity.UpdatedBy = UserName;
                        entity.UpdatedDate = now;
                    }

                }

            }

        }
    }
}

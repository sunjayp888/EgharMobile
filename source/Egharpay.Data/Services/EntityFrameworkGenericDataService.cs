using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Data.Extensions;
using Egharpay.Data.Interfaces;
using Egharpay.Entity.Dto;
using System.Data.SqlClient;

namespace Egharpay.Data.Services
{
    public class EntityFrameworkGenericDataService : IGenericDataService<DbContext>
    {
        public DbContext Context { get; set; }

        #region Create

        public void Create<T>(T entity) where T : class
        {
            Context.Set<T>().Add(entity);
        }

        public void CreateRange<T>(IEnumerable<T> t) where T : class
        {
            Context.Set<T>().AddRange(t);
        }

        #endregion

        #region Retrieve

        public virtual T RetrieveById<T>(int Id) where T : class
        {
            return Context.Set<T>().Find(Id);
        }

        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Context.Set<T>().CountAsync(predicate);
        }

        public virtual async Task<T> RetrieveByIdAsync<T>(int Id) where T : class
        {
            return await Context.Set<T>().FindAsync(Id);
        }

        protected virtual IQueryable<T> RetrieveQueryable<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            var query = Context.Set<T>().AsQueryable<T>();
            
            if (includeExpressions?.Any() ?? false)
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return query;
        }

        protected virtual IEnumerable<TResult> RetrieveQueryable<T, TJoin, TResult>(Expression<Func<T, bool>> predicate, Func<T, object> outerKey, Func<TJoin, object> innerKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class
        {
            var query = Context.Set<T>().AsQueryable<T>();
            var joined = Context.Set<TJoin>().AsQueryable();

            if (includeExpressions?.Any() ?? false)
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return query.GroupJoin(joined, outerKey, innerKey, result);
        }

        protected IQueryable<TResult> RetrieveQueryable<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult : class
        {
            var query = Context.Set<T>().AsQueryable<T>();
            if (includeExpressions?.Any() ?? false)
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return query.Select(selectExpression);
        }

        public virtual IEnumerable<T> Retrieve<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            return RetrieveQueryable(predicate, orderBy, includeExpressions);
        }

        public virtual async Task<IEnumerable<T>> RetrieveAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            return await RetrieveQueryable(predicate, orderBy, includeExpressions).ToListAsync();
        }

        public virtual async Task<IEnumerable<TResult>> RetrieveAsync<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult : class
        {
            return await RetrieveQueryable<T, TResult>(predicate, selectExpression, orderBy, includeExpressions).ToListAsync();
        }

        public virtual async Task<IEnumerable<TResult>> RetrieveAsync<T, TJoin, TResult>(Expression<Func<T, bool>> predicate, Func<T, object> outerKey, Func<TJoin, object> joinKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class
        {
            return await Task.FromResult(RetrieveQueryable(predicate, outerKey, joinKey, result, orderBy, includeExpressions)); //TODO : Refactor Async
        }

        public virtual PagedResult<T> RetrievePagedResult<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class
        {
            return RetrieveQueryable(predicate, orderBy).Paginate(paging);
        }

        public virtual async Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class
        {
            return await RetrieveQueryable(predicate, orderBy).PaginateAsync(paging);
        }

        public virtual async Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            return await RetrieveQueryable(predicate, orderBy, includeExpressions).PaginateAsync(paging);
        }

        public virtual async Task<PagedResult<T>> RetrieveStoreProcPagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class
        {
            var query = Context.Set<T>().
                SqlQuery("[dbo].[Search] @SearchKeyword", new SqlParameter("@SearchKeyword", "Nokia"))
                .AsQueryable<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy(orderBy);
            return await query.PaginateAsync(paging);
        }

        #endregion

        #region Update

        public void Update<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete

        public void Delete<T>(T entity) where T : class
        {
            var dbSet = Context.Set<T>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void DeleteById<T>(int Id) where T : class
        {
            T entity = Context.Set<T>().Find(Id);
            Delete(entity);
        }

        public void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entities = Retrieve(predicate);
            if (entities == null)
                return;
            
            Context.Set<T>().RemoveRange(entities);
        }

        #endregion

        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual async Task<int> SaveAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();

            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
                return 0;
            }

        }

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }
    }
}

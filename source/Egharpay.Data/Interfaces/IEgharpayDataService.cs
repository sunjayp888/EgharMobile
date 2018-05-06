using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Entity.Dto;

namespace Egharpay.Data.Interfaces
{
    public interface IEgharpayDataService
    {
        void Create<T>(T entity) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        void CreateRange<T>(IEnumerable<T> entities) where T : class;
        Task CreateRangeAsync<T>(IEnumerable<T> entities) where T : class;
        Task<T> CreateGetAsync<T>(T entity) where T : class;

        T RetrieveById<T>(int Id) where T : class;
        Task<T> RetrieveByIdAsync<T>(int Id) where T : class;
        IEnumerable<T> Retrieve<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class;
        IEnumerable<T> RetrieveAll<T>(List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class;
        Task<IEnumerable<TResult>> RetrieveAsync<T, TJoin, TResult>(Expression<Func<T, bool>> predicate, Func<T, object> outerKey, Func<TJoin, object> joinKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class;
        Task<IEnumerable<T>> RetrieveAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class;
        Task<IEnumerable<TResult>> RetrieveAsync<T, TResult>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null,
            params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult : class;

        Task<IEnumerable<TResult>> RetrieveAllAsync<T, TJoin, TResult>(Func<T, object> outerKey, Func<TJoin, object> joinKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class;
        Task<IEnumerable<T>> RetrieveAllAsync<T>(List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class;
        Task<IEnumerable<TResult>> RetrieveAllAsync<T, TResult>(Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null,
            params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult : class;

        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        PagedResult<T> RetrievePagedResult<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class;
        Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class;

        Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null, params Expression<Func<T, object>>[] includeExpression)
            where T : class;

        Task<PagedResult<T>> RetrieveStoreProcPagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class;

        void Update<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        void DeleteById<T>(int Id) where T : class;
        Task DeleteByIdAsync<T>(int Id) where T : class;
        void DeleteRange<T>(IEnumerable<T> entities) where T : class;
        Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : class;
        void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}

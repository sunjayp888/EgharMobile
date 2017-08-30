using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using Egharpay.Entity.Dto;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Models;

namespace Egharpay.Data.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class EgharpayDataService : IEgharpayDataService
    {
        protected readonly IDatabaseFactory<EgharpayDatabase> _databaseFactory;
        protected readonly IGenericDataService<DbContext> _genericDataService;
        protected TransactionScope ReadUncommitedTransactionScope => new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });
        protected TransactionScope ReadUncommitedTransactionScopeAsync => new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }, TransactionScopeAsyncFlowOption.Enabled);
        public EgharpayDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService)
        {
            _databaseFactory = databaseFactory;
            _genericDataService = genericDataService;
        }

        #region Create

        public void Create<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Create(entity);
                _genericDataService.Save();
            }
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Create(entity);
                await _genericDataService.SaveAsync();
            }
        }

        public async Task<T> CreateGetAsync<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Create(entity);
                await _genericDataService.SaveAsync();
                return entity;
            }
        }

        public void CreateRange<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.CreateRange(entities);
                _genericDataService.Save();
            }
        }

        public virtual async Task CreateRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.CreateRange(entities);
                await _genericDataService.SaveAsync();
            }
        }

        #endregion

        #region Retrieve

        public virtual T RetrieveById<T>(int Id) where T : class
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return _genericDataService.RetrieveById<T>(Id);
            }
        }

        public virtual async Task<T> RetrieveByIdAsync<T>(int Id) where T : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrieveByIdAsync<T>(Id);
            }
        }

        public virtual IEnumerable<T> RetrieveAll<T>(List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            return Retrieve(t => true, orderBy, includeExpressions);
        }

        public virtual IEnumerable<T> Retrieve<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return _genericDataService.Retrieve<T>(predicate, orderBy, includeExpressions).ToList();
            }
        }

        public virtual async Task<IEnumerable<T>> RetrieveAllAsync<T>(List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            return await RetrieveAsync<T>(t => true, orderBy, includeExpressions);
        }

        public virtual async Task<IEnumerable<T>> RetrieveAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrieveAsync<T>(predicate, orderBy, includeExpressions);
            }
        }

        public virtual async Task<IEnumerable<TResult>> RetrieveAllAsync<T,TResult>(Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult:class
        {
            return await RetrieveAsync(t => true, selectExpression, orderBy, includeExpressions);
        }

        public virtual async Task<IEnumerable<TResult>> RetrieveAsync<T,TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selectExpression, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TResult:class 
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrieveAsync<T,TResult>(predicate, selectExpression, orderBy, includeExpressions);
            }
        }

        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.CountAsync(predicate);
            }
        }
        public virtual async Task<IEnumerable<TResult>> RetrieveAllAsync<T, TJoin, TResult>(Func<T, object> outerKey, Func<TJoin, object> joinKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class
        {
            return await RetrieveAsync(t => true, outerKey, joinKey, result, orderBy, includeExpressions);
        }

        public virtual async Task<IEnumerable<TResult>> RetrieveAsync<T, TJoin, TResult>(Expression<Func<T, bool>> predicate, Func<T, object> outerKey, Func<TJoin, object> joinKey, Func<T, IEnumerable<TJoin>, TResult> result, List<OrderBy> orderBy = null, params Expression<Func<T, object>>[] includeExpressions) where T : class where TJoin : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrieveAsync(predicate, outerKey, joinKey, result, orderBy, includeExpressions);
            }
        }

        public virtual PagedResult<T> RetrievePagedResult<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return _genericDataService.RetrievePagedResult<T>(predicate, orderBy, paging);
            }
        }

        public virtual async Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null, params Expression<Func<T, object>>[] includeExpression) where T : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrievePagedResultAsync<T>(predicate, orderBy, paging, includeExpression);
            }
        }

        public virtual async Task<PagedResult<T>> RetrievePagedResultAsync<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                return await _genericDataService.RetrievePagedResultAsync<T>(predicate, orderBy, paging);
            }
        }

        #endregion

        #region Update

        public void Update<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Update(entity);
                _genericDataService.Save();
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Update(entity);
                await _genericDataService.SaveAsync();
            }
        }

        #endregion

        #region Delete

        public void Delete<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Delete(entity);
                _genericDataService.Save();
            }
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.Delete(entity);
                await _genericDataService.SaveAsync();
            }
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteRange(entities);
                _genericDataService.Save();
            }
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteRange(entities);
                await _genericDataService.SaveAsync();
            }
        }

        public void DeleteById<T>(int Id) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteById<T>(Id);
                _genericDataService.Save();
            }
        }

        public async Task DeleteByIdAsync<T>(int Id) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteById<T>(Id);
                await _genericDataService.SaveAsync();
            }
        }

        public void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteWhere<T>(predicate);
                _genericDataService.Save();
            }
        }

        public async Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _databaseFactory.CreateContext())
            {
                _genericDataService.Context = context;
                _genericDataService.DeleteWhere<T>(predicate);
                await _genericDataService.SaveAsync();
            }
        }

        #endregion
    }
}




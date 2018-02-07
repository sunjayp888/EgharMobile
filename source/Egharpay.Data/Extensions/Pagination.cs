using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Egharpay.Entity.Dto;

namespace Egharpay.Data.Extensions
{
    public static class Pagination
    {

        public static PagedResult<T> Paginate<T>(this IOrderedQueryable<T> source, Paging paging)
        {
            return source.AsQueryable().Paginate(paging);
        }

        public static PagedResult<T> Paginate<T>(this IQueryable<T> source, Paging paging)
        {
            var isEmpty = source == null || !source.Any();
            if (isEmpty)
                return PagedResult<T>.Empty;
            try
            {
                var totalResu = source.Count();
                var totalResult = source.Count();

                if (paging == null)
                    return PagedResult<T>.Create(source.ToList(), 1, totalResult, 1, totalResult);

                if (paging.Page <= 0)
                    paging.Page = 1;

                if (paging.PageSize <= 0)
                    paging.PageSize = 10;

                var totalPage = (int)Math.Ceiling((double)totalResult / paging.PageSize);
                var dat= source.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize).ToList();

            }
            catch (Exception ex)
            {
                throw  new Exception();
            }
            var totalResults = source.Count();

            if (paging == null)
                return PagedResult<T>.Create(source.ToList(), 1, totalResults, 1, totalResults);

            if (paging.Page <= 0)
                paging.Page = 1;

            if (paging.PageSize <= 0)
                paging.PageSize = 10;
            
            var totalPages = (int)Math.Ceiling((double)totalResults / paging.PageSize);
            var data = source.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize).ToList();

            return PagedResult<T>.Create(data, paging.Page, paging.PageSize, totalPages, totalResults);
        }

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IOrderedQueryable<T> source, Paging paging)
        {
            return await source.AsQueryable().PaginateAsync(paging);
        }

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> source, Paging paging)
        {
            var isEmpty = source == null || !source.Any();
            if (isEmpty)
                return PagedResult<T>.Empty;

       
            var totalResults = await source.CountAsync();

            if (paging == null)
                return PagedResult<T>.Create(await source.ToListAsync(), 1, totalResults, 1, totalResults);

            if (paging.Page <= 0)
                paging.Page = 1;

            if (paging.PageSize <= 0)
                paging.PageSize = 10;

            var totalPages = (int)Math.Ceiling((double)totalResults / paging.PageSize);
            var data = await source.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            return PagedResult<T>.Create(data, paging.Page, paging.PageSize, totalPages, totalResults);
        }

    }
}

using System.Collections.Generic;
using Egharpay.Entity.Dto;


namespace AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static List<TDestination> MapToList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        {
            return mapper.Map<List<TDestination>>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IMapper mapper, object source)
        {
            return mapper.Map<List<TDestination>>(source);
        }

        public static PagedResult<TDestination> MapToPagedResult<TSource, TDestination>(this IMapper mapper, PagedResult<TSource> source)
        {
            return source.Map((item) => mapper.Map<TDestination>(item));
        }

        public static PagedResult<TDestination> MapToPagedResult<TDestination>(this IMapper mapper, PagedResultBase source)
        {
            var listDest = mapper.MapToList<TDestination>(source.GetEnumerableItems());
            return PagedResult<TDestination>.Create(listDest, source.CurrentPage, source.ResultsPerPage, source.TotalPages, source.TotalResults);
        }
    }
}

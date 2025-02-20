using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CrossCutting.Common;

namespace ZeroStoreApp.CrossCutting.Helpers;

public static class PaginatedListExtensions
{
    public static PaginatedList<TDestination> MapPaginatedList<TSource, TDestination>(this IMapper mapper, PaginatedList<TSource> source)
    {
        var items = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        return new PaginatedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}

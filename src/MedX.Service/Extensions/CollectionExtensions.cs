using MedX.Domain.Configurations;

namespace MedX.Service.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
    {
        var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        return source;
    }
}
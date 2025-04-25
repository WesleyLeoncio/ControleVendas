using X.PagedList;

namespace ControleVendas.Modules.Common.Pagination;

public class MetaData<T>
{
    public readonly int Count;
    public readonly int PageSize;
    public readonly int PageCount;
    public readonly int TotalItemCount;
    public readonly bool HasNextPage;
    public readonly bool HasPreviousPage;

    public MetaData(int count, int pageSize, int pageCount, int totalItemCount, bool hasNextPage, bool hasPreviousPage)
    {
        Count = count;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalItemCount = totalItemCount;
        HasNextPage = hasNextPage;
        HasPreviousPage = hasPreviousPage;
    }

    public static MetaData<T> ToValue(IPagedList<T> page)
    {
        return new MetaData<T>(
            page.Count,
            page.PageSize,
            page.PageCount,
            page.TotalItemCount,
            page.HasNextPage,
            page.HasPreviousPage
        );
    }
}
namespace Infrastructure.Utility.Pagination;

public record PaginatorResult<T> where T: class
{
    public int TotalItems { get; }
    public int ItemsOnPage { get; }
    public List<T> Items { get; }
    public int CurrentPage { get; }
    public int TotalPages { get; }

    public PaginatorResult(int totalResults, int currentResultNumber, List<T> items, int currentPage, int totalPages)
    {
        TotalItems = totalResults;
        ItemsOnPage = currentResultNumber;
        Items = items;
        CurrentPage = currentPage;
        TotalPages = totalPages;
    }
    
    public static PaginatorResult<F> MapToOtherType<F>(PaginatorResult<T> paginatorResult, Func<T,F> mapper) where F: class
    {
        return new PaginatorResult<F>(
            paginatorResult.TotalItems,
            paginatorResult.ItemsOnPage,
            paginatorResult.Items.Select(c => mapper(c)).ToList(),
            paginatorResult.CurrentPage,
            paginatorResult.TotalPages
        );
    }

}
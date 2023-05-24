namespace Infrastructure.EF.Pagination;

public record PaginatorResult<T> where T: class
{
    public int TotalItems { get; }
    public int ItemsOnPage { get; }
    public List<T> Items { get; set; }
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
    
    public PaginatorResult<F> MapToOtherType<F>(Func<T,F> mapper) where F: class
    {
        return new PaginatorResult<F>(
            TotalItems,
            ItemsOnPage,
            Items.Select(c => mapper(c)).ToList(),
            CurrentPage,
            TotalPages
        );
    }

}
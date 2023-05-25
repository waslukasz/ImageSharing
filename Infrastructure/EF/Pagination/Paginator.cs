using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Pagination;

public class Paginator<TEntity> : IPaginator<TEntity> where TEntity: class
{
    private int _itemNumberPerPage;

    public Paginator(int itemNumberPerPage)
    {
        if (itemNumberPerPage < 1)
            throw new ArgumentException($"{nameof(itemNumberPerPage)} cannot be less than 1");
        _itemNumberPerPage = itemNumberPerPage;
    }

    public Paginator()
    {
        _itemNumberPerPage = 5;
    }

    public Paginator<TEntity> SetItemNumberPerPage(int itemNumberPerPage)
    {
        _itemNumberPerPage = itemNumberPerPage;
        return this;
    }

    public async Task<PaginatorResult<TEntity>> Paginate(IQueryable<TEntity> query, int pageNumber)
    {
        int totalItemsCount = query.Count();
        int totalPages = (int) Math.Ceiling((double) totalItemsCount / _itemNumberPerPage);
        
        List<TEntity> items = await query
            .Skip((pageNumber - 1) * _itemNumberPerPage)
            .Take(_itemNumberPerPage)
            .ToListAsync();

        return new PaginatorResult<TEntity>(
            totalItemsCount, 
            items.Count(), 
            items,
            pageNumber,
            totalPages
            );
    }
    public   PaginatorResult<TEntity> PaginateEnumerable(IEnumerable<TEntity> query, int pageNumber)
    {
        int totalItemsCount = query.Count();
        int totalPages = (int) Math.Ceiling((double) totalItemsCount / _itemNumberPerPage);

        List<TEntity> items = query
            .Skip((pageNumber - 1) * _itemNumberPerPage)
            .Take(_itemNumberPerPage).ToList();

        return new PaginatorResult<TEntity>(
            totalItemsCount, 
            items.Count(), 
            items,
            pageNumber,
            totalPages
            );
    }
}
namespace Infrastructure.EF.Pagination;

public interface IPaginator<TEntity> where TEntity: class
{
    public Task<PaginatorResult<TEntity>> Paginate(IQueryable<TEntity> query, int pageNumber);
    public PaginatorResult<TEntity> PaginateEnumerable(IEnumerable<TEntity> query, int pageNumber);
}
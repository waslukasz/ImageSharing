﻿namespace Infrastructure.Utility.Pagination;

public interface IPaginator<TEntity> where TEntity: class
{
    public Task<PaginatorResult<TEntity>> Paginate(IQueryable<TEntity> query, int pageNumber);
}
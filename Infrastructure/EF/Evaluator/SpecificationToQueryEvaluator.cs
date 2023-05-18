using Application_Core.Common.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Evaluator;

public class SpecificationToQueryEvaluator<TEntity> where TEntity: class
{
    public static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        query = spec.Criteria.Aggregate(query, (current, criteria) => current.Where(criteria));
        
        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }
}
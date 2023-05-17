using System.Data.Entity;
using System.Linq.Expressions;
using Application_Core.Common.Specification;

namespace Infrastructure.EF.Evaluator;

public class SpecificationToQueryEvaluator<TEntity> where TEntity: class
{
    public static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        foreach (Expression<Func<TEntity, bool>> filter in spec.Criteria) 
        {
            query = query.Where(filter);
        }
        
        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        
        query = spec.Includes.Aggregate(query, (current, include) => current);
        return query;
    }
}
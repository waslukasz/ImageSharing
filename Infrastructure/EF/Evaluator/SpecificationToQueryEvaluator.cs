using System.Data.Entity;
using Application_Core.Common.Specification;

namespace Infrastructure.EF.Evaluator;

public class SpecificationToQueryEvaluator<TEntity> where TEntity: class
{
    public static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
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
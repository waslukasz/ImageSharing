using System.Linq.Expressions;

namespace Application_Core.Common.Specification;

public class BaseSpecification<T>: ISpecification<T>
{
    public List<Expression<Func<T, bool>>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public BaseSpecification()
    {
        Criteria = new List<Expression<Func<T, bool>>>();
        Includes = new List<Expression<Func<T, object>>>();
    }

    public ISpecification<T> AddCriteria(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria.Add(criteriaExpression);
        return this;
    }

    public ISpecification<T> AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
        return this;
    }
    
    public void SetOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    public void SetOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
}
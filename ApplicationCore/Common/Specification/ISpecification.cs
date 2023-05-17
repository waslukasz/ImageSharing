using System.Linq.Expressions;

namespace Application_Core.Common.Specification;

public interface ISpecification<T>
{
    List<Expression<Func<T, bool>>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    
    public ISpecification<T> AddCriteria(Expression<Func<T, bool>> criteriaExpression);

    public ISpecification<T> AddInclude(Expression<Func<T, object>> includeExpression);
}
namespace Application_Core.Common.Specification.Comment;

using Model;

public class GetCommentByGuidSpecification : BaseSpecification<Comment>
{
    public GetCommentByGuidSpecification(Guid id)
    {
        this.AddCriteria(c => c.Guid == id) 
            .AddInclude(c => c.Post)
            .AddInclude(c => c.User);
    }
}
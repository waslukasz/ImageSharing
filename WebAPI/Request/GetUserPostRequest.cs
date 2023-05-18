namespace WebAPI.Request;

public class GetUserPostRequest : PaginationRequest
{
    public Guid Id { get; set; }
}
namespace WebAPI.Request
{
    public class GetAllCommentsRequest : PaginationRequest
    {
        public Guid PostGuid { get; set; }
    }
}
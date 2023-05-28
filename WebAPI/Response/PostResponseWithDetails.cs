namespace WebAPI.Response;

public class PostResponseWithDetails : PostResponse
{
    public int ReactionCount { get; set; }
    
    public int CommentCount { get; set; }
    
}
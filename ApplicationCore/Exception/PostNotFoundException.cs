namespace Application_Core.Exception;

public sealed class PostNotFoundException : NotFoundException
{
    private const string MESSAGE = "Post not found !";
        
    public PostNotFoundException() : base(MESSAGE) { }
}
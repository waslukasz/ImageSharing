namespace Application_Core.Exception;

public sealed class CommentNotFoundException : NotFoundException
{
    private const string MESSAGE = "Comment not found !";

    public CommentNotFoundException() : base(MESSAGE) { }
}
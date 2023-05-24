namespace Application_Core.Exception;

public class AlbumNotFoundException : NotFoundException
{
    private const string MESSAGE = "Album not found !";
    public AlbumNotFoundException() : base(MESSAGE) { }
}
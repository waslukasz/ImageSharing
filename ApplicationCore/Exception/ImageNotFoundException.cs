namespace Application_Core.Exception;
public class ImageNotFoundException : NotFoundException
{ 
    private const string MESSAGE = "Image not found !";
    public ImageNotFoundException() : base(MESSAGE) { }

    public ImageNotFoundException(string message) : base(message) { }
}



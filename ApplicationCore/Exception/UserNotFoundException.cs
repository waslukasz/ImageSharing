namespace Application_Core.Exception;

public sealed class UserNotFoundException : NotFoundException
{ 
    private const string MESSAGE = "User not found !"; 
    public UserNotFoundException() : base(MESSAGE) { }
}



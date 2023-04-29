namespace Application_Core.Exception;

public sealed class UserNotFoundException : NotFoundException
{
    const string MESSAGE = "User not found !"; 
    public UserNotFoundException() : base(MESSAGE) { }
}



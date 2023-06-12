using Infrastructure.EF.Entity;
using Infrastructure.Enum;

namespace Infrastructure.Struct;

public struct CurrentUser
{ 
    public UserEntity? User { get; set; }
    public RoleEnum UserRole { get; set; }
}
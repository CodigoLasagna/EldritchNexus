using System.ComponentModel.DataAnnotations;

namespace Model;

public class UserCreateModel
{
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Nickname { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Email { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Password { get; set; }
    //[StringLength(maximumLength: 200, MinimumLength = 0)]
    //public string? ProfileImageUrl { get; set; }
    //public int? UserRoleId { get; set; }
}
public class UserLoginModel
{
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Email { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Password { get; set; }
}

public class UserUpdateRoleModel
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
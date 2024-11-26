using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ProfileImageUrl { get; set; }
    public int? UserRoleId { get; set; }
    //public List<Team>? Teams { get; set; }
    //public List<Organization>? Organizations { get; set; }
}
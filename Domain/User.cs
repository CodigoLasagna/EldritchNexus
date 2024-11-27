using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    [StringLength(maximumLength: 1200, MinimumLength = 0)]
    public string? ProfileImageUrl { get; set; }
    public int? UserRoleId { get; set; }
    public List<Team>? Teams { get; set; }
    public List<Organization>? Organizations { get; set; }
}
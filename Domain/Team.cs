using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Team
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<int>? UserIds { get; set; }
    public List<int>? AdminsIds { get; set; }
    //public List<Project>? Projects { get; set; }
    public Organization? Organization { get; set; }
}
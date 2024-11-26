using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Organization
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? OrganizationIconUrl { get; set; }
    public string? MainHostUrl { get; set; }
    public List<Team>? Teams { get; set; }
    public List<User>? Users { get; set; }
    public List<Node>? Nodes { get; set; }
    
    public List<int>? AdminsIds { get; set; }
}
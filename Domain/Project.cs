using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Project
{
    [Key]
    public int Id { get; set; }
    public User? Creator { get; set; }
    public int? CreatorId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? GitRepositoryPath { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
}
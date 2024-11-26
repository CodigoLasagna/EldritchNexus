using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Node
{
    [Key]
    public int Id { get; set; }
    public string? Url { get; set; }
    public List<Organization>? Organizations { get; set; }
}
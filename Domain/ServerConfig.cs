using System.ComponentModel.DataAnnotations;

namespace Domain;

public class ServerConfig
{
    [Key]
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
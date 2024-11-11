using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DetectedNode
{
    [Key]
    public int Id { get; set; }
    public string IPAddress { get; set; }
}
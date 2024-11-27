using System.ComponentModel.DataAnnotations;

namespace Model;

public class OrganizationCreateModel
{
    public string? Name { get; set; }
}
public class OrganizationPerTypeModel
{
    [Required]
    public int userId { get; set; }
    public int type { get; set; }
}

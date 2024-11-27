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

public class UserOrgModel
{
    [Required]
    public int userId { get; set; }
    public int orgId { get; set; }
}

public class OrganizationUpdateModel
{
    [Required]
    public int Id { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Name { get; set; }
    [StringLength(maximumLength: 1200, MinimumLength = 0)]
    public string? OrganizationIconUrl { get; set; }
}

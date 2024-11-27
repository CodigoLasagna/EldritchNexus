using System.ComponentModel.DataAnnotations;

namespace Model;

public class ProjectCreateModel
{
    public int? CreatorId { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Name { get; set; }
    [StringLength(maximumLength: 200, MinimumLength = 0)]
    public string? Description { get; set; }
    public int? TeamId { get; set; }
}

public class ProjectListModel
{
    public int userId { get; set; }
}
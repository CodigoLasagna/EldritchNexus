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

public class ProjectGetDetailsModel
{
    public int ProjectId { get; set; }
}
public class ProjectDetailsModel
{
    public int ProjectId { get; set; }
    public List<CommitDetailsModel> CommitHistory { get; set; } = new List<CommitDetailsModel>();
}

public class CommitDetailsModel
{
    public string CommitId { get; set; }
    public string Author { get; set; }
    public string Message { get; set; }
    public DateTimeOffset Date { get; set; }
    public List<string> Changes { get; set; } = new List<string>();
}

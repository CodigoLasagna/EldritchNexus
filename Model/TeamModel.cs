namespace Model;

public class TeamCreateModel
{
    public int organizationId { get; set; }
    public string? Name { get; set; }
    public int adminUserId { get; set; }
}

public class TeamGetListModel
{
    public int userId { get; set; }
    public int type { get; set; }
}

public class TeamDelModel
{
    public int teamId { get; set; }
}

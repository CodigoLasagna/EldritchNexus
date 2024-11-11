namespace FastSurvey.Controllers;

public interface IRoleService
{
    string Role { get; set; }
}

public class RoleService : IRoleService
{
    public string Role { get; set; }
}
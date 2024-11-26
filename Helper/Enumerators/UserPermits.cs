namespace Helper.Enumerators;

[Flags]
public enum UserPermits
{
    NoRole = 0,
    ManageSystem = 1 << 0,
    ManageOrganization = 1 << 1,
    ManageTeam = 1 << 2,
    ManageProject = 1 << 3,
}
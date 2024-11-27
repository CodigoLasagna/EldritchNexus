using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Implementation;

public class TeamRepository : ITeamRepository
{
    public int CreateTeamInOrganization(TeamCreateModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        Organization curOrg = ctx.Organizations.FirstOrDefault(o => o.Id == model.organizationId);
        if (curOrg == null)
            return -1;
        User curUser = ctx.Users.FirstOrDefault(u => u.Id == model.adminUserId);
        if (curUser == null)
            return -1;
        Team newTeam = new Team();
        newTeam.Organization = curOrg;
        newTeam.Name = model.Name;
        newTeam.AdminsIds = new List<int>();
        newTeam.AdminsIds.Add(model.adminUserId);
        newTeam.Users = new List<User>();
        newTeam.Users.Add(curUser);
        newTeam.State = true;
        ctx.Teams.Add(newTeam);
        ctx.SaveChanges();
        return newTeam.Id;
    }

    public List<Team> GetListOfTeamsByType(TeamGetListModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User curUser = ctx.Users.FirstOrDefault(u => u.Id == model.userId);
        if (curUser == null)
            return null;
        List<Organization> userOrganizations = new List<Organization>();
        List<Team> teams = new List<Team>();
        userOrganizations = ctx.Organizations
            .Where(o => o.Users.Any(u => u.Id == curUser.Id))
            .ToList();
        if (model.type == 0)
        {
            var userOrganizationIds = ctx.Organizations
                .Where(o => o.Users.Any(u => u.Id == curUser.Id))
                .Select(o => o.Id) // Solo los IDs
                .ToList();
            
            teams = ctx.Teams
                .Include(t => t.Users)
                .Where(t => !t.Users.Any(u => u.Id == curUser.Id)) // Equipos donde no está el usuario
                .Where(t => userOrganizationIds.Contains(t.Organization.Id)) // Equipos de las organizaciones del usuario
                .Where(t => t.State == true)
                .ToList();
        }
        if (model.type == 1)
        {
            teams = ctx.Teams
                .Include( t => t.Users)
                .Where(t => t.Users.Any(u => u.Id == curUser.Id))
                .Where(t => t.State == true)
                .ToList();
        }

        return teams;
    }

    public bool TeamDelete(TeamDelModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        Team curTeam = ctx.Teams.FirstOrDefault(t => t.Id == model.teamId);
        if (curTeam == null)
            return false;
        curTeam.State = false;
        ctx.Teams.Update(curTeam);
        ctx.SaveChanges();
        return true;
    }

    public int Create(Team entity)
    {
        throw new NotImplementedException();
    }

    public int Read(int Id)
    {
        throw new NotImplementedException();
    }

    public int Update(Team entity, int Id)
    {
        throw new NotImplementedException();
    }

    public int Delete(int Id)
    {
        throw new NotImplementedException();
    }

}
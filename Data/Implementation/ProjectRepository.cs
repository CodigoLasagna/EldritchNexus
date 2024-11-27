using Data.Contracts;
using Domain;
using Helper.lib2git;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Implementation;

public class ProjectRepository : IProjectRepository
{
    public int CreateProject(ProjectCreateModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        Project curProject = ctx.Projects.FirstOrDefault( p => p.Name == model.Name );
        if (curProject != null)
            return 0;
        Project newProject = new Project();
        User curUser = ctx.Users
            .Include( u => u.Projects)
            .FirstOrDefault( u => u.Id == model.CreatorId);
        if (curUser == null)
            return 0;
        newProject.Creator = curUser;
        newProject.CreatorId = model.CreatorId;
        newProject.Name = model.Name;
        newProject.Description = model.Description;
        newProject.TeamId = model.TeamId;
        string repostPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"GitNexus/ServerRepos"
        );
        GitServer server = new GitServer(repostPath);
        newProject.GitRepositoryPath = Path.Combine(repostPath, model.Name);
        //curUser.Projects.Add( newProject );
        server.CreateRepository(newProject.Name);
        ctx.Projects.Add(newProject);
        ctx.SaveChanges();
        return newProject.Id;
    }

    public List<Project> GetProjectsFromTeams(ProjectListModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
    
        List<int> userTeamIds = ctx.Teams
            .Where(t => t.Users.Any(u => u.Id == model.userId))
            .Select(t => t.Id)
            .ToList();
    
        List<Project> projects = ctx.Projects
            .Where(p => userTeamIds.Contains((int)p.TeamId))
            .Include(p => p.Creator)
            .ToList();
    
        return projects; 
    }

    public int Create(Project entity)
    {
        throw new NotImplementedException();
    }

    public int Read(int Id)
    {
        throw new NotImplementedException();
    }

    public int Update(Project entity, int Id)
    {
        throw new NotImplementedException();
    }

    public int Delete(int Id)
    {
        throw new NotImplementedException();
    }

}
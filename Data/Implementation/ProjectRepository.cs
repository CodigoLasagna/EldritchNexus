using Data.Contracts;
using Domain;
using Helper.lib2git;
using LibGit2Sharp;
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

public List<CommitDetailsModel> GetCommitHistory(ProjectGetDetailsModel model)
{
    var connectioOptions = DbConnection.GetDbContextOptions();
    using var ctx = new GitNexusDBContext(options: connectioOptions);
    var commitHistory = new List<CommitDetailsModel>();
    Project project = ctx.Projects.FirstOrDefault(p => p.Id == model.ProjectId);

    if (project == null || string.IsNullOrEmpty(project.GitRepositoryPath))
    {
        throw new Exception("Project or repository path is invalid.");
    }

    string repoPath = project.GitRepositoryPath;

    // Verificar si el repositorio existe
    if (!LibGit2Sharp.Repository.IsValid(repoPath))
    {
        throw new Exception($"Repository {repoPath} is not valid.");
    }

    // Acceder al repositorio
    using var repo = new LibGit2Sharp.Repository(repoPath);

    // Obtener los commits del repositorio
    var commits = repo.Commits.OrderByDescending(c => c.Committer.When).Take(100); // Limitamos a los últimos 100 commits

    Commit previousCommit = null;
    foreach (var commit in commits)
    {
        var commitDetails = new CommitDetailsModel
        {
            CommitId = commit.Id.ToString(),
            Author = commit.Author.Name,
            Message = commit.Message,
            Date = commit.Committer.When,
            Changes = new List<string>()
        };

        // Si no es el primer commit, obtener las diferencias con el commit anterior
        if (previousCommit != null)
        {
            var diff = repo.Diff.Compare<TreeChanges>(previousCommit.Tree, commit.Tree);
            foreach (var change in diff)
            {
                string changeDetails = $"{change.Status}: {change.Path}";

                // Obtener las diferencias de línea para archivos modificados
                if (change.Status == LibGit2Sharp.ChangeKind.Modified || change.Status == LibGit2Sharp.ChangeKind.Added)
                {
                    // Aquí estamos comparando los archivos modificados entre dos commits
                    var patch = repo.Diff.Compare<Patch>(previousCommit.Tree, commit.Tree);

                    // Analizamos las diferencias en los archivos modificados
                    foreach (var patchLine in patch)
                    {
                        foreach (var line in patchLine.AddedLines)
                        {
                            commitDetails.Changes.Add($"Added: {line.Content}");
                        }
                        foreach (var line in patchLine.DeletedLines)
                        {
                            commitDetails.Changes.Add($"Deleted: {line.Content}");
                        }
                    }
                }
                else
                {
                    commitDetails.Changes.Add(changeDetails);
                }
            }
        }
        else
        {
            // Si es el primer commit, obtenemos sus cambios directamente
            var diff = repo.Diff.Compare<TreeChanges>(commit.Tree, commit.Tree); // Comparamos el commit consigo mismo, ya que no tiene commit anterior
            foreach (var change in diff)
            {
                string changeDetails = $"{change.Status}: {change.Path}";

                if (change.Status == LibGit2Sharp.ChangeKind.Added)
                {
                    commitDetails.Changes.Add($"Added: {change.Path}");
                }
            }
        }

        commitHistory.Add(commitDetails);
        previousCommit = commit;
    }

    return commitHistory;
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
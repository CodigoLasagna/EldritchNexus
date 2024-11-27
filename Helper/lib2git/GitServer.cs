using LibGit2Sharp;

namespace Helper.lib2git;

public class GitServer
{
    private readonly string _reposBasePath;

    public GitServer(string repoBasePath)
    {
        _reposBasePath = repoBasePath;
    }

    private void EnsureDirectoryExists(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            Console.WriteLine($"Created directory: {dir}");
        }
        else
        {
            Console.WriteLine("Directory already exists.");
        }
    }

    public void CreateRepository(string repoName)
    {
        string repoPath = Path.Combine(_reposBasePath, repoName);
        if (Directory.Exists(repoPath) && Repository.IsValid(repoPath))
        {
            Console.WriteLine($"Repository {repoPath} already exists.");
            return;
        }
        Repository.Init(repoPath, isBare: true);
        
        Console.WriteLine($"Repository {repoPath} has been created.");
    }

    public void ListAllRepositories()
    {
        var directories = Directory.GetDirectories(_reposBasePath, "*", SearchOption.AllDirectories);
        Console.WriteLine("Available repositories:");
        foreach (var dir in directories)
        {
            Console.WriteLine($"- {Path.GetFileName(dir)}");
        }
    }
    
}
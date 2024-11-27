using Business.Contracts;
using Data.Contracts;
using Domain;
using Model;

namespace Business.Implementation;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public int CreateProject(ProjectCreateModel model)
    {
        return (projectRepository.CreateProject(model));
    }

    public List<Project> GetProjectsFromTeams(ProjectListModel model)
    {
       return (projectRepository.GetProjectsFromTeams(model));
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
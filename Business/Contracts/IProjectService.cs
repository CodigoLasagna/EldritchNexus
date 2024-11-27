using Data.Contracts;
using Domain;
using Model;

namespace Business.Contracts;

public interface IProjectService : IGenericRespository<Project>
{
   int CreateProject(ProjectCreateModel model); 
   List<Project> GetProjectsFromTeams(ProjectListModel model); 
}
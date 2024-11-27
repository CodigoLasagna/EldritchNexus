using Domain;
using Model;

namespace Data.Contracts;

public interface IProjectRepository : IGenericRespository<Project>
{
   int CreateProject(ProjectCreateModel model); 
   List<Project> GetProjectsFromTeams(ProjectListModel model); 
}
using Domain;
using Model;

namespace Data.Contracts;

public interface IProjectRepository : IGenericRespository<Project>
{
   int CreateProject(ProjectCreateModel model); 
   List<Project> GetProjectsFromTeams(ProjectListModel model);
   //List<CommitHistoryModel> GetCommitHistory(ProjectDetailsMode model);
   List<CommitDetailsModel> GetCommitHistory(ProjectGetDetailsModel model);
}
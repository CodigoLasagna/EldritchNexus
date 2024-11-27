using Data.Contracts;
using Domain;
using Model;

namespace Business.Contracts;

public interface ITeamService : IGenericRespository<Team>
{
    int CreateTeamInOrganization(TeamCreateModel model);
    List<Team> GetListOfTeamsByType(TeamGetListModel model);
    bool TeamDelete(TeamDelModel model);
}
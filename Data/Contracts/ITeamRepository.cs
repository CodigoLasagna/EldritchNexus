using Domain;
using Model;

namespace Data.Contracts;

public interface ITeamRepository : IGenericRespository<Team>
{
    int CreateTeamInOrganization(TeamCreateModel model);
    List<Team> GetListOfTeamsByType(TeamGetListModel model);
    bool TeamDelete(TeamDelModel model);
}
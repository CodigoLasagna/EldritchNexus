using Business.Contracts;
using Data.Contracts;
using Data.Implementation;
using Domain;
using Model;

namespace Business.Implementation;

public class TeamService(ITeamRepository teamRepository) : ITeamService
{
    public int CreateTeamInOrganization(TeamCreateModel model)
    {
        return (teamRepository.CreateTeamInOrganization(model));
    }

    public List<Team> GetListOfTeamsByType(TeamGetListModel model)
    {
        return (teamRepository.GetListOfTeamsByType(model));
    }

    public bool TeamDelete(TeamDelModel model)
    {
        return (teamRepository.TeamDelete(model));
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
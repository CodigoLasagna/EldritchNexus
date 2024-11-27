using Domain;
using Model;

namespace Data.Contracts;

public interface IOrganizationRepository : IGenericRespository<Organization>
{
    int CreateOrganization(Organization organization);
    Organization GetExistingOrganization(string hostname);
}
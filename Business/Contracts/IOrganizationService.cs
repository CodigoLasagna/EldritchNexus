using Domain;
using Model;

namespace Business.Contracts;

public interface IOrganizationService : IGenericService<Organization>
{
    int CreateOrganization(OrganizationCreateModel model);
    List<Organization> GetOrganizations(OrganizationPerTypeModel model);
}
using System.Net;
using Business.Contracts;
using Data.Contracts;
using Domain;
using Model;

namespace Business.Implementation;

public class OrganizationService(IOrganizationRepository organizationRepository) : IOrganizationService
{
    public int CreateOrganization(OrganizationCreateModel model)
    {
        string machineHostname = Dns.GetHostName();
        Organization existingOrganization = organizationRepository.GetExistingOrganization(machineHostname);
        if (existingOrganization != null)
            return 0;
        Organization newOrg = new Organization();
        newOrg.Name = model.Name;
        newOrg.Hostname = machineHostname;
        return (organizationRepository.CreateOrganization(newOrg));
    }

    public List<Organization> GetOrganizations(OrganizationPerTypeModel model)
    {
        return (organizationRepository.GetOrganizations(model));
    }

    public int Create(Organization entity)
    {
        throw new NotImplementedException();
    }

    public int Read(int Id)
    {
        throw new NotImplementedException();
    }

    public int Update(Organization entity, int Id)
    {
        throw new NotImplementedException();
    }

    public int Delete(int Id)
    {
        throw new NotImplementedException();
    }

}
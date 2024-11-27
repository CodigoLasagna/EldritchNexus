using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Implementation;

public class OrganizationRepository : IOrganizationRepository
{
    public int CreateOrganization(Organization organization)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        organization.AdminsIds = new List<int>();
        ctx.Organizations.Add(organization);
        ctx.SaveChanges();
        int orgId = organization.Id;
        return(orgId);
    }

    public Organization GetExistingOrganization(string hostname)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        Organization existingOrganization = ctx.Organizations.FirstOrDefault( o => o.Hostname == hostname);
        return existingOrganization;
    }

    public List<Organization> GetOrganizations(OrganizationPerTypeModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        
        List<Organization> organizations = new List<Organization>();

        if (model.type == 0)
        {
            organizations = ctx.Organizations
                .Where(o => o.Users.Any(u => u.Id == model.userId))
                .ToList();
        }
        if (model.type == 1)
        {
            organizations = ctx.Organizations
                .Include( o => o.Users)
                .Where(o => o.Users.Any(u => u.Id != model.userId))
                .ToList();
        }
        if (model.type == 2)
        {
            organizations = ctx.Organizations
                .ToList()
                .Where(o => o.AdminsIds.Contains(model.userId))
                .ToList();
        }
        return organizations;
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
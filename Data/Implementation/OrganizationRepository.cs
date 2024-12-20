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
        organization.Users = new List<User>();
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
                .Include( o => o.Users)
                .Where(o => o.Users.Any(u => u.Id == model.userId))
                .ToList();
        }
        if (model.type == 1)
        {
            organizations = ctx.Organizations
                .Include( o => o.Users)
                .Where(o => !o.Users.Any(u => u.Id == model.userId))
                .ToList();
        }
        if (model.type == 2)
        {
            organizations = ctx.Organizations
                .Include( o => o.Users)
                .ToList()
                .Where(o => o.AdminsIds.Contains(model.userId))
                .ToList();
        }
        return organizations;
    }

    public bool AddUserToOrganization(UserOrgModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User curUser = ctx.Users.FirstOrDefault(u => u.Id == model.userId);
        if (curUser == null)
            return false;
        Organization curOrg = ctx.Organizations
            .Include( o => o.Users)
            .FirstOrDefault( o=> o.Id == model.orgId);
        if (curOrg == null)
            return false;
        
        curOrg.Users.Add( curUser );
        ctx.SaveChanges();
        return true;
    }

    public bool UpdateOrganization(OrganizationUpdateModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        var curOrg = ctx.Organizations.FirstOrDefault( o => o.Id == model.Id);
        if (curOrg == null)
            return false;
        if (!String.IsNullOrEmpty(model.Name) && curOrg.Name != model.Name)
            curOrg.Name = model.Name;
        if (!String.IsNullOrEmpty(model.OrganizationIconUrl) && curOrg.OrganizationIconUrl != model.OrganizationIconUrl)
            curOrg.OrganizationIconUrl = model.OrganizationIconUrl;
        
        ctx.Organizations.Update(curOrg);
        ctx.SaveChanges();
        return true;
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
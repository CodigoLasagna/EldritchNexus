using System.Data.Common;
using System.Net;
using Data.Contracts;
using Domain;
using Helper.Security;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Implementation;

public class UserRepository : IUserRepository
{

    public int CreateUser(User user)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        user.ProfileImageUrl = "";
        user.Projects = new List<Project>();
        ctx.Users.Add(user);
        ctx.SaveChanges();
        return user.Id;
    }

    public User GetUserByEmail(string email)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        if (string.IsNullOrEmpty(email)) return null;
        return ctx.Users.FirstOrDefault(u => u.Email == email);
    }

    public int UpdateUserRole(UserUpdateRoleModel model)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User curUser = ctx.Users.FirstOrDefault(u => u.Id == model.UserId);
        if (curUser == null) return 0;
        curUser.UserRoleId = model.RoleId;
        ctx.Users.Update(curUser);
        ctx.SaveChanges();
        return 1;
    }

    public bool AdminUserExists()
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        bool doesAdminExists = ctx.Users
            .Where( u => u.Email == "admin@admin.com" )
            .FirstOrDefault(u => u.UserRoleId == 1) != null;
        return doesAdminExists;
    }

    public bool AddUserAdminToOrganization(int userId)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User SysAdmin = ctx.Users.FirstOrDefault(u => u.Id == userId);
        string machineHostname = Dns.GetHostName();
        Organization mainOrg = ctx.Organizations
            .Include(o => o.Users)
            .FirstOrDefault( o => o.Hostname == machineHostname);
        if (SysAdmin == null || mainOrg == null)
            return false;
        mainOrg.Users.Add(SysAdmin);
        mainOrg.AdminsIds.Add(SysAdmin.Id);
        ctx.SaveChanges();
        return true;
    }

    public bool UpdateUserData(UserUpdateModel model)
    {
        var TokenManager = new TokenManager();
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User curUser = ctx.Users.FirstOrDefault( u => u.Id == model.UserId);
        if (curUser == null) return false;
        if (!String.IsNullOrEmpty(model.Nickname) && curUser.Nickname != model.Nickname)
            curUser.Nickname = model.Nickname;
        if (!String.IsNullOrEmpty(model.Email) && curUser.Email != model.Email)
            curUser.Email = model.Email;
        if (!String.IsNullOrEmpty(model.ProfileImageUrl) && curUser.ProfileImageUrl != model.ProfileImageUrl)
            curUser.ProfileImageUrl = model.ProfileImageUrl;
        if (!String.IsNullOrEmpty(model.Password) && !TokenManager.VerifyPassword(model.Password, curUser.Password))
            curUser.Password = TokenManager.HashPassword(model.Password);
        ctx.Users.Update(curUser);
        ctx.SaveChanges();
        return true;
    }

    User IUserRepository.Read(int Id)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
        User curUser = ctx.Users.FirstOrDefault( u => u.Id == Id);
        return curUser;
    }


    public int Create(User entity)
    {
        throw new NotImplementedException();
    }

    public int Read(int Id)
    {
        throw new NotImplementedException();
    }

    public int Update(User entity, int Id)
    {
        throw new NotImplementedException();
    }

    public int Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public string UpdateUserRole(int userId, int roleId)
    {
        throw new NotImplementedException();
    }
}
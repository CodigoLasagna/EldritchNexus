using System.Data.Common;
using Data.Contracts;
using Domain;
using Helper.Security;
using Model;

namespace Data.Implementation;

public class UserRepository : IUserRepository
{

    public int CreateUser(User user)
    {
        var connectioOptions = DbConnection.GetDbContextOptions();
        using var ctx = new GitNexusDBContext(options: connectioOptions);
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
        bool doesAdminExists = ctx.Users.FirstOrDefault(u => u.UserRoleId == 1) != null;
        return doesAdminExists;
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
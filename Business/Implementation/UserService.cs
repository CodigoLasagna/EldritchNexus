using System.Text.Json;
using Business.Contracts;
using Data.Contracts;
using Domain;
using Helper.Security;
using Model;

namespace Business.Implementation;

public class UserService(IUserRepository userRepository) : IUserService
{
    public int CreateUser(UserCreateModel model)
    {
        User ExistingUser = userRepository.GetUserByEmail(model.Email);
        if (ExistingUser != null) return -1;
        var NewUser = new User();
        NewUser.Nickname = model.Nickname;
        NewUser.Email = model.Email;
        var TokenManager = new TokenManager();
        NewUser.Password = TokenManager.HashPassword(model.Password);
        NewUser.UserRoleId = 0;
        return userRepository.CreateUser(NewUser);
    }

    public string LoginUser(UserLoginModel model)
    {
        User existingUser = userRepository.GetUserByEmail(model.Email);
        if (existingUser == null) return "0";
        var TokenManager = new TokenManager();
        if (!TokenManager.VerifyPassword(model.Password, existingUser.Password)) return "0";
        string token = TokenManager.GenerateJwtToken(existingUser);
        var tokenData = new { Token = token };
        string json = JsonSerializer.Serialize(tokenData);
        return json;
    }

    public int UpdateRole(UserUpdateRoleModel model)
    {
        
        return (userRepository.UpdateUserRole(model));
    }
    
    public bool AddGenericSysAdmin()
    {
        if (userRepository.AdminUserExists()) return false;
        User newSysAdmin = new User();
        newSysAdmin.UserRoleId = 1;
        newSysAdmin.Email = "admin@admin.com";
        newSysAdmin.Password = "root";
        newSysAdmin.Nickname = "admin";
        var TokenManager = new TokenManager();
        newSysAdmin.Password = TokenManager.HashPassword(newSysAdmin.Password);
        //int user_id = userRepository.CreateUser(newSysAdmin);
        userRepository.AddUserAdminToOrganization(userRepository.CreateUser(newSysAdmin));
        return true;
    }

    public bool UpdateUserData(UserUpdateModel model)
    {
        return (userRepository.UpdateUserData(model));
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
    
    public string RenewUserToken(string existingToken, int userId)
    {
        var TokenManager = new TokenManager();
        User updatedUser = userRepository.Read(userId);
        if (updatedUser == null) return null;
    
        return TokenManager.RenewJwtToken(existingToken, updatedUser);
    }

}
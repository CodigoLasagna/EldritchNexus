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
        var TokenManager = new TokenManager();
        User ExistingUser = userRepository.GetUserByEmail(model.Email);
        if (ExistingUser != null) return -1;
        var NewUser = new User();
        NewUser.Nickname = model.Nickname;
        NewUser.Email = model.Email;
        NewUser.Password = TokenManager.HashPassword(model.Password);
        NewUser.UserRoleId = 0;
        NewUser.ProfileImageUrl = "";
        return userRepository.CreateUser(NewUser);
    }

    public string LoginUser(UserLoginModel model)
    {
        var TokenManager = new TokenManager();
        User existingUser = userRepository.GetUserByEmail(model.Email);
        if (existingUser == null) return "0";
        if (!TokenManager.VerifyPassword(model.Password, existingUser.Password)) return "0";
        string token = TokenManager.GenerateJwtToken(existingUser);
        var tokenData = new { Token = token };
        string json = JsonSerializer.Serialize(tokenData);
        return json;
    }

    public int UpdateRole(UserUpdateRoleModel model)
    {
        
        throw new NotImplementedException();
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

}
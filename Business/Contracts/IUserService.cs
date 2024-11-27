using Domain;
using Model;

namespace Business.Contracts;

public interface IUserService : IGenericService<User>
{
    int CreateUser(UserCreateModel model);
    string LoginUser(UserLoginModel model);
    
    int UpdateRole(UserUpdateRoleModel model);
    bool AddGenericSysAdmin();
    bool UpdateUserData(UserUpdateModel model);
    string RenewUserToken(string existingToken, int userId);
}
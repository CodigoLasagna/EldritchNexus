using Domain;
using Model;

namespace Business.Contracts;

public interface IUserService : IGenericService<User>
{
    int CreateUser(UserCreateModel model);
    string LoginUser(UserLoginModel model);
    
    int UpdateRole(UserUpdateRoleModel model);
}
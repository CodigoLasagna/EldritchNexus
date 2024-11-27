using Domain;
using Model;

namespace Data.Contracts;

public interface IUserRepository : IGenericRespository<User>
{
    int CreateUser(User user);
    User GetUserByEmail(string email);
    int UpdateUserRole(UserUpdateRoleModel model);
    bool AdminUserExists();

    bool AddUserAdminToOrganization(int userId);
    bool UpdateUserData(UserUpdateModel model);
    User Read(int Id);
}
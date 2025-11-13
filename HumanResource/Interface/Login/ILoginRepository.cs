using HumanResource.Models;
using HumanResource.Models.Login;

namespace HumanResource.Interface.Login
{
    public interface ILoginRepository
    {
        Task<UserModel> GetEmployeeById(UserModel _user);
    }
}

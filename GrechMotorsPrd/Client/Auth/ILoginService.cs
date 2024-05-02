using GrechMotorsPrd.Shared.Models;

namespace GrechMotorsPrd.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
    }
}

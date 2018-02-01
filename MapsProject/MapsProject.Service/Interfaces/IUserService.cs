using MapsProject.Models.Models;

namespace MapsProject.Service.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserInfo(string name, string hashPass);
    }
}

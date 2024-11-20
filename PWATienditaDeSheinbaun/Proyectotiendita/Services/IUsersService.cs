using TiendaSModels;

namespace TiendaS.Client.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserDetail(string id);
        Task SaveUser(Users user);
        Task DeleteUser(string id);
        Task<Users> AuthenticateUser(UsersDTO userAuthDTO);
    }
}

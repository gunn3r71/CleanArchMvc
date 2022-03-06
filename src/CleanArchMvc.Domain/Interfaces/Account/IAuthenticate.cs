using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces.Account
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> RegisterUserTask(string email, string password);
        Task Logout();
    }
}
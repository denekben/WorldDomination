using System;
using System.Threading.Tasks;

namespace AppUser.Application.Services
{
    public interface IUserService
    {
        Task<bool> ExistsByNameAsync(string username);
        Task<string> GetUsernameAsync(Guid id);
    }
}

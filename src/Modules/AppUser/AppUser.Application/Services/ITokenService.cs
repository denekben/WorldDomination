using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUser.Application.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(string email, string username, IList<string> roles);
        public string GenerateRefreshToken();
    }
}

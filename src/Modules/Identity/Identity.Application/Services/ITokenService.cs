using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(string userId, string email, string username, IList<string> roles);
        public string GenerateRefreshToken();
    }
}

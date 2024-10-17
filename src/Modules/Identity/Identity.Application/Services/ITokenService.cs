using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(string userId, string email, string username, IList<string> roles);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

namespace Identity.Application.Services
{
    public interface IAuthService
    {

        // User section
        Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email);
        Task<bool> SigninUserAsync(string userName, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsByEmailAsync(string email);
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> IsUniqueUserName(string userName);
        Task<List<(string id, string userName, string email)>> GetAllUsersAsync();
        Task<bool> UpdateUserEmail(string id, string email);
        Task<bool> UpdateUserName(string id, string userName);

        // Tokens
        Task<bool> UpdateRefreshToken(string id, string refreshToken);
        Task<bool> RefreshTokenExpired(string id);


        // Role Section
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<bool> UpdateRole(string id, string roleName);

        // User's Role section
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<bool> AssignUserToRole(string email, string role);
        Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);
    }
}

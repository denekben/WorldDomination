﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure.Models;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using Identity.Application.Services;

namespace Identity.Infrastructure.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Helper
        public async Task<AuthUser> GetUserById(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
        public async Task<AuthUser> GetUserByUsername(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
        //

        public async Task<bool> AssignUserToRole(string email, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (_roleManager.Roles.FirstOrDefaultAsync(role => role.Name == roleName) != null)
            {
                throw new BadRequestException("Role already exists");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;

        }


        // Return multiple value
        public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email)
        {
            if (await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email) != null || await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName) != null)
            {
                throw new BadRequestException("Email or Username already taken");
            }
            var user = new AuthUser()
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return (result.Succeeded, user.Id);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId.ToString());
            if (roleDetails == null)
            {
                throw new NotFoundException("Role not found");
            }

            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await GetUserById(userId);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<List<(string id, string userName, string email)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null)
            {
                throw new NotFoundException("Users not found");
            }

            return users.Select(user => (user.Id, user.UserName, user.Email)).ToList();
        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null)
            {
                throw new NotFoundException("Roles not found");
            }

            return roles.Select(role => (role.Id, role.Name)).ToList();
        }

        public async Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await GetUserById(userId);

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                throw new NotFoundException("Roles not found");
            }
            return (user.Id, user.UserName, user.Email, roles);
        }

        public async Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                throw new NotFoundException("Roles not found");
            }
            return (user.Id, user.UserName, user.Email, roles);
        }

        public async Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                throw new NotFoundException("Roles not found");
            }
            return (user.Id, user.UserName, user.Email, roles);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await GetUserById(userId);

            return await _userManager.GetUserNameAsync(user);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await GetUserById(userId);

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                throw new NotFoundException("Roles not found");
            }
            return roles.ToList();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await GetUserById(userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        public async Task<bool> SigninUserAsync(string email, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public async Task<bool> UpdateUserEmail(string id, string email)
        {
            var user = await GetUserById(id);

            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }
            return (role.Id, role.Name);
        }

        public async Task<bool> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    throw new NotFoundException("Role not found");
                }
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    throw new ValidationException(result.Errors);
                }
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var existingRoles = await _userManager.GetRolesAsync(user);
            if (existingRoles == null)
            {
                throw new NotFoundException("Roles not found");
            }
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            result = await _userManager.AddToRolesAsync(user, usersRole);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserName(string id, string userName)
        {

            var user = await GetUserById(id);

            user.UserName = userName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateRefreshToken(string id, string refreshToken)
        {
            var user = await GetUserById(id);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(360);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> IsRefreshTokenValid(string username, string refreshToken)
        {
            var user = await GetUserByUsername(username);
            return (user.RefreshToken == refreshToken && user.RefreshTokenExpires > DateTime.UtcNow);
        }
    }
}

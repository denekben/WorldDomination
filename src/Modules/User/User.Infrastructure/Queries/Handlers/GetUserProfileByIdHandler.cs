using User.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Application.Users.Queries;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, UserDto>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserProfileByIdHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<UserDto> Handle(GetUserProfileById query, CancellationToken cancellationToken)
        {
            var userProfile = await _users
                .Where(u => u.Id == query.id)
                .Include(u => u.UserStatusReadModel)
                .Include(u => u.UserAchievmentsReadModel)
                .Select(u => new UserReadModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Bio = u.Bio,
                    Username = u.Username,
                    Email = u.Email,
                    ProfileImagePath = u.ProfileImagePath,
                    DefaultProfileImagePath = u.DefaultProfileImagePath,
                    UserStatusReadModel = new UserStatusReadModel
                    {
                        UserId = u.UserStatusReadModel.UserId,
                        ActivityStatus = u.UserStatusReadModel.ActivityStatus,
                        Country = u.UserStatusReadModel.Country,
                        RoundNumber = u.UserStatusReadModel.RoundNumber,
                        GameRole = u.UserStatusReadModel.GameRole
                    },
                    UserAchievmentsReadModel = u.UserAchievmentsReadModel.Select(ua => 
                    new UserAchievmentReadModel {
                        UserId = ua.UserId,
                        AchievmentId = ua.AchievmentReadModel.Id,
                        AchievedTime = ua.AchievedTime
                    }).ToList(),
                    CreatedTime = u.CreatedTime,
                    UpdatedTime = u.UpdatedTime
                }.AsDto()).SingleAsync();


            return userProfile;
        }
    }
}
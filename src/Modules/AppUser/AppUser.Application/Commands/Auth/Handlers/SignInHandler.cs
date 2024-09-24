using AppUser.Shared.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Auth.Handlers
{
    public class SignInHandler : IRequestHandler<SignIn, UserDto>
    {
        public Task<UserDto> Handle(SignIn request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

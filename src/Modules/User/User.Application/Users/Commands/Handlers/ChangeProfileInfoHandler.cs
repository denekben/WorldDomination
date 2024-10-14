using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace User.Application.Users.Commands.Handlers
{
    internal class ChangeProfileInfoHandler : IRequestHandler<ChangeProfileInfo>
    {
        public Task Handle(ChangeProfileInfo request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

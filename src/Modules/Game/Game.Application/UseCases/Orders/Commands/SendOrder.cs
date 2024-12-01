using Game.Application.DTOs;
using MediatR;

namespace Game.Application.UseCases.Orders.Commands
{
    public sealed record SendOrder(Guid CallerId, OrderDto Order) : IRequest;
}

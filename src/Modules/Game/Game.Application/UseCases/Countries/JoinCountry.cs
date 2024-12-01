using Game.Application.DTOs;
using MediatR;

namespace Game.Application.UseCases.Countries
{
    public sealed record JoinCountry(Guid CallerId, Guid CountryId, Guid RoomId) : IRequest<CountryDto>;
}

using MediatR;

namespace Game.Application.Countries
{
    public sealed record JoinCountry(Guid CountryId, Guid RoomId) : IRequest;
}

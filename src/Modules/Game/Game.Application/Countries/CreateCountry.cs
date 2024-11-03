using MediatR;

namespace Game.Application.Countries
{
    public sealed record CreateCountry(string NormalizedName, Guid RoomId) : IRequest;
}

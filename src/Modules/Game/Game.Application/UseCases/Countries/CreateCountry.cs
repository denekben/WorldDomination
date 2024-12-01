using MediatR;

namespace Game.Application.UseCases.Countries
{
    public sealed record CreateCountry(Guid CallerId, string NormalizedName, Guid RoomId) : IRequest<Guid>;
}

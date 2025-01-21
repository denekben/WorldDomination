namespace Game.Application.DTOs
{
    public sealed record GameEventDto
    (
        Guid Id,
        string Quality,
        string Title,
        string Description
    );
}

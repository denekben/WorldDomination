using User.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Shared.DTOs
{
    public sealed record UserStatusDto(
        string ActivityStatus,
        string? Country,
        int? RoundNumber,
        string? GameRole
    );
}
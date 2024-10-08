﻿using AppUser.Domain.ValueObjects;
using UserAccess.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace AppUser.Domain.Entities
{
    public sealed class ActivityStatus
    {
        public IdValueObject UserId { get; private set; }
        public IsInGameStatus IsInGameStatus { get; private set; }
        public string? Country { get; private set; }
        public int? RoundNumber { get; private set; }
        public string? GameRole { get; private set; }
        public User User { get; private set; }

        // EF
        private ActivityStatus() { }

        public static ActivityStatus Create(Guid userId)
        {
            return new ActivityStatus { 
                UserId = userId,
                IsInGameStatus = IsInGameStatus.NotInGame
            };
        }

        public override string ToString()
        {
            return $"{IsInGameStatus},{Country},{RoundNumber},{GameRole}";
        }
    }
}

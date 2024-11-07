﻿using Game.Domain.DomainModels.Games.Entities;
using Game.Shared.DTOs;
using MediatR;

namespace Game.Application.Orders.Commands
{
    public sealed record ApplyOrder(Order Order) : IRequest;
}
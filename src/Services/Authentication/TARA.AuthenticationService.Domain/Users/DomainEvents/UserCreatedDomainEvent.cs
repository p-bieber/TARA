﻿using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;
public sealed record UserCreatedDomainEvent(Guid Id, UserId UserId, Username Username, Email Email) : DomainEvent(Id);

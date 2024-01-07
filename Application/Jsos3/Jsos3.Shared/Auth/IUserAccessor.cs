﻿namespace Jsos3.Shared.Auth;

public interface IUserAccessor
{
    int Id { get; }
    int UserId { get; }
    string Email { get; }
    string Name { get; }
    string Surname { get; }
}
﻿namespace Jsos3.Shared.Auth;

internal class DummyUserAccessor : IUserAccessor
{
    public int Id => 163;

    public int UserId => 2;

    public UserType Type => UserType.Student;

    public string Email => "TestEmail@test.pl";

    public string Name => "Maciej";

    public string Surname => "Padula";
}

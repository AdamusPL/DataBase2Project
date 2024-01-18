namespace Jsos3.Shared.Auth;

internal class DummyUserAccessor : IUserAccessor
{
    public int Id => 231;

    public int UserId => 2;

    public UserType Type => UserType.Lecturer;

    public string Email => "TestEmail@test.pl";

    public string Name => "Maciej";

    public string Surname => "Padula";
}

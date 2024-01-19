namespace Jsos3.Shared.Auth;

internal class DummyUserAccessor : IUserAccessor
{
    public int Id => 163;

    public UserType Type => UserType.Student;

    public string NameAndSurname => "Dummy User";

    public string Login => "baka";

}

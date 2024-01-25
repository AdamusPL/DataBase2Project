namespace Jsos3.Shared.Auth;

public interface IUserAccessor
{
    int Id { get; }
    UserType Type { get; }
    string NameAndSurname { get; }
    bool IsAuthenticated => Id > 0;
    string Login { get; }

}

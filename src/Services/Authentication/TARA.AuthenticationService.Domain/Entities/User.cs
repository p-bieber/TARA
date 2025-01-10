using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User
{
    public UserId Id { get; private set; }
    public UserName Name { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private User(UserId userId, UserName name, Password password, Email email)
    {
        Id = userId;
        Name = name;
        Password = password;
        Email = email;
    }

    public static User Create(UserName username, Password password, Email email)
    {
        return new(UserId.Create(), username, password, email);
    }
}

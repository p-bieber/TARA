using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User
{
    public UserId Id { get; private set; }
    public UserName Name { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private User(UserName name, Password password, Email email)
    {
        Id = new UserId();
        Name = name;
        Password = password;
        Email = email;
    }

    public static User Create(string username, string password, string email)
    {
        User user = new(new(username), new(password), new Email(email));
        return user;
    }
}

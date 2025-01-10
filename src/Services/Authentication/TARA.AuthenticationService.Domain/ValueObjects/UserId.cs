namespace TARA.AuthenticationService.Domain.ValueObjects;

public class UserId
{
    public Guid Value { get; private set; }

    private UserId(Guid guid)
    {
        Value = guid;
    }

    public static UserId Create()
    {
        var guid = Guid.NewGuid();
        return new(guid);
    }
}
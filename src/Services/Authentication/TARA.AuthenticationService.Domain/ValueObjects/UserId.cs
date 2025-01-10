namespace TARA.AuthenticationService.Domain.ValueObjects;

public class UserId
{
    public Guid Value { get; private set; }

    public UserId()
    {
        var guid = Guid.NewGuid();
        Value = guid;
    }
}
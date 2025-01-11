namespace TARA.AuthenticationService.Domain.ValueObjects;

public class UserId
{
    public Guid Value { get; private set; }

    private UserId(Guid guid)
    {
        Value = guid;
    }

    public static UserId Create(Guid? guid = null)
    {
        if (guid == null)
            guid = Guid.NewGuid();
        return new((Guid)guid);
    }
}
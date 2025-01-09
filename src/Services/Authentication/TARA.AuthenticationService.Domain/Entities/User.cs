using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
}

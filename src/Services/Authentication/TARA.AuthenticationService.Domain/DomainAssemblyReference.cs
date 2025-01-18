using System.Reflection;

namespace TARA.AuthenticationService.Domain;
public static class DomainAssemblyReference
{
    public static Assembly Assembly => typeof(DomainAssemblyReference).Assembly;
}

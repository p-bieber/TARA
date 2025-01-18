using System.Reflection;

namespace TARA.AuthenticationService.Application;
public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => typeof(ApplicationAssemblyReference).Assembly;
}

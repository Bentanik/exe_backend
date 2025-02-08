using System.Reflection;

namespace exe_backend.Infrastructure;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

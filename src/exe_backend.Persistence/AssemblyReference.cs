using System.Reflection;

namespace exe_backend.Persistence;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

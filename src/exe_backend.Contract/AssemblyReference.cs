using System.Reflection;

namespace exe_backend.Contract;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

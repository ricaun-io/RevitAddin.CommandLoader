using System.Reflection;

namespace RevitAddin.CommandLoader.Tests.CodeDom
{
    public class CodeTester
    {
        const string code = """
public class Tests
{
    public void Test() { }
#if DEBUG
    public void Debug() { }
#endif
}
""";
        public string Code => code;
        public string[] Defines => new[] { "DEBUG" };
        public bool HasMethodTest(Assembly assembly)
        {
            var method = assembly.GetType("Tests").GetMethod("Test");
            return method is not null;
        }
        public bool HasMethodDebug(Assembly assembly)
        {
            var method = assembly.GetType("Tests").GetMethod("Debug");
            return method is not null;
        }
    }
}
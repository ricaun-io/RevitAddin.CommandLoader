using System.Reflection;

namespace RevitAddin.CommandLoader.Tests.CodeDom
{
    public class CodeTester
    {
        const string code = """
using System;
public class Tests
{
    public void Test() { }
#if DEBUG
    public void Debug() { }
#endif
}
""";
        const string code2 = """
using System;
public class Tests2
{
    public void Test() { }
#if DEBUG
    public void Debug() { }
#endif
}
""";
        public string[] Code => new[] { code, code2 };
        public string[] Defines => new[] { "DEBUG" };
        public bool HasMethodTest(Assembly assembly)
        {
            var method = assembly.GetType("Tests")?.GetMethod("Test");
            return method is not null;
        }
        public bool HasMethodDebug(Assembly assembly)
        {
            var method = assembly.GetType("Tests")?.GetMethod("Debug");
            return method is not null;
        }
        public bool HasMethodTest2(Assembly assembly)
        {
            var method = assembly.GetType("Tests2")?.GetMethod("Test");
            return method is not null;
        }
        public bool HasMethodDebug2(Assembly assembly)
        {
            var method = assembly.GetType("Tests2")?.GetMethod("Debug");
            return method is not null;
        }
    }
}
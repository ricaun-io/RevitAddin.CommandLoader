using RevitAddin.CommandLoader.Services.CodeDom;
using System.Reflection;

namespace RevitAddin.CommandLoader.Tests.CodeDom
{
    public class CodeDomTester
    {
        private readonly ICodeDomService codeDomService;
        private readonly CodeTester codeTester;

        public CodeDomTester(ICodeDomService codeDomService, CodeTester codeTester)
        {
            this.codeDomService = codeDomService;
            this.codeTester = codeTester;
        }

        public Assembly GenerateCode()
        {
            var assembly = codeDomService
                .SetDefines(codeTester.Defines)
                .GenerateCode(codeTester.Code);
            return assembly;
        }

        public bool Test()
        {
            var assembly = GenerateCode();
            return codeTester.HasMethodTest(assembly) && codeTester.HasMethodDebug(assembly);
        }
    }
}
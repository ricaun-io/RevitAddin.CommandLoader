using NUnit.Framework;
using RevitAddin.CommandLoader.Services;
using RevitAddin.CommandLoader.Services.CodeDom;
using System;

namespace RevitAddin.CommandLoader.Tests.CodeDom
{
    public class CodeDomTests
    {
        CodeTester CodeTester = new CodeTester();

        public void Test_ICodeDomService(ICodeDomService codeDomService)
        {
            var assembly = codeDomService
                .SetDefines(CodeTester.Defines)
                .GenerateCode(CodeTester.Code);

            Console.WriteLine(assembly);

            Assert.IsTrue(CodeTester.HasMethodTest(assembly), "Test method not found.");
            Assert.IsTrue(CodeTester.HasMethodDebug(assembly), "Debug method not found.");
        }

        [Test]
        public void Test_CodeDomService()
        {
            var codeDomService = CodeDomFactory.Instance;
            Test_ICodeDomService(codeDomService);
        }
    }
}
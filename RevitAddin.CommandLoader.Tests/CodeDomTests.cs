using NUnit.Framework;
using RevitAddin.CommandLoader.Services;
using RevitAddin.CommandLoader.Services.CodeDom;
using RevitAddin.CommandLoader.Tests.CodeDom;
using System;

namespace RevitAddin.CommandLoader.Tests
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

            Assert.IsTrue(CodeTester.HasMethodTest2(assembly), "Test method not found in the Tests2 class.");
            Assert.IsTrue(CodeTester.HasMethodDebug2(assembly), "Debug method not found in the Tests2 class.");
        }

        [Test]
        public void Test_CodeDomService()
        {
            var codeDomService = CodeDomFactory.Instance;
            Test_ICodeDomService(codeDomService);
        }
    }
}
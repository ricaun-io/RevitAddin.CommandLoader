using RevitAddin.CommandLoader.Services.CodeDom;

namespace RevitAddin.CommandLoader.Services
{
    public class CodeDomFactory
    {
        public static ICodeDomService Instance { get; private set; } = CreateCodeDomService();

        private static ICodeDomService CreateCodeDomService()
        {
            var provider = CodeProviderService.GetCSharpCodeProvider();

            return new CodeDomService(provider);
        }

    }
}
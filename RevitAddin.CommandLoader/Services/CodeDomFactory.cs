using RevitAddin.CommandLoader.Services.CodeDom;

namespace RevitAddin.CommandLoader.Services
{
    public class CodeDomFactory
    {
        public static ICodeDomService Instance { get; private set; } = CreateCodeDomService();

        private static ICodeDomService CreateCodeDomService()
        {
#if NET8_0_OR_GREATER
            return new CodeAnalysisCodeDomService();
#else
            var provider = CodeProviderService.GetCSharpCodeProvider();
            return new CodeDomService(provider);
#endif
        }

    }
}
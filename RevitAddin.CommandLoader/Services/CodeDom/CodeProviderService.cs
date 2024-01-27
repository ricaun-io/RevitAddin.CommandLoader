using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;

namespace RevitAddin.CommandLoader.Services.CodeDom
{
    public class CodeProviderService
    {
        public static CodeDomProvider GetCSharpCodeProvider(bool useLegacyCodeDom = false)
        {
#if NET48_OR_GREATER
            if (!useLegacyCodeDom)
            {
                return NewCSharpCodeProvider();
            }
#endif
            return new CSharpCodeProvider();
        }

#if NET48_OR_GREATER
        /// <summary>
        /// NewCSharpCodeProvider
        /// <code>https://github.com/aspnet/RoslynCodeDomProvider</code>
        /// </summary>
        /// <returns></returns>
        internal static CodeDomProvider NewCSharpCodeProvider()
        {
            var compilerSettings = new ProviderOptions()
            {
                CompilerFullPath = CompilerFullPath(@"roslyn/csc.exe"),
                CompilerServerTimeToLive = 300
            };
            return new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider(compilerSettings);
        }
        internal class ProviderOptions : Microsoft.CodeDom.Providers.DotNetCompilerPlatform.IProviderOptions
        {
            public string CompilerFullPath { get; set; }
            public int CompilerServerTimeToLive { get; set; }
            public string CompilerVersion { get; set; }
            public bool WarnAsError { get; set; }
            public bool UseAspNetSettings { get; set; }
            public IDictionary<string, string> AllOptions { get; set; }
        }
        private static string CompilerFullPath(string relativePath)
        {
            string frameworkFolder = Path.GetDirectoryName(typeof(CodeDomService).Assembly.Location);
            string compilerFullPath = Path.Combine(frameworkFolder, relativePath);

            return compilerFullPath;
        }
#endif
    }
}
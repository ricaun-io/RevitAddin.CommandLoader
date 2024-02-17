#if NET8_0_OR_GREATER

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RevitAddin.CommandLoader.Services.CodeDom
{
    public class CodeAnalysisCodeDomService : ICodeDomService
    {
        private string[] PreprocessorSymbols { get; set; }
        public ICodeDomService SetDefines(params string[] defines)
        {
            PreprocessorSymbols = defines;
            return this;
        }

        public Assembly GenerateCode(params string[] sourceCode)
        {
            var compilation = CompilationCode(sourceCode, PreprocessorSymbols);

            var filePath = Path.Combine(Path.GetTempPath(), compilation.Assembly.Name + ".dll");
            using (var file = File.Create(filePath))
            {
                var result = compilation.Emit(file);
            }

            return Assembly.LoadFrom(filePath);
        }

        private CSharpCompilation CompilationCode(string[] sourceCodes, string[] preprocessorSymbols = null)
        {
            var options = CSharpParseOptions.Default
                .WithLanguageVersion(LanguageVersion.Latest)
                .WithPreprocessorSymbols(preprocessorSymbols);

            var parsedSyntaxTrees = sourceCodes
                .Select(sourceCode => SyntaxFactory.ParseSyntaxTree(sourceCode, options))
                .ToArray();

            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
            };

            #region Add GetReferencedAssemblies
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var nameAssemblies = new Dictionary<string, Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assemblyNames.Any(e => e.Name == assembly.GetName().Name))
                {
                    nameAssemblies[assembly.GetName().Name] = assembly;
                }
            }
            foreach (var keyAssembly in nameAssemblies)
            {
                references.Add(MetadataReference.CreateFromFile(keyAssembly.Value.Location));
            }
            #endregion

            return CSharpCompilation.Create(Guid.NewGuid().ToString(),
                parsedSyntaxTrees,
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }
    }
}

#endif
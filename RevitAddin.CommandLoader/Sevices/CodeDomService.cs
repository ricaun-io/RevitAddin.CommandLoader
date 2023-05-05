using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RevitAddin.CommandLoader.Services
{
    public class CodeDomService
    {
        public Assembly GenerateCode(params string[] sources)
        {
            var compilationUnits = sources
                .Select(s => new CodeSnippetCompileUnit(s))
                .ToArray();

            return GenerateCode(compilationUnits);
        }

        public Assembly GenerateCode(params CodeCompileUnit[] compilationUnits)
        {
            CodeDomProvider provider = CodeProviderService.GetCSharpCodeProvider();
            CompilerParameters compilerParametes = new CompilerParameters();

            compilerParametes.GenerateExecutable = false;
            compilerParametes.IncludeDebugInformation = false;
            compilerParametes.GenerateInMemory = false;

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
                compilerParametes.ReferencedAssemblies.Add(keyAssembly.Value.Location);
            }
            #endregion

            CompilerResults results = provider.CompileAssemblyFromDom(compilerParametes, compilationUnits);
            return results.CompiledAssembly;
        }
    }
}
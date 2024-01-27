using System.Reflection;

namespace RevitAddin.CommandLoader.Services.CodeDom
{
    public interface ICodeDomService
    {
        Assembly GenerateCode(params string[] sourceCode);
        public ICodeDomService SetDefines(params string[] defines);

    }
}
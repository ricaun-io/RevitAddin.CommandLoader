using System;
using System.Linq;
using System.Reflection;

namespace RevitAddin.CommandLoader.Extensions
{
    public static class AppName
    {
        public static string GetNameVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return $"{assembly.GetName().Name} {assembly.GetName().Version.ToString(3)}";
        }

        public static string GetIcon()
        {
            return "Resources/CommandLoader.tiff";
        }

        public static string GetInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var result = $"App: {assemblyName.Name}\n";
            result += $"Version: {assemblyName.Version.ToString(3)}\n";
            result += $"Location: {assembly.Location}\n";
            result += $"ContextNumber: {GetContextNumber()}";

            return result;
        }

        public static string GetUri()
        {
            return "https://github.com/ricaun-io/RevitAddin.CommandLoader";
        }

        private static string GetContextNumber()
        {
#if NET
            var context = System.Runtime.Loader.AssemblyLoadContext.GetLoadContext(typeof(AppName).Assembly);
            return context.ToString().Split('#').LastOrDefault();
#else
            return "0";
#endif
        }
    }
}

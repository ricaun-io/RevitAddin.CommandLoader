using System;
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

        public static string GetInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var result = $"App: {assemblyName.Name}\n";
            result += $"Version: {assemblyName.Version.ToString(3)}\n";
            result += $"Location: {assembly.Location}";

            return result;
        }
    }
}

using Autodesk.Revit.UI;
using System;

namespace RevitAddin.CommandLoader.Extensions
{
    public static class AutodeskIconGeneratorUtils
    {
        private static string[] icons = new[] { "Grey", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Brown" };
        private static string[] types = new[] { "Box", "Cube" };
        private static string[] themes = new[] { "Light", "Dark" };
        private static char separetor = '-';
        private static string extension = ".tiff";
        private static string url = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/2.0.0/";

        private static int icon = 0;
        public static bool IsDark => UIThemeManager.CurrentTheme == UITheme.Dark;
        public static string GetBox()
        {
            return url + CreateIcon(icon++, 0, IsDark ? 1 : 0);
        }

        public static string GetCube()
        {
            return url + CreateIcon(icon++, 1, IsDark ? 1 : 0);
        }

        private static string CreateIcon(int icon = 0, int type = 0, int theme = 0)
        {
            var typeStr = types[type % types.Length];
            var iconStr = icons[icon % icons.Length];
            var themeStr = themes[theme % themes.Length];

            var name = $"{typeStr}{separetor}{iconStr}";
            if (string.IsNullOrEmpty(themeStr) == false)
                name += $"{separetor}{themeStr}";

            return $"{name}{extension}";
        }
    }
}

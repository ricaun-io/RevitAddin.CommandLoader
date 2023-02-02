using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.CommandLoader.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("CommandLoader");
            ribbonPanel.CreatePushButton<Commands.Command>("Command\rLoader")
                .SetLargeImage(Properties.Resources.CommandLoader.GetBitmapSource())
                .SetToolTip("Open CommandLoader window that compiles Revit code and creates pushbuttons for each `IExternalCommand`, `IExternalCommandAvailability` could be used in the same class to enable the availability features.")
                .SetLongDescription(GetLongDescription())
                .SetContextualHelp("https://github.com/ricaun-io/RevitAddin.CommandLoader");
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }

        private string GetLongDescription()
        {
            var assembly = this.GetType().Assembly;
            var assemblyName = assembly.GetName();
            var result = $"App: {assemblyName.Name}\n";
            result += $"Version: {assemblyName.Version.ToString(3)}\n";
            result += $"Location: {assembly.Location}";

            return result;
        }
    }

}
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandLoader.Extensions;
using ricaun.Revit.Github;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Drawing;
using ricaun.Revit.UI.Tasks;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RevitAddin.CommandLoader.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static GithubRequestService service;
        private static RibbonPanel ribbonPanel;
        private static RibbonPanel ribbonPanelAssembly;
        private static UIControlledApplication UIControlledApplication;

        private static RevitTaskService revitTaskService;
        public static IRevitTask RevitTask => revitTaskService;
        public Result OnStartup(UIControlledApplication application)
        {
            revitTaskService = new RevitTaskService(application);
            revitTaskService.Initialize();

            UIControlledApplication = application;
            ribbonPanel = application.CreatePanel("CommandLoader");
            ribbonPanel.CreatePushButton<Commands.Command>("Command\rLoader")
                .SetLargeImage(AppName.GetIcon())
                .SetToolTip("Open CommandLoader window that compiles Revit code and creates pushbuttons for each `IExternalCommand`, `IExternalCommandAvailability` could be used in the same class to enable the availability features.")
                .SetLongDescription(AppName.GetInfo())
                .SetContextualHelp(AppName.GetUri());

            service = new GithubRequestService("ricaun-io", "RevitAddin.CommandLoader");

            application.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;

#if DEBUG
            ribbonPanel.GetRibbonPanel().CustomPanelTitleBarBackground = System.Windows.Media.Brushes.Salmon;
#endif

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            revitTaskService?.Dispose();

            ribbonPanel?.Remove();
            ribbonPanelAssembly?.Remove();

            application.ControlledApplication.ApplicationInitialized -= ControlledApplication_ApplicationInitialized;
            return Result.Succeeded;
        }

        private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            Task.Run(async () =>
            {
                bool downloadedNewVersion = await service.Initialize();
                if (downloadedNewVersion)
                {
                    InfoCenterUtils.ShowBalloon("Download New Release!", null, AppName.GetUri());
                    Console.WriteLine($"RevitAddin.CommandLoader: {downloadedNewVersion}");
                }
            });
        }

        public static void CreateCommands(Assembly assembly)
        {
            if (ribbonPanelAssembly is not null) ribbonPanelAssembly?.Remove();

            var commands = assembly.GetTypes().Where(e => typeof(IExternalCommand).IsAssignableFrom(e));

            ribbonPanelAssembly = UIControlledApplication.CreatePanel("");
            foreach (var command in commands)
            {
                var button = ribbonPanelAssembly
                    .AddItem(ribbonPanel.NewPushButtonData(command));

                if (command.TryGetAttribute(out DisplayNameAttribute displayNameAttribute))
                {
                    if (!string.IsNullOrEmpty(displayNameAttribute.DisplayName))
                    {
                        button.SetText(displayNameAttribute.DisplayName);
                    }
                }
                if (command.TryGetAttribute(out DescriptionAttribute descriptionAttribute))
                {
                    if (!string.IsNullOrEmpty(descriptionAttribute.Description))
                    {
                        button.SetToolTip(descriptionAttribute.Description);
                    }
                }

                var needImage = true;
                if (command.TryGetAttribute(out DesignerAttribute designerAttribute))
                {
                    if (!string.IsNullOrEmpty(designerAttribute.DesignerTypeName))
                    {
                        button.SetLargeImage(designerAttribute.DesignerTypeName);
                        needImage = false;
                    }
                }

                if (needImage)
                {
                    button.SetLargeImage(AutodeskIconGeneratorUtils.GetCube());
                }
            }
        }

    }
}
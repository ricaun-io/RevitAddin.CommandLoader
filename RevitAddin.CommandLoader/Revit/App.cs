using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.Github;
using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using RevitAddin.CommandLoader.Extensions;
using System.ComponentModel;
using Revit.Async;

namespace RevitAddin.CommandLoader.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static GithubRequestService service;
        private static RibbonPanel ribbonPanel;
        private static RibbonPanel ribbonPanelAssembly;
        private static UIControlledApplication UIControlledApplication;
        public Result OnStartup(UIControlledApplication application)
        {
            RevitTask.Initialize(application);

            UIControlledApplication = application;
            ribbonPanel = application.CreatePanel("CommandLoader");
            ribbonPanel.CreatePushButton<Commands.Command>("Command\rLoader")
                .SetLargeImage(Properties.Resources.CommandLoader.GetBitmapSource())
                .SetToolTip("Open CommandLoader window that compiles Revit code and creates pushbuttons for each `IExternalCommand`, `IExternalCommandAvailability` could be used in the same class to enable the availability features.")
                .SetLongDescription(AppName.GetInfo())
                .SetContextualHelp("https://github.com/ricaun-io/RevitAddin.CommandLoader");

            service = new GithubRequestService("ricaun-io", "RevitAddin.CommandLoader");

            application.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;

#if DEBUG
            ribbonPanel.GetRibbonPanel().CustomPanelTitleBarBackground = System.Windows.Media.Brushes.Salmon;
#endif

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
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
                    button.SetLargeImage(ImageGeneratorUtils.GetLargeImageUri());
                }
            }
        }

    }
}
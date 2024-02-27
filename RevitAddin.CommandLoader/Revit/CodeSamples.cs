using System;
using System.Net.NetworkInformation;
using System.Security.Policy;

namespace RevitAddin.CommandLoader.Revit
{
    public class CodeSamples
    {
        public static string CommandThemeGist => "https://gist.github.com/ricaun/86334ff6560e3e8c4671148c5c995b39";
        public static string CommandVersionGist => "https://gist.github.com/ricaun/200a576c3baa45cba034ceedac1e708e";
        public static string Command =>
@"using System;
using System.ComponentModel;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
    [DisplayName(""Revit\rVersion"")]
    [Description(""Show a Window with the Revit VersionName."")]
    [Designer(""/UIFrameworkRes;component/ribbon/images/revit.ico"")]
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;
            System.Windows.MessageBox.Show(uiapp.Application.VersionName);
            return Result.Succeeded;
        }
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}";

        public static string CommandVersion =>
@"using System;
using System.ComponentModel;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
    [DisplayName(""Revit\rVersion"")]
    [Description(""Show a Window with the Revit VersionName."")]
    [Designer(""/UIFrameworkRes;component/ribbon/images/revit.ico"")]
    [Transaction(TransactionMode.Manual)]
    public class CommandVersion : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;
            System.Windows.MessageBox.Show(uiapp.Application.VersionName);
            return Result.Succeeded;
        }
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}";

        public static string CommandDeleteWalls =>
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
    [Transaction(TransactionMode.Manual)]
    public class DeleteWalls : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Get all walls in the document
            List<Wall> walls = new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            // Delete all walls
            using (Transaction trans = new Transaction(doc))
            {
                trans.Start(""Delete Walls"");
                foreach (Wall wall in walls)
                {
                    doc.Delete(wall.Id);
                }
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}";

        public static string CommandTask =>
@"using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
    [DisplayName(""Task"")]
    [Designer(""/UIFrameworkRes;component/ribbon/images/revit.ico"")]
    [Transaction(TransactionMode.Manual)]
    public class CommandTask : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            Task.Run(async () =>
            {
                await Task.Delay(100);
                System.Windows.MessageBox.Show(uiapp.Application.VersionName);
            });

            return Result.Succeeded;
        }
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}";
    }
}




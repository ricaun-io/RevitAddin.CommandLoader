using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandLoader.Services;
using System.ComponentModel;

namespace RevitAddin.CommandLoader.Revit.Commands
{
    [DisplayName("Command Test - Compile and Ribbon")]
    [Transaction(TransactionMode.Manual)]
    public class CommandTest : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;


            var source = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit_Delete_Walls
{
    [Transaction(TransactionMode.Manual)]
    public class DeleteWalls2 : IExternalCommand
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
    [DisplayName(""Command Name"")]
    [Description(""This is a command tooltip"")]
    [Designer("" / UIFrameworkRes;component/ribbon/images/revit.ico"")]
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
}
";
 
            try
            {
                var assembly = new CodeDomService().GenerateCode(source);
                App.CreateCommands(assembly);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            //System.Windows.MessageBox.Show(uiapp.Application.VersionName);

            return Result.Succeeded;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandLoader.Services;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace RevitAddin.CommandLoader.Revit.Commands
{
    [DisplayName("Command Test - Compile and Ribbon")]
    [Transaction(TransactionMode.Manual)]
    public class CommandTest : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            try
            {
                var codeDomService = CodeDomFactory.Instance;
                var assembly = codeDomService.GenerateCode(
                    CodeSamples.CommandVersion,
                    CodeSamples.CommandTask,
                    CodeSamples.CommandDeleteWalls);

                App.CreateCommands(assembly);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return Result.Succeeded;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}

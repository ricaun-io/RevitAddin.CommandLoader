using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandLoader.Services;
using System.ComponentModel;

namespace RevitAddin.CommandLoader.Revit.Commands
{
    [DisplayName("Command Test - Compile Gist Files")]
    [Transaction(TransactionMode.Manual)]
    public class CommandTestGistFiles : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var gistUrlFiles = "https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539";

            GistGithubUtils.TryGetGistFilesContent(gistUrlFiles, out string[] gistFilesContent);

            try
            {
                System.Console.WriteLine(gistFilesContent.Length);
                var codeDomService = CodeDomFactory.Instance;
                var assembly = codeDomService.GenerateCode(gistFilesContent);

                App.CreateCommands(assembly);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return Result.Succeeded;
        }
    }

}

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandLoader.Services;
using System;
using System.ComponentModel;

namespace RevitAddin.CommandLoader.Revit.Commands
{
    [DisplayName("Command Test - Compile Gist")]
    [Transaction(TransactionMode.Manual)]
    public class CommandTestGist : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var gistUrl = "https://gist.github.com/ricaun/4f62b8650d29f1ff837e7e77f9e8b552";

            GistGithubUtils.TryGetGistString(gistUrl, out string gistContent);

            try
            {
                CodeDomService codeDomService = new CodeDomService() { UseLegacyCodeDom = true };
                var assembly = codeDomService.GenerateCode(gistContent);

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

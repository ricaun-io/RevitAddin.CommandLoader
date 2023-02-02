using RevitAddin.CommandLoader.Extensions;
using RevitAddin.CommandLoader.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Windows;

namespace RevitAddin.CommandLoader.ViewModels
{
    public class CompileViewModel : ObservableObject
    {
        public static CompileViewModel ViewModel { get; set; } = new CompileViewModel();

        #region Public Properties
        public string Text { get; set; } = GetText();
        public IRelayCommand Command => new RelayCommand(CompileText);
        #endregion

        #region Constructor
        public CompileViewModel()
        {

        }
        #endregion

        #region View / Window
        public string Title { get; set; } = AppName.GetNameVersion();
        public object Icon { get; set; } = Properties.Resources.CommandLoader.GetBitmapSource();
        public CompileView Window { get; private set; }
        public void Show()
        {
            if (Window is null)
            {
                Window = new CompileView();
                Window.DataContext = this;
                Window.SetAutodeskOwner();
                Window.Closed += (s, e) => { Window = null; };
            }
            Window?.Show();
            Window?.Activate();
        }
        #endregion

        #region Private Methods
        private void CompileText()
        {
            Console.WriteLine(Text);
        }

        private static string GetText()
        {
            return @"using System;
using System.ComponentModel;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin
{
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
}";
        }
        #endregion
    }
}
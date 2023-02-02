using RevitAddin.CommandLoader.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Windows;

namespace RevitAddin.CommandLoader.ViewModels
{
    public class CompileViewModel : ObservableObject
    {
        #region Public Properties
        public string Text { get; set; }
        public IRelayCommand Command => new RelayCommand(CompileText);
        #endregion

        #region Constructor
        public CompileViewModel()
        {

        }
        #endregion

        #region View / Window
        public string Title { get; set; } = $"RevitAddin.CommandLoader";
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
        #endregion
    }
}
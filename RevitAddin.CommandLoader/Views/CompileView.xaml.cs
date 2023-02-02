using System;
using System.Windows;

namespace RevitAddin.CommandLoader.Views
{
    public partial class CompileView : Window
    {
        public CompileView()
        {
            InitializeComponent();
            InitializeWindow();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion
    }
}
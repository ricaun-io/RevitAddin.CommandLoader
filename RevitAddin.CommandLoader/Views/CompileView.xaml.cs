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
            this.KeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Escape) { this.Close(); } };
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
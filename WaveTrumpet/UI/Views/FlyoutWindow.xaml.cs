using System;
using System.Windows;
using System.Windows.Input;

namespace WaveTrumpet.UI.Views
{
    public partial class FlyoutWindow : Window
    {
        public FlyoutWindow()
        {
            InitializeComponent();
        }

        public event EventHandler HideRequested;

        public void PrepareForDisplay()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            var workArea = SystemParameters.WorkArea;
            var width = ActualWidth > 0 ? ActualWidth : (double.IsNaN(Width) ? 360 : Width);
            var height = ActualHeight > 0 ? ActualHeight : 320;
            Left = Math.Max(workArea.Left + 12, workArea.Right - width - 20);
            Top = Math.Max(workArea.Top + 12, workArea.Bottom - height - 20);
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            RaiseHideRequested();
        }

        private void OnWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                RaiseHideRequested();
            }
        }

        private void OnWindowDeactivated(object sender, EventArgs e)
        {
            RaiseHideRequested();
        }

        private void RaiseHideRequested()
        {
            var handler = HideRequested;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;
using WaveTrumpet.Extensions;
using WaveTrumpet.Interop;
using WaveTrumpet.Interop.Helpers;
using WaveTrumpet.UI.Themes;

namespace WaveTrumpet.UI.Views
{
    public partial class FlyoutWindow : Window
    {
        public FlyoutWindow()
        {
            InitializeComponent();
            SourceInitialized += OnSourceInitialized;
            Closed += OnClosed;

            if (Manager.Current != null)
            {
                Manager.Current.ThemeChanged += OnThemeChanged;
            }
        }

        public event EventHandler HideRequested;

        public void PrepareForDisplay()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            var workArea = SystemParameters.WorkArea;
            var width = Width * this.DpiX();
            UpdateLayout();
            LayoutRoot.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var height = LayoutRoot.DesiredSize.Height * this.DpiY();
            var top = Math.Max(workArea.Top + 12, workArea.Bottom - height - 20);
            var left = Math.Max(workArea.Left + 12, workArea.Right - width - 20);
            this.SetWindowPos(top, left, height, width);
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            this.Cloak();
            this.EnableRoundedCornersIfApplicable();
            this.ApplyExtendedWindowStyle(User32.WS_EX_TOOLWINDOW);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            if (Manager.Current != null)
            {
                Manager.Current.ThemeChanged -= OnThemeChanged;
            }
        }

        private void OnThemeChanged()
        {
            EnableAcrylicIfApplicable();
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

        public new void Show()
        {
            base.Show();
            this.Cloak(false);
            EnableAcrylicIfApplicable();
        }

        public new void Hide()
        {
            AccentPolicyLibrary.DisableAcrylic(this);
            this.Cloak();
            base.Hide();
        }

        private void EnableAcrylicIfApplicable()
        {
            AccentPolicyLibrary.EnableAcrylic(this, Manager.Current.ResolveRef("AcrylicColor_Flyout"), GetAccentFlags());
        }

        private User32.AccentFlags GetAccentFlags()
        {
            if (Environment.OSVersion.IsAtLeast(OSVersions.Windows11))
            {
                return User32.AccentFlags.DrawAllBorders;
            }

            return User32.AccentFlags.DrawTopBorder | User32.AccentFlags.DrawLeftBorder;
        }
    }
}

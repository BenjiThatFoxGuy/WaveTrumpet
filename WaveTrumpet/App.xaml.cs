using System;
using System.Windows;
using WaveTrumpet.DataModel;
using WaveTrumpet.UI.Helpers;
using WaveTrumpet.UI.ViewModels;

namespace WaveTrumpet
{
    public partial class App : Application
    {
        private static Mutex _mutex;
        private ShellNotifyIcon _trayIcon;
        private FlyoutWindow _flyoutWindow;
        private FlyoutViewModel _flyoutViewModel;
        private WaveDeviceManager _deviceManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ensure single instance
            bool createdNew;
            _mutex = new System.Threading.Mutex(true, "WaveTrumpet_SingleInstance", out createdNew);
            if (!createdNew)
            {
                Current.Shutdown();
                return;
            }

            ContinueStartup();
        }

        private void ContinueStartup()
        {
            // Load or initialize settings
            // Set up theme manager (use dark by default)
            var themeManager = new UI.Themes.Manager();

            // Initialize audio device manager (placeholder)
            _deviceManager = new WaveDeviceManager();
            _deviceManager.Initialize();

            // Create view model
            _flyoutViewModel = new FlyoutViewModel(_deviceManager);

            // Create tray icon
            _trayIcon = new ShellNotifyIcon();
            _trayIcon.ToolTip = "WaveTrumpet";
            _trayIcon.Icon = System.Drawing.SystemIcons.Application;
            _trayIcon.IsVisible = true;
            _trayIcon.TrayLeftMouseClick += OnTrayLeftClick;
            _trayIcon.TrayRightMouseClick += OnTrayRightClick;

            // Create flyout window (not shown yet)
            _flyoutWindow = new FlyoutWindow
            {
                DataContext = _flyoutViewModel,
                Owner = Current.MainWindow
            };
            _flyoutWindow.Closed += (s, e) => { /* reopen? */ };

            CompleteStartup();
        }

        private void CompleteStartup()
        {
            // Everything ready, show main window if needed (we start with tray-only)
            // EarTrumpet doesn't show main window on startup, only tray.
        }

        private void OnTrayLeftClick(object sender, EventArgs e)
        {
            if (_flyoutWindow.IsVisible)
            {
                _flyoutWindow.Hide();
            }
            else
            {
                // Position near tray? We'll just center on primary screen for now.
                _flyoutWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _flyoutWindow.Show();
                _flyoutWindow.Activate();
            }
        }

        private void OnTrayRightClick(object sender, EventArgs e)
        {
            // Context menu
            var menu = new System.Windows.Controls.ContextMenu();
            var exitItem = new System.Windows.Controls.MenuItem { Header = "Exit" };
            exitItem.Click += (s, e2) => Shutdown();
            menu.Items.Add(exitItem);
            menu.IsOpen = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _trayIcon?.Dispose();
            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
            base.OnExit(e);
        }
    }
}

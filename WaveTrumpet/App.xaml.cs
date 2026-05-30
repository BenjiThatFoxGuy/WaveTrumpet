using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using WaveTrumpet.DataModel;
using WaveTrumpet.UI.Helpers;
using WaveTrumpet.UI.ViewModels;
using WaveTrumpet.UI.Views;

namespace WaveTrumpet
{
    public partial class App : Application
    {
        private static Mutex _mutex;
        private ShellNotifyIcon _trayIcon;
        private FlyoutWindow _flyoutWindow;
        private FlyoutViewModel _flyoutViewModel;
        private WaveDeviceManager _deviceManager;
        private bool _ownsMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool createdNew;
            _mutex = new Mutex(true, "WaveTrumpet_SingleInstance", out createdNew);
            _ownsMutex = createdNew;
            if (!createdNew)
            {
                Shutdown();
                return;
            }

            new UI.Themes.Manager();
            _deviceManager = new WaveDeviceManager();
            _deviceManager.Initialize();
            _flyoutViewModel = new FlyoutViewModel(_deviceManager);
            _flyoutWindow = new FlyoutWindow
            {
                DataContext = _flyoutViewModel,
                ShowInTaskbar = false,
                Topmost = true
            };
            _flyoutWindow.HideRequested += OnFlyoutHideRequested;

            _trayIcon = new ShellNotifyIcon();
            _trayIcon.ToolTip = "WaveTrumpet";
            _trayIcon.Icon = System.Drawing.SystemIcons.Application;
            _trayIcon.IsVisible = true;
            _trayIcon.TrayLeftMouseClick += OnTrayLeftClick;
            _trayIcon.TrayRightMouseClick += OnTrayRightClick;
        }

        private void OnFlyoutHideRequested(object sender, EventArgs e)
        {
            if (_flyoutWindow != null)
            {
                _flyoutWindow.Hide();
            }
        }

        private void OnTrayLeftClick(object sender, EventArgs e)
        {
            ToggleFlyout();
        }

        private void OnTrayRightClick(object sender, EventArgs e)
        {
            if (_flyoutWindow != null)
            {
                _flyoutWindow.Hide();
            }

            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("Open", null, (s, args) => Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(ToggleFlyout)));
            menu.Items.Add("Exit", null, (s, args) => Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(Shutdown)));
            _trayIcon.ShowContextMenu(menu);
        }

        private void ToggleFlyout()
        {
            if (_flyoutWindow == null)
            {
                return;
            }

            if (_flyoutWindow.IsVisible)
            {
                _flyoutWindow.Hide();
                return;
            }

            _flyoutWindow.PrepareForDisplay();
            _flyoutWindow.Show();
            _flyoutWindow.Activate();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_trayIcon != null)
            {
                _trayIcon.TrayLeftMouseClick -= OnTrayLeftClick;
                _trayIcon.TrayRightMouseClick -= OnTrayRightClick;
                _trayIcon.Dispose();
            }

            if (_flyoutWindow != null)
            {
                _flyoutWindow.HideRequested -= OnFlyoutHideRequested;
                _flyoutWindow.Close();
            }

            if (_mutex != null)
            {
                if (_ownsMutex)
                {
                    _mutex.ReleaseMutex();
                }

                _mutex.Dispose();
            }

            base.OnExit(e);
        }
    }
}

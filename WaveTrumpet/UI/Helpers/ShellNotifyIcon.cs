using System;
using System.Drawing;
using System.Windows.Forms;

namespace WaveTrumpet.UI.Helpers
{
    public sealed class ShellNotifyIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private ContextMenuStrip _currentMenu;
        private bool _isContextMenuOpen;
        private bool _isDisposed;

        public ShellNotifyIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.MouseClick += OnMouseClick;
        }

        public event EventHandler TrayLeftMouseClick;

        public event EventHandler TrayRightMouseClick;

        public bool IsVisible
        {
            get { return _notifyIcon.Visible; }
            set { _notifyIcon.Visible = value; }
        }

        public string ToolTip
        {
            get { return _notifyIcon.Text; }
            set { _notifyIcon.Text = value; }
        }

        public Icon Icon
        {
            get { return _notifyIcon.Icon; }
            set { _notifyIcon.Icon = value; }
        }

        public void ShowContextMenu(ContextMenuStrip menu)
        {
            if (menu == null || _isDisposed || _isContextMenuOpen)
            {
                return;
            }

            CloseCurrentMenu();
            _currentMenu = menu;
            _currentMenu.Closed += OnContextMenuClosed;
            _isContextMenuOpen = true;
            _currentMenu.Show(Cursor.Position);
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            var menu = sender as ContextMenuStrip;
            if (menu == null)
            {
                return;
            }

            menu.Closed -= OnContextMenuClosed;
            if (ReferenceEquals(_currentMenu, menu))
            {
                _currentMenu = null;
            }

            _isContextMenuOpen = false;
            menu.Dispose();
        }

        private void CloseCurrentMenu()
        {
            if (_currentMenu == null)
            {
                return;
            }

            _currentMenu.Closed -= OnContextMenuClosed;
            if (!_currentMenu.IsDisposed)
            {
                _currentMenu.Close();
                _currentMenu.Dispose();
            }

            _currentMenu = null;
            _isContextMenuOpen = false;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var handler = TrayLeftMouseClick;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                var handler = TrayRightMouseClick;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            CloseCurrentMenu();
            _notifyIcon.MouseClick -= OnMouseClick;
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}

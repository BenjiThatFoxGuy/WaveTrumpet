using System;
using System.Drawing;
using System.Windows.Forms;

namespace WaveTrumpet.UI.Helpers
{
    public sealed class ShellNotifyIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

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
            if (menu == null)
            {
                return;
            }

            _notifyIcon.ContextMenuStrip = menu;
            menu.Closed += OnContextMenuClosed;
            menu.Show(Cursor.Position);
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            var menu = sender as ContextMenuStrip;
            if (menu != null)
            {
                menu.Closed -= OnContextMenuClosed;
                menu.Dispose();
                if (ReferenceEquals(_notifyIcon.ContextMenuStrip, menu))
                {
                    _notifyIcon.ContextMenuStrip = null;
                }
            }
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
            _notifyIcon.MouseClick -= OnMouseClick;
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}

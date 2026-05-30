using System.Windows;

namespace WaveTrumpet.UI.Helpers
{
    public class WindowHolder
    {
        public Window Window { get; private set; }

        public void Set(Window window)
        {
            Window = window;
        }

        public void Show()
        {
            if (Window != null)
            {
                Window.Show();
            }
        }

        public void Hide()
        {
            if (Window != null)
            {
                Window.Hide();
            }
        }
    }
}

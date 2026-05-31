using System;
using System.Windows.Media;

namespace WaveTrumpet.UI.Themes
{
    public class Manager
    {
        public static Manager Current { get; private set; }

        public event Action ThemeChanged;

        public Manager()
        {
            Current = this;
        }

        public Color ResolveRef(string key)
        {
            switch (key)
            {
                case "AcrylicColor_Flyout":
                    return Color.FromArgb(0xCC, 0x1B, 0x10, 0x27);
                default:
                    return Color.FromArgb(0xCC, 0x1B, 0x10, 0x27);
            }
        }

        public void RaiseThemeChanged()
        {
            var handler = ThemeChanged;
            if (handler != null)
            {
                handler();
            }
        }
    }
}

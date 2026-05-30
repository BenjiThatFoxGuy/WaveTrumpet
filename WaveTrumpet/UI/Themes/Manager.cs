using System.Windows;
using System.Windows.Media;

namespace WaveTrumpet.UI.Themes
{
    public class Manager
    {
        public Manager()
        {
            Apply(Application.Current != null ? Application.Current.Resources : null);
        }

        public void Apply(ResourceDictionary resources)
        {
            if (resources == null)
            {
                return;
            }

            SetBrush(resources, "ThemeFlyoutBackgroundBrush", Color.FromRgb(0x20, 0x20, 0x20));
            SetBrush(resources, "ThemeFlyoutChromeBrush", Color.FromRgb(0x2A, 0x2A, 0x2A));
            SetBrush(resources, "ThemeFlyoutBorderBrush", Color.FromRgb(0x3A, 0x3A, 0x3A));
            SetBrush(resources, "ThemeAccentBrush", Color.FromRgb(0x5A, 0xA9, 0xFF));
            SetBrush(resources, "ThemeTextBrush", Color.FromRgb(0xF4, 0xF4, 0xF4));
            SetBrush(resources, "ThemeSecondaryTextBrush", Color.FromRgb(0xB5, 0xB5, 0xB5));
            SetBrush(resources, "ThemeMutedBrush", Color.FromRgb(0x66, 0x66, 0x66));
            SetBrush(resources, "ThemeSliderBackgroundBrush", Color.FromRgb(0x3B, 0x3B, 0x3B));
        }

        private static void SetBrush(ResourceDictionary resources, string key, Color color)
        {
            resources[key] = new SolidColorBrush(color);
        }
    }
}

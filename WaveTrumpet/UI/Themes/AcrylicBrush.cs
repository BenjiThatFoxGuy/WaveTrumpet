using System.Windows.Media;

namespace WaveTrumpet.UI.Themes
{
    public static class AcrylicBrush
    {
        public static SolidColorBrush CreateFallback()
        {
            return new SolidColorBrush(Color.FromArgb(0xF2, 0x20, 0x20, 0x20));
        }
    }
}

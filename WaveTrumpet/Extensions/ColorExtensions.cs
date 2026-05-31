using System.Windows.Media;

namespace WaveTrumpet.Extensions
{
    public static class ColorExtensions
    {
        public static uint ToABGR(this Color abgrValue)
        {
            return (uint)(
                abgrValue.A << 24 |
                abgrValue.B << 16 |
                abgrValue.G << 8 |
                abgrValue.R);
        }
    }
}

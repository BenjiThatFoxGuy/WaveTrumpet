namespace WaveTrumpet.DataModel
{
    public class WaveDevice
    {
        public string DisplayName { get; set; }

        public string IconGlyph { get; set; }

        public double Volume { get; set; }

        public bool IsMuted { get; set; }

        public double PeakValueLeft { get; set; }

        public double PeakValueRight { get; set; }
    }
}

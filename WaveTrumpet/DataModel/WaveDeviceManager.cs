using System.Collections.Generic;

namespace WaveTrumpet.DataModel
{
    public class WaveDeviceManager
    {
        private readonly List<WaveDevice> _devices = new List<WaveDevice>();

        public void Initialize()
        {
            _devices.Clear();
            _devices.Add(new WaveDevice { DisplayName = "Mic", IconGlyph = "", Volume = 74, PeakValueLeft = 65, PeakValueRight = 58 });
            _devices.Add(new WaveDevice { DisplayName = "Game", IconGlyph = "", Volume = 52, PeakValueLeft = 48, PeakValueRight = 44 });
            _devices.Add(new WaveDevice { DisplayName = "Browser", IconGlyph = "", Volume = 37, PeakValueLeft = 29, PeakValueRight = 32 });
        }

        public IEnumerable<WaveDevice> GetDevices()
        {
            return _devices;
        }
    }
}

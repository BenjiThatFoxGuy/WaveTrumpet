using System.Collections.Generic;

namespace WaveTrumpet.DataModel
{
    public class WaveDeviceManager
    {
        private readonly List<WaveDevice> _devices = new List<WaveDevice>();

        public void Initialize()
        {
            _devices.Clear();
            _devices.Add(new WaveDevice { DisplayName = "Monitor Mix", IconGlyph = "", Volume = 100, PeakValueLeft = 92, PeakValueRight = 90 });
            _devices.Add(new WaveDevice { DisplayName = "Stream Mix", IconGlyph = "", Volume = 100, PeakValueLeft = 88, PeakValueRight = 86 });
            _devices.Add(new WaveDevice { DisplayName = "Elgato Wave XLR", IconGlyph = "", Volume = 100, PeakValueLeft = 94, PeakValueRight = 89 });
            _devices.Add(new WaveDevice { DisplayName = "Voice Chat", IconGlyph = "", Volume = 100, PeakValueLeft = 76, PeakValueRight = 72 });
            _devices.Add(new WaveDevice { DisplayName = "Music", IconGlyph = "", Volume = 100, PeakValueLeft = 83, PeakValueRight = 78 });
            _devices.Add(new WaveDevice { DisplayName = "Game", IconGlyph = "", Volume = 100, PeakValueLeft = 87, PeakValueRight = 80 });
            _devices.Add(new WaveDevice { DisplayName = "Aux 1", IconGlyph = "", Volume = 100, PeakValueLeft = 64, PeakValueRight = 60 });
            _devices.Add(new WaveDevice { DisplayName = "SFX", IconGlyph = "", Volume = 100, PeakValueLeft = 59, PeakValueRight = 55 });
        }

        public IEnumerable<WaveDevice> GetDevices()
        {
            return _devices;
        }
    }
}

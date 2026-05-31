using System;
using WaveTrumpet.DataModel;

namespace WaveTrumpet.UI.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {
        private readonly WaveDevice _device;

        public DeviceViewModel(WaveDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            _device = device;
        }

        public string DisplayName
        {
            get { return _device.DisplayName; }
        }

        public string BaseIconGlyph
        {
            get { return _device.IconGlyph; }
        }

        public string IconGlyph
        {
            get { return IsMuted ? "" : _device.IconGlyph; }
        }

        public double Volume
        {
            get { return _device.Volume; }
            set
            {
                if (Math.Abs(_device.Volume - value) < 0.01)
                {
                    return;
                }

                _device.Volume = value;
                OnPropertyChanged();
                OnPropertyChanged("VolumeText");
                OnPropertyChanged("PeakValueLeft");
                OnPropertyChanged("PeakValueRight");
            }
        }

        public bool IsMuted
        {
            get { return _device.IsMuted; }
            set
            {
                if (_device.IsMuted == value)
                {
                    return;
                }

                _device.IsMuted = value;
                OnPropertyChanged();
                OnPropertyChanged("IconGlyph");
                OnPropertyChanged("PeakValueLeft");
                OnPropertyChanged("PeakValueRight");
            }
        }

        public string VolumeText
        {
            get { return string.Format("{0:0}", Volume); }
        }

        public double PeakValueLeft
        {
            get { return IsMuted ? 0 : Math.Min(100, _device.PeakValueLeft + (Volume * 0.1)); }
        }

        public double PeakValueRight
        {
            get { return IsMuted ? 0 : Math.Min(100, _device.PeakValueRight + (Volume * 0.1)); }
        }
    }
}

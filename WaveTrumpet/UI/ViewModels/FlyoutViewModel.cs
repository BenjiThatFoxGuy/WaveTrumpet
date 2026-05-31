using System;
using System.Collections.ObjectModel;
using WaveTrumpet.DataModel;

namespace WaveTrumpet.UI.ViewModels
{
    public class FlyoutViewModel : BaseViewModel
    {
        public FlyoutViewModel(WaveDeviceManager deviceManager)
        {
            if (deviceManager == null)
            {
                throw new ArgumentNullException("deviceManager");
            }

            Devices = new ObservableCollection<DeviceViewModel>();
            foreach (var device in deviceManager.GetDevices())
            {
                Devices.Add(new DeviceViewModel(device));
            }
        }

        public string Header
        {
            get { return "WaveTrumpet (Elgato Wave Link 2.0)"; }
        }

        public ObservableCollection<DeviceViewModel> Devices { get; private set; }
    }
}


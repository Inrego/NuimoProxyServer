using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NuimoHass.Shared;
using NuimoSDK;

namespace NuimoHass.Service
{
    public class NuimoManager
    {
        private readonly PairedNuimoManager _pairedNuimoManager;
        private bool _isTimerRunning;
        public Dictionary<string, INuimoDevice> AvailableDevices { get; set; }

        public NuimoManager()
        {
            _pairedNuimoManager = new PairedNuimoManager();
            var timer = new Timer(5000);
            timer.Elapsed += ScanTimerOnElapsed;
            ScanTimerOnElapsed(null, null);
            timer.Start();
        }

        private async void ScanTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_isTimerRunning)
                return;
            _isTimerRunning = true;
            if (AvailableDevices == null)
                AvailableDevices = new Dictionary<string, INuimoDevice>();
            var devices = await _pairedNuimoManager.ListPairedNuimosAsync();
            foreach (var nuimoController in devices)
            {
                INuimoDevice availableDevice;
                if (!AvailableDevices.TryGetValue(nuimoController.Identifier, out availableDevice))
                {
                    availableDevice = new AvailableNuimoDevice(nuimoController);
                    AvailableDevices.Add(nuimoController.Identifier, availableDevice);
                    NuimoHub.AllClients.DeviceFound(availableDevice);
                }
                if (availableDevice.ShouldConnect &&
                    availableDevice.ConnectionState == NuimoConnectionState.Disconnected)
                {
                    await availableDevice.ConnectAsync();
                }
            }
            _isTimerRunning = false;
        }
    }
}

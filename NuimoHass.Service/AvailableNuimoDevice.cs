using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuimoHass.Shared;
using NuimoSDK;

namespace NuimoHass.Service
{
    public class AvailableNuimoDevice : INuimoDevice
    {
        public AvailableNuimoDevice(INuimoController controller)
        {
            Controller = controller;
            Controller.BatteryPercentageChanged += ControllerOnBatteryPercentageChanged;
            Controller.ConnectionStateChanged += Controller_ConnectionStateChanged;
            Controller.FirmwareVersionRead += Controller_FirmwareVersionRead;
            Controller.GestureEventOccurred += Controller_GestureEventOccurred;
        }

        private void Controller_GestureEventOccurred(INuimoController arg1, NuimoGestureEvent arg2)
        {
            NuimoHub.AllClients.ConnectionStateChanged(arg1.Identifier, arg2);
        }

        private void Controller_FirmwareVersionRead(INuimoController arg1, string arg2)
        {
            NuimoHub.AllClients.ConnectionStateChanged(arg1.Identifier, arg2);
        }

        private void Controller_ConnectionStateChanged(INuimoController arg1, NuimoConnectionState arg2)
        {
            NuimoHub.AllClients.ConnectionStateChanged(arg1.Identifier, arg2);
        }

        private void ControllerOnBatteryPercentageChanged(INuimoController nuimoController, int i)
        {
            Battery = i;
            NuimoHub.AllClients.BatteryPercentageChanged(nuimoController.Identifier, i);
        }

        public string Id => Controller.Identifier;
        public string FriendlyName { get; set; }
        public int Battery { get; private set; }
        public NuimoConnectionState ConnectionState => Controller.ConnectionState;
        public float LedBrightness => Controller.MatrixBrightness;
        public bool ShouldConnect => Config.Instance.AddedNuimos.Contains(Id);
        public Task<bool> ConnectAsync()
        {
            return Controller.ConnectAsync();
        }

        public Task<bool> DisconnectAsync()
        {
            return Controller.DisconnectAsync();
        }

        internal INuimoController Controller { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NuimoHass.Shared;
using NuimoHass.WPF.Properties;
using NuimoSDK;

namespace NuimoHass.WPF.ViewModels
{
    public class NuimoDevice : INuimoDevice, INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string FriendlyName { get; set; }
        public int Battery { get; }
        public NuimoConnectionState ConnectionState { get; }
        public float LedBrightness { get; }
        public bool ShouldConnect { get; }
        public Task<bool> ConnectAsync()
        {
            throw new NotImplementedException();
        }
        #region UIHelpers

        public string ConnectButtonText
        {
            get
            {
                switch (ConnectionState)
                {
                    case NuimoConnectionState.Connected:
                        return "Disconnect";
                    case NuimoConnectionState.Connecting:
                        return "Connecting";
                    case NuimoConnectionState.Disconnected:
                        return "Connect";
                    case NuimoConnectionState.Disconnecting:
                        return "Disconnecting";
                    default:
                        return null;
                }
            }
        }

        public bool ConnectButtonEnabled
        {
            get
            {
                switch (ConnectionState)
                {
                    case NuimoConnectionState.Disconnected:
                    case NuimoConnectionState.Connected:
                        return true;
                    default:
                        return false;
                }
            }
        }
        #endregion

        public Task<bool> DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NuimoDevice) obj);
        }

        protected bool Equals(NuimoDevice other)
        {
            return string.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

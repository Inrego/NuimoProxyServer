using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuimoSDK;

namespace NuimoHass.Shared
{
    public interface INuimoDevice
    {
        string Id { get; }
        string FriendlyName { get; }
        int Battery { get; }
        NuimoConnectionState ConnectionState { get; }
        float LedBrightness { get; }
        bool ShouldConnect { get; }
        Task<bool> ConnectAsync();
        Task<bool> DisconnectAsync();
    }
}

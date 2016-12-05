using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NuimoHass.Shared;
using NuimoSDK;

namespace NuimoHass.Service
{
    public class NuimoHub : Hub
    {
        public static readonly Lazy<IHubContext> Instance = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<NuimoHub>());

        public static dynamic AllClients => Instance.Value.Clients.All;
        public Bootstrap<INuimoDevice, Scene> GetBootstrap()
        {
            return new Bootstrap<INuimoDevice, Scene>
            {
                Devices = Initializer.NuimoManager.AvailableDevices.Values.ToList(),
                Scenes = Config.Instance.Scenes.Values.ToList(),
                Settings = new Settings
                {
                    HassUrl = Config.Instance.HassUrl,
                    HassPassword = Config.Instance.HassPassword
                }
            };
        }

        public void AddScene(Scene scene)
        {
            Config.Instance.Scenes.Add(scene.Id, scene);
            Config.Save();
            Clients.Others.AddScene(scene);
        }

        public void UpdateScene(Scene scene)
        {
            Config.Instance.Scenes[scene.Id] = scene;
            Config.Save();
            Clients.Others.UpdateScene(scene);
        }

        public void UpdateSettings(Settings settings)
        {
            Config.Instance.HassUrl = settings.HassUrl;
            Config.Instance.HassPassword = settings.HassPassword;
            Config.Save();
            Clients.Others.UpdateSettings(settings);
        }
    }
}

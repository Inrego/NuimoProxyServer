using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace NuimoHass.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var signalRConfig = new HubConfiguration
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR(signalRConfig);
        }
    }
}

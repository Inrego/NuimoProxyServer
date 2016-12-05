using System;
using Microsoft.Owin.Hosting;

namespace NuimoHass.Service
{
    public class Initializer : IDisposable
    {
        public static NuimoManager NuimoManager;
        private IDisposable _webServer;
        public void Initialize()
        {
            var url = "http://localhost:8124";
            _webServer = WebApp.Start(url);
            if (NuimoManager == null)
                NuimoManager = new NuimoManager();
        }

        public void Dispose()
        {
            _webServer.Dispose();
        }
    }
}

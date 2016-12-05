using System;
using System.ServiceProcess;

namespace NuimoHass.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (IsConsole)
            {
                var initializer = new Initializer();
                initializer.Initialize();
                Console.ReadLine();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static bool? _isConsole;
        public static bool IsConsole
        {
            get
            {
                if (!_isConsole.HasValue)
                {
                    try
                    {
                        var windowHeight = Console.WindowHeight;
                        _isConsole = true;
                    }
                    catch
                    {
                        _isConsole = false;
                    }
                }
                return _isConsole.Value;
            }
        }
    }
}

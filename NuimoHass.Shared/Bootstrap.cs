using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuimoHass.Shared
{
    public class Bootstrap<TDevice, TScene> where TDevice : INuimoDevice where TScene : IScene
    {
        public List<TDevice> Devices { get; set; }
        public List<TScene> Scenes { get; set; }
        public Settings Settings { get; set; }
    }
}

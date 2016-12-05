using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NuimoHass.Service
{
    public class Config
    {
        private static string FilePath => Path.Combine(Path.GetDirectoryName(typeof(Config).Assembly.Location), "config.json");
        static Config()
        {
            try
            {
                var json = File.ReadAllText(FilePath);
                Instance = JsonConvert.DeserializeObject<Config>(json);
            }
            catch (Exception)
            {
                Instance = new Config();
            }
        }

        public static void Save()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(Instance));
        }
        public static Config Instance { get; }
        public List<string> AddedNuimos = new List<string>();
        public Dictionary<Guid, Scene> Scenes = new Dictionary<Guid, Scene>();
        public string HassUrl { get; set; }
        public string HassPassword { get; set; }
    }
}

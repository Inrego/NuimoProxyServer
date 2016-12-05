using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuimoSDK;

namespace NuimoHass.Shared
{
    public class ServiceParameter
    {
        public string Domain { get; set; }
        public string Service { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}

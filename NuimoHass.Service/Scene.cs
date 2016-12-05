using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuimoHass.Shared;

namespace NuimoHass.Service
{
    public class Scene : IScene
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LedMatrix LedMatrix { get; set; }
        public ServiceParameter ClickParameter { get; set; }
        public ServiceParameter RotateParameter { get; set; }
        public ServiceParameter SwipeLeftParameter { get; set; }
        public ServiceParameter SwipeRightParameter { get; set; }
        public ServiceParameter SwipeUpParameter { get; set; }
        public ServiceParameter SwipeDownParameter { get; set; }
        public ServiceParameter FlyLeftParameter { get; set; }
        public ServiceParameter FlyRightParameter { get; set; }
        public ServiceParameter FlyBackwardsParameter { get; set; }
        public ServiceParameter FlyForwardsParameter { get; set; }
        public ServiceParameter FlyUpDownParameter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuimoHass.Shared
{
    public interface IScene
    {
        Guid Id { get; }
        string Name { get; }
        LedMatrix LedMatrix { get; }
        ServiceParameter ClickParameter { get; }
        ServiceParameter RotateParameter { get; }
        ServiceParameter SwipeLeftParameter { get; }
        ServiceParameter SwipeRightParameter { get; }
        ServiceParameter SwipeUpParameter { get; }
        ServiceParameter SwipeDownParameter { get; }
        ServiceParameter FlyLeftParameter { get; }
        ServiceParameter FlyRightParameter { get; }
        ServiceParameter FlyBackwardsParameter { get; }
        ServiceParameter FlyForwardsParameter { get; }
        ServiceParameter FlyUpDownParameter { get; }
    }
}

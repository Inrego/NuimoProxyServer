using System.ComponentModel;
using System.Runtime.CompilerServices;
using NuimoHass.Shared.Annotations;

namespace NuimoHass.Shared
{
    public class Settings : INotifyPropertyChanged
    {
        private string _hassUrl;
        public string HassUrl { get { return _hassUrl; } set { _hassUrl = value; OnPropertyChanged(); } }
        private string _hassPassword;
        public string HassPassword { get { return _hassPassword; } set { _hassPassword = value; OnPropertyChanged(); } }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

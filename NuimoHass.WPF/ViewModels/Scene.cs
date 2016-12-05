using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NuimoHass.Shared;
using NuimoHass.WPF.Annotations;

namespace NuimoHass.WPF.ViewModels
{
    public class Scene : IScene, INotifyPropertyChanged
    {
        private string _name = "Scene";
        public Guid Id { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private Shared.LedMatrix _matrix;

        public Shared.LedMatrix LedMatrix
        {
            get
            {
                if (_matrix == null)
                {
                    _matrix = new Shared.LedMatrix(true);
                    _matrix.PropertyChanged += Matrix_PropertyChanged;
                }
                return _matrix;
            }
            set
            {
                if (_matrix != null)
                    _matrix.PropertyChanged -= Matrix_PropertyChanged;
                _matrix = value;
                if (_matrix != null)
                    _matrix.PropertyChanged += Matrix_PropertyChanged;
                OnPropertyChanged();
            }
        }

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

        private void Matrix_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LedMatrix));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Scene) obj);
        }

        protected bool Equals(Scene other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

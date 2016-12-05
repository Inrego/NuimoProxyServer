using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using NuimoHass.Shared.Annotations;

namespace NuimoHass.Shared
{
    public class LedMatrix : INotifyPropertyChanged
    {
        public LedMatrix(bool initialize = false)
        {
            if (initialize)
                ResetMatrix();
        }
        [JsonIgnore]
        public Dictionary<string, BoolWrapperClass> Matrix { get; set; }
        private void ResetMatrix()
        {
            Matrix = new Dictionary<string, BoolWrapperClass>();
            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var val = new BoolWrapperClass(false);
                    val.PropertyChanged += Matrix_PropertyChanged;
                    Matrix.Add($"Led{x}{y}", val);
                }
            }
            OnPropertyChanged(nameof(Matrix));
        }

        private void Matrix_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(MatrixString));
            OnPropertyChanged(nameof(Matrix));
        }

        public string MatrixString
        {
            get
            {
                var sb = new StringBuilder();
                for (var x = 0; x < 9; x++)
                {
                    for (var y = 0; y < 9; y++)
                    {
                        sb.Append(Matrix[$"Led{x}{y}"].Value ? "." : " ");
                    }
                }
                return sb.ToString();
            }
            set
            {
                ResetMatrix();
                var charArr = value.ToCharArray();
                for (var x = 0; x < 9; x++)
                {
                    for (var y = 0; y < 9; y++)
                    {
                        Matrix[$"Led{x}{y}"].Value = charArr[x * 9 + y] != ' ';
                    }
                }
                OnPropertyChanged(nameof(Matrix));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public class BoolWrapperClass : INotifyPropertyChanged
        {
            private bool _value;

            public BoolWrapperClass(bool value)
            {
                Value = value;
            }
            public bool Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NuimoHass.Shared;
using NuimoHass.Shared.Annotations;

namespace NuimoHass.WPF.ViewModels
{
    public class ServiceDomain : NotifyPropertyChangedBase
    {
        private string _domain;
        public string Domain { get { return _domain; } set { _domain = value; OnPropertyChanged(); } }
        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services { get { return _services; } set { _services = value; OnPropertyChanged(); } }
    }

    public class Service : NotifyPropertyChangedBase
    {
        private string _domain;
        public string Domain { get { return _domain; } set { _domain = value; OnPropertyChanged(); } }
        private string _serviceName;
        public string ServiceName { get { return _serviceName; } set { _serviceName = value; OnPropertyChanged(); } }
        private string _description;
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }

    }
}

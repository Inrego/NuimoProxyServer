using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HassRestClient.Models;
using NuimoHass.Shared.Annotations;
using NuimoHass.WPF.ViewModels;

namespace NuimoHass.WPF
{
    /// <summary>
    /// Interaction logic for SceneControl.xaml
    /// </summary>
    public partial class SceneControl : UserControl
    {
        public static readonly DependencyProperty SceneProperty = DependencyProperty.Register("Scene",
            typeof(Scene), typeof(SceneControl), new FrameworkPropertyMetadata(new Scene()));

        public Scene Scene
        {
            get { return (Scene)GetValue(SceneProperty); }
            set
            {
                SetValue(SceneProperty, value);
            }
        }

        public static readonly DependencyProperty ServiceDomainsProperty = DependencyProperty.Register("ServiceDomains",
            typeof(ObservableCollection<ServiceDomain>), typeof(SceneControl), new FrameworkPropertyMetadata(new ObservableCollection<ServiceDomain>()));

        public ObservableCollection<ServiceDomain> ServiceDomains
        {
            get { return (ObservableCollection<ServiceDomain>) GetValue(ServiceDomainsProperty); }
            set { SetValue(ServiceDomainsProperty, value); }
        }

        public static readonly DependencyProperty EntityStatesProperty = DependencyProperty.Register(
            "EntityStates", typeof(ObservableCollection<EntityState>), typeof(SceneControl), new PropertyMetadata(default(ObservableCollection<EntityState>)));

        public ObservableCollection<EntityState> EntityStates
        {
            get { return (ObservableCollection<EntityState>) GetValue(EntityStatesProperty); }
            set { SetValue(EntityStatesProperty, value); }
        }
        public SceneControl()
        {
            InitializeComponent();
        }
    }
}

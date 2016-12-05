using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Newtonsoft.Json.Linq;
using NuimoHass.WPF.ViewModels;

namespace NuimoHass.WPF
{
    /// <summary>
    /// Interaction logic for ServiceDomainControl.xaml
    /// </summary>
    public partial class ServiceDomainControl : UserControl
    {
        public static readonly DependencyProperty ServiceDomainsProperty = DependencyProperty.Register(
            "ServiceDomains", typeof(ObservableCollection<ServiceDomain>), typeof(ServiceDomainControl), new PropertyMetadata(new ObservableCollection<ServiceDomain>()));

        public ObservableCollection<ServiceDomain> ServiceDomains
        {
            get { return (ObservableCollection<ServiceDomain>) GetValue(ServiceDomainsProperty); }
            set { SetValue(ServiceDomainsProperty, value); }
        }

        public static readonly DependencyProperty EntityStatesProperty = DependencyProperty.Register(
            "EntityStates", typeof(ObservableCollection<EntityState>), typeof(ServiceDomainControl), new PropertyMetadata(default(ObservableCollection<EntityState>)));

        public ObservableCollection<EntityState> EntityStates
        {
            get { return (ObservableCollection<EntityState>) GetValue(EntityStatesProperty); }
            set { SetValue(EntityStatesProperty, value); }
        }

        public static readonly DependencyProperty SelectedServiceDomainProperty = DependencyProperty.Register(
            "SelectedServiceDomain", typeof(string), typeof(ServiceDomainControl), new PropertyMetadata(default(string), SelectedServiceDomainChanged));

        private static void SelectedServiceDomainChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = (ServiceDomainControl) dependencyObject;
            var serviceDomain =
                control.ServiceDomains.First(d => d.Domain == (string) dependencyPropertyChangedEventArgs.NewValue);
            control.ServicesComboBox.Items.Clear();
            foreach (var serviceDomainService in serviceDomain.Services)
            {
                control.ServicesComboBox.Items.Add(serviceDomainService.Value);
            }
            control.EntitiesComboBox.Items.Clear();
            control.EntitiesComboBox.Items.Add(new HassEntity());
            foreach (var entityState in control.EntityStates)
            {
                if (entityState.EntityId.StartsWith(serviceDomain.Domain + ".") ||
                    (entityState.EntityId.StartsWith("group.") &&
                     GroupContainsService(serviceDomain.Domain, entityState.EntityId, control.EntityStates)))
                    control.EntitiesComboBox.Items.Add(new HassEntity(entityState));
            }
        }

        private static bool GroupContainsService(string service, string group, ObservableCollection<EntityState> allEntities)
        {
            var groupEntity = allEntities.First(e => e.EntityId == group);
            var ids = ((JArray)groupEntity.Attributes["entity_id"]).ToObject<string[]>();
            return ids.Any(id => id.StartsWith(service + ".")) || ids.Any(id => id.StartsWith(".group") && GroupContainsService(service, id, allEntities));
        }

        public string SelectedServiceDomain
        {
            get { return (string) GetValue(SelectedServiceDomainProperty); }
            set { SetValue(SelectedServiceDomainProperty, value); }
        }

        public static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register(
            "SelectedEntity", typeof(string), typeof(ServiceDomainControl), new PropertyMetadata(default(string)));

        public string SelectedEntity
        {
            get { return (string) GetValue(SelectedEntityProperty); }
            set { SetValue(SelectedEntityProperty, value); }
        }
        public ServiceDomainControl()
        {
            InitializeComponent();
        }
    }
}

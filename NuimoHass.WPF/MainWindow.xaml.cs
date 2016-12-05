using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.AspNet.SignalR.Client;
using NuimoHass.Shared;
using NuimoHass.WPF.ViewModels;

namespace NuimoHass.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHubProxy _hubProxy;
        private HubConnection _hubConnection;
        private readonly DeviceControl _deviceControl = new DeviceControl();
        private readonly SceneControl _sceneControl = new SceneControl();
        private readonly SettingsControl _settingsControl = new SettingsControl();
        public ObservableCollection<NuimoDevice> Devices { get; set; }
        public ObservableCollection<Scene> Scenes { get; set; }
        public ObservableCollection<ServiceDomain> HassServices { get; set; }
        public ObservableCollection<EntityState> HassStates { get; set; }

        public MainWindow()
        {
            Devices = new ObservableCollection<NuimoDevice>();
            Scenes = new ObservableCollection<Scene>();
            HassServices = new ObservableCollection<ServiceDomain>();
            HassStates = new ObservableCollection<EntityState>();
            _sceneControl.ServiceDomains = HassServices;
            _sceneControl.EntityStates = HassStates;
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                _hubConnection = new HubConnection("http://localhost:8124/signalr");
                _hubConnection.Reconnected += HubConnection_Reconnected;
                _hubConnection.Error += HubConnection_Error;
                _hubConnection.Closed += HubConnection_Closed;
                _hubProxy = _hubConnection.CreateHubProxy("NuimoHub");
                _hubProxy.On<Scene>("AddScene", AddScene);
                _hubProxy.On<Scene>("UpdateScene", UpdateScene);
                _hubProxy.On<NuimoDevice>("UpdateDevice", UpdateDevice);
                _hubProxy.On<NuimoDevice>("DeviceFound", DeviceFound);
                _hubProxy.On<Settings>("UpdateSettings", UpdateSettings);
                await _hubConnection.Start();
                var bootstrap = await _hubProxy.Invoke<Bootstrap<NuimoDevice, Scene>>("GetBootstrap");
                HandleBootstrap(bootstrap);
                SettingsButton_Click(null, null);
                await UpdateHassBootstrap();
            }
            catch (Exception)
            {

            }
        }

        private void UpdateSettings(Settings settings)
        {
            _settingsControl.Settings = settings;
        }

        private async void HubConnection_Closed()
        {
            await Task.Delay(5000);
            await _hubConnection.Start();
        }

        private async void HubConnection_Error(Exception obj)
        {
            await Task.Delay(5000);
            await _hubConnection.Start();
        }

        private async Task UpdateHassBootstrap()
        {
            if (!string.IsNullOrWhiteSpace(_settingsControl.Settings.HassUrl))
            {
                try
                {
                    var apiClient = new HassRestClient.ApiClient(_settingsControl.Settings.HassUrl, _settingsControl.Settings.HassPassword);
                    var bootstrap = await apiClient.GetBootstrapAsync();
                    HassServices.Clear();
                    foreach (var service in bootstrap.Services)
                    {
                        HassServices.Add(service);
                    }
                    HassStates.Clear();
                    foreach (var bootstrapState in bootstrap.States)
                    {
                        HassStates.Add(bootstrapState);
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }
        private void UpdateDevice(NuimoDevice device)
        {
            var i = Devices.IndexOf(device);
            Devices[i].PropertyChanged -= Device_PropertyChanged;
            Devices.RemoveAt(i);
            device.PropertyChanged += Device_PropertyChanged;
            Devices.Insert(i, device);
        }

        private void UpdateScene(Scene scene)
        {
            var i = Scenes.IndexOf(scene);
            Scenes[i].PropertyChanged -= Scene_PropertyChanged;
            Scenes.RemoveAt(i);
            scene.PropertyChanged += Scene_PropertyChanged;
            Scenes.Insert(i, scene);
        }

        private async void HubConnection_Reconnected()
        {
            var bootstrap = await _hubProxy.Invoke<Bootstrap<NuimoDevice, Scene>>("GetBootstrap");
            await Dispatcher.BeginInvoke((Action) (() => { HandleBootstrap(bootstrap); }));
        }
        
        private void HandleBootstrap(Bootstrap<NuimoDevice, Scene> bootstrap)
        {
            Devices.Clear();
            foreach (var device in bootstrap.Devices)
            {
                Devices.Add(device);
                device.PropertyChanged += Device_PropertyChanged;
            }
            Scenes.Clear();
            foreach (var scene in bootstrap.Scenes)
            {
                Scenes.Add(scene);
                scene.PropertyChanged += Scene_PropertyChanged;
            }
            _settingsControl.Settings = bootstrap.Settings;
            _settingsControl.Settings.PropertyChanged += Settings_PropertyChanged;
        }

        private async void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_settingsControl.Settings.HassUrl))
            {
                if (!_settingsControl.Settings.HassUrl.EndsWith("/"))
                {
                    _settingsControl.Settings.HassUrl += "/";
                    return;
                }
                await UpdateHassBootstrap();
            }
            if (e.PropertyName == nameof(_settingsControl.Settings.HassPassword))
                await UpdateHassBootstrap();
            await _hubProxy.Invoke("UpdateSettings", sender);
        }

        private void Scene_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _hubProxy.Invoke("UpdateScene", sender);
        }

        private void Device_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _hubProxy.Invoke("UpdateDevice", sender);
        }

        private async void AddScene(Scene scene)
        {
            await Dispatcher.BeginInvoke((Action) (() => { Scenes.Add(scene); }));
        }

        private async void DeviceFound(NuimoDevice device)
        {
            await Dispatcher.BeginInvoke((Action)(() => { Devices.Add(device); }));
        }

        private void DeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _deviceControl.Device = (NuimoDevice)e.AddedItems[0];
                ContentPanel.Content = _deviceControl;
                SceneList.SelectedItem = null;
            }
        }

        private void SceneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _sceneControl.Scene= (Scene) e.AddedItems[0];
                ContentPanel.Content = _sceneControl;
                DeviceList.SelectedItem = null;
            }
        }

        private void AddSceneButton_Click(object sender, RoutedEventArgs e)
        {
            var scene = new Scene
            {
                Id = Guid.NewGuid()
            };
            Scenes.Add(scene);
            SceneList.SelectedIndex = Scenes.Count - 1;
            _hubProxy.Invoke("AddScene", scene);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPanel.Content = _settingsControl;
            SceneList.SelectedItem = null;
            DeviceList.SelectedItem = null;
        }
    }
}

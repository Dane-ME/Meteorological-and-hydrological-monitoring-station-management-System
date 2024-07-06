using MQTT;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;

namespace System
{
    public partial class EventChanged
    {
        public event EventHandler StatusChanged;
        public virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        public static EventChanged instance;
        public static EventChanged Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventChanged();
                }
                return instance;
            }
        }
    }
}

namespace IService.Views
{
    public partial class StatusView : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<string> _topicKeys;
        public ObservableCollection<string> TopicKeys
        {
            get => _topicKeys;
            set
            {
                SetProperty(ref _topicKeys, value);
            }
        }

        public StatusView()
        {
            InitializeComponent();
            DataContext = this;
            UpdateTopicKeys();

            EventChanged.Instance.StatusChanged += (s, e) =>
            {
                Application.Current.Dispatcher.Invoke(UpdateTopicKeys);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void UpdateTopicKeys()
        {
            var newKeys = new ObservableCollection<string>(Broker.Instance.topicCallbacks.Keys.ToList());
            TopicKeys = newKeys;
        }
    }
}
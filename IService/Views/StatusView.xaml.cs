using MQTT;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IService.Views
{
    public class EventChanged
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
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView : UserControl
    {
        public ObservableCollection<string> _topickey;
        public ObservableCollection<string> TopicKeys 
        { 
            get => _topickey;
            set
            {
                _topickey = value;
                SetProperty(ref _topickey, value);
            } 
        }

        public StatusView()
        {
            InitializeComponent();
            TopicKeys = new ObservableCollection<string>(Broker.Instance.topicCallbacks.Keys);
            EventChanged.Instance.StatusChanged += (s, e) =>
            {
                if (TopicKeys == null)
                {
                    TopicKeys = new ObservableCollection<string>(Broker.Instance.topicCallbacks.Keys);
                }
                else
                {
                    TopicKeys.Clear();
                    foreach (var key in Broker.Instance.topicCallbacks.Keys)
                    {
                        TopicKeys.Add(key);
                    }
                }
            };
            DataContext = this;
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
            if (TopicKeys == null)
            {
                TopicKeys = new ObservableCollection<string>(Broker.Instance.topicCallbacks.Keys);
            }
            else
            {
                TopicKeys.Clear();
                foreach (var key in Broker.Instance.topicCallbacks.Keys)
                {
                    TopicKeys.Add(key);
                }
            }
        }
    }
}

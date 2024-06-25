using DoAn.Models;
using DoAn.Services;
using DoAn.Views;
using DoAn.Views.Loading;
using Microsoft.Maui.Layouts;
using MQTT;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private View _content;
        public View content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged();
                }
            }
        }
        public FlexLayout flexlayout { get; set; }
        public ScrollView scrollView { get; set; }
        public DocumentList _data;
        public List<Document> Data
        {
            get;
            set;
        }
        /// <summary>
        /// ////////////
        /// </summary>
        public event Action MyEvent;

        // Phương thức để kích hoạt sự kiện
        protected virtual void OnMyEvent()
        {
            MyEvent?.Invoke();
        }
        /// <summary>
        /// /////////
        /// </summary>
        public ICommand t { get; set; }

        public HomeViewModel()
        {
            SendAndListen();
            Data = new List<Document>();
            MyEvent += OnMyEventHandler;
            flexlayout = new FlexLayout()
            {
                Direction = FlexDirection.Row,
                Wrap = FlexWrap.Wrap,
                HorizontalOptions = LayoutOptions.Fill,
            };
            scrollView = new ScrollView();

            scrollView.Content = new WaitingView();
            content = scrollView;
            scrollView.Scrolled += OnScrolled;
        }
        private void OnScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;
            if (scrollView != null)
            {
                if (e.ScrollY <= 0)
                {
                    OnScrolledToStart();
                }
            }
        }

        private void OnScrolledToStart()
        {
            Broker.Instance.Listen($"dane/service/home/hhdangev02", HandleReceivedData);
        }
        public async Task SendAndListen()
        {
            await Task.Delay(500);

            Broker.Instance.Send($"dane/service/home/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });
            Broker.Instance.Listen($"dane/service/home/{Service.Instance.UserID}", HandleReceivedData);

        }
        private void HandleReceivedData(Document doc)
        {
            if (doc != null && doc.HomeData != null)
            {
                Data = doc.HomeData;
                OnMyEvent();
            }
        }
        private void OnMyEventHandler()
        {
            Task.Delay(1000);
            List<Document> hydrological = HydrologicalFilter(Data);
            List<Document> meteorological = MeteorologicalFilter(Data);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                flexlayout.Children.Clear();
                foreach(Document doc in hydrological)
                {
                    InitHydroView(doc);
                }
                foreach (Document doc in meteorological)
                {
                    InitMeteoView(doc);
                }
                scrollView.Content = flexlayout;
            });

            
            content = scrollView;
        }
        public List<Document> HydrologicalFilter(List<Document> dl)
        {
            List<Document> result = new List<Document>();
            foreach (var item in dl)
            {
                if (item.StationType == "Hydrological")
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public List<Document> MeteorologicalFilter(List<Document> dl)
        {
            List<Document> result = new List<Document>();
            foreach (var item in dl)
            {
                if (item.StationType == "Meteorological")
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public View InitHydroView(Document e)
        {
            var item = new HydrologicalView
            {
                BindingContext = e,
            };
            flexlayout.Children.Add(item);
            return item;
        }
        public View InitMeteoView(Document e)
        {
            var item = new MeteorologyView
            {
                BindingContext = e,
            };
            flexlayout.Children.Add(item);
            return item;
        }
    }
}

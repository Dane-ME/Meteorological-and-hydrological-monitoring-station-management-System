using IService.Services;
using IService.Views;
using IService.Views.Import;
using MQTT;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace IService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UIElement uIElement;
        public MainWindow()
        {
            InitializeComponent();
            uIElement = new StatusView();
            MainContent.Child = uIElement;
            
            
            //Task.Run(async () => {
            //    while(true)
            //    {
            //        await Task.Delay(2000);
            //        if (Broker.Instance.IsConnected)
            //        {
            //            Marklayer.Visibility = Visibility.Visible;
            //        }
            //        else { Marklayer.Visibility = Visibility.Hidden; }
            //    }
                
            //});
            Marklayer.Visibility = Visibility.Hidden;
            status.Click += (s, e) =>
            {
                uIElement = new StatusView();
                MainContent.Child = uIElement;
            };
            CreateAccount.Click += (s, e) =>
            {
                uIElement = new RegisterView();
                MainContent.Child = uIElement;
            };

            ImportData.Click += (s, e) => 
            {
                uIElement = new ImportView();
                MainContent.Child = uIElement;
            };

            InitStation.Click += (s, e) => 
            {
                uIElement = new ImportListView();
                MainContent.Child = uIElement;
            };
            
            var handle = new RequestManager();

            connect.Click += async (s, e) => {
                Broker.Instance.Connect();
                await Task.Delay(1000);
                if (Broker.Instance.IsConnected)
                {
                    Marklayer.Visibility = Visibility.Visible;
                }
                Broker.Instance.Listen("dane/usercontroller/login", (doc) =>
                {
                    handle.IsItStoredandSendResponse(doc);
                });
                Broker.Instance.Listen("dane/usercontroller/forgotpassword", (doc) =>
                {
                    handle.ForgotPasswordRequest(doc);
                });
                Broker.instance.Listen("dane/user/regis", (doc) =>
                {
                    if (doc != null)
                    {
                        DB.User.Insert(doc);
                    }
                });
            };

            disconnect.Click += async (s, e) =>
            {
                Broker.Instance?.Disconnect();
                await Task.Delay(1000);
                if (Broker.Instance.IsConnected)
                {
                    Marklayer.Visibility = Visibility.Visible;
                }
                else Marklayer.Visibility = Visibility.Hidden;
                //Broker.Instance.StopListening("dane/usercontroller/login", null);
            };
            
            
        }
        public Document FindToken(string id) { return DB.Token.Find(id); }
    }
}
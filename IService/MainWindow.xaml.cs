using IService.Views;
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

namespace IService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateAccount.Click += (s, e) =>
            {
                UIElement uIElement = new RegisterView();
                MainContent.Child = uIElement;
            };

            ImportData.Click += (s, e) => 
            {
                UIElement uIElement = new ImportView();
                MainContent.Child = uIElement;
            };
            
            
            var handle = new ManageThread();

            connect.Click += (s, e) => {
                Broker.Instance.Connect();
            };
            
            listen.Click += (s, e) => 
            {
                Broker.Instance.Listen("dane/usercontroller/login", (doc) =>
                {
                    handle.IsItStoredandSendResponse(doc);
                });
            };
        }
        public Document FindToken(string id) { return DB.Token.Find(id); }
    }
}
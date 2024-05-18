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

            save.Click += (s, e) =>
            {
                //Document topic = new Document() 
                //{
                //    ObjectId = "admin",
                //    ControllerName = controller.Text,
                //    ActionName = action.Text,
                //};
                //DB.Topic.Insert(topic);
                //Document data = FindToken("25dd720d4dbe5fee654ea5254a4405a1");
                //string t = data.Email;
            };

            regis.Click += (s, e) => 
            {
                string encode = (password.Text).ToMD5();
                Document user = new Document() 
                {
                    ObjectId = account.Text,
                    EncodePass = encode,

                };
                DB.User.Insert(user);
            };
            var handle = new ManageThread();

            connect.Click += (s, e) => {
                Broker.Instance.Connect();
            };
            
            listen.Click += (s, e) => 
            {
                //while (true)
                //{
                //    Broker.Instance.Listen("usercontroller/login/083155f7262837582f1c4c9c5d103155", (doc) =>
                //    {
                //        handle.IsItStoredandSendResponse(doc);
                //    });
                //    Thread.Sleep(2000);
                //}
                Broker.Instance.Listen("dane/usercontroller/login", (doc) =>
                {
                    handle.IsItStoredandSendResponse(doc);
                });
            };
        }
        public Document FindToken(string id) { return DB.Token.Find(id); }
    }
}
using System;
using System.Collections.Generic;
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

namespace IService.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
            regis.Click += (s, e) =>
            {
                string[] sm = manager.Text.Split(",");
                List<string> dsm = new List<string>();
                for(int i = 0;  i < sm.Length; i++)
                {
                    dsm.Add(sm[i]);
                }
                string encode = (password.Text).ToMD5();
                Document user = new Document()
                {
                    ObjectId = account.Text,
                    EncodePass = encode,
                    Role = role.Text,
                    StationManagement = dsm

                };
                DB.User.Insert(user);
            };
        }
    }
}

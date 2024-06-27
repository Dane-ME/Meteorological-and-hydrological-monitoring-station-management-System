using DoAn.Services;
using Microsoft.Maui.Controls;
using MQTT;

namespace DoAn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}

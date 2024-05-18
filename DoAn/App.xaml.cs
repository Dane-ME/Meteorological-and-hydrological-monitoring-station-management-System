using DoAn.Views;
using DoAn.ViewModels;
using DoAn.Services;

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

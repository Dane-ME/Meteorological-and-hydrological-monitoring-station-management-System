using DoAn.Views;
using DoAn.ViewModels;

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

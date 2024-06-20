using DoAn.ViewModels;
using DoAn.Views;

namespace DoAn
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
        }
    }
}

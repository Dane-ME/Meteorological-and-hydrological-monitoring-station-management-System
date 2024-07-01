using DoAn.ViewModels;
using DoAn.Views;
using DoAn.Views.AdminView;

namespace DoAn
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute("AddUserView", typeof(AddUserView));
            Routing.RegisterRoute("StationDetailView", typeof(StationDetailView));
            Routing.RegisterRoute("ForgotPasswordView", typeof(ForgotPasswordView));
            Routing.RegisterRoute("ChangePasswordView", typeof(ChangePasswordView));
        }


    }
}

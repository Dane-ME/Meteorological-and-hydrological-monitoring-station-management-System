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
            Routing.RegisterRoute("HomeView", typeof(HomeView));
            Routing.RegisterRoute("UserView", typeof(UserView));
            Routing.RegisterRoute("AdminPageView", typeof(AdminPageView));
            Routing.RegisterRoute("AddUserView", typeof(AddUserView));
            Routing.RegisterRoute("StationDetailView", typeof(StationDetailView));
            Routing.RegisterRoute("ForgotPasswordView", typeof(ForgotPasswordView)); 
        }

       
    }
}

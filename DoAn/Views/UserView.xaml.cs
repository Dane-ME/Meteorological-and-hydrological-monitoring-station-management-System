using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class UserView : ContentPage
{
	public UserView()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
	}
}
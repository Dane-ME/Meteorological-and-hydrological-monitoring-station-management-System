using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class UserView : ContentPage
{
	private readonly AuthService _authService;
	public UserView(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
        BindingContext = new UserViewModel(_authService);
	}
}
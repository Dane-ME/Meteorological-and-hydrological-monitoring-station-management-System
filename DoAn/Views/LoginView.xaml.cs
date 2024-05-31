using DoAn.Models;
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class LoginView : ContentPage
{
    private readonly UserModel _userModel;
    public LoginView( UserModel userModel)
	{
		InitializeComponent();
        _userModel = userModel;
        BindingContext = new LoginViewModel(_userModel);
    }
}
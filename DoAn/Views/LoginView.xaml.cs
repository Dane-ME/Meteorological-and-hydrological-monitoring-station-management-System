using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;
using System.Windows.Input;

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
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class ForgotPasswordView : ContentPage
{
	public ForgotPasswordView()
	{
		InitializeComponent();
		BindingContext = new ForgotPasswordViewModel();
	}
}
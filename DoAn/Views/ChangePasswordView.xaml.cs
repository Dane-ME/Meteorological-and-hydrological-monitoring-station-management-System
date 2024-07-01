using DoAn.ViewModels;

namespace DoAn.Views;

public partial class ChangePasswordView : ContentPage
{
	public ChangePasswordView()
	{
		InitializeComponent();
		BindingContext = new ChangePasswordViewModel();
	}
}
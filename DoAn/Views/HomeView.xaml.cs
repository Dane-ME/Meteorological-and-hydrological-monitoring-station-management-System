using DoAn.ViewModels;

namespace DoAn.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
		BindingContext = new HomeViewModel();
	}
}
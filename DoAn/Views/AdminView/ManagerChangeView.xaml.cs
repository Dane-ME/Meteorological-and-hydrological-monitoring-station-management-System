using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class ManagerChangeView : ContentView
{
	public ManagerChangeView(string ID)
	{
		InitializeComponent();
		BindingContext = new ManagerChangeViewModel(ID);
	}
}
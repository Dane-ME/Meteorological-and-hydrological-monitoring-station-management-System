using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class UserListView : ContentView
{
	private readonly UserListViewModel _model;
	public UserListView(UserListViewModel userListViewModel)
	{
		_model = userListViewModel;
		InitializeComponent();
	}
}
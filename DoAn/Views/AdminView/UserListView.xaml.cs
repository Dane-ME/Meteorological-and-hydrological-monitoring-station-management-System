using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class UserListView : ContentView
{
	private readonly UserListViewModel _userlistViewModel;
	public UserListView(UserListViewModel userListViewModel)
	{
		InitializeComponent();
        _userlistViewModel = userListViewModel;
		BindingContext = _userlistViewModel;
    }
}
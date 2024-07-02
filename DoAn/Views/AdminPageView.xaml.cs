using DoAn.ViewModels;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views.AdminView;

namespace DoAn.Views;

public partial class AdminPageView : ContentPage
{
    private readonly StationListViewModel _stationListViewModel;
    private readonly UserListViewModel _userListViewModel;
	public AdminPageView(StationListViewModel stationListViewModel,
		UserListViewModel userListViewModel)
	{
		InitializeComponent();
		_stationListViewModel = stationListViewModel;
		_userListViewModel = userListViewModel;
		BindingContext = new AdminPageViewModel(_stationListViewModel, _userListViewModel);
	}
}
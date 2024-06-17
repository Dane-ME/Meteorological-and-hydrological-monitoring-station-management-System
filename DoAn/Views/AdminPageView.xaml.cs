using DoAn.ViewModels;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views.AdminView;

namespace DoAn.Views;

public partial class AdminPageView : ContentPage
{
    private readonly StationListViewModel _stationListViewModel;
    private readonly StationProfileViewModel _stationProfileViewModel;
    private readonly UserListViewModel _userListViewModel;
    private readonly UserProfileViewModel _userprofileViewModel; 
	public AdminPageView(StationListViewModel stationListViewModel, 
		StationProfileViewModel stationProfileViewModel,
		UserListViewModel userListViewModel,
		UserProfileViewModel userProfileViewModel)
	{
		InitializeComponent();
		_stationListViewModel = stationListViewModel;
		_stationProfileViewModel = stationProfileViewModel;
		_userListViewModel = userListViewModel;
		_userprofileViewModel = userProfileViewModel;
		BindingContext = new AdminPageViewModel(_stationListViewModel, _stationProfileViewModel, _userListViewModel, _userprofileViewModel);
	}
}
using DoAn.ViewModels;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views.AdminView;

namespace DoAn.Views;

public partial class AdminPageView : ContentPage
{
	private readonly StationListViewModel _viewModel;
	public AdminPageView(StationListViewModel stationListViewModel)
	{
		InitializeComponent();
		_viewModel = stationListViewModel;
		BindingContext = new AdminPageViewModel(_viewModel);
	}
}
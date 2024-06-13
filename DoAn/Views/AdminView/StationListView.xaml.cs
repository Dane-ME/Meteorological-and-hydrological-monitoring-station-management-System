using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class StationListView : ContentView
{
	private readonly StationListViewModel _model;
	public StationListView(StationListViewModel stationListViewModel)
	{
		InitializeComponent();
		_model = stationListViewModel;
		BindingContext = _model;
	}
}
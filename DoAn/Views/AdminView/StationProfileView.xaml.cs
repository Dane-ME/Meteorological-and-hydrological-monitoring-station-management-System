using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class StationProfileView : ContentView
{
    private readonly StationListViewModel _model;

    public StationProfileView(StationListViewModel stationListViewModel)
	{
		InitializeComponent();
        _model = stationListViewModel;
        BindingContext = _model;
    }
}
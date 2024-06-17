using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class StationProfileView : ContentView
{
    private readonly StationProfileViewModel _model;

    public StationProfileView(StationProfileViewModel stationprofileViewModel)
	{
		InitializeComponent();
        _model = stationprofileViewModel;
        BindingContext = _model;
    }
}
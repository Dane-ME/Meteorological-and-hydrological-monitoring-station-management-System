using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class StationProfileView : ContentView
{
    public StationProfileView(string stationid)
	{
		InitializeComponent();
        BindingContext = new StationProfileViewModel(stationid);
    }
}
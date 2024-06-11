using DoAn.Models;
using DoAn.Services;

namespace DoAn.Views;

public partial class ListView : ContentPage
{
    private readonly AuthService _authService;
    private readonly StationModel _stationModel;
    public ListView(AuthService authService, StationModel stationModel)
	{
		InitializeComponent();
        _authService = authService;
        _stationModel = stationModel;

        reload.Clicked += (s, e) => 
        {
            _stationModel.SendRequest("home");
            Shell.Current.GoToAsync("//HomeView");
        };
	}
}
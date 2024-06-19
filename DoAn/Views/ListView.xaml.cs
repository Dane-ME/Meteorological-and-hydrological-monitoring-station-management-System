using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;

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
	}
}
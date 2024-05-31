using DoAn.Models;
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class HomeView : ContentPage
{
	private readonly StationModel _stationModel;
	public HomeView ( StationModel stationModel)
	{
		InitializeComponent();
		_stationModel = stationModel;
		BindingContext = new HomeViewModel(_stationModel);
	}
}
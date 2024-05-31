using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views.Loading
{
    public partial class LoadingView : ContentPage
    {
        private readonly StationModel _stationModel;
        public LoadingView( StationModel stationModel)
        {
            InitializeComponent();
            _stationModel = stationModel;
            BindingContext = new LoadingViewModel(_stationModel);
        }
        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            //string action = Service.Instance.objectrequested;
            int count = 0;
            bool isloadeddata = await _stationModel.IsLoadedData();
            while (!isloadeddata || count < 2)
            {
                isloadeddata = await _stationModel.IsLoadedData();
                count++;
            }
            await Shell.Current.GoToAsync($"//HomeView");
        }
    }
}


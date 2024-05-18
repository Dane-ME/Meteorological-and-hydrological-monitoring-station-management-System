using DoAn.Services;

namespace DoAn.Views.Loading
{
    public partial class CheckingNetworkView : ContentPage
    {
        private readonly AuthService _authService;
        public CheckingNetworkView(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            bool serverReady = await _authService.IsConnectedToNetworkAsync();
            while (!serverReady)
            {
                await DisplayAlert("Error", "L?i k?t n?i m?ng", "Th? l?i");
                serverReady = await _authService.IsConnectedToNetworkAsync();
            }

            // User is logged in
            // redirect to main page
            await Shell.Current.GoToAsync($"//LoginView");
        }
    }
}


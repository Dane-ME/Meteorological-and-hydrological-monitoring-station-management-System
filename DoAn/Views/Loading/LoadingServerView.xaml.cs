using DoAn.Services;

namespace DoAn.Views.Loading;

public partial class LoadingServerView : ContentPage
{
	private readonly AuthService _authService;
	public LoadingServerView(AuthService authServices )
	{
		InitializeComponent();
		_authService = authServices;
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        bool serverReady = await _authService.IsServerReady();
        while (!serverReady)
        {
            await DisplayAlert("Error", "Server not ready", "TryAgain");
            serverReady = await _authService.IsServerReady();
        }

        // User is logged in
        // redirect to main page
        await Shell.Current.GoToAsync($"//LoginView");
    }

}
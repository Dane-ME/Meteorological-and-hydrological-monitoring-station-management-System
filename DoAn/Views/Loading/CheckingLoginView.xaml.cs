using DoAn.Services;

namespace DoAn.Views.Loading;

public partial class CheckingLoginView : ContentPage
{
	private readonly AuthService _authService;
	public CheckingLoginView(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        int count = 0;
        bool serverReady = await Service.Instance.IsLoginAsync();
        while (!serverReady && count < 2)
        {
            serverReady = await Service.Instance.IsLoginAsync();
            count++;
        }

        if (serverReady)
        {
            await Shell.Current.GoToAsync("//HomeView");
        }
        else { await Shell.Current.GoToAsync("//LoginView"); }


    }
}
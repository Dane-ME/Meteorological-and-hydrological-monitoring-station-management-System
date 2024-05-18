using DoAn.Services;

namespace DoAn.Views;

public partial class ListView : ContentPage
{
    private readonly AuthService _authService;
    public ListView(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}
}
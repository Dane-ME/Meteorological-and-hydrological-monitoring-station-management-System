using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class UserProfileView : ContentView
{
	private readonly UserProfileViewModel _viewModel;
	public UserProfileView(UserProfileViewModel userProfileViewModel)
	{
		InitializeComponent();
		_viewModel = userProfileViewModel;
	}
}
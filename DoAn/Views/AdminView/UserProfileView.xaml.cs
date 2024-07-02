using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class UserProfileView : ContentView
{
	public UserProfileView(string userid)
	{
		InitializeComponent();
		BindingContext = new UserProfileViewModel(userid);
	}
}
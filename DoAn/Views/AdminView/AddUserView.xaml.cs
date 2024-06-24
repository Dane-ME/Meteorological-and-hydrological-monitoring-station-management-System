using DoAn.Services;
using DoAn.ViewModels.AdminViewModel;
using System.Text;


namespace DoAn.Views.AdminView;

public partial class AddUserView : ContentPage
{    
    private readonly AddUserViewModel _viewModel;
    public AddUserView()
	{
		InitializeComponent();
        _viewModel = new AddUserViewModel(); ;
        BindingContext = _viewModel;
        EventChanged.Instance.PopupHandle += (s, e) =>
        {
            OnPopupHandle();
        }; 
	}
    public async void OnPopupHandle()
    {   
        bool anwser = await DisplayAlert("Warning", "Bạn có muốn gửi đăng ký không?", "Có", "Không");
        _viewModel.Answer = anwser;
    }
}
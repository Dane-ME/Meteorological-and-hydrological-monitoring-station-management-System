using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views;

public partial class HomeView : ContentPage
{
    public HomeView()
	{
		//InitializeComponent();
		//BindingContext = new HomeViewModel();
        
        try
        {
            InitializeComponent();
            // Rest of your code
            BindingContext = new HomeViewModel();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in InitializeComponent: {ex.Message}");
            // Có thể thêm xử lý lỗi ở đây
        }

    }
}
using DoAn.ViewModels.RecordViewModel;
namespace DoAn.Views;


public partial class HydrologicalView : ContentView
{
	public HydrologicalView()
	{
		InitializeComponent();
        var displayInfo = DeviceDisplay.MainDisplayInfo;

        // L?y chi?u r?ng v� chi?u cao m�n h�nh
        double width = displayInfo.Width;
        double height = displayInfo.Height;

        // ?? ph�n gi?i m�n h�nh (density)
        double density = displayInfo.Density;

        // Chi?u r?ng v� chi?u cao m�n h�nh theo ??n v? logic
        double widthInDp = width / density;
        double heightInDp = height / density;

        Content.WidthRequest = widthInDp;

        //var doc = this.BindingContext as Document;
    }
    private async void OnGridTapped(object sender, EventArgs e)
    {
        Document doc = BindingContext as Document;

        var navigationParameter = new ShellNavigationQueryParameters
        {
            { "test", doc }
        };
        await Shell.Current.GoToAsync($"//StationDetailView", navigationParameter);
    }
}
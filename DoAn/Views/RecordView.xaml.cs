namespace DoAn.Views;


public partial class RecordView : ContentView
{
	public RecordView()
	{
		InitializeComponent();
        var displayInfo = DeviceDisplay.MainDisplayInfo;

        // L?y chi?u r?ng và chi?u cao màn hình
        double width = displayInfo.Width;
        double height = displayInfo.Height;

        // ?? phân gi?i màn hình (density)
        double density = displayInfo.Density;

        // Chi?u r?ng và chi?u cao màn hình theo ??n v? logic
        double widthInDp = width / density;
        double heightInDp = height / density;

        Content.WidthRequest = widthInDp;
    }
    private async void OnGridTapped(object sender, EventArgs e)
    {
        // X? lý s? ki?n nh?p chu?t ? ?ây
        await Shell.Current.GoToAsync("//StationDetailView");
    }
}
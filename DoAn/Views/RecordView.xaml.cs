namespace DoAn.Views;


public partial class RecordView : ContentView
{
	public RecordView()
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
    }
    private async void OnGridTapped(object sender, EventArgs e)
    {
        // X? l� s? ki?n nh?p chu?t ? ?�y
        await Shell.Current.GoToAsync("//StationDetailView");
    }
}
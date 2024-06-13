using DoAn.Services;
using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.Loading;

public partial class WaitingView : StackLayout
{
	public WaitingView()
	{
		InitializeComponent();


        //while (true)
        //{
        //	if(_stationListViewModel.IsLoading is false)
        //	{
        //		EventChanged.Instance.OnLoaded();
        //              break;
        //	}
        //}
    }
	public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
		propertyName: nameof(IndicatorColor),
		returnType: typeof(Color),
		defaultValue: Colors.LightBlue,
		declaringType:typeof(WaitingView),
		defaultBindingMode: BindingMode.OneWay);
	public Color IndicatorColor
	{
		get => (Color)GetValue(IndicatorColorProperty);
		set => SetValue(IndicatorColorProperty, value);
	}

	
}
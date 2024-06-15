using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;
namespace DoAn.Views;

public partial class StationDetailView : ContentPage
{
    ChartEntry[] entries = new[]
        {
            new ChartEntry(300)
            {
                Label = "0000",
                ValueLabel = "112",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(310)
            {
                Label = "0010",
                ValueLabel = "648",
                Color = SKColor.Parse("#77d065"),
            },
            new ChartEntry(290)
            {
                Label = "0020",
                ValueLabel = "428",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(250)
            {
                Label = "0040",
                ValueLabel = "214",
                Color = SKColor.Parse("#3498db")
            },
            new ChartEntry(210)
            {
                Label = "0050",
                ValueLabel = "112",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(230)
            {
                Label = "0100",
                ValueLabel = "648",
                Color = SKColor.Parse("#77d065"),
            },
            new ChartEntry(290)
            {
                Label = "0020",
                ValueLabel = "428",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(290)
            {
                Label = "0030",
                ValueLabel = "214",
                Color = SKColor.Parse("#3498db")
            }
        };
    public StationDetailView()
	{
		InitializeComponent();
        BindingContext = new ViewModel();
        #region MICROCHARTS

        LineChart chart = new LineChart() 
        {
            Entries = entries,
        };

        ChartView chartView = new ChartView();
        chartView.Chart = chart;
        //main.Chart = new LineChart() 
        //{
        //    Entries = entries
        //};
        #endregion

        #region HIGHCHART

        SfCartesianChart chart1 = new SfCartesianChart() { };
        SplineAreaSeries series1 = new SplineAreaSeries() { };
        
        #endregion
    }
}
public class Model
{
    public string Time { get; set; }

    public double Values { get; set; }

    public Model(string xValue, double yValue)
    {
        Time = xValue;
        Values = yValue;
    }
}

public class ViewModel
{
    public ObservableCollection<Model> Data { get; set; }

    public ViewModel()
    {
        Data = new ObservableCollection<Model>()
        {
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
            new Model("08:20", 2.1),
            new Model("08:30", 1.5),
            new Model("08:40", 1.7),
            new Model("08:50", 1.7),
            new Model("09:00", 2.4),
            new Model("09:00", 2.2),
        };
    }
}
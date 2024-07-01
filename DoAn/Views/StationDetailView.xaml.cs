using DoAn.ViewModels;
using Microcharts.Maui;
using SkiaSharp;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;
using System;
//using System.Collections.ObjectModel;


namespace DoAn.Views;
[QueryProperty(nameof(Data), "test")]
public partial class StationDetailView : ContentPage
{
    public Document Data
    {
        set
        {
            ShowMessage(value);
        }
    }
    public StationDetailView() 
    {
        InitializeComponent();
        //BindingContext = new StationDetailViewModel("6868");

    }

    public void ShowMessage(Document doc)
    {
        string stationid = doc.StationID;
        BindingContext = new StationDetailViewModel(stationid);
    }
    public async Task Wait()
    {
        await Task.Delay(2000);
    }

    
}
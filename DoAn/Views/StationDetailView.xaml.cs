using DoAn.ViewModels;
using Microcharts;
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
    }

    public async void ShowMessage(Document doc)
    {
        string stationid = doc.StationID;
        BindingContext = new StationDetailViewModel(stationid);
    }

    
}
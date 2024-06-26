using DoAn.Models.AdminModel;
using DoAn.ViewModels.AdminViewModel;

namespace DoAn.Views.AdminView;

public partial class StationChangeView : ContentView
{
	public StationChangeView(string userid)
	{
		InitializeComponent();
		BindingContext = new StationChangeViewModel(userid);
	}
    private string _filterText = string.Empty;
    public string FilterText
    {
        get => _filterText;
        set
        {
            if (_filterText != value)
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                OnFilterChanged();
            }
        }
    }
    private void FilterTextChanged(object sender, TextChangedEventArgs e)
    {
        FilterText = e.NewTextValue ?? string.Empty;
    }

    public void OnFilterChanged()
    {
        if (this.dataGrid?.View != null)
        {
            this.dataGrid.View.Filter = this.FilterRecords;
            this.dataGrid.View.RefreshFilter();
        }
    }

    public bool FilterRecords(object record)
    {
        if (string.IsNullOrWhiteSpace(FilterText))
            return true;

        StationChangeModel? orderInfo = record as StationChangeModel;
        if (orderInfo == null)
            return false;

        string selectedColumn = columns.SelectedItem?.ToString() ?? string.Empty;
        string filterTextLower = FilterText.ToLower();

        switch (selectedColumn)
        {
            case "StationName":
                return orderInfo.StationName.ToLower().Contains(filterTextLower);
            case "StationID":
                return orderInfo.StationID.ToLower().Contains(filterTextLower);
            default:
                return false;
        }
    }

    private void SearchButton_Pressed(object sender, EventArgs e)
    {
        OnFilterChanged();
    }

    private void ClearFilter_Clicked(object sender, EventArgs e)
    {
        FilterText = string.Empty;
        OnFilterChanged();
    }
}
using DoAn.Models.AdminModel;
using DoAn.ViewModels.AdminViewModel;
using Syncfusion.Maui.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DoAn.Views.AdminView;

public partial class ManagerChangeView : ContentView, INotifyPropertyChanged
{
    public ManagerChangeView(string ID)
    {
        InitializeComponent();
        BindingContext = new ManagerChangeViewModel(ID);
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

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        ManagerChangeModel? orderInfo = record as ManagerChangeModel;
        if (orderInfo == null)
            return false;

        string selectedColumn = columns.SelectedItem?.ToString() ?? string.Empty;
        string filterTextLower = FilterText.ToLower();

        switch (selectedColumn)
        {
            case "UserName":
                return orderInfo.UserName.ToLower().Contains(filterTextLower);
            case "UserID":
                return orderInfo.UserID.ToLower().Contains(filterTextLower);
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

//public partial class ManagerChangeView : ContentView
//{
//    private string _filterText = string.Empty;

//    public string FilterText
//    {
//        get => _filterText;
//        set
//        {
//            if (_filterText != value)
//            {
//                _filterText = value;
//                OnPropertyChanged(nameof(FilterText));
//                OnFilterChanged();
//            }
//        }
//    }
//    public ManagerChangeView(string ID)
//	{
//		InitializeComponent();
//		BindingContext = new ManagerChangeViewModel(ID);
//	}
//    //public string FilterText = string.Empty;

//    private void FilterTextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (e.NewTextValue == null)
//        {
//            this.FilterText = string.Empty;
//        }
//        else
//        {
//            this.FilterText = e.NewTextValue;
//        }
//    }

//    public void OnFilterChanged()
//    {
//        if (this.dataGrid!.View != null)
//        {
//            this.dataGrid.View.Filter = this.FilterRecords;
//            this.dataGrid.View.RefreshFilter();
//        }
//    }

//    public bool FilterRecords(object record)
//    {
//        ManagerChangeModel? orderInfo = record as ManagerChangeModel;

//        if (orderInfo != null)
//        {
//            if (columns.SelectedItem != null)
//            {
//                if (columns.SelectedItem.ToString() == "All columns")
//                {
//                    if (conditions.SelectedItem != null)
//                    {
//                        if (conditions.SelectedItem.ToString() == "Contains")
//                        {
//                            var filterText = FilterText.ToLower();
//                            if (orderInfo.UserName.ToString().ToLower().Contains(filterText) ||
//                                orderInfo.UserID.ToLower().Contains(filterText))
//                                return true;
//                            return false;
//                        }
//                        else if (conditions.SelectedItem.ToString() == "Equals")
//                        {
//                            if (FilterText.Equals(orderInfo.UserName.ToString()) ||
//                                FilterText.Equals(orderInfo.UserID))
//                                return true;
//                            return false;
//                        }
//                        else
//                        {
//                            if (!FilterText.Equals(orderInfo.UserName.ToString()) ||
//                               !FilterText.Equals(orderInfo.UserID))
//                                return true;
//                            return false;
//                        }
//                    }
//                }
//                else
//                {
//                    var property = record.GetType().GetProperty(columns.SelectedItem.ToString());
//                    var exactValue = property.GetValue(record, null);
//                    if (conditions.SelectedItem != null)
//                    {
//                        if (conditions.SelectedItem.ToString() == "Contains")
//                        {
//                            return exactValue.ToString().ToLower().Contains(FilterText.ToLower());
//                        }
//                        else if (conditions.SelectedItem.ToString() == "Equals")
//                        {
//                            return FilterText.Equals(exactValue.ToString());
//                        }
//                        else
//                        {
//                            return !FilterText.Equals(exactValue.ToString());
//                        }
//                    }
//                }
//            }

//        }

//        return false;

//    }


//    private void SearchButton_Pressed(object sender, EventArgs e)
//    {
//        OnFilterChanged();
//    }
//}
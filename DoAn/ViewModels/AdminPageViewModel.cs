using DoAn.Services;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views;
using DoAn.Views.AdminView;
using DoAn.Views.Loading;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class AdminPageViewModel
    {
        private readonly StationListViewModel _stationListViewModel;
        public ICommand StationTappedCommand { get; set; }
        public ICommand UserTappedCommand { get; set; }
        public View view {  get; set; }
        public AdminPageViewModel(StationListViewModel stationListViewModel) 
        {
            _stationListViewModel = stationListViewModel;
            Grid grid = new Grid();
            //this.view = new AdminPageView();
            var scrollview = new ScrollView();
            var stationlist = new StationListView(_stationListViewModel);


            grid.Children.Add(new WaitingView());
            scrollview.Content = grid;
            this.view = scrollview;
            LoadDataAsync(grid, scrollview, stationlist);


            //Check(grid, scrollview);
            //Task.Run(async () => 
            //{
            //    await Notice();
            //});
            EventChanged.Instance.Loaded += (s, e) => 
            {
                grid.Children.Clear();
                grid.Children.Add(stationlist);
                scrollview.Content = grid;
                this.view = scrollview;
            };

            StationTappedCommand = new Command(() =>
            {
                grid.Children.Clear();
                grid.Children.Add(new StationListView(_stationListViewModel));
                Check(grid, scrollview);
            });
            UserTappedCommand = new Command(() =>
            {
                grid.Children.Clear();
                grid.Children.Add(new UserListView());
                Check(grid, scrollview);

            });
        }
        public void Check(Grid grid, ScrollView scrollView)
        {
            if(_stationListViewModel.IsLoading)
            {
                grid.Children.Clear();
                grid.Children.Add(new WaitingView());
            }
            scrollView.Content = grid;
            this.view = scrollView;
        }

        public async Task Notice()
        {
            while(_stationListViewModel.IsLoading) { Task.Delay(500); }
            EventChanged.Instance.OnLoaded();
        }

        private async Task LoadDataAsync(Grid grid, ScrollView scrollview, StationListView stationlist)
        {
            await Task.Delay(1000);
            grid.Children.Add(stationlist);
            scrollview.Content = grid;
            this.view = scrollview;
        }

    }
}

using DoAn.Models.AdminModel;
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
        public readonly StationProfileViewModel _stationProfileViewModel;
        private readonly UserListViewModel _userListViewModel;
        private readonly UserProfileViewModel _userprofileViewModel;

        public StationListView _stationListView;
        public StationProfileView _stationProfileView;
        public UserListView _userListView;
        public UserProfileView _userprofileView;
        public ICommand StationTappedCommand { get; set; }
        public ICommand UserTappedCommand { get; set; }
        public View view {  get; set; }
        public Grid grid {  get; set; }
        public ScrollView scrollview{ get; set; }
        public readonly WaitingView waitingview = new WaitingView();
        public AdminPageViewModel(StationListViewModel stationListViewModel, 
            StationProfileViewModel stationProfileViewModel,
            UserListViewModel userListViewModel,
            UserProfileViewModel userProfileViewModel) 
        {
            _stationListViewModel = stationListViewModel;
            _stationProfileViewModel = stationProfileViewModel;
            _userListViewModel = userListViewModel;
            _userprofileViewModel = userProfileViewModel;

            _stationListView = new StationListView(_stationListViewModel);
            _stationProfileView = new StationProfileView(_stationProfileViewModel);
            _userListView = new UserListView(_userListViewModel);
            _userprofileView = new UserProfileView(_userprofileViewModel);

            _stationListViewModel.OnNavigateToStationDetail += NavigatedToStationDetail;
            _userListViewModel.OnNavigateToUserDetail += NavigatedToUserDetail;

            grid = new Grid();
            scrollview = new ScrollView();
            

            grid.Children.Add(waitingview);
            scrollview.Content = grid;
            this.view = scrollview;
            LoadDataAsync(grid, scrollview, _stationListView);

            //Check(grid, scrollview);

            EventChanged.Instance.Loaded += (s, e) => 
            {
                grid.Children.Clear();
                grid.Children.Add(_stationListView);
                scrollview.Content = grid;
                this.view = scrollview;
            };

            StationTappedCommand = new Command(() =>
            {
                grid.Children.Clear();
                grid.Children.Add(_stationListView);
                scrollview.Content = grid;
                this.view = scrollview;
                //Check(grid, scrollview);
            });
            UserTappedCommand = new Command(() =>
            {
                grid.Children.Clear();
                grid.Children.Add(_userListView);
                scrollview.Content = grid;
                this.view = scrollview;
                //Check(grid, scrollview);
            });
        }
        public void Check(Grid grid, ScrollView scrollView)
        {
            if(_stationListViewModel.IsLoading)
            {
                grid.Children.Clear();
                grid.Children.Add(waitingview);
            }
            scrollView.Content = grid;
            this.view = scrollView;
        }

        private async Task LoadDataAsync(Grid grid, ScrollView scrollview, StationListView stationlist)
        {
            await Task.Delay(2000);
            grid.Children.Add(stationlist);

            scrollview.Content = grid;
            this.view = scrollview;
        }

        public void NavigatedToStationDetail(StationListModel stationListModel)
        {
            this.grid.Children.Clear();
            _stationProfileViewModel.ID = stationListModel.ID;
            grid.Children.Add(_stationProfileView);
            scrollview.Content = grid;
            this.view = scrollview;
        }
        public void NavigatedToUserDetail(UserListModel userListModel)
        {
            this.grid.Children.Clear();
            _userprofileViewModel.ID = userListModel.ID;
            grid.Children.Add(_userprofileView);
            scrollview.Content = grid;
            this.view = scrollview;
        }
        public void NavigatedToWaitingView()
        {
            this.grid.Clear();
            grid.Children.Add(new WaitingView());
            Task.Delay(1000);
            grid.Children.Clear();
            grid.Children.Add(new StationListView(_stationListViewModel));
            Check(grid, scrollview);
        }

    }
}

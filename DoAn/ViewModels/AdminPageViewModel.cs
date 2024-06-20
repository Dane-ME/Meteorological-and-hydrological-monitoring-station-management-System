using DoAn.Models.AdminModel;
using DoAn.Services;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views.AdminView;
using DoAn.Views.Loading;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class AdminPageViewModel : ObservableObject
    {
        private readonly StationListViewModel _stationListViewModel;
        private readonly StationProfileViewModel _stationProfileViewModel;
        private readonly UserListViewModel _userListViewModel;
        private readonly UserProfileViewModel _userprofileViewModel;

        private StationListView _stationListView;
        private StationProfileView _stationProfileView;
        private UserListView _userListView;
        private UserProfileView _userprofileView;

        public ICommand StationTappedCommand { get; set; }
        public ICommand UserTappedCommand { get; set; }

        public View view { get; set; }
        public Grid Grid { get; set; }
        public ScrollView ScrollView { get; set; }
        public readonly WaitingView WaitingView = new WaitingView();

        private bool isStationListViewInitialized = false;
        private bool isStationProfileViewInitialized = false;
        private bool isUserListViewInitialized = false;
        private bool isUserProfileViewInitialized = false;

        public AdminPageViewModel(StationListViewModel stationListViewModel,
            StationProfileViewModel stationProfileViewModel,
            UserListViewModel userListViewModel,
            UserProfileViewModel userProfileViewModel)
        {
            _stationListViewModel = stationListViewModel;
            _stationProfileViewModel = stationProfileViewModel;
            _userListViewModel = userListViewModel;
            _userprofileViewModel = userProfileViewModel;

            _stationListViewModel.OnNavigateToStationDetail += NavigatedToStationDetail;
            _userListViewModel.OnNavigateToUserDetail += NavigatedToUserDetail;

            Grid = new Grid();
            ScrollView = new ScrollView
            {
                Content = Grid
            };
            view = ScrollView;

            LoadInitialView();

            StationTappedCommand = new Command(async () => await ShowStationListViewAsync());
            UserTappedCommand = new Command(async () => await ShowUserListViewAsync());
        }

        private async Task LoadInitialView()
        {
            Grid.Children.Add(WaitingView);
            await Task.Delay(2000);
            await ShowStationListViewAsync();
        }

        private async Task ShowStationListViewAsync()
        {
            if (!isStationListViewInitialized)
            {
                _stationListView = new StationListView(_stationListViewModel);
                Grid.Children.Clear();
                Grid.Children.Add(_stationListView);
                isStationListViewInitialized = true;
            }
            else
            {
                Grid.Children.Clear();
                Grid.Children.Add(_stationListView);
            }
            ScrollView.Content = Grid;
            view = ScrollView;
        }

        private async Task ShowUserListViewAsync()
        {
            if (!isUserListViewInitialized)
            {
                _userListView = new UserListView(_userListViewModel);
                Grid.Children.Clear();
                Grid.Children.Add(_userListView);
                isUserListViewInitialized = true;
            }
            else
            {
                Grid.Children.Clear();
                Grid.Children.Add(_userListView);
            }
            ScrollView.Content = Grid;
            view = ScrollView;
        }

        public void NavigatedToStationDetail(StationListModel stationListModel)
        {
            if (!isStationProfileViewInitialized)
            {
                _stationProfileViewModel.ID = stationListModel.ID;
                _stationProfileView = new StationProfileView(_stationProfileViewModel);
                isStationProfileViewInitialized = true;
            }
            else
            {
                _stationProfileViewModel.ID = stationListModel.ID;
            }
            Grid.Children.Clear();
            Grid.Children.Add(_stationProfileView);
            ScrollView.Content = Grid;
            view = ScrollView;
        }

        public void NavigatedToUserDetail(UserListModel userListModel)
        {
            if (!isUserProfileViewInitialized)
            {
                _userprofileViewModel.ID = userListModel.ID;
                _userprofileView = new UserProfileView(_userprofileViewModel);
                isUserProfileViewInitialized = true;
            }
            else
            {
                _userprofileViewModel.ID = userListModel.ID;
            }
            Grid.Children.Clear();
            Grid.Children.Add(_userprofileView);
            ScrollView.Content = Grid;
            view = ScrollView;
        }
    }

    //public class AdminPageViewModel : ObservableObject
    //{
    //    private readonly StationListViewModel _stationListViewModel;
    //    public readonly StationProfileViewModel _stationProfileViewModel;
    //    private readonly UserListViewModel _userListViewModel;
    //    private readonly UserProfileViewModel _userprofileViewModel;

    //    public StationListView _stationListView;
    //    public StationProfileView _stationProfileView;
    //    public UserListView _userListView;
    //    public UserProfileView _userprofileView;
    //    public ICommand StationTappedCommand { get; set; }
    //    public ICommand UserTappedCommand { get; set; }
    //    //public View _view;
    //    public View view {  get; set; }
    //    public Grid grid {  get; set; }
    //    public ScrollView scrollview{ get; set; }
    //    public readonly WaitingView waitingview = new WaitingView();
    //    public AdminPageViewModel(StationListViewModel stationListViewModel, 
    //        StationProfileViewModel stationProfileViewModel,
    //        UserListViewModel userListViewModel,
    //        UserProfileViewModel userProfileViewModel) 
    //    {
    //        _stationListViewModel = stationListViewModel;
    //        _stationProfileViewModel = stationProfileViewModel;
    //        _userListViewModel = userListViewModel;
    //        _userprofileViewModel = userProfileViewModel;

    //        _stationListView = new StationListView(_stationListViewModel);
    //        _stationProfileView = new StationProfileView(_stationProfileViewModel);
    //        _userListView = new UserListView(_userListViewModel);
    //        _userprofileView = new UserProfileView(_userprofileViewModel);

    //        _stationListViewModel.OnNavigateToStationDetail += NavigatedToStationDetail;
    //        _userListViewModel.OnNavigateToUserDetail += NavigatedToUserDetail;

    //        grid = new Grid();
    //        scrollview = new ScrollView();


    //        grid.Children.Add(waitingview);
    //        scrollview.Content = grid;
    //        this.view = scrollview;
    //        LoadDataAsync(grid, scrollview, _stationListView);

    //        //Check(grid, scrollview);

    //        //EventChanged.Instance.Loaded += (s, e) => 
    //        //{
    //        //    grid.Children.Clear();
    //        //    grid.Children.Add(_stationListView);
    //        //    scrollview.Content = grid;
    //        //    this.view = scrollview;
    //        //};

    //        StationTappedCommand = new Command(() =>
    //        {
    //            grid.Children.Clear();
    //            grid.Children.Add(_stationListView);
    //            scrollview.Content = grid;
    //            this.view = scrollview;
    //            //Check(grid, scrollview);
    //        });
    //        UserTappedCommand = new Command(() =>
    //        {
    //            grid.Children.Clear();
    //            grid.Children.Add(_userListView);
    //            scrollview.Content = grid;
    //            this.view = scrollview;
    //            //Check(grid, scrollview);
    //        });
    //    }
    //    public void Check(Grid grid, ScrollView scrollView)
    //    {
    //        if(_stationListViewModel.IsLoading)
    //        {
    //            grid.Children.Clear();
    //            grid.Children.Add(waitingview);
    //        }
    //        scrollView.Content = grid;
    //        this.view = scrollView;
    //    }

    //    private async Task LoadDataAsync(Grid grid, ScrollView scrollview, StationListView stationlist)
    //    {
    //        await Task.Delay(2000);
    //        grid.Children.Add(stationlist);

    //        scrollview.Content = grid;
    //        this.view = scrollview;
    //    }

    //    public void NavigatedToStationDetail(StationListModel stationListModel)
    //    {
    //        this.grid.Children.Clear();
    //        _stationProfileViewModel.ID = stationListModel.ID;
    //        grid.Children.Add(_stationProfileView);
    //        scrollview.Content = grid;
    //        this.view = scrollview;
    //    }
    //    public void NavigatedToUserDetail(UserListModel userListModel)
    //    {
    //        this.grid.Children.Clear();
    //        _userprofileViewModel.ID = userListModel.ID;
    //        grid.Children.Add(_userprofileView);
    //        scrollview.Content = grid;
    //        this.view = scrollview;
    //    }
    //    public void NavigatedToWaitingView()
    //    {
    //        this.grid.Clear();
    //        grid.Children.Add(new WaitingView());
    //        Task.Delay(1000);
    //        grid.Children.Clear();
    //        grid.Children.Add(new StationListView(_stationListViewModel));
    //        Check(grid, scrollview);
    //    }

    //}
}

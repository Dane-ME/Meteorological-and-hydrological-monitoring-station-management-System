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
        private readonly UserListViewModel _userListViewModel;

        private StationListView _stationListView;
        private UserListView _userListView;

        public ICommand StationTappedCommand { get; set; }
        public ICommand UserTappedCommand { get; set; }
        private View _view;
        public View view { get => _view; set => SetProperty(ref _view, value); }
        public Grid Grid { get; set; }
        public ScrollView ScrollView { get; set; }
        public readonly WaitingView WaitingView = new WaitingView();

        private bool isStationListViewInitialized = false;
        private bool isUserListViewInitialized = false;

        public AdminPageViewModel(StationListViewModel stationListViewModel,
            UserListViewModel userListViewModel)
        {
            _stationListViewModel = stationListViewModel;
            _userListViewModel = userListViewModel;

            _stationListViewModel.OnNavigateToStationDetail += NavigatedToStationDetail;
            _userListViewModel.OnNavigateToUserDetail += NavigatedToUserDetail;

            Grid = new Grid();
            ScrollView = new ScrollView
            {
                Content = Grid
            };
            view = ScrollView;

            LoadInitialView();

            StationTappedCommand = new Command(() => ShowStationListViewAsync());
            UserTappedCommand = new Command(() => ShowUserListViewAsync());
        }

        private async Task LoadInitialView()
        {
            Grid.Children.Add(WaitingView);
            await Task.Delay(2000);
            ShowStationListViewAsync();
        }

        private void ShowStationListViewAsync()
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
            EventChanged.Instance.OnStationList();
            ScrollView.Content = Grid;
            view = ScrollView;
        }
        private void ShowUserListViewAsync()
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
            EventChanged.Instance.OnUserList();
            ScrollView.Content = Grid;
            view = ScrollView;
        }

        public void NavigatedToStationDetail(StationListModel stationListModel)
        {
            StationProfileView content = new StationProfileView(stationListModel.ID);

            Grid.Children.Clear();
            Grid.Children.Add(content);

            EventChanged.Instance.OnStationIDChanged();
            ScrollView.Content = Grid;
            view = ScrollView;
        }

        public void NavigatedToUserDetail(UserListModel userListModel)
        {
            UserProfileView content = new UserProfileView(userListModel.ID);
            Grid.Children.Clear();
            Grid.Children.Add(content);
            ScrollView.Content = Grid;
            view = ScrollView;
        }
    }
}

namespace Taskban.WPF.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Taskban.WPF.Commands;
    using Taskban.WPF.Entities;

    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Board> ListBoxSource { get; set; }

        private Board _selectedBoard = new Board();

        public Board SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenBoardCommand { get; }
        public ICommand DeleteBoardCommand { get; }

        private readonly HomeViewModel _homeViewModel;
        private readonly NewBoardViewModel _boardViewModel;

        private readonly AppStates _appStates;

        public MainViewModel()
        {
            _homeViewModel = new HomeViewModel(GoToBoardView);
            _boardViewModel = new NewBoardViewModel(GoToHomeView);
            _appStates = App.UnitOfWork.AppStates.GetById(AppStates.AppStatesId);

            CurrentViewModel = _homeViewModel;

            OpenBoardCommand = new RelayCommand(OpenBoard, o => true);
            DeleteBoardCommand = new RelayCommand(DeleteBoard, o => true);

            ListBoxSource = new ObservableCollection<Board>(App.UnitOfWork.Boards.Get(orderBy: board => board.CreatedAt, ascending: false));
        }

        private void OpenBoard(object parameter)
        {
            var selectedBoard = SelectedBoard;
            App.WindowService.ShowBoard(selectedBoard);

            _appStates.LastBoardId = selectedBoard.Id;
            App.UnitOfWork.AppStates.Update(_appStates);
        }

        private void GoToBoardView()
        {
            CurrentViewModel = _boardViewModel;
        }

        private void GoToHomeView()
        {
            CurrentViewModel = _homeViewModel;
        }

        private void DeleteBoard(object parameter)
        {
            var board = (Board) parameter;
            ListBoxSource.Remove(board);
            App.UnitOfWork.Boards.Delete(board.Id);
        }
    }
}
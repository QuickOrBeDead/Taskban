namespace Taskban.WPF.Services
{
    using System.Windows;

    using Taskban.WPF.Entities;
    using Taskban.WPF.ViewModels;
    using Taskban.WPF.Views;

    public sealed class WindowService : IWindowService
    {
        private MainWindow _mainWindow;
        private BoardWindow _boardWindow;

        public void ShowMain()
        {
            _mainWindow = new MainWindow { DataContext = new MainViewModel(), WindowStartupLocation = WindowStartupLocation.CenterScreen };
            _mainWindow.SourceInitialized += (s, a) => _mainWindow.WindowState = WindowState.Maximized;
            _mainWindow.Show();
        }

        public void ShowBoard(Board board)
        {
            _boardWindow = new BoardWindow { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            _boardWindow.SourceInitialized += (s, a) => _boardWindow.WindowState = WindowState.Maximized;
            _boardWindow.DataContext = new BoardViewModel {BoardWindow = _boardWindow, Board = board};
            _boardWindow.Show();

            _mainWindow?.Close();
        }

        public void ShowMainCloseBoard()
        {
            ShowMain();
            _boardWindow.Close();
        }
    }
}
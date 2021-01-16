namespace Taskban.WPF
{
    using Taskban.WPF.Entities;
    using Taskban.WPF.Database;
    using Taskban.WPF.Services;

    using System.Windows;

    public partial class App
    {
        public static IWindowService WindowService { get; } = new WindowService();

        public static IUnitOfWork UnitOfWork { get; } = new UnitOfWork();

        protected override void OnStartup(StartupEventArgs e)
        {
            var lastBoard = GetLastBoard();

            if (lastBoard != null)
            {
                WindowService.ShowBoard(lastBoard);
            }
            else
            {
                WindowService.ShowMain();
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            UnitOfWork.Dispose();

            base.OnExit(e);
        }

        private static Board GetLastBoard()
        {
            var appStates = GetAppStates();

            if (appStates.LastBoardId.HasValue)
            {
                return UnitOfWork.Boards.GetById(appStates.LastBoardId.Value);
            }

            return null;
        }

        private static AppStates GetAppStates()
        {
            var appStatesRepository = UnitOfWork.AppStates;
            var appStates = appStatesRepository.GetById(AppStates.AppStatesId);
            if (appStates == null)
            {
                appStates = new AppStates();
                appStatesRepository.Insert(appStates);
            }

            return appStates;
        }
    }
}
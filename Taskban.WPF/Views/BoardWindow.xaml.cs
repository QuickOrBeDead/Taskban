namespace Taskban.WPF.Views
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using Microsoft.Win32;

    using Syncfusion.UI.Xaml.Kanban;

    using Taskban.WPF.Entities;
    using Taskban.WPF.ViewModels;

    public partial class BoardWindow
    {
        public BoardWindow()
        {
            InitializeComponent();
        }

        private void SfKanban_OnCardTapped(object sender, KanbanTappedEventArgs e)
        {
            var viewModel = (BoardViewModel) DataContext;
            viewModel.SelectedTask = (Task) e.SelectedCard.Content;
            viewModel.TaskViewWidth = 250;
            ClearTaskTagEntry();
            ClearSubTaskEntry();
        }

        public void ClearSubTaskEntry()
        {
            SubTaskIsCompletedCheckBox.IsChecked = false;
            SubTaskTitleTextBox.Text = string.Empty;
        }

        public void ClearTaskTagEntry()
        {
            TaskTagTextBox.Text = string.Empty;
        }

        private void BoardWindow_OnClosed(object sender, EventArgs e)
        {
            SaveBoard();
        }

        private void SaveBoard()
        {
            var viewModel = (BoardViewModel)DataContext;
            viewModel.SaveBoard(null);
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }

        private void AddFileLinkButton_Click(object sender, RoutedEventArgs e)
        { 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var viewModel = (BoardViewModel)DataContext;
                viewModel.SelectedTask.FilePath = openFileDialog.FileName;
            }
        }

        private void OpenFileLinkButton_Click(object sender, RoutedEventArgs e)
        {
            var control = (FrameworkContentElement)sender;
            var task = (Task)control.DataContext;

            try
            {
                Process.Start(new ProcessStartInfo(task.FilePath)
                                  {
                                      UseShellExecute = true
                                  });
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"File Open Error!!{Environment.NewLine}{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SfKanban_OnCardDragEnd(object sender, KanbanDragEndEventArgs e)
        {
            SaveBoard();
        }
    }
}
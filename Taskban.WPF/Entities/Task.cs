namespace Taskban.WPF.Entities
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Taskban.WPF.Annotations;

    public sealed class Task : INotifyPropertyChanged
    {
        private string _filePath;

        private string _title;

        private string _description;

        public int Id { get; set; }
        public int BoardId { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                {
                    return;
                }

                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description)
                {
                    return;
                }

                _description = value;
                OnPropertyChanged();
            }
        }

        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public ObservableCollection<SubTask> SubTasks { get; set; }

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (value == _filePath)
                {
                    return;
                }

                _filePath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
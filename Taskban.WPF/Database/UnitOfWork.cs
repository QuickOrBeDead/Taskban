namespace Taskban.WPF.Database
{
    using LiteDB;

    using Taskban.WPF.Entities;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private const string DB_NAME = "taskban.db";

        public IRepository<Board> Boards { get; }

        /// <inheritdoc />
        public IRepository<AppStates> AppStates { get; }

        private readonly ILiteDatabase _database;

        public UnitOfWork()
        {
            _database = new LiteDatabase(DB_NAME);

            Boards = new BaseRepository<Board>(_database, nameof(Boards));
            AppStates = new BaseRepository<AppStates>(_database, nameof(AppStates));
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
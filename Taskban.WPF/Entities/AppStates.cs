namespace Taskban.WPF.Entities
{
    public sealed class AppStates
    {
        public static int AppStatesId = 1;

        public int Id { get; set; }

        public int? LastBoardId { get; set; }
    }
}

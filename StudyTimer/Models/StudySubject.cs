using SQLite;

namespace StudyTimer.Models
{
    public class StudySubject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = "#4ADE80";

        public int TargetMinutes { get; set; } = 25;
    }
}

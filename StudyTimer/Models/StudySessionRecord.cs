using SQLite;

namespace StudyTimer.Models
{
    public class StudySessionRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public int CycleCount { get; set; } = 1;

        public DateTime CompletedAt { get; set; } = DateTime.Now;

        [Ignore]
        public string SubjectColor { get; set; } = "#4ADE80";

        [Ignore]
        public string SubjectInitial => !string.IsNullOrEmpty(SubjectName) ? SubjectName.Substring(0, 1).ToUpper() : "S";
    }
}

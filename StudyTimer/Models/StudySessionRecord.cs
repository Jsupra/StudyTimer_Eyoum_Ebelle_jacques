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
    }
}

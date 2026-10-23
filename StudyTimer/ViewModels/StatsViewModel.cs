using System.Collections.ObjectModel;
using StudyTimer.Models;
using StudyTimer.Services;

namespace StudyTimer.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private string _totalStudyTime = "0h 00m";
        private string _completedSessionsCount = "0";
        private ObservableCollection<StudySessionRecord> _sessionHistory = new();

        public string TotalStudyTime
        {
            get => _totalStudyTime;
            set => SetProperty(ref _totalStudyTime, value);
        }

        public string CompletedSessionsCount
        {
            get => _completedSessionsCount;
            set => SetProperty(ref _completedSessionsCount, value);
        }

        public ObservableCollection<StudySessionRecord> SessionHistory
        {
            get => _sessionHistory;
            set => SetProperty(ref _sessionHistory, value);
        }

        public StatsViewModel()
        {
            _databaseService = new DatabaseService();
            _ = LoadStatsAsync();
        }

        public async Task LoadStatsAsync()
        {
            if (AppSession.CurrentUser == null) return;

            var records = await _databaseService.GetSessionRecordsAsync(AppSession.CurrentUser.Id);
            SessionHistory.Clear();
            foreach (var r in records)
            {
                SessionHistory.Add(r);
            }

            int totalMinutes = records.Sum(r => r.DurationMinutes);
            int hours = totalMinutes / 60;
            int mins = totalMinutes % 60;
            TotalStudyTime = $"{hours}h {mins:D2}m";
            CompletedSessionsCount = records.Count.ToString();
        }
    }
}

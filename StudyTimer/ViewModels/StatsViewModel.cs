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
        private string _selectedPeriodText = "Cette\nsemaine";

        public string SelectedPeriodText
        {
            get => _selectedPeriodText;
            set => SetProperty(ref _selectedPeriodText, value);
        }

        private double _mondayHeight = 15;
        private double _tuesdayHeight = 15;
        private double _wednesdayHeight = 15;
        private double _thursdayHeight = 15;
        private double _fridayHeight = 15;
        private double _saturdayHeight = 15;
        private double _sundayHeight = 15;

        public double MondayHeight { get => _mondayHeight; set => SetProperty(ref _mondayHeight, value); }
        public double TuesdayHeight { get => _tuesdayHeight; set => SetProperty(ref _tuesdayHeight, value); }
        public double WednesdayHeight { get => _wednesdayHeight; set => SetProperty(ref _wednesdayHeight, value); }
        public double ThursdayHeight { get => _thursdayHeight; set => SetProperty(ref _thursdayHeight, value); }
        public double FridayHeight { get => _fridayHeight; set => SetProperty(ref _fridayHeight, value); }
        public double SaturdayHeight { get => _saturdayHeight; set => SetProperty(ref _saturdayHeight, value); }
        public double SundayHeight { get => _sundayHeight; set => SetProperty(ref _sundayHeight, value); }

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
            await LoadStatsAsync("Cette semaine");
        }

        public async Task LoadStatsAsync(string period)
        {
            if (AppSession.CurrentUser == null) return;

            SelectedPeriodText = period == "Cette semaine" ? "Cette\nsemaine" : (period == "Ce mois" ? "Ce\nmois" : "Tout\nl'historique");

            var allRecords = await _databaseService.GetSessionRecordsAsync(AppSession.CurrentUser.Id);
            var subjects = await _databaseService.GetSubjectsAsync(AppSession.CurrentUser.Id);
            var subjectDict = subjects.ToDictionary(s => s.Id, s => s);

            var today = DateTime.Today;
            IEnumerable<StudySessionRecord> filteredRecords = allRecords;

            if (period == "Cette semaine")
            {
                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                var startOfWeek = today.AddDays(-diff);
                filteredRecords = allRecords.Where(r => r.CompletedAt >= startOfWeek && r.CompletedAt < startOfWeek.AddDays(7));
            }
            else if (period == "Ce mois")
            {
                filteredRecords = allRecords.Where(r => r.CompletedAt.Month == today.Month && r.CompletedAt.Year == today.Year);
            }

            SessionHistory.Clear();
            foreach (var r in filteredRecords.OrderByDescending(x => x.CompletedAt))
            {
                if (subjectDict.TryGetValue(r.SubjectId, out var subject))
                {
                    r.SubjectColor = subject.Color;
                }
                else if (!string.IsNullOrEmpty(r.SubjectName))
                {
                    var matchByName = subjects.FirstOrDefault(s => s.Name.Equals(r.SubjectName, StringComparison.OrdinalIgnoreCase));
                    if (matchByName != null)
                    {
                        r.SubjectColor = matchByName.Color;
                    }
                }
                SessionHistory.Add(r);
            }

            int totalMinutes = filteredRecords.Sum(r => r.DurationMinutes);
            int hours = totalMinutes / 60;
            int mins = totalMinutes % 60;
            TotalStudyTime = $"{hours}h {mins:D2}m";
            CompletedSessionsCount = filteredRecords.Count().ToString();

            int diffWeek = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfCurrentWeek = today.AddDays(-diffWeek);

            double[] dayMinutes = new double[7];
            foreach (var r in allRecords)
            {
                if (r.CompletedAt >= startOfCurrentWeek && r.CompletedAt < startOfCurrentWeek.AddDays(7))
                {
                    int dayIndex = (7 + (r.CompletedAt.DayOfWeek - DayOfWeek.Monday)) % 7;
                    dayMinutes[dayIndex] += r.DurationMinutes;
                }
            }

            double maxMinutes = dayMinutes.Max();
            if (maxMinutes <= 0) maxMinutes = 1;

            MondayHeight = Math.Max(15, (dayMinutes[0] / maxMinutes) * 100);
            TuesdayHeight = Math.Max(15, (dayMinutes[1] / maxMinutes) * 100);
            WednesdayHeight = Math.Max(15, (dayMinutes[2] / maxMinutes) * 100);
            ThursdayHeight = Math.Max(15, (dayMinutes[3] / maxMinutes) * 100);
            FridayHeight = Math.Max(15, (dayMinutes[4] / maxMinutes) * 100);
            SaturdayHeight = Math.Max(15, (dayMinutes[5] / maxMinutes) * 100);
            SundayHeight = Math.Max(15, (dayMinutes[6] / maxMinutes) * 100);
        }
    }
}

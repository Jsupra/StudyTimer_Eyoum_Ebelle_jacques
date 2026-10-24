using StudyTimer.Models;
using StudyTimer.Services;

namespace StudyTimer.Views;

[QueryProperty(nameof(Subject), "subject")]
[QueryProperty(nameof(TotalMinutes), "minutes")]
[QueryProperty(nameof(TotalCycles), "cycles")]
public partial class SessionSummaryPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private string _subjectName = "Étude";
    private int _durationMinutes = 25;
    private int _cycleCount = 1;
    private bool _isSaved = false;

    public string Subject
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _subjectName = Uri.UnescapeDataString(value);
                if (SubjectLabel != null)
                    SubjectLabel.Text = _subjectName.ToUpper();
            }
        }
    }

    public string TotalMinutes
    {
        set
        {
            if (int.TryParse(value, out int minutes))
            {
                _durationMinutes = minutes;
                int hours = minutes / 60;
                int remainingMinutes = minutes % 60;

                if (TimeLabel != null)
                {
                    if (hours > 0)
                        TimeLabel.Text = $"{hours}h {remainingMinutes}min";
                    else
                        TimeLabel.Text = $"{remainingMinutes} min";
                }
            }
        }
    }

    public string TotalCycles
    {
        set
        {
            if (int.TryParse(value, out int cycles))
            {
                _cycleCount = cycles;
                if (CyclesLabel != null)
                    CyclesLabel.Text = $"{cycles} cycles";
            }
        }
    }

    public SessionSummaryPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_isSaved && AppSession.CurrentUser != null)
        {
            _isSaved = true;
            try
            {
                var subjects = await _databaseService.GetSubjectsAsync(AppSession.CurrentUser.Id);
                var subject = subjects.FirstOrDefault(s => s.Name.Equals(_subjectName, StringComparison.OrdinalIgnoreCase)) ?? subjects.FirstOrDefault();

                var record = new StudySessionRecord
                {
                    UserId = AppSession.CurrentUser.Id,
                    SubjectId = subject?.Id ?? 0,
                    SubjectName = _subjectName,
                    DurationMinutes = _durationMinutes,
                    CycleCount = _cycleCount,
                    CompletedAt = DateTime.Now
                };

                await _databaseService.SaveSessionRecordAsync(record);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur enregistrement session summary: {ex.Message}");
            }
        }
    }

    private async void OnReturnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: true);
    }

    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//HomePage", animate: true);
        });
        return true;
    }
}

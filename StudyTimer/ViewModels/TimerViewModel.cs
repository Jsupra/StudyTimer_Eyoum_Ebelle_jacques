using System.Windows.Input;
using StudyTimer.Models;
using StudyTimer.Services;

namespace StudyTimer.ViewModels;

public class TimerViewModel : BaseViewModel
{
    private TimeSpan _remainingTime = TimeSpan.FromMinutes(25); // Session Pomodoro par défaut : 25 min
    private string _timerDisplay = "25:00";
    private bool _isRunning;
    private IDispatcherTimer? _timer;
    private readonly DatabaseService _databaseService;

    public string TimerDisplay
    {
        get => _timerDisplay;
        set => SetProperty(ref _timerDisplay, value);
    }

    public bool IsRunning
    {
        get => _isRunning;
        set => SetProperty(ref _isRunning, value);
    }

    public ICommand StartStopCommand { get; }
    public ICommand ResetCommand { get; }

    public TimerViewModel()
    {
        _databaseService = new DatabaseService();

        // Initialisation du Timer natif .NET MAUI
        _timer = Application.Current?.Dispatcher.CreateTimer();
        if (_timer != null)
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
        }

        StartStopCommand = new Command(ToggleTimer);
        ResetCommand = new Command(ResetTimer);
    }

    private void ToggleTimer()
    {
        if (IsRunning)
        {
            _timer?.Stop();
            IsRunning = false;
        }
        else
        {
            _timer?.Start();
            IsRunning = true;
        }
    }

    private async void OnTimerTick(object? sender, EventArgs e)
    {
        if (_remainingTime.TotalSeconds > 0)
        {
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            TimerDisplay = _remainingTime.ToString(@"mm\:ss");
        }
        else
        {
            _timer?.Stop();
            IsRunning = false;

            // Enregistrer la session terminée en base de données
            if (AppSession.CurrentUser != null)
            {
                try
                {
                    var subjects = await _databaseService.GetSubjectsAsync(AppSession.CurrentUser.Id);
                    var subject = subjects.FirstOrDefault();

                    var record = new StudySessionRecord
                    {
                        UserId = AppSession.CurrentUser.Id,
                        SubjectId = subject?.Id ?? 0,
                        SubjectName = subject?.Name ?? "Session Focus",
                        DurationMinutes = 25,
                        CycleCount = 1,
                        CompletedAt = DateTime.Now
                    };

                    await _databaseService.SaveSessionRecordAsync(record);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur enregistrement session: {ex.Message}");
                }
            }

            // Navigation vers le résumé ou alerte
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current?.MainPage?.DisplayAlert("Session Terminée !", "Votre session de révision a été enregistrée avec succès.", "OK")!;
                await Shell.Current.GoToAsync("//HomePage");
            });
        }
    }

    private void ResetTimer()
    {
        _timer?.Stop();
        IsRunning = false;
        _remainingTime = TimeSpan.FromMinutes(25);
        TimerDisplay = "25:00";
    }
}

using System.Windows.Input;

namespace StudyTimer.ViewModels;

public class TimerViewModel : BaseViewModel
{
    private TimeSpan _remainingTime = TimeSpan.FromMinutes(25); // Session Pomodoro par défaut : 25 min
    private string _timerDisplay = "25:00";
    private bool _isRunning;
    private IDispatcherTimer? _timer;

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

    private void OnTimerTick(object? sender, EventArgs e)
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
            // TODO: Déclencher une alerte ou enregistrer la session terminée
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
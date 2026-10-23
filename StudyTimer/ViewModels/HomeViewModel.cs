namespace StudyTimer.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private int _currentStreak = 4;
    private string _dailyGoalText = "3h 00m / 4h 00m";
    private double _progressValue = 0.75;

    public int CurrentStreak
    {
        get => _currentStreak;
        set => SetProperty(ref _currentStreak, value);
    }

    public string DailyGoalText
    {
        get => _dailyGoalText;
        set => SetProperty(ref _dailyGoalText, value);
    }

    public double ProgressValue
    {
        get => _progressValue;
        set => SetProperty(ref _progressValue, value);
    }

    public HomeViewModel()
    {
        // Plus tard, tu pourras charger ces données depuis un service de stockage local
    }
}
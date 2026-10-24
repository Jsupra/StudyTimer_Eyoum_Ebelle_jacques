namespace StudyTimer.Views;

public partial class HomePage : ContentPage
{
    private string _selectedMode = "25";

    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        int goalMins = Preferences.Get("DailyGoalMinutes", 120);
        int hours = goalMins / 60;
        int mins = goalMins % 60;
        if (DailyGoalDisplayLabel != null)
        {
            DailyGoalDisplayLabel.Text = $"0h / {hours}h{(mins > 0 ? $"{mins:D2}" : "00")}";
        }
    }

    // --- LOGIQUE DE SÉLECTION DES CARTES ---
    private void OnSelect25Tapped(object sender, EventArgs e) { _selectedMode = "25"; UpdateSelectionUI(Card25); }
    private void OnSelect50Tapped(object sender, EventArgs e) { _selectedMode = "50"; UpdateSelectionUI(Card50); }
    private void OnSelectLibreTapped(object sender, EventArgs e) { _selectedMode = "Libre"; UpdateSelectionUI(CardLibre); }

    private void UpdateSelectionUI(Border selectedCard)
    {
        Card25.Stroke = Colors.Transparent;
        Card50.Stroke = Colors.Transparent;
        CardLibre.Stroke = Colors.Transparent;
        selectedCard.Stroke = Color.FromArgb("#4ADE80");
    }

    // --- NAVIGATION ---
    private async void OnStartSessionClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"SessionConfigPage?mode={Uri.EscapeDataString(_selectedMode)}");
    }

    private async void OnNavigateStats(object sender, EventArgs e) { await Shell.Current.GoToAsync("//StatsPage", animate: false); }
    private async void OnNavigateSettings(object sender, EventArgs e) { await Shell.Current.GoToAsync("//SettingsPage", animate: false); }
    private async void OnBackClicked(object sender, EventArgs e) { await Shell.Current.GoToAsync("//LoginPage"); }
}

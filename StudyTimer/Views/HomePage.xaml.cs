namespace StudyTimer.Views;

public partial class HomePage : ContentPage
{
    // On stocke la durée par défaut (25 minutes)
    private int _selectedDuration = 25;
    private string _selectedMode = "25";


    public HomePage()
    {
        InitializeComponent();
    }

    // --- LOGIQUE DE SÉLECTION DES CARTES ---
    private void OnSelect25Tapped(object sender, EventArgs e) { _selectedMode = "25"; UpdateSelectionUI(Card25); }
    private void OnSelect50Tapped(object sender, EventArgs e) { _selectedMode = "50"; UpdateSelectionUI(Card50); }
    private void OnSelectLibreTapped(object sender, EventArgs e) { _selectedMode = "Libre"; UpdateSelectionUI(CardLibre); }

    // Met à jour la bordure verte pour montrer quelle carte est active
    private void UpdateSelectionUI(Border selectedCard)
    {
        // On enlève la couleur sur tous les bords (Stroke)
        Card25.Stroke = Colors.Transparent;
        Card50.Stroke = Colors.Transparent;
        CardLibre.Stroke = Colors.Transparent;

        // On applique le contour vert uniquement sur la carte cliquée
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
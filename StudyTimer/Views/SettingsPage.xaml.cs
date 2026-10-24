using StudyTimer.Services;
using StudyTimer.ViewModels;

namespace StudyTimer.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        LoadDailyGoal();
    }

    private void LoadDailyGoal()
    {
        int goal = Preferences.Get("DailyGoalMinutes", 120);
        if (DailyGoalLabel != null)
        {
            DailyGoalLabel.Text = $"{goal} min";
        }
    }

    private async void OnEditDailyGoalTapped(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Objectif quotidien", "Entrez votre nouvel objectif quotidien en minutes :", initialValue: Preferences.Get("DailyGoalMinutes", 120).ToString(), keyboard: Keyboard.Numeric);
        if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int newGoal) && newGoal > 0)
        {
            Preferences.Set("DailyGoalMinutes", newGoal);
            LoadDailyGoal();
            await DisplayAlert("Succès", $"Objectif mis à jour : {newGoal} minutes", "OK");
        }
    }

    // Protection : redirige vers l'accueil si on utilise le retour natif du téléphone
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//HomePage", animate: false);
        });

        return true;
    }

    // Bouton de retour visuel en haut à gauche
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

    // Clic sur l'icône Accueil de la barre de navigation
    private async void OnNavigateHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

    // Clic sur l'icône Statistiques de la barre de navigation
    private async void OnNavigateStats(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StatsPage", animate: false);
    }

    // Clic sur une option de réglage classique
    private void OnSettingTapped(object sender, EventArgs e)
    {
        DisplayAlert("Paramètres", "Cette fonctionnalité sera bientôt disponible.", "OK");
    }

    // Clic sur le bouton rouge "Se déconnecter"
    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Déconnexion", "Voulez-vous vraiment vous déconnecter ?", "Oui", "Annuler");

        if (confirm)
        {
            AppSession.Logout();
            await Shell.Current.GoToAsync("//LoginPage", animate: true);
        }
    }
}

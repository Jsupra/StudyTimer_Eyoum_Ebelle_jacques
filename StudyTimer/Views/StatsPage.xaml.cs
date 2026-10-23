namespace StudyTimer.Views;

public partial class StatsPage : ContentPage
{
    public StatsPage()
    {
        InitializeComponent();
    }

    // Capture le bouton retour matériel (téléphone) pour éviter de fermer l'application
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//HomePage", animate: false);
        });

        return true;
    }

    // Bouton de retour en haut à gauche
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

    // Icône Accueil de la barre de navigation
    private async void OnNavigateHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

    // Icône Paramètres de la barre de navigation
    private async void OnNavigateSettings(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SettingsPage", animate: false);
    }
}
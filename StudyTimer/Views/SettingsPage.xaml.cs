<<<<<<< HEAD
using StudyTimer.Services;
using StudyTimer.ViewModels;

=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
namespace StudyTimer.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

<<<<<<< HEAD
    // Protection : redirige vers l'accueil si on utilise le retour natif du tÃ©lÃ©phone
=======
    // Protection : redirige vers l'accueil si on utilise le retour natif du téléphone
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//HomePage", animate: false);
        });

        return true;
    }

<<<<<<< HEAD
    // Bouton de retour visuel en haut Ã  gauche
=======
    // Bouton de retour visuel en haut à gauche
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

<<<<<<< HEAD
    // Clic sur l'icÃ´ne Accueil de la barre de navigation
=======
    // Clic sur l'icône Accueil de la barre de navigation
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnNavigateHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

<<<<<<< HEAD
    // Clic sur l'icÃ´ne Statistiques de la barre de navigation
=======
    // Clic sur l'icône Statistiques de la barre de navigation
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnNavigateStats(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StatsPage", animate: false);
    }

<<<<<<< HEAD
    // Clic sur une option de rÃ©glage classique
    private void OnSettingTapped(object sender, EventArgs e)
    {
        DisplayAlert("ParamÃ¨tres", "Cette fonctionnalitÃ© sera bientÃ´t disponible.", "OK");
    }

    // Clic sur le bouton rouge "Se dÃ©connecter"
    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("DÃ©connexion", "Voulez-vous vraiment vous dÃ©connecter ?", "Oui", "Annuler");

        if (confirm)
        {
            AppSession.Logout();
            await Shell.Current.GoToAsync("//LoginPage", animate: true);
        }
    }
}
=======
    // Clic sur une option de réglage classique
    private void OnSettingTapped(object sender, EventArgs e)
    {
        // Tu pourras rajouter la logique d'ouverture des sous-pages ici
        Console.WriteLine("Paramètre cliqué !");
    }

    // Clic sur le bouton rouge "Se déconnecter"
    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        // Demande une confirmation avant de déconnecter l'utilisateur
        bool confirm = await DisplayAlert("Déconnexion", "Voulez-vous vraiment vous déconnecter ?", "Oui", "Annuler");

        if (confirm)
        {
            // Redirige vers la page de Login (Assure-toi que la route est bien définie dans AppShell)
            await Shell.Current.GoToAsync("//LoginPage", animate: true);
        }
    }
}
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc

<<<<<<< HEAD
using StudyTimer.ViewModels;

=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
namespace StudyTimer.Views;

public partial class StatsPage : ContentPage
{
    public StatsPage()
    {
        InitializeComponent();
    }

<<<<<<< HEAD
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StatsViewModel vm)
        {
            await vm.LoadStatsAsync();
        }
    }

    // Capture le bouton retour matÃ©riel (tÃ©lÃ©phone) pour Ã©viter de fermer l'application
=======
    // Capture le bouton retour matériel (téléphone) pour éviter de fermer l'application
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
    // Bouton de retour en haut Ã  gauche
=======
    // Bouton de retour en haut à gauche
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

<<<<<<< HEAD
    // IcÃ´ne Accueil de la barre de navigation
=======
    // Icône Accueil de la barre de navigation
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnNavigateHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage", animate: false);
    }

<<<<<<< HEAD
    // IcÃ´ne ParamÃ¨tres de la barre de navigation
=======
    // Icône Paramètres de la barre de navigation
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnNavigateSettings(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SettingsPage", animate: false);
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc

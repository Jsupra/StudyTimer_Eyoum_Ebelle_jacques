namespace StudyTimer.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    // Bouton retour dans l'en-tête (ramène à l'écran de connexion)
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    // Le bouton vert "S'inscrire"
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Plus tard, tu ajouteras ici la logique pour créer le compte en base de données.
        // Une fois l'inscription réussie, on envoie l'utilisateur sur le tableau de bord (racine)
        await Shell.Current.GoToAsync("//HomePage");
    }

    // Le lien texte "Se connecter" en bas
    private async void OnLoginTapped(object sender, EventArgs e)
    {
        // Ramène simplement à l'écran précédent (qui est censé être LoginPage)
        await Shell.Current.GoToAsync("..");
    }
}
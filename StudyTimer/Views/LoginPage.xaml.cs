namespace StudyTimer.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    // Le bouton vert "Se connecter"
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Une fois l'authentification validée, on navigue vers la racine de l'application
        await Shell.Current.GoToAsync("//HomePage");
    }

    // Le lien texte "Créer un compte" en bas
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navigation empilée vers l'inscription (permet le bouton retour)
        await Shell.Current.GoToAsync("RegisterPage");
    }

    // Le lien "Mot de passe oublié"
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Mot de passe oublié cliqué !");
        DisplayAlert("Mot de passe oublié", "Cette fonctionnalité n'est pas encore implémentée.", "OK");
    }
}
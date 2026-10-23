<<<<<<< HEAD
using StudyTimer.Services;

=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
namespace StudyTimer.Views;

public partial class RegisterPage : ContentPage
{
<<<<<<< HEAD
    private readonly DatabaseService _databaseService;

    public RegisterPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

=======
    public RegisterPage()
    {
        InitializeComponent();
    }

    // Bouton retour dans l'en-tête (ramène à l'écran de connexion)
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

<<<<<<< HEAD
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas.", "OK");
            return;
        }

        try
        {
            var user = await _databaseService.RegisterAsync(email, username, password);
            if (user != null)
            {
                AppSession.CurrentUser = user;
                await DisplayAlert("SuccÃ¨s", "Compte crÃ©Ã© avec succÃ¨s !", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
=======
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
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc

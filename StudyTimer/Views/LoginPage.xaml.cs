<<<<<<< HEAD
using StudyTimer.Services;

=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
namespace StudyTimer.Views;

public partial class LoginPage : ContentPage
{
<<<<<<< HEAD
    private readonly DatabaseService _databaseService;

    public LoginPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();

        // PrÃ©-remplir pour faciliter le test immÃ©diat avec le compte de test
        EmailEntry.Text = "test@studytimer.com";
        PasswordEntry.Text = "Password123!";
    }

    // Le bouton vert "Se connecter"
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
            else
            {
                await DisplayAlert("Échec de connexion", "Email ou mot de passe incorrect. (Utilisez test@studytimer.com / Password123!)", "OK");
}
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        DisplayAlert("Mot de passe oubliÃ©", "Pour le compte de test, utilisez l'email : test@studytimer.com et le mot de passe : Password123!", "OK");
    }
}
=======
    public LoginPage()
    {
        InitializeComponent();
    }

    // Le lien texte "Cr�er un compte" en bas
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navigation empil�e vers l'inscription (permet le bouton retour)
        await Shell.Current.GoToAsync("RegisterPage");
    }

    // Le lien "Mot de passe oubli�"
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Mot de passe oublié cliqué !");
        DisplayAlert("Mot de passe oublié", "Cette fonctionnalité n'est pas encore implémentée.", "OK");
    }
}
using StudyTimer.Services;

namespace StudyTimer.Views;

public partial class LoginPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public LoginPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();

        // Pré-remplir pour faciliter le test immédiat avec le compte de test
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

    // Le lien texte "Cr�er un compte" en bas
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navigation empil�e vers l'inscription (permet le bouton retour)
        await Shell.Current.GoToAsync("RegisterPage");
    }

    // Le lien "Mot de passe oubli�"
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        DisplayAlert("Mot de passe oublié", "Pour le compte de test, utilisez l'email : test@studytimer.com et le mot de passe : Password123!", "OK");
    }
}
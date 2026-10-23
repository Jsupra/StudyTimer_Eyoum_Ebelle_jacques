using StudyTimer.Services;

namespace StudyTimer.Views;

public partial class RegisterPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public RegisterPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

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
                await DisplayAlert("Succès", "Compte créé avec succès !", "OK");
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

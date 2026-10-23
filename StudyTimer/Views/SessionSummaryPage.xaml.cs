namespace StudyTimer.Views;

[QueryProperty(nameof(Subject), "subject")]
[QueryProperty(nameof(TotalMinutes), "minutes")]
[QueryProperty(nameof(TotalCycles), "cycles")]
public partial class SessionSummaryPage : ContentPage
{
    public string Subject
    {
        set
        {
            if (SubjectLabel != null)
                SubjectLabel.Text = Uri.UnescapeDataString(value).ToUpper();
        }
    }

    public string TotalMinutes
    {
        set
        {
            if (int.TryParse(value, out int minutes))
            {
                int hours = minutes / 60;
                int remainingMinutes = minutes % 60;

                if (TimeLabel != null)
                {
                    if (hours > 0)
                        TimeLabel.Text = $"{hours}h {remainingMinutes}min";
                    else
                        TimeLabel.Text = $"{remainingMinutes} min";
                }
            }
        }
    }

    public string TotalCycles
    {
        set
        {
            if (CyclesLabel != null)
                CyclesLabel.Text = $"{value} cycles";
        }
    }

    public SessionSummaryPage()
    {
        InitializeComponent();
    }

    private async void OnReturnHomeClicked(object sender, EventArgs e)
    {
        // Enregistrement des statistiques en base de données à insérer ici plus tard

        // Retourne à la racine (Accueil) de manière fluide
        await Shell.Current.GoToAsync("//HomePage", animate: true);
    }

    // On bloque le bouton retour natif pour obliger l'utilisateur à valider via le bouton
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//HomePage", animate: true);
        });
        return true;
    }
}
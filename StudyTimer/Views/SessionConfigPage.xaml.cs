namespace StudyTimer.Views;

[QueryProperty(nameof(Mode), "mode")]
public partial class SessionConfigPage : ContentPage
{
    private string _mode;
    private int _baseDuration = 25;

    public string Mode
    {
        set
        {
            _mode = value;
            if (_mode == "50") _baseDuration = 50;
            else _baseDuration = 25; // Par défaut

            // On vérifie que l'élément UI existe bien avant de le toucher
            if (CustomTimeContainer != null)
            {
                CustomTimeContainer.IsVisible = (_mode == "Libre");
            }
        }
    }

   
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (CustomTimeContainer != null)
        {
            CustomTimeContainer.IsVisible = (_mode == "Libre");
        }
    }

    public SessionConfigPage()
    {
        InitializeComponent();
    }

    private void OnCyclesChanged(object sender, ValueChangedEventArgs e)
    {
        int cycles = (int)e.NewValue;
        CyclesLabel.Text = cycles > 1 ? $"{cycles} cycles" : "1 cycle";
    }

    private async void OnStartTimerClicked(object sender, EventArgs e)
    {
        // 1. Récupérer la matière
        string subject = string.IsNullOrWhiteSpace(SubjectEntry.Text) ? "Session d'étude" : SubjectEntry.Text;

        // 2. Récupérer la durée
        int finalDuration = _baseDuration;
        if (_mode == "Libre" && int.TryParse(CustomTimeEntry.Text, out int customMin))
        {
            finalDuration = customMin;
        }

        // 3. Récupérer les cycles
        int totalCycles = (int)CyclesStepper.Value;

        // 4. Envoyer tout ça à la page du chronomètre !
        string route = $"TimerPage?duration={finalDuration}&subject={Uri.EscapeDataString(subject)}&cycles={totalCycles}";
        await Shell.Current.GoToAsync(route);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
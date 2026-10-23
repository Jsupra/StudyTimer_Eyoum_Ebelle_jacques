namespace StudyTimer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Enregistrement des pages secondaires (empilables)
        Routing.RegisterRoute("TimerPage", typeof(Views.TimerPage));
        Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
        Routing.RegisterRoute("SessionConfigPage", typeof(Views.SessionConfigPage));
        Routing.RegisterRoute("SessionSummaryPage", typeof(Views.SessionSummaryPage));
    }
}

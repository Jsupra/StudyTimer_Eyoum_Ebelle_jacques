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
<<<<<<< HEAD
<<<<<<< HEAD
        Routing.RegisterRoute("SessionSummaryPage", typeof(Views.SessionSummaryPage));
=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    }
}
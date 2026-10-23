using Microsoft.Maui.Controls;
using System;

namespace StudyTimer.Views;

[QueryProperty(nameof(Duration), "duration")]
[QueryProperty(nameof(Subject), "subject")]
[QueryProperty(nameof(TotalCycles), "cycles")]
public partial class TimerPage : ContentPage
{
    private IDispatcherTimer _timer;
    private TimeSpan _timeLeft;
    private TimeSpan _initialDuration; // Mémorise la durée pour réinitialiser le chrono au cycle suivant
    private bool _isRunning;

    private int _currentCycle = 1;
    private int _totalCycles = 1;

<<<<<<< HEAD
<<<<<<< HEAD
    private bool _isBreakMode = false;
    private TimeSpan _breakDuration = TimeSpan.FromMinutes(5); 

=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
    // --- PROPRIÉTÉS ALIMENTÉES PAR LA NAVIGATION ---

    public string Duration
    {
        set
        {
            if (int.TryParse(value, out int minutes))
            {
                _initialDuration = TimeSpan.FromMinutes(minutes);
                _timeLeft = _initialDuration;
                UpdateTimerDisplay();
            }
<<<<<<< HEAD
<<<<<<< HEAD
        }
    }

    public string Subject
    {
        set
        {
            // Décode le texte (qui a été formaté pour l'URL) et l'affiche en majuscules
            if (SubjectLabel != null)
            {
                SubjectLabel.Text = Uri.UnescapeDataString(value).ToUpper();
            }
        }
    }

    public string TotalCycles
    {
        set
        {
            if (int.TryParse(value, out int cycles))
            {
                _totalCycles = cycles;
                UpdateCycleLabel();
                UpdateDotsUI();
            }
        }
    }

    // --- CONSTRUCTEUR ---

    public TimerPage()
    {
        InitializeComponent();

    }

    // --- CYCLE DE VIE DE LA PAGE ---

    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        if (_timer == null)
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
        }

        StartTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PauseTimer(); 
    }

    // --- LOGIQUE DU CHRONOMÈTRE ---

    private async void OnTimerTick(object sender, EventArgs e)
    {
        if (_timeLeft.TotalSeconds > 0)
        {
            _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
            UpdateTimerDisplay();
        }
        else
        {
            PauseTimer(); 

            if (!_isBreakMode)
            {
                // === FIN D'UNE SESSION DE FOCUS ===
                if (_currentCycle < _totalCycles)
                {
                    await DisplayAlert("Focus terminé !", "Excellent travail. Prenez 5 minutes de pause pour souffler.", "Lancer la pause");

                    _isBreakMode = true; 
                    _timeLeft = _breakDuration;

                    UpdateCycleLabel(); 
                    UpdateTimerDisplay();

                    StartTimer(); 
                }
                else
                {
                    // La session entière est terminée
                    int totalMinutes = (int)_initialDuration.TotalMinutes * _totalCycles;
                    string encodedSubject = Uri.EscapeDataString(SubjectLabel.Text ?? "Étude");

                    await Shell.Current.GoToAsync($"SessionSummaryPage?subject={encodedSubject}&minutes={totalMinutes}&cycles={_totalCycles}");
                }
            }
            else
            {
                // === FIN D'UNE SESSION DE PAUSE ===
                await DisplayAlert("Pause terminée", "Fin de la récréation ! Concentrez-vous pour le cycle suivant.", "C'est parti !");

                _isBreakMode = false; 
                _currentCycle++;      

                UpdateCycleLabel();
                UpdateDotsUI();       

                _timeLeft = _initialDuration; 
                UpdateTimerDisplay();

                StartTimer(); 
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        if (TimeLabel != null)
        {
            TimeLabel.Text = _timeLeft.ToString(@"mm\:ss");
        }
    }

    private void UpdateCycleLabel()
    {
        if (CycleLabel != null)
        {
            string modeText = _isBreakMode ? "MODE PAUSE" : "MODE FOCUS";
            CycleLabel.Text = $"• CYCLE {_currentCycle}/{_totalCycles}  • {modeText}";


            CycleLabel.TextColor = _isBreakMode ? Color.FromArgb("#FBBF24") : Color.FromArgb("#38BDF8");
        }
    }

    private void StartTimer()
    {
        _timer.Start();
        _isRunning = true;

        if (PauseResumeButton != null)
        {
            PauseResumeButton.Text = "METTRE EN PAUSE";
            PauseResumeButton.BackgroundColor = Color.FromArgb("#4ADE80"); // Vert
            PauseResumeButton.TextColor = Colors.Black;
        }
    }

    private void PauseTimer()
    {
        _timer.Stop();
        _isRunning = false;

        if (PauseResumeButton != null)
        {
            PauseResumeButton.Text = "REPRENDRE";
            PauseResumeButton.BackgroundColor = Color.FromArgb("#38BDF8"); // Bleu clair
            PauseResumeButton.TextColor = Colors.Black;
        }
    }

    // --- GESTION DES CLICS SUR LES BOUTONS ---

    private void OnPauseResumeClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            PauseTimer();
        }
        else
        {
            StartTimer();
        }
    }

    private async void OnStopClicked(object sender, EventArgs e)
    {
        PauseTimer();

        bool confirm = await DisplayAlert("Arrêter", "Voulez-vous vraiment arrêter cette session ? Sa progression sera perdue.", "Oui, arrêter", "Annuler");

        if (confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            StartTimer();
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        PauseTimer();

        bool confirm = await DisplayAlert("Quitter", "Quitter la session en cours ? Toute progression non sauvegardée sera perdue.", "Oui", "Non");

        if (confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            StartTimer();
        }
    }

    // Capture le bouton retour physique d'Android pour éviter une fermeture accidentelle
    protected override bool OnBackButtonPressed()
    {
        PauseTimer();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool confirm = await DisplayAlert("Quitter", "Quitter la session en cours ? Toute progression non sauvegardée sera perdue.", "Oui", "Non");

            if (confirm)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                StartTimer();
            }
        });

        return true;
    }
    private void UpdateDotsUI()
    {
        if (DotsContainer == null) return;
        DotsContainer.Children.Clear();

        // On crée autant de points que de cycles totaux
        for (int i = 1; i <= _totalCycles; i++)
        {
            var dot = new BoxView
            {
                WidthRequest = 6,
                HeightRequest = 6,
                CornerRadius = 3,
                // Si le point correspond au cycle en cours ou à un cycle passé -> Vert. Sinon -> Gris
                Color = (i <= _currentCycle) ? Color.FromArgb("#4ADE80") : Color.FromArgb("#27272A")
            };

            DotsContainer.Children.Add(dot);
=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
=======
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc
        }
    }

    public string Subject
    {
        set
        {
            // Décode le texte (qui a été formaté pour l'URL) et l'affiche en majuscules
            if (SubjectLabel != null)
            {
                SubjectLabel.Text = Uri.UnescapeDataString(value).ToUpper();
            }
        }
    }

    public string TotalCycles
    {
        set
        {
            if (int.TryParse(value, out int cycles))
            {
                _totalCycles = cycles;
                UpdateCycleLabel();
            }
        }
    }

    // --- CONSTRUCTEUR ---

    public TimerPage()
    {
        InitializeComponent();

    }

    // --- CYCLE DE VIE DE LA PAGE ---

    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        if (_timer == null)
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
        }

        StartTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PauseTimer(); 
    }

    // --- LOGIQUE DU CHRONOMÈTRE ---

    private async void OnTimerTick(object sender, EventArgs e)
    {
        if (_timeLeft.TotalSeconds > 0)
        {
            _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
            UpdateTimerDisplay();
        }
        else
        {
            PauseTimer(); // Le temps est écoulé pour ce cycle

            if (_currentCycle < _totalCycles)
            {
                await DisplayAlert("Cycle terminé !", "Prenez une petite pause, puis préparez-vous pour le cycle suivant.", "C'est parti !");

                _currentCycle++;
                UpdateCycleLabel();

                // On remet le compteur à sa valeur initiale pour le nouveau cycle
                _timeLeft = _initialDuration;
                UpdateTimerDisplay();

                StartTimer();
            }
            else
            {
                await DisplayAlert("Session terminée", "Félicitations, vous avez terminé tous vos cycles de révision !", "Super");

                // Retour au tableau de bord
                await Shell.Current.GoToAsync("..");
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        if (TimeLabel != null)
        {
            TimeLabel.Text = _timeLeft.ToString(@"mm\:ss");
        }
    }

    private void UpdateCycleLabel()
    {
        if (CycleLabel != null)
        {
            CycleLabel.Text = $"• CYCLE {_currentCycle}/{_totalCycles}  • MODE FOCUS";
        }
    }

    private void StartTimer()
    {
        _timer.Start();
        _isRunning = true;

        if (PauseResumeButton != null)
        {
            PauseResumeButton.Text = "METTRE EN PAUSE";
            PauseResumeButton.BackgroundColor = Color.FromArgb("#4ADE80"); // Vert
            PauseResumeButton.TextColor = Colors.Black;
        }
    }

    private void PauseTimer()
    {
        _timer.Stop();
        _isRunning = false;

        if (PauseResumeButton != null)
        {
            PauseResumeButton.Text = "REPRENDRE";
            PauseResumeButton.BackgroundColor = Color.FromArgb("#38BDF8"); // Bleu clair
            PauseResumeButton.TextColor = Colors.Black;
        }
    }

    // --- GESTION DES CLICS SUR LES BOUTONS ---

    private void OnPauseResumeClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            PauseTimer();
        }
        else
        {
            StartTimer();
        }
    }

    private async void OnStopClicked(object sender, EventArgs e)
    {
        PauseTimer();

        bool confirm = await DisplayAlert("Arrêter", "Voulez-vous vraiment arrêter cette session ? Sa progression sera perdue.", "Oui, arrêter", "Annuler");

        if (confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            StartTimer();
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        PauseTimer();

        bool confirm = await DisplayAlert("Quitter", "Quitter la session en cours ? Toute progression non sauvegardée sera perdue.", "Oui", "Non");

        if (confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            StartTimer();
        }
    }

    // Capture le bouton retour physique d'Android pour éviter une fermeture accidentelle
    protected override bool OnBackButtonPressed()
    {
        PauseTimer();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool confirm = await DisplayAlert("Quitter", "Quitter la session en cours ? Toute progression non sauvegardée sera perdue.", "Oui", "Non");

            if (confirm)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                StartTimer();
            }
        });

        return true;
    }
}
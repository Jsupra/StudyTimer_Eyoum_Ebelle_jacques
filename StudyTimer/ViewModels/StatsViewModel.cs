namespace StudyTimer.ViewModels
{
    public class StatsViewModel : ContentPage
    {
        public StatsViewModel()
        {
            Content = new VerticalStackLayout
            {
                Children = {
                    new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                    }
                }
            };
        }
    }
}
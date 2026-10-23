namespace StudyTimer.Models
{
    public class StudySubject : ContentPage
    {
        public StudySubject()
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
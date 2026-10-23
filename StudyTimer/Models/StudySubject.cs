<<<<<<< HEAD
using SQLite;

namespace StudyTimer.Models
{
    public class StudySubject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = "#4ADE80";

        public int TargetMinutes { get; set; } = 25;
    }
}
=======
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
>>>>>>> 0832fb5f0367f7ff38136b808dee924e419fccbc

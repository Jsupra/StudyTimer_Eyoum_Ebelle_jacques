<<<<<<< HEAD
using SQLite;

namespace StudyTimer.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Unique, NotNull]
        public string Email { get; set; } = string.Empty;
        
        public string Username { get; set; } = string.Empty;
        
        public string PasswordHash { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
=======
namespace StudyTimer.Models
{
    public class UserSession : ContentPage
    {
        public UserSession()
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

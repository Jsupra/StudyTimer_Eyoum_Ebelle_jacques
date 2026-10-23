using StudyTimer.Models;

namespace StudyTimer.Services
{
    public static class AppSession
    {
        public static User? CurrentUser { get; set; }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}

<<<<<<< HEAD
using SQLite;
using StudyTimer.Models;

namespace StudyTimer.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private async Task InitAsync()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "studytimer.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<StudySubject>();
            await _database.CreateTableAsync<StudySessionRecord>();

            await SeedDataAsync();
        }

        private async Task SeedDataAsync()
        {
            if (_database == null) return;

            // Check if test user exists
            var testUser = await _database.Table<User>().Where(u => u.Email == "test@studytimer.com").FirstOrDefaultAsync();
            if (testUser == null)
            {
                testUser = new User
                {
                    Email = "test@studytimer.com",
                    Username = "Étudiant Test",
                    PasswordHash = "Password123!",
                    CreatedAt = DateTime.Now.AddDays(-7)
                };
                await _database.InsertAsync(testUser);
            }

            // Check if subjects exist for test user
            var subjects = await _database.Table<StudySubject>().Where(s => s.UserId == testUser.Id).ToListAsync();
            if (subjects.Count == 0)
            {
                var defaultSubjects = new List<StudySubject>
                {
                    new StudySubject { UserId = testUser.Id, Name = "Algorithmique Avancée", Color = "#38BDF8", TargetMinutes = 25 },
                    new StudySubject { UserId = testUser.Id, Name = "Architecture .NET MAUI", Color = "#4ADE80", TargetMinutes = 50 },
                    new StudySubject { UserId = testUser.Id, Name = "Physique Quantique", Color = "#F43F5E", TargetMinutes = 25 }
                };
                await _database.InsertAllAsync(defaultSubjects);
                subjects = await _database.Table<StudySubject>().Where(s => s.UserId == testUser.Id).ToListAsync();
            }

            // Check if session records exist for test user
            var records = await _database.Table<StudySessionRecord>().Where(r => r.UserId == testUser.Id).ToListAsync();
            if (records.Count == 0 && subjects.Count > 0)
            {
                var defaultRecords = new List<StudySessionRecord>
                {
                    new StudySessionRecord { UserId = testUser.Id, SubjectId = subjects[0].Id, SubjectName = subjects[0].Name, DurationMinutes = 25, CycleCount = 1, CompletedAt = DateTime.Now.AddDays(-1) },
                    new StudySessionRecord { UserId = testUser.Id, SubjectId = subjects[1].Id, SubjectName = subjects[1].Name, DurationMinutes = 50, CycleCount = 2, CompletedAt = DateTime.Now.AddDays(-2) },
                    new StudySessionRecord { UserId = testUser.Id, SubjectId = subjects[0].Id, SubjectName = subjects[0].Name, DurationMinutes = 25, CycleCount = 1, CompletedAt = DateTime.Now.AddDays(-3) },
                    new StudySessionRecord { UserId = testUser.Id, SubjectId = subjects[2].Id, SubjectName = subjects[2].Name, DurationMinutes = 25, CycleCount = 1, CompletedAt = DateTime.Now.AddDays(-4) }
                };
                await _database.InsertAllAsync(defaultRecords);
            }
        }

        // Authentication Methods
        public async Task<User?> LoginAsync(string email, string password)
        {
            await InitAsync();
            if (_database == null) return null;

            return await _database.Table<User>()
                .Where(u => u.Email == email && u.PasswordHash == password)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> RegisterAsync(string email, string username, string password)
        {
            await InitAsync();
            if (_database == null) return null;

            var existing = await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new Exception("Un utilisateur avec cet email existe déjà.");
            }

            var newUser = new User
            {
                Email = email,
                Username = username,
                PasswordHash = password,
                CreatedAt = DateTime.Now
            };

            await _database.InsertAsync(newUser);

            // Add default subject for new user
            var defaultSubject = new StudySubject
            {
                UserId = newUser.Id,
                Name = "Général / Révision",
                Color = "#4ADE80",
                TargetMinutes = 25
            };
            await _database.InsertAsync(defaultSubject);

            return newUser;
        }

        // Subjects Methods
        public async Task<List<StudySubject>> GetSubjectsAsync(int userId)
        {
            await InitAsync();
            if (_database == null) return new List<StudySubject>();

            return await _database.Table<StudySubject>().Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<int> SaveSubjectAsync(StudySubject subject)
        {
            await InitAsync();
            if (_database == null) return 0;

            if (subject.Id != 0)
            {
                return await _database.UpdateAsync(subject);
            }
            else
            {
                return await _database.InsertAsync(subject);
            }
        }

        // Study Session Records Methods
        public async Task<List<StudySessionRecord>> GetSessionRecordsAsync(int userId)
        {
            await InitAsync();
            if (_database == null) return new List<StudySessionRecord>();

            return await _database.Table<StudySessionRecord>()
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CompletedAt)
                .ToListAsync();
        }

        public async Task<int> SaveSessionRecordAsync(StudySessionRecord record)
        {
            await InitAsync();
            if (_database == null) return 0;

            return await _database.InsertAsync(record);
        }
    }
}
=======
namespace StudyTimer.Services
{
    public class DatabaseService : ContentPage
    {
        public DatabaseService()
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

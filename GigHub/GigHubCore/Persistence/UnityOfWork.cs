using GigHubCore.Core;
using GigHubCore.Core.IRepositories;
using GigHubCore.Persistence.DbContexts;

namespace GigHubCore.Persistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IGenreRepository Genres { get; private set; } 

        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Notifications = new NotificationRepository(context);
            Genres = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}

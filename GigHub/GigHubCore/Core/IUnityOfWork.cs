using GigHubCore.Core.IRepositories;

namespace GigHubCore.Core
{
    public interface IUnityOfWork
    {
        IGigRepository Gigs { get; }

        IAttendanceRepository Attendances { get; }

        IFollowingRepository Followings { get; }

        INotificationRepository Notifications { get; }

        IGenreRepository Genres { get; }

        void Complete();
    }
}

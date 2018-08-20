using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using GigHubCore.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Notification> GetNotifications(string userId)
        {
            return _context.UserNotifications
                             .Where(un => un.UserId == userId && !un.IsRead)
                             .Select(un => un.Notification)
                             .Include(n => n.Gig.Artist)
                             .ToList();
        }

        public List<UserNotification> GetUserNotificationsIsNotRead(string userId)
        {
            return _context.UserNotifications
                            .Where(un => un.UserId == userId && !un.IsRead)
                            .ToList();
        }
    }
}

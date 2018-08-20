using GigHubCore.Core.Models;
using System.Collections.Generic;

namespace GigHubCore.Core.IRepositories
{
    public interface INotificationRepository
    {
        List<Notification> GetNotifications(string userId);
        List<UserNotification> GetUserNotificationsIsNotRead(string userId);
    }
}

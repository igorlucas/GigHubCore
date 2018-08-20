using AutoMapper;
using GigHubCore.Core;
using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GigHubCore.Controllers.APIs
{
    [Produces("application/json")]
    [Route("api/Notifications")]
    public class NotificationsController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public NotificationsController(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> Notifications()
        {
            var notifications = _unityOfWork.Notifications.GetNotifications(_userManager.GetUserId(this.User));
            return _mapper.Map<List<Notification>, List<NotificationDto>>(notifications);
        }

        [HttpPost]
        [Route("MarkAsRead")]
        public IActionResult MarkAsRead()
        {
            var notificationsNotRead = _unityOfWork.Notifications.GetUserNotificationsIsNotRead(_userManager.GetUserId(this.User));

            //marca como lido
            notificationsNotRead.ForEach(n => n.Read());

            _unityOfWork.Complete();

            return Ok();
        }
    }
}

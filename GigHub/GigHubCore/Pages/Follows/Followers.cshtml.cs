using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GigHubCore.Pages.Follows
{
    public class FollowersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public FollowersModel(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        public IEnumerable<Following> Followers { get; set; }

        public void OnGet()
        {
            Followers = _unityOfWork.Followings.GetFollowers(_userManager.GetUserId(HttpContext.User));
        }
    }
}
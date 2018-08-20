using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GigHubCore.Pages.Follows
{
    public class FolloweesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public FolloweesModel(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        public IEnumerable<Following> Followees { get; set; }

        public void OnGet()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            Followees = _unityOfWork.Followings.GetFollowees(userId); 
        }
        
    }
}
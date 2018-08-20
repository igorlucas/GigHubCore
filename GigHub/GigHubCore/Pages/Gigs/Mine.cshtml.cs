using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GigHubCore.Pages.Gigs
{
    public class MineModel : PageModel
    {   
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public MineModel(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        public IEnumerable<Gig> Gigs { get; set; }

        public void OnGetAsync()
        {
            Gigs = _unityOfWork.Gigs.GetUpcomingGigsByArtist(_userManager.GetUserId(this.User));
        }
    }
}

using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GigHubCore.Pages.Gigs
{
    public class GigDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnityOfWork _unityOfWork;

        public GigDetailsModel(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unityOfWork = unityOfWork;
            Input = new InputModelGig();
        }

        public InputModelGig Input { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            var gig = _unityOfWork.Gigs.GetGig(id);

            if (gig == null)
                return NotFound();

            Input.Gig = gig; 

            if (_signInManager.IsSignedIn(this.User))
            {
                var userId = _userManager.GetUserId(this.User);

                Input.IsAttending = _unityOfWork.Attendances.GetAttendance(gig.Id, userId) != null;

                Input.IsFollowing = _unityOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;
            }

            return Page();
        }
    }
}

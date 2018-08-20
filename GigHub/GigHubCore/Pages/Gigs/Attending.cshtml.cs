using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Pages.Gigs
{
    public class AttendingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public AttendingModel(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        public IEnumerable<Gig> Gigs { get; set; }
        public ILookup<int, Attendance> Attendances { get; private set; }

        public void OnGet()
        {
            Gigs = _unityOfWork.Gigs.GetGigsUserAttending(_userManager.GetUserId(this.User)); 
            Attendances = _unityOfWork.Attendances.GetFutureAttendances(_userManager.GetUserId(this.User));
        }
    }
}
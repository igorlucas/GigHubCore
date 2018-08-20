using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public IndexModel(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        public ILookup<int, Attendance> Attendances { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; private set; }
        
        public void OnPost(string query = null)
        {
            Attendances = _unityOfWork.Attendances.GetFutureAttendances(_userManager.GetUserId(this.User)); 

            if (!String.IsNullOrWhiteSpace(query))
                UpcomingGigs = _unityOfWork.Gigs.GetSearchGigs(query);
        }

        public void OnGet()
        {
            UpcomingGigs = _unityOfWork.Gigs.GetUpcomings(); 
            Attendances = _unityOfWork.Attendances.GetFutureAttendances(_userManager.GetUserId(this.User));
        }
    }
}

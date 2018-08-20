using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GigHubCore.Pages.Gigs
{
    public class CreateGigModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public CreateGigModel(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        [BindProperty]
        public InputModelGig Input { get; set; }        

        public void OnGet()
        {
            ViewData["Genres"] = new SelectList(_unityOfWork.Genres.GetGenres(), "Id", "Name");
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            try
            {
                _unityOfWork.Gigs.AddGig(_userManager.GetUserId(this.User), Input);
                _unityOfWork.Complete();
            }
            catch (System.Exception)
            {
                throw;
            }

            return RedirectToPage("Mine");
        }
    }
}
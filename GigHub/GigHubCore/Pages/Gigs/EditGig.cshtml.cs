using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHubCore.Pages.Gigs
{
    public class EditGigModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public EditGigModel(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        [BindProperty]
        public InputModelGig Input { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            var gig = _unityOfWork.Gigs.GetGig(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != _userManager.GetUserId(this.User))
                return new UnauthorizedResult();

            Input = new InputModelGig()
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("yyyy-MM-dd"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Genres = _unityOfWork.Genres.GetGenres()
            };

            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var gig = _unityOfWork.Gigs.GetGigWithAttendees(Input.Id);

            if (_unityOfWork.Gigs.GigChangedIsValid(gig, Input))
            {
                if (gig == null)
                    return NotFound();

                if (gig.ArtistId != _userManager.GetUserId(this.User))
                    return new UnauthorizedResult();

                try
                {
                    _unityOfWork.Gigs.UpdateGig(gig, Input);
                    _unityOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //exite uma exceção na atualização
                    throw;
                }
            }

            return RedirectToPage("Mine");
        }
    }
}

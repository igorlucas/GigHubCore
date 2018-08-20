using System;
using GigHubCore.Core;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHubCore.Controllers.APIs
{
    [Produces("application/json")]
    [Route("api/Gigs")]
    [Authorize]
    public class GigsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public GigsController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public GigsController(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = _userManager.GetUserId(this.User);
            var gigWithAttendances = _unityOfWork.Gigs.GetGigWithAttendeesByUserId(id, userId);

            if (gigWithAttendances == null || gigWithAttendances.IsCanceled)
                return NotFound();

            if (gigWithAttendances.ArtistId != userId)
                return Unauthorized();

            gigWithAttendances.Cancel();

            _unityOfWork.Complete();

            return Ok();
        }

        public static implicit operator HttpContext(GigsController v)
        {
            throw new NotImplementedException();
        }
    }
}
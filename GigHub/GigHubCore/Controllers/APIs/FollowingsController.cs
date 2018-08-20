using GigHubCore.Core;
using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHubCore.Controllers.APIs
{
    [Produces("application/json")]
    [Route("api/Followings")]
    public class FollowingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public FollowingsController(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        public IActionResult Follow(FollowingDto dto)
        {
            var userId = _userManager.GetUserId(this.User);

            //se já estiver seguindo 
            if (_unityOfWork.Followings.GetFollowing(dto.FolloweeId, userId) != null)
                return BadRequest();

            _unityOfWork.Followings.AddFollower(dto, userId);
            _unityOfWork.Complete();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Unfollow(string id)
        {
            var userId = _userManager.GetUserId(this.User);            
            var following = _unityOfWork.Followings.GetFollowing(id, userId);

            if (following == null)
                return NotFound();

            _unityOfWork.Followings.RemoveFollower(id, userId);
            _unityOfWork.Complete();

            return Ok(id);
        }
    }
}
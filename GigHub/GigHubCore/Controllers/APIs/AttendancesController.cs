using GigHubCore.Core;
using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;
using GigHubCore.Persistence.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GigHubCore.Controllers.APIs
{
    [Produces("application/json")]
    [Route("api/Attendances")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnityOfWork _unityOfWork;

        public AttendancesController(UserManager<ApplicationUser> userManager, IUnityOfWork unityOfWork)
        {
            _userManager = userManager;
            _unityOfWork = unityOfWork;
        }
       
        [HttpGet("{gigId}")]
        public IActionResult GetAttendances(int gigId)
        {
            var userId = _userManager.GetUserId(this.User);
            try
            {
                var futureAttendances = _unityOfWork.Attendances.GetFutureAttendances(userId, gigId);
            }
            catch (Exception)
            {
                //existe exceção
                throw;
            }

            return Ok();
        }

        // POST: api/Attendances/gigId
        [HttpPost]
        public IActionResult Attend(AttendanceDto dto)
        {
            var userId = _userManager.GetUserId(this.User);

            if (_unityOfWork.Attendances.GetAttendance(dto.GigId, userId) != null)
                return BadRequest();

            _unityOfWork.Attendances.AttendGig(dto, userId);
            _unityOfWork.Complete();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttendance(int id)
        {
            var userId = _userManager.GetUserId(this.User);
            var attendance = _unityOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unityOfWork.Attendances.DeleteAttend(attendance);
            _unityOfWork.Complete();

            return Ok(id);
        }
    }
}
using FluentAssertions;
using GigHub.Tests.Extensions;
using GigHubCore.Controllers.APIs;
using GigHubCore.Core;
using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private string _userId;
        private Mock<IGigRepository> _mockReposutory;

        public GigsControllerTests()
        {
            _mockReposutory = new Mock<IGigRepository>();

            var mokUoW = new Mock<IUnityOfWork>();
            mokUoW.SetupGet(u => u.Gigs).Returns(_mockReposutory.Object);

            var appUser = new MockApplicationUser();
            _controller = new GigsController(mokUoW.Object, appUser.TestUserManager<ApplicationUser>());
            _userId = "1"; 
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }        

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();

            gig.Cancel();
            _mockReposutory.Setup(r => r.GetGigWithAttendeesByUserId(1, _userId)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGigs_ShoulReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "-"};

            _mockReposutory.Setup(r => r.GetGigWithAttendeesByUserId(1, _userId)).Returns(gig);

            var result = _controller.Cancel(1);
            
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId};

            _mockReposutory.Setup(r => r.GetGigWithAttendeesByUserId(1, _userId)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }

    }
}

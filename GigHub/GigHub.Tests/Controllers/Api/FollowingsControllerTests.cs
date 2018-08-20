using FluentAssertions;
using GigHub.Tests.Extensions;
using GigHubCore.Controllers.APIs;
using GigHubCore.Core;
using GigHubCore.Core.Dtos;
using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class FollowingsControllerTests
    {
        private FollowingsController _controller;
        private Mock<IFollowingRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IFollowingRepository>();

            var mockUoW = new Mock<IUnityOfWork>();
            mockUoW.SetupGet(u => u.Followings).Returns(_mockRepository.Object);

            var appUser = new MockApplicationUser();
            _controller = new FollowingsController(appUser.TestUserManager<ApplicationUser>(), mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Follow_UserFollowingAArtistForWhichHeHasFollow_ShouldReturnBadRequest()
        {
            var following = new Following();            
            _mockRepository.Setup(r => r.GetFollowing("-1",_userId)).Returns(following);

            var result = _controller.Follow(new FollowingDto { FolloweeId = "-1"});

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void Follow_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Follow(new FollowingDto());

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Unfollow_NoFollowingWithProvidedId_ShouldReturnNotFound()
        {            
            var result = _controller.Unfollow("-1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Unfollow_ValidRequest_ShouldReturnOk()
        {
            var following = new Following();            
            _mockRepository.Setup(r => r.GetFollowing("-1", _userId)).Returns(following);

            var result = _controller.Unfollow("-1");

            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var following = new Following();
            var gig = new Gig();
            _mockRepository.Setup(r => r.GetFollowing("-1", _userId)).Returns(following);

            var result = (OkObjectResult)_controller.Unfollow("-1");

            result.Value.Should().Be("-1");
        }

    }
}

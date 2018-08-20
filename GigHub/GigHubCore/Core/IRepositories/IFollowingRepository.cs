using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;
using System.Collections.Generic;

namespace GigHubCore.Core.IRepositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string id, string userId);

        IEnumerable<Following> GetFollowees(string userId);
        IEnumerable<Following> GetFollowers(string userId);

        void AddFollower(FollowingDto dto, string userId);

        void RemoveFollower(string id, string userId);
    }
}

using GigHubCore.Core.Dtos;
using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using GigHubCore.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFollower(FollowingDto dto, string userId)
        {
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
        }

        public void RemoveFollower(string id, string userId)
        {
            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                throw new Exception();
            else
                _context.Followings.Remove(following);
        }

        public Following GetFollowing(string id, string userId)
        {
            return _context.Followings.FirstOrDefault(f => f.FolloweeId == id && f.FollowerId == userId);
        }

        public IEnumerable<Following> GetFollowees(string userId)
        {
            return _context.Followings
                            .Where(f => f.FollowerId == userId)
                            .Include(f => f.Followee)
                            .ToList();
        }
        public IEnumerable<Following> GetFollowers(string userId)
        {
            return _context.Followings
                            .Where(f => f.FolloweeId == userId)
                            .Include(f => f.Follower)
                            .ToList();
        }   
    }
}

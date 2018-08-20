using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using GigHubCore.Pages.Gigs;
using GigHubCore.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddGig(string artistId, InputModelGig input)
        {
            var gig = new Gig
            {
                ArtistId = artistId,
                Venue = input.Venue,
                DateTime = input.DateTime,
                GenreId = input.Genre
            };

            _context.Gigs.Add(gig);
        }

        public void UpdateGig(Gig gig, InputModelGig input)
        {
            try
            {
                gig.Modify(input.Venue, input.DateTime, input.Genre);
                _context.Attach(gig).State = EntityState.Modified;
            }
            catch (Exception)
            {
                if (!_context.Gigs.Any(e => e.Id == gig.Id))
                    throw;
            }

        }

        public bool GigChangedIsValid(Gig gig, InputModelGig input)
        {
            if (!(gig.Venue == input.Venue && gig.DateTime == input.DateTime && gig.GenreId == input.Genre))
                return true;
            else
                return false;
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
               .Where(g =>
                   g.ArtistId == userId &&
                   g.DateTime > DateTime.Now &&
                   !g.IsCanceled)
               .Include(g => g.Genre)
               .ToList();
        }

        public Gig GetGig(int? id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Include(g => g.Attendances)
                .SingleOrDefault(g => g.Id == id);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendeesByUserId(int gigId, string userId)
        {
            return _context.Gigs
                            .Include(g => g.Attendances)
                            .ThenInclude(a => a.Attendee)
                            .Single(g => g.Id == gigId && g.ArtistId == userId);
        }

        public IOrderedQueryable<Gig> GetUpcomings()
        {
            return _context.Gigs
                                .Include(g => g.Artist)
                                .Include(g => g.Genre)
                                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                                .OrderBy(g => g.DateTime);
        }

        public IQueryable<Gig> GetSearchGigs(string query)
        {
            return GetUpcomings()
                .Where(g =>
                       g.Artist.Name.Contains(query) ||
                       g.Genre.Name.Contains(query) ||
                       g.Venue.Contains(query)
                       );
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .Select(a => a.Gig)
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .ToList();
        }
    }
}

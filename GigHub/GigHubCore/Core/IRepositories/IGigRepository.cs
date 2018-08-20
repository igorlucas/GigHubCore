using GigHubCore.Core.Models;
using GigHubCore.Pages.Gigs;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Core.IRepositories
{
    public interface IGigRepository
    {
        void AddGig(string artistId, InputModelGig input);

        void UpdateGig(Gig gig, InputModelGig input);

        bool GigChangedIsValid(Gig gig, InputModelGig input);

        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        
        Gig GetGig(int? id);

        Gig GetGigWithAttendees(int gigId);

        Gig GetGigWithAttendeesByUserId(int gigId, string userId);

        IOrderedQueryable<Gig> GetUpcomings();

        IQueryable<Gig> GetSearchGigs(string query);

        IEnumerable<Gig> GetGigsUserAttending(string userId);
    }
}

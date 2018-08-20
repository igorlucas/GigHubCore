using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using GigHubCore.Persistence.DbContexts;
using System.Collections.Generic;
using System.Linq;

namespace GigHubCore.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}

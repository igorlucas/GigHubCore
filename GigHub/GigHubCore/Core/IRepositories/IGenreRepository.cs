using GigHubCore.Core.Models;
using System.Collections.Generic;

namespace GigHubCore.Core.IRepositories
{
    public interface IGenreRepository
    {
        List<Genre> GetGenres();

    }
}

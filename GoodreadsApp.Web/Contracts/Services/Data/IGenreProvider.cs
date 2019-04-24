using GoodreadsApp.Contracts.Persistence.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Contracts.Services.Data
{
    public interface IGenreProvider
    {
        Task<IEnumerable<Genre>> GetGenresAsync();

        Task<Genre> MapGenreAsync(string genreName);
    }
}

using GoodreadsApp.Contracts.Persistence.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Contracts.Services.Data
{
    public interface IBookProvider
    {
        Task<IEnumerable<Book>> GetLatestBooksByGenreAsync(Genre genre);
        Task<IEnumerable<Book>> GetMostReadBooksByGenreThisWeekAsync(Genre genre);
        Task<IEnumerable<Book>> GetPopularBooksByGenreAsync(Genre genre);
    }
}

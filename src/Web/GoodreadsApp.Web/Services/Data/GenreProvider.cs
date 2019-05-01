using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GoodreadsApp.Domain.Models;
using GoodreadsApp.Web.Constants;
using GoodreadsApp.Web.Contracts.Services.Data;
using GoodreadsApp.Web.Helpers;

namespace GoodreadsApp.Web.Services.Data
{
    public class GenreProvider : IGenreProvider
    {
        private static IEnumerable<Genre> _genres;

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            if (_genres == null)
            {
                var doc = await HtmlHelper.GetHtmlDocumentAsync(UrlConstants.GenresUrl);
                var links = doc.DocumentNode.SelectNodes(XPathConstants.GenreLink);

                _genres = links.Select(link => new Genre
                {
                    Name = HttpUtility.HtmlDecode(link.InnerHtml),
                    Path = link.Attributes["href"].Value
                });
            }

            return _genres;
        }

        public async Task<Genre> MapGenreAsync(string genreName)
        {
            var genres = await GetGenresAsync();
            return genres.FirstOrDefault(g => g.Name == genreName);
        }
    }
}

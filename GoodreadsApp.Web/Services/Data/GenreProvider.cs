using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GoodreadsApp.Contracts.Persistence.Domain;
using GoodreadsApp.Web.Constants;
using GoodreadsApp.Web.Contracts.Services.Data;
using GoodreadsApp.Web.Helpers;

namespace GoodreadsApp.Web.Services.Data
{
    public class GenreProvider : IGenreProvider
    {
        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            var doc = await HtmlHelper.GetHtmlDocumentAsync(UrlConstants.GenresUrl);
            var links = doc.DocumentNode.SelectNodes(XPathConstants.GenreLink);

            return links.Select(link => new Genre
            {
                Name = HttpUtility.HtmlDecode(link.InnerHtml),
                Path = link.Attributes["href"].Value
            });
        }
    }
}

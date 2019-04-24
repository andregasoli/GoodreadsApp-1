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
    public class BookProvider : IBookProvider
    {
        public Task<IEnumerable<Book>> GetLatestBooksByGenreAsync(Genre genre)
        {
            return GetBooksByGenreAsync(genre, XPathConstants.LatestBooks);
        }

        public Task<IEnumerable<Book>> GetMostReadBooksByGenreThisWeekAsync(Genre genre)
        {
            return GetBooksByGenreAsync(genre, XPathConstants.MostReadBooksThisWeek);
        }

        public Task<IEnumerable<Book>> GetPopularBooksByGenreAsync(Genre genre)
        {
            return GetBooksByGenreAsync(genre, XPathConstants.PopularBooks);
        }

        private static async Task<IEnumerable<Book>> GetBooksByGenreAsync(Genre genre, string XPath)
        {
            var url = $"{UrlConstants.BaseUrl}{genre.Path}";
            var doc = await HtmlHelper.GetHtmlDocumentAsync(url);
            var nodes = doc.DocumentNode.SelectNodes(XPath);

            return nodes.Select(node => 
            {
                var image = node.FirstChild;

                return new Book
                {
                    Name = HttpUtility.HtmlDecode(image.Attributes["alt"].Value),
                    Image = image.Attributes["src"].Value,
                    Path = node.Attributes["href"].Value
                };
            });
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GoodreadsApp.Domain.Models;
using GoodreadsApp.Web.Constants;
using GoodreadsApp.Web.Contracts.Services.Data;
using GoodreadsApp.Web.Helpers;

namespace GoodreadsApp.Web.Services.Data
{
    public class AuthorProvider : IAuthorProvider
    {
        public async Task<Author> GetAuthorDetailsAsync(string path)
        {
            var url = $"{UrlConstants.AuthorDetailsUrl}{path}";
            var doc = await HtmlHelper.GetHtmlDocumentAsync(url);
            var node = doc.DocumentNode;

            return new Author
            {
                Name = HttpUtility.HtmlDecode(node.SelectSingleNode(XPathConstants.AuthorName).InnerText),
                Description = HttpUtility.HtmlDecode(node.SelectSingleNode(XPathConstants.AuthorDescription).InnerHtml.Replace("<br>", "\n")),
                Website = HttpUtility.HtmlDecode(node.SelectSingleNode(XPathConstants.AuthorWebsite)?.InnerText ?? "Not found"),
                Image = node.SelectSingleNode(XPathConstants.AuthorImage).Attributes["src"].Value,
                Path = path,
                Genres = node.SelectNodes(XPathConstants.AuthorGenres).Select(n => new Genre
                {
                    Name = HttpUtility.HtmlDecode(n.InnerText),
                    Path = n.Attributes["href"].Value
                })
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using GoodreadsApp.Contracts.Persistence.Domain;
using GoodreadsApp.Web.Constants;
using GoodreadsApp.Web.Contracts.Services.Data;
using GoodreadsApp.Web.Helpers;
using HtmlAgilityPack;

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

        private async Task<IEnumerable<Book>> GetBooksByGenreAsync(Genre genre, string XPath)
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
                    Path = ConvertPath(node.Attributes["href"].Value)
                };
            });
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int page)
        {
            searchTerm = searchTerm.Replace(' ', '+');

            if (page < 1)
                page = 1;

            var url = string.Format(UrlConstants.SearchUrl, searchTerm, page);
            var doc = await HtmlHelper.GetHtmlDocumentAsync(url);
            var nodes = doc.DocumentNode.SelectNodes(XPathConstants.BookRow);

            return nodes.Select(node =>
            {
                var titleAnchor = node.SelectSingleNode(XPathConstants.BookAnchor);
                var image = node.SelectSingleNode(XPathConstants.BookImage);
                var authorsContainer = node.SelectNodes(XPathConstants.AuthorRow);
                var ratingsContainer = node.SelectSingleNode(XPathConstants.RatingsContainer).InnerText;
                var ratings = HttpUtility.HtmlDecode(ratingsContainer).Trim().Split(" ").ToList();

                var startNumber = ratings.First(str => double.TryParse(str, out double avgRating));
                var startIndex = ratings.IndexOf(startNumber);

                return new Book
                {
                    Name = HttpUtility.HtmlDecode(titleAnchor.SelectSingleNode(XPathConstants.BookName).InnerHtml),
                    Path = ConvertPath(titleAnchor.Attributes["href"].Value),
                    Authors = ConvertAuthors(authorsContainer),
                    AverageRating = double.Parse(ratings[startIndex], CultureInfo.InvariantCulture),
                    RatingsCount = int.Parse(ratings[startIndex + 4].Replace(",", "")),
                    Image = image.Attributes["src"].Value
                };
            });
        }

        public async Task<Book> GetBookDetailsAsync(string path)
        {
            var url = $"{UrlConstants.BookDetailsUrl}{path}";
            var doc = await HtmlHelper.GetHtmlDocumentAsync(url);
            var node = doc.DocumentNode;
            var descriptionHtml = node.SelectSingleNode(XPathConstants.BookDescription).InnerHtml.Replace("<br>", "\n");
            var details = node.SelectSingleNode(XPathConstants.BookDetais);
            var publishingContainer = ConvertPublishingDetails(details.SelectSingleNode(XPathConstants.BookPublished)).Replace("Published ", "");
            var publishingDetails = publishingContainer.Split("by");

            return new Book
            {
                Name = HttpUtility.HtmlDecode(node.SelectSingleNode(XPathConstants.BookTitle).InnerText.Trim()),
                Description = HttpUtility.HtmlDecode(descriptionHtml),
                Image = node.SelectSingleNode(XPathConstants.BookDetailsImage).Attributes["src"].Value,
                Path = path,
                Authors = ConvertAuthors(node.SelectNodes(XPathConstants.AuthorRow)),
                AverageRating = double.Parse(node.SelectSingleNode(XPathConstants.BookRating).InnerText, CultureInfo.InvariantCulture),
                RatingsCount = int.Parse(node.SelectSingleNode(XPathConstants.BookRatingCount).Attributes["content"].Value),
                BookFormat = HttpUtility.HtmlDecode(details.SelectSingleNode(XPathConstants.BookFormat).InnerText),
                NumberOfPages = int.Parse(details.SelectSingleNode(XPathConstants.BookPages).InnerText.Split()[0]),
                PublishedBy = publishingDetails[1].Trim(),
                PublishingDate = ConvertPublishingDate(publishingDetails[0].Trim()),
                ISBN = details.SelectSingleNode(XPathConstants.BookISBN).InnerText
            };
        }

        private IEnumerable<Author> ConvertAuthors(HtmlNodeCollection nodes)
        {
            return nodes.Select(node =>
            {
                var nameAnchor = node.SelectSingleNode(XPathConstants.AuthorAnchor);
                var roleSpan = node.SelectSingleNode(XPathConstants.AuthorRole);

                return new Author
                {
                    Name = HttpUtility.HtmlDecode(nameAnchor.SelectSingleNode(XPathConstants.AuthorName).InnerHtml),
                    Path = nameAnchor.Attributes["href"].Value,
                    Role = roleSpan?.InnerHtml.Replace("(", "").Replace(")", "") ?? string.Empty
                };
            });
        }

        private string ConvertPath(string path)
        {
            var enumerable = path.Split('/');
            return enumerable.Last().Replace("?from_search=true", "");
        }

        private string ConvertPublishingDetails(HtmlNode node)
        {
            var text = node.InnerText.Trim().Replace("\n", "");
            var regexOptions = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", regexOptions);

            return regex.Replace(text, " ");
        }

        private DateTime ConvertPublishingDate(string publishingDate)
        {
            publishingDate = publishingDate
                .Replace("th", "")
                .Replace("st", "")
                .Replace("rd", "")
                .Replace("nd", "")
                .Replace("Augu", "August");

            return DateTime.Parse(publishingDate);
        }
    }
}

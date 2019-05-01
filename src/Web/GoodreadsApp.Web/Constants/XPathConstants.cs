namespace GoodreadsApp.Web.Constants
{
    public class XPathConstants
    {
        // Genres
        public const string GenresContainer = "//div[contains(@class, 'containerWithHeaderContent ')]";
        public const string GenreLink = "//div[contains(@class, 'left')]/a[contains(@class, 'gr-hyperlink')]";

        // Books by genre
        public const string LatestBooks = "/html/body/div[2]/div[3]/div[1]/div[2]/div[2]/div[4]/div[2]/div/div[contains(@class, 'coverRow')]/div[contains(@class, 'bookBox')]/div/a";
        public const string MostReadBooksThisWeek = "/html/body/div[2]/div[3]/div[1]/div[2]/div[2]/div[6]/div[2]/div/div[contains(@class, 'coverRow')]/div[contains(@class, 'bookBox')]/div/a";
        public const string PopularBooks = "/html/body/div[2]/div[3]/div[1]/div[2]/div[2]/div[8]/div[2]/div/div[contains(@class, 'coverRow')]/div[contains(@class, 'bookBox')]/div/a";

        // Books search
        public const string BookRow = "//tr[contains(@itemtype, 'http://schema.org/Book')]";
        public const string BookAnchor = ".//a[contains(@class, 'bookTitle')]";
        public const string BookImage = ".//img[contains(@class, 'bookCover')]";
        public const string BookName = ".//span[contains(@itemprop, 'name')]";
        public const string AuthorRow = ".//div[contains(@class, 'authorName__container')]";
        public const string RatingsContainer = ".//span[contains(@class, 'minirating')]";

        // Author
        public const string AuthorAnchor = ".//a[contains(@class, 'authorName')]";
        public const string AuthorName = ".//span[contains(@itemprop, 'name')]";
        public const string AuthorRole = ".//span[contains(@class, 'role')]";
        public const string AuthorDescription = ".//div[contains(@class, 'aboutAuthorInfo')]/span[contains(@id, 'freeTextContainerauthor')]";
        public const string AuthorWebsite = ".//div[contains(@class, 'dataItem')]/a[@target='_blank' and @itemprop='url']";
        public const string AuthorImage = ".//div[contains(@class, 'authorLeftContainer')]/a/img";
        public const string AuthorGenres = ".//div[@class='dataItem']/a[contains(@href, '/genres/')]";

        // Book details
        public const string BookTitle = "//*[@id=\"bookTitle\"]";
        public const string BookDescription = "//*[@id=\"description\"]/span[2]";
        public const string BookRating = "//*[@id=\"bookMeta\"]/span[contains(@itemprop, 'ratingValue')]";
        public const string BookRatingCount = "//*[@id=\"bookMeta\"]/a/meta[contains(@itemprop, 'ratingCount')]";
        public const string BookDetailsImage = "//*[@id=\"coverImage\"]";
        public const string BookDetais = "//*[@id=\"details\"]";
        public const string BookFormat = ".//span[contains(@itemprop, 'bookFormat')]";
        public const string BookPages = ".//span[contains(@itemprop, 'numberOfPages')]";
        public const string BookPublished = ".//div[2]";
        public const string BookISBN = ".//span[contains(@itemprop, 'isbn')]";
    }
}

using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Helpers
{
    public static class HtmlHelper
    {
        /// <summary>
        /// Get an HtmlDocument async
        /// </summary>
        /// <param name="url">Url to get the html</param>
        /// <returns></returns>
        public static async Task<HtmlDocument> GetHtmlDocumentAsync(string url)
        {
            string html = string.Empty;

            using (var client = new HttpClient())
                html = await client.GetStringAsync(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }
    }
}

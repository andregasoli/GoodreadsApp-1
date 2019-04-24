using GoodreadsApp.Contracts.Persistence.Domain;
using GoodreadsApp.Web.Contracts.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookProvider _bookProvider;
        private readonly IGenreProvider _genreProvider;

        public BooksController(IBookProvider bookProvider, IGenreProvider genreProvider)
        {
            _bookProvider = bookProvider;
            _genreProvider = genreProvider;
        }

        // GET: Books/Latest
        [HttpGet("Latest/{genreName}")]
        public Task<IActionResult> GetLatestBooksByGenreAsync(string genreName)
        {
            return GetBooksByGenreAsync(genreName, _bookProvider.GetLatestBooksByGenreAsync);
        }

        // GET: Books/MostRead
        [HttpGet("MostRead/{genreName}")]
        public Task<IActionResult> GetMostReadBooksByGenreThisWeekAsync(string genreName)
        {
            return GetBooksByGenreAsync(genreName, _bookProvider.GetMostReadBooksByGenreThisWeekAsync);
        }

        // GET: Books/Popular
        [HttpGet("Popular/{genreName}")]
        public Task<IActionResult> GetPopularBooksByGenreAsync(string genreName)
        {
            return GetBooksByGenreAsync(genreName, _bookProvider.GetPopularBooksByGenreAsync);
        }

        private async Task<IActionResult> GetBooksByGenreAsync(string genreName, Func<Genre, Task<IEnumerable<Book>>> func)
        {
            try
            {
                if (await _genreProvider.MapGenreAsync(genreName) is Genre genre)
                {
                    return Ok(await func.Invoke(genre));
                }
                else
                    return NotFound($"Genre \"{genreName}\" not found.");
            }
            catch (Exception e)
            {
                // TODO: Handle exception
                return BadRequest(e);
            }
        }
    }
}
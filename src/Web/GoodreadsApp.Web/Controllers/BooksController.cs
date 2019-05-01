using GoodreadsApp.Domain.Models;
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

        // GET: Books/Latest/{genreName}
        [HttpGet("Latest/{genreName}")]
        public Task<IActionResult> GetLatestBooksByGenreAsync(string genreName)
        {
            return GetBooksByGenreAsync(genreName, _bookProvider.GetLatestBooksByGenreAsync);
        }

        // GET: Books/MostRead/{genreName}
        [HttpGet("MostRead/{genreName}")]
        public Task<IActionResult> GetMostReadBooksByGenreThisWeekAsync(string genreName)
        {
            return GetBooksByGenreAsync(genreName, _bookProvider.GetMostReadBooksByGenreThisWeekAsync);
        }

        // GET: Books/Popular/{genreName}
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

        // GET: Books/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchBooksAsync(string searchTerm, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term is empty.");

            try
            {
                return Ok(await _bookProvider.SearchBooksAsync(searchTerm, page));
            }
            catch (Exception e)
            {
                // TODO: Handle exception
                return BadRequest(e);
            }
        }

        // GET: Books/Details/{id}
        [HttpGet("Details/{path}")]
        public async Task<IActionResult> GetBookDetailsAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("Path is empty.");

            try
            {
                return Ok(await _bookProvider.GetBookDetailsAsync(path));
            }
            catch (Exception e)
            {
                // TODO: Handle exception
                return BadRequest(e);
            }
        }
    }
}
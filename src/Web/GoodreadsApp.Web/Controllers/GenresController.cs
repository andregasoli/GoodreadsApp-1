using GoodreadsApp.Web.Contracts.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : Controller
    {
        private readonly IGenreProvider _genreProvider;

        public GenresController(IGenreProvider provider)
        {
            _genreProvider = provider;
        }

        // GET: Genres
        public async Task<IActionResult> GetGenresAsync()
        {
            try
            {
                return Ok(await _genreProvider.GetGenresAsync());
            }
            catch (Exception e)
            {
                // TODO: Handle exception
                return BadRequest(e);
            }
        }
    }
}
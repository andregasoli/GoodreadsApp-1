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
        private readonly IGenreProvider _provider;

        public GenresController(IGenreProvider provider)
        {
            _provider = provider;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _provider.GetGenresAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
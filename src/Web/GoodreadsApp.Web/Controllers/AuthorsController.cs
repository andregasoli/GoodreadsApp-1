using GoodreadsApp.Web.Contracts.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly IAuthorProvider _authorProvider;

        public AuthorsController(IAuthorProvider authorProvider)
        {
            _authorProvider = authorProvider;
        }

        // GET: Authors/Details/{path}
        [HttpGet("Details/{path}")]
        public async Task<IActionResult> GetAuthorDetailsAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("Path is empty.");

            try
            {
                return Ok(await _authorProvider.GetAuthorDetailsAsync(path));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
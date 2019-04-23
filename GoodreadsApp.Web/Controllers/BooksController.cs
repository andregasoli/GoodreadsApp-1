using Microsoft.AspNetCore.Mvc;

namespace GoodreadsApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        // GET: Books
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            return Ok();
        }

    }
}
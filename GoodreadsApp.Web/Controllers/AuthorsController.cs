using Microsoft.AspNetCore.Mvc;

namespace GoodreadsApp.Web.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace DecomposedApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Controllers
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

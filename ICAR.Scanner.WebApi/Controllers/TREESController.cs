using Microsoft.AspNetCore.Mvc;

namespace ICAR.Scanner.WebApi.Controllers
{
    public class TREESController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ICAR.Scanner.WebApi.Controllers
{
    public class SENSORSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

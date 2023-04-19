using Microsoft.AspNetCore.Mvc;

namespace Office.Controllers
{
    public class ExternaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

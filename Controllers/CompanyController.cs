using Microsoft.AspNetCore.Mvc;

namespace Office.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

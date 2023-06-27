using Microsoft.AspNetCore.Mvc;
using Office.Dataset;
using Office.Models;
using System.Diagnostics;

namespace Office.Controllers
{
    /// <summary>
    /// Classe controladora da página Home
    /// </summary>
    public class HomeController : Controller
    {
        [HttpGet]
        /// <summary>
        /// Método que retorna a página home
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<EncargoViewModel> dat = EncargoDataSet.Completed();
            ViewBag.encYear = HomeDataSet.allNumEncargos();
            ViewBag.encCompleted = HomeDataSet.allEncCompleted() ;
            if( MoldeDataSet.Index() == null)
            {
                ViewBag.allmoldes = 0;
            }
            else
            {
                ViewBag.allmoldes = MoldeDataSet.Index().Count();
            }
            try
            {
                if (dat != null) ViewBag.encargo = dat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
    }
}
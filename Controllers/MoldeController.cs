using Microsoft.AspNetCore.Mvc;
using Office.Models;
using Office.Dataset;

namespace Office.Controllers
{
    /// <summary>
    /// Classe controladora de moldes
    /// </summary>
    public class MoldeController : Controller
    {
        
        public MoldeController()
        {
            
        }
        /// <summary>
        /// Método para obter a página de moldes
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a página de moldes</returns>
        [HttpGet]
        [Route("Molde/")]
        public ActionResult Index()
        {
            List<MoldeModel>? dat = MoldeDataSet.Index();
            ViewBag.molde =dat;
            return View();
        }

        /// <summary>
        /// Método para obter um molde pelo id
        /// </summary>
        /// <param name="id">id do molde</param>
        /// <returns>a página do molde</returns>
        [HttpGet]
        [Route("Molde/Info/{id}")]
        public IActionResult Info(int id)
        {
            MoldeModel? dat = MoldeDataSet.Get(id);
            ViewBag.molde = dat;
            return View();
        }

    }
}

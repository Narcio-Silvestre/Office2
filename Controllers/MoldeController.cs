using Microsoft.AspNetCore.Mvc;
using Office.Models;
using Office.Dataset;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using System.Text;

namespace Office.Controllers
{
    /// <summary>
    /// Classe controladora de moldes
    /// </summary>
    public class MoldeController : Controller
    {
        SessionKeys? _session;
        string path;

        public MoldeController(IWebHostEnvironment system)
        {
            path = system.WebRootPath;
        }

        /// <summary>
        /// Método para obter a página de moldes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Molde/")]
        public ActionResult Index()
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            List<MoldeModel> dat = MoldeDataSet.Index();
            List<MoldeModel> dat2 = MoldeDataSet.MoldesEmIntv();
            List<MoldeModel> dat3 = MoldeDataSet.MoldesSemEncargo();

            try
            {
                if (dat != null) ViewBag.allMoldes = dat;
                if (dat2 != null) ViewBag.molIntv = dat2;
                if (dat3 != null) ViewBag.molSemEnc = dat3;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Método para obter a página para criar moldes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Molde/Create")]
        public ActionResult Create()
        {

          
            return View();
        }

        /// <summary>
        /// Criar um molde
        /// </summary>
        /// <param name="molde"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Molde/Create")]
        public ActionResult Create(MoldeModel molde)
        {

            if (!MoldeDataSet.Create(molde))
            {
                TempData["ErrorMessage"] = "Campos inválidos! Tente alterar os dados. ";

                return RedirectToAction("Create","Molde");
            }
            else
            {
                return RedirectToAction("Index", "Molde");

            }
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
            if(dat != null) {
                ViewBag.molde = dat; 
                List<EncargoViewModel> dat2 = EncargoDataSet.AllByMolde(ViewBag.molde.id);
                if (dat != null) ViewBag.encargoVal = dat2;
            }
            return View();
        }

        /// <summary>
        /// Método para editar a informação de um molde
        /// </summary>
        /// <param name="molde"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(MoldeModel molde)
        {
            MoldeDataSet.Edit(molde);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Método para obter a página de editar molde pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            MoldeModel? dat = MoldeDataSet.Get(id);
            if (dat != null)
            {
                ViewBag.molde = dat;
            }
            return View();
        }

        /// <summary>
        /// Método para deletar um molde pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!MoldeDataSet.Delete(id))
            {
                TempData["ErrorMessage"] = "No momento não é possível apagar o molde.";
                return RedirectToAction("Index", "Molde");
            }
            else
            {
                return RedirectToAction("Index", "Molde");

            }
        }

    }
}

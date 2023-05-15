using Microsoft.AspNetCore.Mvc;
using Office.Models;
using Office.Dataset;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;

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

        [HttpGet]
        [Route("Molde/")]
        public ActionResult Index()
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            List<MoldeModel> dat = MoldeDataSet.Index();
            List<MoldeModel> dat2 = MoldeDataSet.MoldesEmIntv();
            try
            {
                if (dat != null) ViewBag.allMoldes = dat;
                if (dat2 != null) ViewBag.molIntv = dat2;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        [HttpGet]
        [Route("Molde/Create")]
        public ActionResult Create()
        {

          
            return View();
        }

       
        [HttpPost]
        [Route("Molde/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MoldeModel molde, IEnumerable<IFormFile> files)
        {
            List<String> list = new();
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            string caminhoPasta = path + "\\Imagens\\";

            if (_session.funcaoid != 1)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para criar encargos.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index", "Home");
            }

            


            foreach (var item in files)
            {
                string novoNomeImg = Guid.NewGuid().ToString() + item.FileName;
                list.Add(novoNomeImg);
                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                using var stream = System.IO.File.Create(caminhoPasta + novoNomeImg);
                item.CopyTo(stream);
            }

            
           
            return RedirectToAction("Index", "Home");
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

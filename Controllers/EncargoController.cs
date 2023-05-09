using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using Office.Dataset;

namespace Office.Controllers


{




    public class EncargoController : Controller
    {
        SessionKeys? _session;
        string path;

        public EncargoController(IWebHostEnvironment system)
        {
            path = system.WebRootPath;
        }

        public ActionResult Index() { return View(); }


        
        [HttpGet]
        [Route("Encargo/Create")]
        public ActionResult Create()
        {
            
            List<RequisitosModel> data = RequisitosDataSet.Index();
            ViewBag.requisitos = data;

            List<MoldeModel> data2 = MoldeDataSet.Index();
            if (data2 == null)
            {
                 TempData["ErrorMessage"] = "No momento não é possivel criar encargos, todos os moldes têm encargos em execução.";
                 return RedirectToAction("Index", "Home");
            }
            ViewBag.moldes = data2;      
            return View();
        }


        [HttpPost]
        [Route("Encargo/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EncargoMolde encargo,IEnumerable<IFormFile> files)
        {
            List<String> list = new();
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            string caminhoPasta = path + "\\Imagens\\";

            if (_session.funcaoid != 1 && _session.funcaoid != 2)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para criar encargos.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index","Home");
            }

            if (encargo.moldeid<1 || encargo.descProblema.IsEmpty())
            {
                TempData["ErrorMessage"] = "Por favor preencha os campos necessários!";
                return RedirectToAction("Create", "Encargo");
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

            encargo.entidadeid =_session.Id;
            encargo.anexos = list;
            EncargoDataSet.Create(encargo);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            List<RequisitosModel> dat2 = RequisitosDataSet.Index(id);
            List<RequisitosModel> dat3 = RequisitosDataSet.Index2(id);
            List<AnexoModel> dat4 = AnexoDataSet.Index(id);
            List<IntervencaoModel2> dat5 = IntervencaoDataSet.Intervencao(id);
            IntervencaoModel2 dat6 = IntervencaoDataSet.Intervencao2(id);
            EncargoViewModel encargo = EncargoDataSet.Encargo(id);
            try
            {
                if (dat2 != null) ViewBag.encReq = dat2;
                if (dat3 != null) ViewBag.intervencoes = dat3;
                if (dat4 != null) ViewBag.anexos = dat4;
                if (dat6 != null)
                {
                    ViewBag.interAtual = dat6;
                    List<AnexoModel> dat7 = AnexoDataSet.Index2(ViewBag.interAtual.id);
                    if (dat7 != null) ViewBag.anexosInt =dat7;
                }
                if (dat5 != null) {
                    ViewBag.interAll = dat5;
                    List<List<AnexoModel>> anexosAll = new();
                    for(int x=0; x<ViewBag.interAll.Count;x++)
                    {
                        List<AnexoModel> data9 = AnexoDataSet.Index2(ViewBag.interAll[x].id);
                        anexosAll.Add(data9);
                    }
                    ViewBag.anexosAll = anexosAll;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(encargo);
        }

        [HttpGet]
        public IActionResult All()
        {
            List<EncargoViewModel> dat = EncargoDataSet.Completed();
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

        [HttpGet]
        public IActionResult AllVal()
        {
            List<EncargoViewModel> dat = EncargoDataSet.AllVal();
            try
            {
                if(dat != null)  ViewBag.encargo = dat;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }

        [HttpGet]
        public IActionResult AllInter()
        {
            List<EncargoViewModel> dat = EncargoDataSet.AllInter();
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.encargo = EncargoDataSet.Encargo(id);

            return View();
        }

        [HttpPost]
        public IActionResult Edit(EncargoViewModel encargo)
        {
            EncargoDataSet.Edit(encargo);
            return RedirectToAction("Index", "Home");
        }




    }
}

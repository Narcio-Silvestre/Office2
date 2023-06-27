using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using Office.Dataset;
using System.Text;

namespace Office.Controllers


{



    /// <summary>
    /// Classe Controladora de Encargos
    /// </summary>
    public class EncargoController : Controller
    {
        SessionKeys? _session;
        string path;

        public EncargoController(IWebHostEnvironment system)
        {
            path = system.WebRootPath;
        }

        /// <summary>
        /// Método para obter a página de Criar Encargo
        /// </summary>
        /// <returns>a página de criar encargo</returns>
        [HttpGet]
        [Route("Encargo/Create")]
        public ActionResult Create()
        {
            
            List<RequisitosModel> data = RequisitosDataSet.Index();
            ViewBag.requisitos = data;

            List<MoldeModel> data2 = MoldeDataSet.Index();
            List<MoldeModel> data3 = MoldeDataSet.MoldesDisp();
            if (data2 == null)
            {
                 TempData["ErrorMessage"] = "No momento não é possivel criar encargos, todos os moldes têm encargos em execução.";
                 return RedirectToAction("Alert", "Encargo");
            }
            if (data3 == null)
            {
                TempData["ErrorMessage"] = "No momento não é possivel criar encargos, todos os moldes têm encargos em execução.";
                return RedirectToAction("Alert", "Encargo");
            }
            ViewBag.moldes = data2;      
            ViewBag.moldesDisp = data3;
            return View();
        }

        /// <summary>
        /// Método para criar um encargo
        /// </summary>
        /// <param name="encargo">molde de encargo</param>
        /// <param name="files">anexos de encargo</param>
        /// <returns>a página home se for bem-sucedido e a página de encargo se falhar</returns>
        [HttpPost]
        [Route("Encargo/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EncargoMolde encargo,IEnumerable<IFormFile> files)
        {
            List<String> list = new();
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            string caminhoPasta = path + "\\Imagens\\";

            if (_session.funcaoid != 1 && _session.funcaoid != 2 && _session.funcaoid != 5)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para criar encargos.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Alert","Encargo");
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
            if(encargo.dataNecMeio < DateTime.Today)
            {
                encargo.dataNecMeio = DateTime.Now;

            }
            EncargoDataSet.Create(encargo);
            return RedirectToAction("Index","Home");
        }


        /// <summary>
        /// Obtém a página de informação de um encargo
        /// </summary>
        /// <param name="id">id do encargo</param>
        /// <returns>a página do encargo se for bem-sucedido e a página home se falhar</returns>
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

        /// <summary>
        /// Obtém a página de todos os moldes
        /// </summary>
        /// <returns>a página de todos os moldes</returns>
        [HttpGet]
        public IActionResult All()
        {
            List<EncargoViewModel> dat = EncargoDataSet.GetEncargosCompleted();
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

        /// <summary>
        /// Obtém a página de todos os moldes em validação
        /// </summary>
        /// <returns>a página de todos os moldes em validação</returns>
        [HttpGet]
        public IActionResult AllVal()
        {
            List<EncargoViewModel> dat = EncargoDataSet.AllVal();
            try
            {
                if(dat != null)  ViewBag.encargoVal = dat;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }

        /// <summary>
        /// Obtém a página de todos os moldes em intervenção
        /// </summary>
        /// <returns>a página de todos os moldes em intervenção</returns>
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

        /// <summary>
        /// Método para obter a página para editar os encargos
        /// </summary>
        /// <param name="id">id do encargo</param>
        /// <returns>a página para editar o encargo</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<EncargoViewModel> dat = EncargoDataSet.Completed();
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
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

        /// <summary>
        /// Método para editar um encargo pelo seu id
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="id"></param>
        /// <returns>a página inicial</returns>
        [HttpPost]
        public IActionResult Edit(string desc,int id)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(desc);
            stringBuilder.AppendLine(_session.Name);
            EncargoDataSet.Edit(stringBuilder.ToString(),id);
            return RedirectToAction("Index","Home");
        }

        /// <summary>
        /// Método para obter a página de editar um encargo pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit2(int id)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            EncargoViewModel encargo = EncargoDataSet.Encargo(id);
            return View(encargo);
        }

        /// <summary>
        /// Método para página de alertas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Alert()
        {

            return View();
        }

        /// <summary>
        /// Método para deletar um encargo pleo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!EncargoDataSet.Delete(id))
            {
                TempData["ErrorMessage"] = "No momento não é possível apagar o encargo.";
                return RedirectToAction("Alert", "Encargo");
            }
            else
            {
                return RedirectToAction("Edit", "Encargo");

            }
        }


    }
}

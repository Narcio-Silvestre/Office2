using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using Office.Dataset;

namespace Office.Controllers
{
    public class IntervencaoController : Controller
    {
       
        string path;
        
        public IntervencaoController(IWebHostEnvironment system)
        {
            
            path = system.WebRootPath;
        }

        [HttpPost]
        [Route("Intervencao/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IntervencaoModel aux, IEnumerable<IFormFile> files)
        {
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session?.funcaoid != 4)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para executar intervenções.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index", "Home");
            }
            
            if (aux.descricao.IsEmpty() || aux.extInt<0 || aux.extInt > 1)
            {
                int id = aux.idEncargo;
                TempData["ErrorMessage"] = "Por favor preencher todos os campos necessários para a intervenção!";
                return RedirectToAction(actionName:"Info",controllerName:"Encargo",new { @id = id }) ;
            }
            List<String> list = new();
            string caminhoPasta = path + "\\Imagens\\";
            
            aux.idEntidade = _session.Id;
            
            

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
            
            aux.anexos = list;
            IntervencaoDataSet.Intervencao(aux);
            return RedirectToAction("Index", "Home");
        }
    }
}

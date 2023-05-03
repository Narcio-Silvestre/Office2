using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using Office.Dataset;

namespace Office.Controllers
{
    public class ValidacaoController : Controller
    {
       

        public ValidacaoController()
        {
            
        }


        

        [HttpPost]
        [Route("Validacao/Prod/{id}")]
        public ActionResult Prod(ValidacaoModel aux, int id, int idEncargo)
        {
            ValidacaoModel data = new ValidacaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session?.funcaoid != 2)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para validar intervenções da área da produção.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index", "Home");
            }
            if (aux.descricao.IsEmpty() || aux.aprovado < 0 || aux.aprovado > 1)
            {
                TempData["ErrorMessage"] = "Por favor preencher todos os campos necessários para a validação(Produção)!";
                return RedirectToAction(actionName: "Info", controllerName: "Encargo", new { @id = idEncargo });
            }
            aux.idEntidade = _session.Id;
            aux.idInter = id;
            ValidacaoDataSet.Producao(aux);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Validacao/Qual/{id}")]
        public ActionResult Qual(ValidacaoModel aux, int id, int idEncargo)
        {
            ValidacaoModel data = new ValidacaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session?.funcaoid != 3)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para validar intervenções da área da qualidade.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index","Home");
            }
            if (aux.descricao.IsEmpty() || aux.aprovado < 0 || aux.aprovado > 1)
            {
                TempData["ErrorMessage"] = "Por favor preencher todos os campos necessários para a validação(Qualidade)!";
                return RedirectToAction(actionName: "Info", controllerName: "Encargo", new { @id = idEncargo });
            }
            aux.idEntidade = _session.Id;
            aux.idInter = id;
            ValidacaoDataSet.Qualidade(aux);
            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Text.Json;
using System.Web.WebPages;
using Office.Dataset;

namespace Office.Controllers
{
    /// <summary>
    /// Classe controladora de validação
    /// </summary>
    public class ValidacaoController : Controller
    {
       

        public ValidacaoController()
        {
            
        }


        
        /// <summary>
        /// Método para validar da área de produção 
        /// </summary>
        /// <param name="aux">modelo da validação</param>
        /// <param name="id">id da intervenção</param>
        /// <param name="idEncargo">id do encargo</param>
        /// <returns>a página home se for bem-sucedido, caso contrário retorna a página do encargo</returns>
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

        /// <summary>
        /// Método para validar da área de qualidade 
        /// </summary>
        /// <param name="aux">modelo da validação</param>
        /// <param name="id">id da intervenção</param>
        /// <param name="idEncargo">id do encargo</param>
        /// <returns>a página home se for bem-sucedido, caso contrário retorna a página do encargo</returns>
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

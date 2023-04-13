using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Office.Controllers
{
    public class ValidacaoController : dbConnetion
    {
        Api api;

        public ValidacaoController()
        {
            api = new Api();
        }


        [HttpGet]
        [Route("Validacao/")]
        [Route("Validacao/{id}")]
        public ActionResult Index(int id)
        {
            if (id > 0)
            {
                List<ValidacaoMenuModel> list = new List<ValidacaoMenuModel>();
                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Validacao/" + id.ToString()).Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                list.Add(JsonSerializer.Deserialize<ValidacaoMenuModel>(dat));
                ViewBag.validacao = list;
            }
            else
            {

                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Validacao").Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                ViewBag.validacao = JsonSerializer.Deserialize<List<ValidacaoMenuModel>>(dat);
            }
            return View();
        }

        [HttpPost]
        [Route("Validacao/Prod/{id}")]
        public ActionResult Prod(ValidacaoModel aux, int id)
        {
            ValidacaoModel data = new ValidacaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session.funcaoid != 2)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para validar intervenções da área da produção.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index", "Home");
            }
            aux.idEntidade = _session.Id;
            aux.idInter = id;
            Console.WriteLine("ent:"+aux.idEntidade);
            Console.WriteLine("idInt:"+aux.idInter);
            Console.WriteLine("val:"+aux.aprovado);
            Console.WriteLine("desc"+aux.descricao);
            api.HttpClient.PutAsJsonAsync("https://localhost:7271/validacao/Prod", aux);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Validacao/Qual/{id}")]
        public ActionResult Qual(ValidacaoModel aux, int id)
        {
            ValidacaoModel data = new ValidacaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session.funcaoid != 3)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para validar intervenções da área da qualidade.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index","Home");
            }
            aux.idEntidade = _session.Id;
            aux.idInter = id;
            api.HttpClient.PutAsJsonAsync("https://localhost:7271/Validacao/Qual", aux);
            return RedirectToAction("Index", "Home");
        }
    }
}

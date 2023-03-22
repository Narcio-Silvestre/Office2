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

        [HttpGet]
        [Route("Validacao/Create/{id}")]
        public IActionResult Create(int id)
        {
            var session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Validacao/" + id.ToString()).Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.validacao = JsonSerializer.Deserialize<ValidacaoMenuModel>(dat);
            ViewBag.name = session.Name;
            return View();
        }

        [HttpPost]
        [Route("Validacao/Create/{id}/{idEncargo}/{idIntv}")]
        public ActionResult Create(ValidacaoModel aux, int idEncargo, int id, int idIntv)
        {
            ValidacaoModel data = new ValidacaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            data.idEntidade = _session.Id;
            data.idIntervencao = id;
            data.idEncargo = idEncargo;
            data.idValidacao = idIntv;
            data.descricao = aux.descricao;
            data.aprovado = aux.aprovado;
            api.HttpClient.PutAsJsonAsync("https://localhost:7271/validacao", data);
            return RedirectToAction("Index", "Home");
        }
    }
}

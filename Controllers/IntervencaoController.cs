using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Office.Controllers
{
    public class IntervencaoController : dbConnetion
    {
        Api api;

        public IntervencaoController()
        {
            api = new Api();
        }


        [HttpGet]
        [Route("Intervencao/")]
        [Route("Intervencao/{id}")]
        public ActionResult Index(int id)
        {
            if (id > 0)
            {
                List<IntervencaoMenuModel> list = new List<IntervencaoMenuModel>();
                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Intervencao/" + id.ToString()).Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                list.Add(JsonSerializer.Deserialize<IntervencaoMenuModel>(dat));
                ViewBag.intervencao = list;
            }
            else
            {

                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Intervencao").Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                ViewBag.intervencao = JsonSerializer.Deserialize<List<IntervencaoMenuModel>>(dat);
            }
            return View();
        }

        [HttpGet]
        [Route("Intervencao/Create/{id}")]
        public IActionResult Create(int id)
        {
            var session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            HttpResponseMessage response2 = api.HttpClient.GetAsync("https://localhost:7271/Reparacao").Result;
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Intervencao/" + id.ToString()).Result;
            
            string dat = response.Content.ReadAsStringAsync().Result;
            string dat2 = response2.Content.ReadAsStringAsync().Result;
            ViewBag.reparacao = JsonSerializer.Deserialize<List<ReparacaoModel>>(dat2);
            ViewBag.intervencao = JsonSerializer.Deserialize<IntervencaoMenuModel>(dat);
            ViewBag.name = session.Name;
            return View();
        }

        [HttpPost]
        [Route("Intervencao/Create/{id}/{idEncargo}")]
        public ActionResult Create(IntervencaoViewModel aux,int idEncargo,int id)
        {
            IntervencaoModel data = new IntervencaoModel();
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            data.idEntidade = _session.Id;
            data.idIntervencao =id;
            data.idEncargo = idEncargo;
            data.nome = _session.Name;
            data.descricao = aux.descricao;
            api.HttpClient.PutAsJsonAsync("https://localhost:7271/Intervencao", data);
            return RedirectToAction("Index", "Home");
        }
    }
}

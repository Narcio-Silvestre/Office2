using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Office.Controllers
{
    public class MoldeController : dbConnetion
    {
        Api api;

        public MoldeController()
        {
            api = new Api();
        }
        // GET: MoldeController

        [HttpGet]
        [Route("Molde/")]
        [Route("Molde/{id}")]
        public ActionResult Index(int id)
        {
            if (id > 0)
            {
                List<MoldeModel> list = new List<MoldeModel>();
                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Molde/" + id.ToString()).Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                list.Add(JsonSerializer.Deserialize<MoldeModel>(dat));
                ViewBag.molde = list;
            }
            else
            {

                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Molde").Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                ViewBag.molde = JsonSerializer.Deserialize<List<MoldeModel>>(dat);
            }
            return View();
        }

        [HttpGet]
        [Route("Molde/Info/{id}")]
        public IActionResult Info(int id)
        {
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Molde/" + id.ToString()).Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.molde = JsonSerializer.Deserialize<MoldeModel>(dat);
            return View();
        }

    }
}

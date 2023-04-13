using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;


namespace Office.Controllers
{
    public class IntervencaoController : dbConnetion
    {
        Api api;
        string path;
        
        public IntervencaoController(IWebHostEnvironment system)
        {
            api = new Api();
            path = system.WebRootPath;
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
        [Route("Intervencao/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IntervencaoModel aux, IEnumerable<IFormFile> files)
        {
            var _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session.funcaoid != 4)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para executar intervenções.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index", "Home");
            }
            List<String> list = new();
            string caminhoPasta = path + "\\Imagens\\";
            
            aux.idEntidade = _session.Id;
            
            Console.WriteLine("Entidade:" + aux.idEntidade);
            Console.WriteLine("Externa:" + aux.extInt);
            Console.WriteLine("Descricao:" + aux.descricao);
            Console.WriteLine("Encargo:" + aux.idEncargo);

            foreach (var item in files)
            {
                Console.WriteLine("ima:" + item.ToString());

                string novoNomeImg = Guid.NewGuid().ToString() + item.FileName;
                list.Add(novoNomeImg);
                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                using var stream = System.IO.File.Create(caminhoPasta + novoNomeImg);
                item.CopyTo(stream);
            }
            foreach (var inter in list)
            {
                Console.WriteLine("imagem:" + inter);

            }
            aux.anexos = list;

            api.HttpClient.PutAsJsonAsync("https://localhost:7271/Intervencao", aux);
            return RedirectToAction("Index", "Home");
        }
    }
}

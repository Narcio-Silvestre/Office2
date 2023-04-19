using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using Microsoft.Web.Helpers;
using System.Web.WebPages;

namespace Office.Controllers


{
    public class dbConnetion:Controller
    {
        protected SqlConnection _connection;
        protected SessionKeys? _session;
        protected SqlDataAdapter _adapter;
        protected DataTable _dataTable;
        protected string pasta_anexos = @"C:\Users\narci\Desktop\vvg\Office\wwwroot\anexos\";

        public dbConnetion() {
            _connection = new SqlConnection("Data Source=lolly;Initial Catalog=WORK;Integrated Security=True");
        }
    }

    public class Api
    {
        Uri uri = new Uri("https://localhost:7271");
        HttpClient httpClient;

        public Api()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
        }

        public HttpClient HttpClient { get { return httpClient; } }
    }



    public class EncargoController : dbConnetion
    {
        Api api;
        string path;

        public EncargoController(IWebHostEnvironment system)
        {
            api = new Api();
            path = system.WebRootPath;
        }

        public ActionResult Index() { return View(); }


        
        [HttpGet]
        [Route("Encargo/Create")]
        public ActionResult Create()
        {
            
           
            HttpResponseMessage response1 = api.HttpClient.GetAsync("https://localhost:7271/Requisitos").Result;
            HttpResponseMessage response2 = api.HttpClient.GetAsync("https://localhost:7271/Molde").Result;
            HttpResponseMessage response3 = api.HttpClient.GetAsync("https://localhost:7271/Prioridade").Result;

            string data = response1.Content.ReadAsStringAsync().Result;
            ViewBag.requisitos = JsonSerializer.Deserialize<List<RequisitosModel>>(data);

            string data2 = response2.Content.ReadAsStringAsync().Result;
            if (data2.IsEmpty())
            {
                 TempData["ErrorMessage"] = "No momento não é possivel criar encargos, todos os moldes têm encargos em execução.";
                 return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.moldes = JsonSerializer.Deserialize<List<MoldeModel>>(data2);

            }

            string data3 = response3.Content.ReadAsStringAsync().Result;
            ViewBag.prioridade = JsonSerializer.Deserialize<List<PrioridadeModel>>(data3);
           
            return View();
        }

        [HttpPost]
        [Route("Encargo/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EncargoMolde encargo,IEnumerable<IFormFile> files)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            if (_session.funcaoid != 1)
            {
                TempData["ErrorMessage"] = "Desculpe, você não tem permissão para criar encargos.\n Por favor, contate o administrador do sistema para mais informações.";
                return RedirectToAction("Index","Home");
            }

            if (encargo.moldeid<1 || encargo.descProblema.IsEmpty())
            {
                TempData["ErrorMessage"] = "Por favor preencha os campos necessários!";
                return RedirectToAction("Create", "Encargo");
            }

            List<String> list = new();
            string caminhoPasta = path + "\\Imagens\\";
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));

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
            foreach(var inter in encargo.intervencao)
            {
                Console.WriteLine("Intervencao:" + inter);

            }
            foreach (var inter in list)
            {
                Console.WriteLine("imagem:" + inter);

            }
            encargo.anexos = list;

            api.HttpClient.PostAsJsonAsync("https://localhost:7271/encargo",encargo);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/" + id.ToString()).Result;
            HttpResponseMessage response2 = api.HttpClient.GetAsync("https://localhost:7271/Requisitos/" + id.ToString()).Result;
            HttpResponseMessage response3 = api.HttpClient.GetAsync("https://localhost:7271/Requisitos/notIn/"+ id.ToString()).Result;
            HttpResponseMessage response4 = api.HttpClient.GetAsync("https://localhost:7271/Anexo/" + id.ToString()).Result;
            HttpResponseMessage response5 = api.HttpClient.GetAsync("https://localhost:7271/Intervencao/All/" + id.ToString()).Result;
            HttpResponseMessage response6 = api.HttpClient.GetAsync("https://localhost:7271/Intervencao/" + id.ToString()).Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            string dat2 = response2.Content.ReadAsStringAsync().Result;
            string dat3 = response3.Content.ReadAsStringAsync().Result;
            string dat4 = response4.Content.ReadAsStringAsync().Result;
            string dat5 = response5.Content.ReadAsStringAsync().Result;
            string dat6 = response6.Content.ReadAsStringAsync().Result;
            EncargoViewModel encargo = JsonSerializer.Deserialize<EncargoViewModel>(dat);
            try
            {
                if(dat2!="") ViewBag.encReq = JsonSerializer.Deserialize<List<RequisitosModel>>(dat2);
                if (dat3 != "") ViewBag.intervencoes = JsonSerializer.Deserialize<List<RequisitosModel>>(dat3);
                if (dat4 != "") ViewBag.anexos = JsonSerializer.Deserialize<List<AnexoModel>>(dat4);
                if (dat6 != "") {
                    ViewBag.interAtual = JsonSerializer.Deserialize<IntervencaoModel2>(dat6);
                    HttpResponseMessage response7 = api.HttpClient.GetAsync("https://localhost:7271/Anexo/Intervencao/" + ViewBag.interAtual.id).Result;
                    string dat7 = response7.Content.ReadAsStringAsync().Result;
                    if (dat7 != "") ViewBag.anexosInt = JsonSerializer.Deserialize<List<AnexoModel>>(dat7);
                }
                if (dat5 != "") {
                    ViewBag.interAll = JsonSerializer.Deserialize<List<IntervencaoModel2>>(dat5);
                    if (ViewBag.interAll != null)
                    {
                        List<List<AnexoModel>> anexosAll = new();
                        for(int x=0; x<ViewBag.interAll.Count;x++)
                        {
                            HttpResponseMessage resp = api.HttpClient.GetAsync("https://localhost:7271/Anexo/Intervencao/" + ViewBag.interAll[x].id).Result;
                            string data9 = resp.Content.ReadAsStringAsync().Result;
                            if (data9 != "") anexosAll.Add(JsonSerializer.Deserialize<List<AnexoModel>>(data9));
                            else anexosAll.Add(null);
                        }
                        ViewBag.anexosAll = anexosAll;
                    }

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
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            try
            {
                if (dat != "") ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
                

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
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/AllVal").Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            try
            {
                if(dat !="")  ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);

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
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/AllInter").Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            try
            {
                if (dat != "") ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }

        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }
       



    }
}

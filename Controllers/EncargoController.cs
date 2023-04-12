using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

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
            ViewBag.moldes =  JsonSerializer.Deserialize<List<MoldeModel>>(data2);

            string data3 = response3.Content.ReadAsStringAsync().Result;
            ViewBag.prioridade = JsonSerializer.Deserialize<List<PrioridadeModel>>(data3);
           
            return View();
        }

        
        [HttpPost]
        [Route("Encargo/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EncargoMolde encargo,IEnumerable<IFormFile> files)
        {
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
            Console.WriteLine("Qualidade:"+encargo.qualidade);
            Console.WriteLine("Intervencao:" + encargo.intervencao[0]);
            Console.WriteLine("Intervencao:" + encargo.intervencao.Count);
            Console.WriteLine("Problema:" + encargo.descProblema);
            Console.WriteLine("Molde:" + encargo.moldeid);
            Console.WriteLine("Prioridade:" + encargo.prioridadeid);
            Console.WriteLine("Data Nec.  Meio:" + encargo.dataNecMeio);
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



        //[HttpGet]
        //[Route("Encargo/ListDone")]
        //[Route("Encargo/ListDone/{id}")]
        //public ActionResult ListDone(int id)
        //{
        //    if (id > 0)
        //    {
        //        List<EncargoViewModel> list = new List<EncargoViewModel>();
        //        HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/Completed" + id.ToString()).Result;
        //        string dat = response.Content.ReadAsStringAsync().Result;
        //        list.Add(JsonSerializer.Deserialize<EncargoViewModel>(dat));
        //        ViewBag.encargo = list;
        //    }
        //    else
        //    {

        //        HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/Completed").Result;
        //        string dat = response.Content.ReadAsStringAsync().Result;
        //        ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
        //    }
        //    return View();
        //}


        //[HttpGet]
        //[Route("Encargo/ListNotDone")]
        //[Route("Encargo/ListNotDone/{id}")]
        //public ActionResult ListNotDone(int id)
        //{
        //    if (id > 0)
        //    {
        //        List<EncargoViewModel> list = new List<EncargoViewModel>();
        //        HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/" + id.ToString()).Result;
        //        string dat = response.Content.ReadAsStringAsync().Result;
        //        list.Add(JsonSerializer.Deserialize<EncargoViewModel>(dat));
        //        ViewBag.encargo = list;
        //    }
        //    else
        //    {

        //        HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
        //        string dat = response.Content.ReadAsStringAsync().Result;
        //        ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
        //    }
        //    return View();
        //}

        [HttpGet]
        public IActionResult Info(int id)
        {
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
                if (dat6 != "") ViewBag.interAtual = JsonSerializer.Deserialize<IntervencaoModel2>(dat6);
                if (dat5 != "") ViewBag.interAll = JsonSerializer.Deserialize<List<IntervencaoModel2>>(dat5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
           
            return View(encargo);
        }

        //[HttpGet]
        //[Route("Encargo/{id}")]
        //public IActionResult InfoCompleted(int id)
        //{
        //    HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/" + id.ToString()).Result;
        //    string dat = response.Content.ReadAsStringAsync().Result;
        //    ViewBag.encargo = JsonSerializer.Deserialize<EncargoViewModel>(dat);
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult ListDone()
        //{

        //    return View();
        //}

        [HttpGet]
        public IActionResult All()
        {
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
            return View();
            
        }

        //[HttpGet]
        //public IActionResult Done()
        //{
        //    HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
        //    string dat = response.Content.ReadAsStringAsync().Result;
        //    ViewBag.encargos = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
        //    return Json(ViewBag.encargos);

        //}

        //[HttpGet]
        //public IActionResult NotDone()
        //{
        //    HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
        //    string dat = response.Content.ReadAsStringAsync().Result;
        //    ViewBag.encargos = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
        //    return Json(ViewBag.encargos);

        //}



    }
}

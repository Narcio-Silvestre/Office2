using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Net.Http;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Office.Controllers


{
    public class dbConnetion:Controller
    {
        protected SqlConnection _connection;
        protected SessionKeys? _session;
        protected SqlDataAdapter _adapter;
        protected DataTable _dataTable;

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

        public EncargoController()
        {
            api = new Api();
        }

        public ActionResult Index() { return View(); }


        
        [HttpGet]
        [Route("Encargo/Create")]
        public ActionResult Create()
        {
            
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Reparacao").Result;
            HttpResponseMessage response2 = api.HttpClient.GetAsync("https://localhost:7271/Molde").Result;
            HttpResponseMessage response3 = api.HttpClient.GetAsync("https://localhost:7271/Prioridade").Result;

            

            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.reparacao = JsonSerializer.Deserialize<List<ReparacaoModel>>(dat);

            string data2 = response2.Content.ReadAsStringAsync().Result;
            ViewBag.moldes =  JsonSerializer.Deserialize<List<MoldeModel>>(data2);

            string data3 = response3.Content.ReadAsStringAsync().Result;
            ViewBag.prioridade = JsonSerializer.Deserialize<List<PrioridadeModel>>(data3);
           
            return View();
        }

        
        [HttpPost]
        [Route("Encargo/Create")]
        public ActionResult Create(EncargoMolde data)
        {
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            data.data = DateTime.Now.Date;
            data.entidadeid =_session.Id;
            api.HttpClient.PostAsJsonAsync("https://localhost:7271/Encargo", data);
            return  RedirectToAction("Index","Home");
        }



        [HttpGet]
        [Route("Encargo/ListDone")]
        [Route("Encargo/ListDone/{id}")]
        public ActionResult ListDone(int id)
        {
            if (id > 0)
            {
                List<EncargoViewModel> list = new List<EncargoViewModel>();
                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/Completed" + id.ToString()).Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                list.Add(JsonSerializer.Deserialize<EncargoViewModel>(dat));
                ViewBag.encargo = list;
            }
            else
            {

                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/Completed").Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
            }
            return View();
        }


        [HttpGet]
        [Route("Encargo/ListNotDone")]
        [Route("Encargo/ListNotDone/{id}")]
        public ActionResult ListNotDone(int id)
        {
            if (id > 0)
            {
                List<EncargoViewModel> list = new List<EncargoViewModel>();
                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/" + id.ToString()).Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                list.Add(JsonSerializer.Deserialize<EncargoViewModel>(dat));
                ViewBag.encargo = list;
            }
            else
            {

                HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo").Result;
                string dat = response.Content.ReadAsStringAsync().Result;
                ViewBag.encargo = JsonSerializer.Deserialize<List<EncargoViewModel>>(dat);
            }
            return View();
        }

        [HttpGet]
        [Route("Encargo/{id}")]
        public IActionResult Info(int id)
        {
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/"+id.ToString()).Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.encargo = JsonSerializer.Deserialize<EncargoViewModel>(dat);
            return View();
        }

        [HttpGet]
        [Route("Encargo/{id}")]
        public IActionResult InfoCompleted(int id)
        {
            HttpResponseMessage response = api.HttpClient.GetAsync("https://localhost:7271/Encargo/" + id.ToString()).Result;
            string dat = response.Content.ReadAsStringAsync().Result;
            ViewBag.encargo = JsonSerializer.Deserialize<EncargoViewModel>(dat);
            return View();
        }





    }
}

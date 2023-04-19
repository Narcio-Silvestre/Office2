using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.WebPages;

namespace Office.Controllers
{
    public class LoginController : dbConnetion
    {
        Api api;

        public LoginController()
        {
            api = new Api();
        }

        // GET: LoginController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login login)
        {
            HttpResponseMessage response = api.HttpClient.PostAsJsonAsync("https://localhost:7271/Login",login).Result;
            
            string dat = response.Content.ReadAsStringAsync().Result;
            if (!dat.IsEmpty())
            {
                UserModel model = JsonSerializer.Deserialize<UserModel>(dat);
                Console.WriteLine(model.funcId);
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(new SessionKeys() { Id = Convert.ToInt32(model.id), Name = model.name, funcaoid = model.funcId }));
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Out()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

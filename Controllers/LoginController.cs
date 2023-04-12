using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            UserModel model= JsonSerializer.Deserialize<UserModel>(dat);
            HttpContext.Session.SetString("User",JsonSerializer.Serialize(new SessionKeys() { Id=Convert.ToInt32(model.id),Name= model.name }));
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

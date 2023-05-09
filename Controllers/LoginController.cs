using Microsoft.AspNetCore.Mvc;
using Office.Dataset;
using Office.Models;
using System.Text.Json;

namespace Office.Controllers
{
    public class LoginController : Controller
    {
       

        public LoginController()
        {
            
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
        public ActionResult Create(LoginModel login)
        {
            
            UserModel dat = LoginDataSet.Create(login);
            if (dat != null)
            {
                UserModel model = dat;
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(new SessionKeys()
                { 
                    Id = Convert.ToInt32(model.Id),
                    Name = model.Name,
                    funcaoid = model.FuncId 
                }));
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Usuário não encontrado";
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Out()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

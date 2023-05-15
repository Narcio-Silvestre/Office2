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

        /// <summary>
        /// Método para obter a página de login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para fazer o login na aaplicação
        /// </summary>
        /// <param name="login">modelo de login</param>
        /// <returns>a página home se for bem-sucedido ou a página inicial de login caso falhe</returns>
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
                return RedirectToAction("Create", "Encargo");
            }
            TempData["ErrorMessage"] = "Usuário não encontrado";
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// Método para terminar a sessão no site
        /// </summary>
        /// <returns>retorna a página de login</returns>
        [HttpGet]
        public ActionResult Out()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

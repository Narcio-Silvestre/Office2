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
            _adapter = new SqlDataAdapter(("select email,password,nome,id from entidade where entidade.email=@v1 and entidade.password=@v2"),_connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("v1", login.Email));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("v2", login.Password));
            DataTable vn = new DataTable();
            _adapter.Fill(vn);
            if (vn.Rows.Count > 0)
            {
                if (vn.Rows[0][0].ToString() == login.Email && vn.Rows[0][1].ToString() == login.Password)
                {
                    HttpContext.Session.SetString("User",JsonSerializer.Serialize(new SessionKeys() { Id=Convert.ToInt32(vn.Rows[0][3]),Name= vn.Rows[0][2].ToString() }));
                    return RedirectToAction("Index","Home");
                }
                else
                    return Json("problem");
            }
            else {
                return Json("problem");
            } 
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

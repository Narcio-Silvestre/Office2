using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Office.Controllers
{
    public class ProfileController : Controller
    {
        // GET: ProfileController
        public ActionResult Index()
        {
            var session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            SqlConnection db = new SqlConnection("Data Source=lolly;Initial Catalog=WORK;Integrated Security=True");
            SqlDataAdapter cmd = new SqlDataAdapter(("select email,nome from utilizador where utilizador.id=@v1"), db);
            cmd.SelectCommand.Parameters.Add(new SqlParameter("v1", session.Id));
            DataTable vn = new DataTable();
            cmd.Fill(vn);
            if (vn.Rows.Count > 0)
            {
                ProfileViewModel model = new ProfileViewModel();
                model.Tel = vn.Rows[0][1].ToString();
                model.Nome = vn.Rows[0][2].ToString();
                model.Email = vn.Rows[0][0].ToString();
                return View(model);
            }
            else
            {
                return Json("problem");
            }
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

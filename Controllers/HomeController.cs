﻿using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Diagnostics;

namespace Office.Controllers
{
    /// <summary>
    /// Classe controladora da página Home
    /// </summary>
    public class HomeController : Controller
    {
        
        /// <summary>
        /// Método que retorna a página home
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
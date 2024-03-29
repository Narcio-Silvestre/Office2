﻿using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Office.Controllers
{
    public class HomeController : dbConnetion
    {
        

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
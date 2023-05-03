﻿using Microsoft.AspNetCore.Mvc;
using Office.Models;
using Office.Dataset;

namespace Office.Controllers
{
    public class MoldeController : Controller
    {
        

        public MoldeController()
        {
            
        }
        // GET: MoldeController

        [HttpGet]
        [Route("Molde/")]
        public ActionResult Index(int id)
        {
            List<MoldeModel>? dat = MoldeDataSet.Index();
            ViewBag.molde =dat;
            return View();
        }

        [HttpGet]
        [Route("Molde/Info/{id}")]
        public IActionResult Info(int id)
        {
            MoldeModel? dat = MoldeDataSet.Get(id);
            ViewBag.molde = dat;
            return View();
        }

    }
}

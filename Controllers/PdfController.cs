using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Dataset;
using Office.Models;
using Rotativa.AspNetCore;
using System.Text.Json;

namespace Office.Controllers
{
    public class PdfController : Controller
    {
        SessionKeys? _session;
        string path;

        [HttpGet]
        public IActionResult Info(int id)
        {
            PdfModel  model = new PdfModel();
            
            _session = JsonSerializer.Deserialize<SessionKeys>(HttpContext.Session.GetString("User"));
            ViewBag.UserFuncId = _session.funcaoid;
            List<RequisitosModel> dat2 = RequisitosDataSet.Index(id);
            List<RequisitosModel> dat3 = RequisitosDataSet.Index2(id);
         
            List<IntervencaoModel2> dat5 = IntervencaoDataSet.Intervencao(id);
          
            EncargoViewModel encargo = EncargoDataSet.Encargo(id);
            model.Encargo = encargo;
            
            
            try
            {
                if (dat2 != null) model.Requisitos = dat2;
                if (dat3 != null) model.Requisitos2 = dat3;
                if (dat5 != null) model.interAll = dat5;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new ViewAsPdf("Info", model)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };
        }

        public IActionResult InfoMolde(int id)
        {
           

            MoldeModel? dat = MoldeDataSet.Get(id);
            MoldeModel model = new MoldeModel();
            if (dat != null)
            {
                model = dat;
            }
            return new ViewAsPdf("InfoMolde", model)
            {
                
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Office.Models
{
    public class EncargoMolde
    {
        public int id { get; set; }
        public string descProblema{ get; set; }
        public DateTime data { get; set; }
        public DateTime dataNecMeio { get; set; }
        public  string empresa { get; set; }
        public int entidadeid { get; set; }
        public int moldeid { get; set; }
        public string prioridadeid { get; set; }
        public int reparacaoid { get; set;}
        public int qualidade { get; set; }
        public int intervencao { get; set; }
    }

    public class EncargoViewModel
    {
        public int id { get; set; }
        public string nrEncargo { get; set; }
        public string descProblema { get; set; }
        public DateTime data { get; set; }
        public DateTime dataNecMeio { get; set; }
        public string empresa { get; set; }
        public string entidade { get; set; }
        public string molde { get; set; }
        public string prioridade { get; set; }
        public string reparacao { get; set; }
        public string estado { get; set; }
        //public DateTime dataConc { get; set; }
    }
}

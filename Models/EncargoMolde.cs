using Microsoft.AspNetCore.Mvc;

namespace Office.Models
{
    public class EncargoMolde
    {
        public string descProblema{ get; set; }
        public DateTime dataNecMeio { get; set; }
        public int entidadeid { get; set; }
        public int moldeid { get; set; }
        public int prioridadeid { get; set; }
        public int qualidade { get; set; }
        public List<string> intervencao { get; set; }
        public List<string> anexos { get; set; }


    }

    public class EncargoViewModel
    {
        public int id { get; set;}
        public string nrEncargo { get; set; }
        public string descProblema { get; set; }
        public DateTime data { get; set; }
        public DateTime dataNecMeio { get; set; }
        public string entidade { get; set; }
        public string molde { get; set; }
        public string prioridade { get; set; }
        public string estado { get; set; }
        public int nrInt { get; set;}
        public int validQual { get; set; }
        //public DateTime dataConc { get; set; }
    }
}

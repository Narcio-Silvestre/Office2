﻿namespace Office.Models
{
    public class IntervencaoMenuModel
    {
        public int idIntervencao { get; set; }
        public int idEncargo { get; set; }
        public int numIntervencao { get; set; }
        public string prioridade { get; set; }
        public string nrEncargo { get; set; }
        public string molde { get; set; }

    }

    public class IntervencaoModel
    {

        public int idEncargo { get; set; }
        public int idEntidade { get; set; }
        public string descricao { get; set; }
        public int extInt { get; set; }
        public List<string> anexos { get; set; }

    }

    public class IntervencaoViewModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public int reparacaoid { get; set; }

    }

    public class AnexoModel
    {
        public string desc { get; set; }

    }

    public class IntervencaoModel2
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public string descInt { get; set; }
        public int valProducao { get; set; }
        public int valQualidade { get; set; }
        public string descProducao { get; set; }
        public string descQualidade { get; set; }
        public DateTime dataValProd { get; set; }
        public DateTime dataValQual { get; set; }
        public string respValQual { get; set; }
        public string respValProd { get; set; }
        public string encargo { get; set; }
        public string utilizador { get; set; }
        public string estado { get; set; }
    }




}

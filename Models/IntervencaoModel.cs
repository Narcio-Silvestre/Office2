namespace Office.Models
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
        public int idIntervencao { get; set; }
        public int idEncargo { get; set; }
        public int idEntidade { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int reparacaoid { get; set; }

    }

    public class IntervencaoViewModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public int reparacaoid { get; set; }

    }
}

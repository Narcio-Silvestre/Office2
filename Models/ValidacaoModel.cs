namespace Office.Models
{
    public class ValidacaoMenuModel
    {
        public int idValidacao { get; set; }
        public int idIntervencao { get; set; }
        public int idEncargo { get; set; }
        public int numValidacoes { get; set; }
        public string nrEncargo { get; set; }
        public string molde { get; set; }
        public string prioridade { get; set; }
        //public string desc { get; set; }
    }

    public class ValidacaoModel
    {
        public int idValidacao { get; set; }
        public int idIntervencao { get; set; }
        public int idEncargo { get; set; }
        public int idEntidade { get; set; }
        public int aprovado { get; set; }
        public string descricao { get; set; }
    }

}

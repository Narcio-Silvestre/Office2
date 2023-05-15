namespace Office.Models
{
    /// <summary>
    /// Modelo de Validação
    /// </summary>
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


    /// <summary>
    /// Segundo Modelo de Validação
    /// </summary>
    public class ValidacaoModel
    {
        
        public int idInter { get; set; }
        public int idEntidade { get; set; }
        public int aprovado { get; set; }
        public string descricao { get; set; }
    }

}

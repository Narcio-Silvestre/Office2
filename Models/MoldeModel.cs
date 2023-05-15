namespace Office.Models
{
    /// <summary>
    /// Modelo de Molde
    /// </summary>
    public class MoldeModel
    {
        public int id { get; set; }
        public string nrMolde { get; set; }
        public string maxShots { get; set; }
        public string descCompleta { get; set; }
        public string descricao { get; set; }
        public int shots { get; set; }
        public int nrEncargos { get; set; }
    }
}

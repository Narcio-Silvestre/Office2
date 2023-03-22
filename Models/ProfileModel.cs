using System.Runtime.InteropServices;
using System;

namespace Office.Models
{
    public class ProfileModel
    {
        private int id;
        private string nome;
        private string email;
        private int nif;
        private int niss;
        private string tel;
        private DateTime dataInicio;
        private DateTime dataFim;
        private string password;
        private int funcaoid;
        private int papelid;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public int Nif { get => nif; set => nif = value; }
        public int Niss { get => niss; set => niss = value; }
        public string Tel { get => tel; set => tel = value; }
        public DateTime DataInicio { get => dataInicio; set => dataInicio = value; }
        public DateTime DataFim { get => dataFim; set => dataFim = value; }
        public string Password { get => password; set => password = value; }
        public int Funcaoid { get => funcaoid; set => funcaoid = value; }
        public int Papelid { get => papelid; set => papelid = value; }
    }

    
}

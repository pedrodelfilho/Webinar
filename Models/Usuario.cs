using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string ActivationCode { get; set; }
        public string Tipo { get; set; }
    }

    public class Convidado
    {
        public int IDConvidado { get; set; }
        public int UserId { get; set; }
        public virtual byte[] FotoConvidado { get; set; }
        public char SexoConvidado { get; set; }
        public string EscolaridadeConvidado { get; set; }
        public string ConvidadoBioP1 { get; set; }
        public string ConvidadoDtNasc { get; set; }
        public string ConvidadoCidadeUF { get; set; }
        public bool ConvidadoReceberEmail { get; set; }
    }
}

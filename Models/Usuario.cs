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
        public DateTime CreatedDate { get; set; }
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

    public class Palestrante
    {
        public int IDPalestrante { get; set; }
        public int UserId { get; set; }
        public virtual byte[] PalestranteFoto { get; set; }
        public DateTime PalestranteDtNasc { get; set; }
        public string PalestranteCidadeUF { get; set; }
        public char PalestranteSexo { get; set; }
        public string PalestranteFormacao { get; set; }
        public string PalestranteEspecialidade { get; set; }
        public string PalestranteBioP1 { get; set; }
        public string PalestranteBioP2 { get; set; }
        public bool PerfilAprovado { get; set; }
        public bool PalestranteReceberEmail { get; set; }
        public bool PalestranteAutoriza { get; set; }
        public string PalestranteTwiter { get; set; }
        public string PalestranteFacebook { get; set; }
        public string PalestranteGoogle { get; set; }
        public string PalestranteLinkedin { get; set; }
    }
    public class Moderador
    {
        public int IDModerador { get; set; }
    }
    public class Administrador
    {
        public int IDAdministrador { get; set; }
    }
}

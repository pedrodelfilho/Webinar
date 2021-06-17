using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Certificados
    {
        public int IDCertificado { get; set; }
        public int UserId { get; set; }
        public int IDPalestra { get; set; }
        public int IDEvento { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFinal { get; set; }
        public bool Finalizado { get; set; }
        public TimeSpan Progresso { get; set; }
        public TimeSpan Alvo { get; set; }
        public string PalestraTitulo { get; set; }
        public string EventoTitulo { get; set; }
        public string Porcentagem { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }        
    }
    public class Maintenance
    {
        public virtual byte[] BackgroundCert { get; set; }
        public string EmailContact { get; set; }
    }
}

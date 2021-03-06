using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Evento
    {
        public int IDEvento { get; set; }
        public int IDAdm { get; set; }
        public string EventoTitulo { get; set; }
        public string EventoSubTitulo { get; set; }
        public string EventoSinopseP1 { get; set; }
        public string EventoSinopseP2 { get; set; }
        public DateTime EventoDtIni { get; set; }
        public DateTime EventoDtTer { get; set; }
        public virtual byte[] EventoCapa { get; set; }
        public string ModResponsavel { get; set; }
        public bool Acervo { get; set; }
    }

    public class SendLinkEvento
    {
        public int IDRequerente { get; set; }
        public int IDEvento { get; set; }
        public int IDPalestra { get; set; }
    }
}

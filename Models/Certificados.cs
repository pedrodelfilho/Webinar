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
    }
}

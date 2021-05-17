using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Conexao
    {
        public int IDConEmpresarial { get; set; }
        public string NmConEmpresarial { get; set; }
        public virtual byte[] LogoConEmpresarial { get; set; }
        public DateTime DtIniConEmpresarial { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Palestra
    {
        public int IDPalestra { get; set; }
        public int IDPalestrante { get; set; }
        public int PalestraCriador { get; set; }
        public string  PalestraLink { get; set; }
        public DateTime PalestraDtCriacao { get; set; }
        public virtual byte[] PalestraCapa { get; set; }
        public string  PalestraCategoria { get; set; }
        public string PalestraTitulo { get; set; }
        public string PalestraSubTitulo { get; set; }
        public string PalestraSinopseP1 { get; set; }
        public string PalestraSinopseP2 { get; set; }
        public string PalestraSinopseP3 { get; set; }
        public string PalestraSinopseP4 { get; set; }
        public bool PalestraDemonstrar { get; set; }
        public string PalestraDuracao { get; set; }
        public DateTime PalestraData { get; set; }
        public bool PalestraAutoriza { get; set; }
        public bool PalestraAprovada { get; set; }
        public int IDEvento { get; set; }
        public bool Acervo { get; set; }
    }
}

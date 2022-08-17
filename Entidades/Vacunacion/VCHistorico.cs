using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Vacunacion
{
    public class VCHistorico
    {
        public int IntOidUsuario { get; set; } 
        public string StrNombre { get; set; }
        public DateTime DtmFecha { get; set; }
        public int IntOidRegistroDiarioVac { get; set; }
        public string StrAccion { get; set; }
    }
}
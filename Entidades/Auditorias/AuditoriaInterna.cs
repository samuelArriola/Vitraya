using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Auditorias
{
    public class AuditoriaInterna
    {
        public int IntOidAuditoriaInterna { get; set; }
        public int IntOidListaArchivos { get; set; }
        public int IntOidUsuarioCreador { get; set; }
        public string  StrResponsable { get; set; }
        public string StrObjetivo { get; set; }
        public string StrAlcance { get; set; }
        public string StrProcesos { get; set; }
        public DateTime DtmFecha { get; set; }
        
    }
}
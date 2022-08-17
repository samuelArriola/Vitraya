using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Auditorias
{
    public class Hallazgo
    {
        public static int AUDITORIA_INTERNA = 1, AUDITORIA_EXTERNA = 2; 
        public int IntOidHallazgo { get; set; }
        public int IntContexto { get; set; }
        public int IntInstancia { get; set; }
        public string StrDescripcion { get; set; }
        public int IntResponsable { get; set; }
    }
}
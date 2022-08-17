using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Power_BI
{
    public class ELIstaReportes
    {

        int OidReportePB, Estado;
        String Nombre, Enlace, Descripcion, Tipo;

        public int OidReportePB1 { get => OidReportePB; set => OidReportePB = value; }
        public int Estado1 { get => Estado; set => Estado = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Enlace1 { get => Enlace; set => Enlace = value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion = value; }
        public string Tipo1 { get => Tipo; set => Tipo = value; }

    }
}
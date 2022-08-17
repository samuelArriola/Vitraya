using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDDivulgacion
    {
        private int intOidGDDivulgacion, intOidCPEjeTematico, intOidGDDocumento, intTempFirma;
        private string strCargos, strSubtemas;
        

        public int IntOidGDDivulgacion { get => intOidGDDivulgacion; set => intOidGDDivulgacion = value; }
        public int IntOidCPEjeTematico { get => intOidCPEjeTematico; set => intOidCPEjeTematico = value; }
        public string StrCargos { get => strCargos; set => strCargos = value; }
        public string StrSubtemas { get => strSubtemas; set => strSubtemas = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntTempFirma { get => intTempFirma; set => intTempFirma = value; }
    }
}
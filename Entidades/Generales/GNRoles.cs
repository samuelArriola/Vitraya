using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNRoles
    {
        private int intOidGNRol;
        private string strNombre;

        public int IntOidGNRol { get => intOidGNRol; set => intOidGNRol = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNModulos
    {
        private int         intOidGNModulo;
        
        private string      strPrefijo, 
                            strNombre,
                            strIcono;

        public int IntOidGNModulo { get => intOidGNModulo; set => intOidGNModulo = value; }
        public string StrPrefijo { get => strPrefijo; set => strPrefijo = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrIcono { get => strIcono; set => strIcono = value; }
    }
}
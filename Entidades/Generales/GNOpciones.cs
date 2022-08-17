using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNOpciones
    {
        private int         intOidGNOpcion,
                            intOidGNModulo,
                            intEstadoBloqueo;

        private string      strPrefijo,
                            strNombre,
                            strNombreModulo;

        public int IntOidGNOpcion { get => intOidGNOpcion; set => intOidGNOpcion = value; }
        public int IntOidGNModulo { get => intOidGNModulo; set => intOidGNModulo = value; }
        public string StrPrefijo { get => strPrefijo; set => strPrefijo = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrNombreModulo { get => strNombreModulo; set => strNombreModulo = value; }
        public int IntEstadoBloqueo { get => intEstadoBloqueo; set => intEstadoBloqueo = value; }
    }
}
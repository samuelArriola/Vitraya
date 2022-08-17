using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Procesos
{
    public class PCNormagrama
    {
        private int intOidPCNormagrama, intNumNorma;

        private string strEmision, strDescripcion, strEstado, strUrl, strTipo;

        private DateTime dtmFecEmision;

        public int IntOidPCNormagrama { get => intOidPCNormagrama; set => intOidPCNormagrama = value; }
        public int IntNumNorma { get => intNumNorma; set => intNumNorma = value; }
        public string StrEmision { get => strEmision; set => strEmision = value; }
        public string StrDescripcion { get => strDescripcion; set => strDescripcion = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string StrUrl { get => strUrl; set => strUrl = value; }
        public DateTime DtmFecEmision { get => dtmFecEmision; set => dtmFecEmision = value; }
        public string StrTipo { get => strTipo; set => strTipo = value; }
    }
}
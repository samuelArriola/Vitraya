using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Entidades.Generales
{
    public class GNEps 
    {
        private int         intOidGNEps;

        private  string     strNomEps, 
                            strEstado;

        public int IntOidGNEps { get => intOidGNEps; set => intOidGNEps = value; }
        public string StrNomEps { get => strNomEps; set => strNomEps = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Entidades.Generales
{
    public class Eps 
    {
        private int         intGnIdEps;

        private  string     strGnCodEps, 
                            strGnNomEps, 
                            strGnEstEps;

        public int IntGnIdEps { get => intGnIdEps; set => intGnIdEps = value; }
        public string StrGnCodEps { get => strGnCodEps; set => strGnCodEps = value; }
        public string StrGnNomEps { get => strGnNomEps; set => strGnNomEps = value; }
        public string StrGnEstEps { get => strGnEstEps; set => strGnEstEps = value; }
    }
}
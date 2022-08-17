using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNDireccion
    {
        private int intCdDir, intOidGNDir;

        private string strEstado, strSiglaDir, strNomDir;

        public int IntCdDir { get => intCdDir; set => intCdDir = value; }
        public int IntOidGNDir { get => intOidGNDir; set => intOidGNDir = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string StrSiglaDir { get => strSiglaDir; set => strSiglaDir = value; }
        public string StrNomDir { get => strNomDir; set => strNomDir = value; }
    }
}
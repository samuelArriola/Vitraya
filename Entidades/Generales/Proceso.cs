using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class Proceso
    {
        private int intOIdProceso;

        private string    strNomPro,
                          strEstado;

        public int IntOIdProceso { get => intOIdProceso; set => intOIdProceso = value; }
        public string StrNomPro { get => strNomPro; set => strNomPro = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
    }
}
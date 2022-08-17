using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class ScriptBloqueo
    {

        public int intOidGnScriptsBloqueos;

        public string  strNombre,
                       strEstado,
                       strResultConsulta;
        private int    intValidacion;

        public int IntOidGnScriptsBloqueos { get => intOidGnScriptsBloqueos; set => intOidGnScriptsBloqueos = value;  }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string IntResultConsulta { get => strResultConsulta; set => strResultConsulta = value; }
        public int IntValidacion { get => intValidacion; set => intValidacion = value; }
    }
}
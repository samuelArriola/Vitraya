using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class OpcionesBloqueadas
    {

        public int intOidGNOpcionBloqueada, intOidGNOpcion, intEstado;

        public string stringNombre, stringPrefijo;

        public int IntOidGNOpcionBloqueada { get => intOidGNOpcionBloqueada; set => intOidGNOpcionBloqueada = value; }
        public int IntOidGNOpcion { get => intOidGNOpcion; set => intOidGNOpcion = value; }
        public int IntEstado { get => intEstado; set => intEstado = value; }
        public string StringNombre { get => stringNombre; set => stringNombre = value; }
        public string StringPrefijo { get => stringPrefijo; set => stringPrefijo = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNListaArchivos

    {
        private int     intOidGNListaArchivos, 
                        intOidGNModulo;

        public int IntOidGNListaArchivos { get => intOidGNListaArchivos; set => intOidGNListaArchivos = value; }
        public int IntOidGNModulo { get => intOidGNModulo; set => intOidGNModulo = value; }
    }
}
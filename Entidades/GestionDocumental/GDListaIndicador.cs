using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDListaIndicador
    {
        private int intOIdGDDocIndicador, intOIdGDDocumento, intOidGDListaIndicador;

        public int IntOIdGDDocIndicador { get => intOIdGDDocIndicador; set => intOIdGDDocIndicador = value; }
        public int IntOIdGDDocumento { get => intOIdGDDocumento; set => intOIdGDDocumento = value; }
        public int IntOidGDListaIndicador { get => intOidGDListaIndicador; set => intOidGDListaIndicador = value; }
    }
}
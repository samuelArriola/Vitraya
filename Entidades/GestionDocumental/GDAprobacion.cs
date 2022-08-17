using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDAprobacion
    {
        private int         intOidGDAprobacion,
                            intOidGDDocumento,
                            intOidRevisor,
                            intEstado;

        private string      strDetalles,
                            strCargo;


        private DateTime    dtmFecha;

        public int IntOidGDAprobacion { get => intOidGDAprobacion; set => intOidGDAprobacion = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntOidRevisor { get => intOidRevisor; set => intOidRevisor = value; }
        public int IntEstado { get => intEstado; set => intEstado = value; }
        public string StrDetalles { get => strDetalles; set => strDetalles = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }
        public string StrCargo { get => strCargo; set => strCargo = value; }
    }
}
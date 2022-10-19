using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ControlEntSal
{
    public class InfofugasModel
    {
        private long ingreso;
        private string ordensalida, cama, nombrecana, documento, nombrecomp;
        private DateTime fecingreso, fectegreso, fechaorden ,opor_min_ingre_orden, opor_min_orden_egre;

        public long Ingreso { get => ingreso; set => ingreso= value; }
        public string OrdenSalida { get => ordensalida; set => ordensalida = value; }
        public string Cama { get => cama; set => cama = value; }
        public string NombreCama { get => nombrecana; set => nombrecana = value; }
        public string Doc { get => documento; set => documento = value; }
        public string NombreCompleto { get => nombrecomp; set => nombrecomp = value; }
        public DateTime? OporMiIngreOrde { get ; set; }
        public DateTime? OporMiOrdEgre { get; set; }

        public DateTime FechaIgre { get => fectegreso; set => fectegreso = value; }
        public DateTime? FechaEgre { get; set ; }
        public DateTime? FechaOrden { get; set; }
}
}
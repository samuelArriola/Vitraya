using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPAgenda
    {
        public int IntOidCPAgenda           { get; set; }
        public int IntOidGNListaArchivos    { get; set; }
        public int IntOidCPCapacitacion     { get; set; }
        public int IntTiempoFirma           { get; set; }
        public int IntEstado                { get; set; }
        public int IntOidUsuarioResponsable { get; set; }
        public int IntOidCPExamen           { get; set; }

        public DateTime DtmFecha            { get; set; }
        public DateTime DtmFechaFirma       { get; set; }
        public DateTime DtmHoraInicial      { get; set; }
        public DateTime DtmHoraFinal        { get; set; }
        public DateTime DtmFechaFinal       { get; set; }

        public string StrModalidad          { get; set; }
        public string StrLugar              { get; set; }
        public string StrResponsable        { get; set; }
        public string StrLinkReunion        { get; set; }
        public string StrLinkAnexo          { get; set; }
        public string StrFacilitador        { get; set; }

    }
}
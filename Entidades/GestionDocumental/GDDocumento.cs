using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDDocumento
    {
        private int intOidGDDocumento,
                        intOidGDSolicitud,
                        intVersion,
                        intEstado,
                        intOidPCProceso,
                        intConsecutivo;

        private string  strNomDoc,
                        strUniFunSolicitante,
                        strNomSolicitante,
                        strCodigoDoc,
                        strTipDoc;

        public const int PRELIMINAR = 0, CONSTRUCCION = 1,REVISION = 2,APROBACION = 3,PUBLICADO = 4,ELIMINADO = 5;



        private DateTime dtFechaE;

        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntOidGDSolicitud { get => intOidGDSolicitud; set => intOidGDSolicitud = value; }
        public string StrNomDoc { get => strNomDoc; set => strNomDoc = value; }
        public string StrUniFunSolicitante { get => strUniFunSolicitante; set => strUniFunSolicitante = value; }
        public string StrNomSolicitante { get => strNomSolicitante; set => strNomSolicitante = value; }
        public string StrCodigoDoc { get => strCodigoDoc; set => strCodigoDoc = value; }
        public string StrTipDoc { get => strTipDoc; set => strTipDoc = value; }
        public DateTime DtFechaE { get => dtFechaE; set => dtFechaE = value; }
        public int IntVersion { get => intVersion; set => intVersion = value; }
        public int IntEstado { get => intEstado; set => intEstado = value; }
        public int IntOidPCProceso { get => intOidPCProceso; set => intOidPCProceso = value; }
        public int IntConsecutivo { get => intConsecutivo; set => intConsecutivo = value; }
    }
}
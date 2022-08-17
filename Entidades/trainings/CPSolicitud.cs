using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPSolicitud
    {
        private int intGNCodUsu;

        private string strInfoMatricula;


        private DateTime dtmHoraFinal;
        private int intOidCpsolicitud;
        private int intOidCPEjeTematico;
        private int intEstado;
        private int intOidListaArchivos;
        private int intOidGNAchivo;
        private string strLugar;
        private string strUnidadFuncional;
        private string strTema;
        private string strModalidad;
        private string strResponsable;
        private string strLink;
        private DateTime dtmFecha;
        private DateTime dtmHoraInicial;

        public int IntOidCpsolicitud { get => intOidCpsolicitud; set => intOidCpsolicitud = value; }
        public int IntOidCPEjeTematico { get => intOidCPEjeTematico; set => intOidCPEjeTematico = value; }
        public int IntEstado { get => intEstado; set => intEstado = value; }
        public int IntOidListaArchivos { get => intOidListaArchivos; set => intOidListaArchivos = value; }
        public int IntOidGNAchivo { get => intOidGNAchivo; set => intOidGNAchivo = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public string StrLugar { get => strLugar; set => strLugar = value; }
        public string StrUnidadFuncional { get => strUnidadFuncional; set => strUnidadFuncional = value; }
        public string StrTema { get => strTema; set => strTema = value; }
        public string StrModalidad { get => strModalidad; set => strModalidad = value; }
        public string StrResponsable { get => strResponsable; set => strResponsable = value; }
        public string StrLink { get => strLink; set => strLink = value; }
        public string StrInfoMatricula { get => strInfoMatricula; set => strInfoMatricula = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }
        public DateTime DtmHoraInicial { get => dtmHoraInicial; set => dtmHoraInicial = value; }
        public DateTime DtmHoraFinal { get => dtmHoraFinal; set => dtmHoraFinal = value; }
    }
}
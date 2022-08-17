using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Procesos
{
    public class PCProceso
    {
        private int intOIdProceso,
                            intOidGNListaArchivo,
                            intVersion,
                            intOidGDSolicitud,
                            intOidGDDocumento,
                            intGnDcDep;

        private string      strNomPro,
                            strEstado,
                            strTipo,
                            strProcesoPadre,
                            strPrefijo,
                            strObjetivo,
                            strAlcance,
                            strLideresProceso,
                            strRecFinancieros,
                            strRecHumanos,
                            strNormas,
                            strRiesgos,
                            strDocRelacionados,
                            strRecursosFis,
                            strRecursosInfo,
                            strRecursosTec,
                            strFlujoGrama,
                            strRecursosMed,
                            strNomProPadre;



        DateTime            dtFecha;
        
        List<PCSIPOC>       sIPOCs;

        public int IntOIdProceso { get => intOIdProceso; set => intOIdProceso = value; }
        public int IntOidGNListaArchivo { get => intOidGNListaArchivo; set => intOidGNListaArchivo = value; }
        public string StrNomPro { get => strNomPro; set => strNomPro = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string StrTipo { get => strTipo; set => strTipo = value; }
        public string StrProcesoPadre { get => strProcesoPadre; set => strProcesoPadre = value; }
        public string StrPrefijo { get => strPrefijo; set => strPrefijo = value; }
        public string StrObjetivo { get => strObjetivo; set => strObjetivo = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public string StrLideresProceso { get => strLideresProceso; set => strLideresProceso = value; }
        public string StrRecFinancieros { get => strRecFinancieros; set => strRecFinancieros = value; }
        public string StrRecHumanos { get => strRecHumanos; set => strRecHumanos = value; }
        public string StrNormas { get => strNormas; set => strNormas = value; }
        public string StrRiesgos { get => strRiesgos; set => strRiesgos = value; }
        public string StrDocRelacionados { get => strDocRelacionados; set => strDocRelacionados = value; }
        public List<PCSIPOC> SIPOCs { get => sIPOCs; set => sIPOCs = value; }
        public int IntVersion { get => intVersion; set => intVersion = value; }
        public DateTime DtFecha { get => dtFecha; set => dtFecha = value; }
        public int IntOidGDSolicitud { get => intOidGDSolicitud; set => intOidGDSolicitud = value; }
        public string StrRecursosFis { get => strRecursosFis; set => strRecursosFis = value; }
        public string StrRecursosInfo { get => strRecursosInfo; set => strRecursosInfo = value; }
        public string StrRecursosTec { get => strRecursosTec; set => strRecursosTec = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntGnDcDep { get => intGnDcDep; set => intGnDcDep = value; }
        public string StrFlujoGrama { get => strFlujoGrama; set => strFlujoGrama = value; }
        public string StrRecursosMed { get => strRecursosMed; set => strRecursosMed = value; }
        public string StrNomProPadre { get => strNomProPadre; set => strNomProPadre = value; }
    }
}
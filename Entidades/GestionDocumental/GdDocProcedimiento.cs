using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GdDocProcedimiento
    {

        private int                     intOIdGdDocprocedimiento,
                                        intOidGDDocumento,
                                        intOidGNListaArchivo,
                                        intOidRevisor,
                                        intOidAprobador,
                                        intOidGDProceso;

        int intOidCPCAPACITACION, OidCPAgenda;

        private string                  strNomProceso,
                                        strNomProcedimiento,
                                        strAlcance,
                                        strObjetivo,
                                        strResponsable,
                                        strRecursosNecesarios,
                                        strEntradas,
                                        strSalidas,
                                        strProEsperado,
                                        strEstCalidad,
                                        strRefNormativas,
                                        strDefiniciones,
                                        strAnexos,
                                        strNomAprobador,
                                        strNomRevisor,
                                        strDocRelacionados,
                                        strActividad,
                                        strFlujoGrama,
                                        strDocumentosRelacionados;



        private string                  strClientes, 
                                        strEquipos,
                                        strMedicamentos, 
                                        strProveedores, 
                                        strRecFin, 
                                        strRecInfo, 
                                        strTalentoHumano,
                                        strIndicadores;


       

        private DateTime                dtFechaC,
                                        dtFechaRevision,
                                        dtFechaAprobacion;

        public int IntOIdGdDocprocedimiento { get => intOIdGdDocprocedimiento; set => intOIdGdDocprocedimiento = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntOidGNListaArchivo { get => intOidGNListaArchivo; set => intOidGNListaArchivo = value; }
        public int IntOidRevisor { get => intOidRevisor; set => intOidRevisor = value; }
        public int IntOidAprobador { get => intOidAprobador; set => intOidAprobador = value; }
        public int IntOidCPCAPACITACION { get => intOidCPCAPACITACION; set => intOidCPCAPACITACION = value; }
        public string StrNomProceso { get => strNomProceso; set => strNomProceso = value; }
        public string StrNomProcedimiento { get => strNomProcedimiento; set => strNomProcedimiento = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public string StrObjetivo { get => strObjetivo; set => strObjetivo = value; }
        public string StrResponsable { get => strResponsable; set => strResponsable = value; }
        public string StrRecursosNecesarios { get => strRecursosNecesarios; set => strRecursosNecesarios = value; }
        public string StrEntradas { get => strEntradas; set => strEntradas = value; }
        public string StrSalidas { get => strSalidas; set => strSalidas = value; }
        public string StrProEsperado { get => strProEsperado; set => strProEsperado = value; }
        public string StrEstCalidad { get => strEstCalidad; set => strEstCalidad = value; }
        public string StrRefNormativas { get => strRefNormativas; set => strRefNormativas = value; }
        public string StrDefiniciones { get => strDefiniciones; set => strDefiniciones = value; }
        public string StrAnexos { get => strAnexos; set => strAnexos = value; }
        public string StrNomAprobador { get => strNomAprobador; set => strNomAprobador = value; }
        public string StrNomRevisor { get => strNomRevisor; set => strNomRevisor = value; }
        public string StrDocRelacionados { get => strDocRelacionados; set => strDocRelacionados = value; }
        public DateTime DtFechaC { get => dtFechaC; set => dtFechaC = value; }
        public DateTime DtFechaRevision { get => dtFechaRevision; set => dtFechaRevision = value; }
        public DateTime DtFechaAprobacion { get => dtFechaAprobacion; set => dtFechaAprobacion = value; }
        public string StrClientes { get => strClientes; set => strClientes = value; }
        public string StrEquipos { get => strEquipos; set => strEquipos = value; }
        public string StrMedicamentos { get => strMedicamentos; set => strMedicamentos = value; }
        public string StrProveedores { get => strProveedores; set => strProveedores = value; }
        public string StrRecFin { get => strRecFin; set => strRecFin = value; }
        public string StrRecInfo { get => strRecInfo; set => strRecInfo = value; }
        public string StrTalentoHumano { get => strTalentoHumano; set => strTalentoHumano = value; }
        public int IntOidGDProceso { get => intOidGDProceso; set => intOidGDProceso = value; }
        public string StrFlujoGrama { get => strFlujoGrama; set => strFlujoGrama = value; }
        public string StrActividad { get => strActividad; set => strActividad = value; }
        public string StrIndicadores { get => strIndicadores; set => strIndicadores = value; }
        public string StrDocumentosRelacionados { get => strDocumentosRelacionados; set => strDocumentosRelacionados = value; }
        public int OidCPAgenda1 { get => OidCPAgenda; set => OidCPAgenda = value; }
    }
}
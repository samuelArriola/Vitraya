using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDManual
    {
        private int intOidGDManual,
            intOidGDProceso;

        private string strDesarrollo;
        private int intOidGDDocumento;
        private string strIntroduccion;
        private string strObjetivos;
        private string strMarcoLegal;
        private string strAlcance;
        private string strGlosario;
        private string strAnexos;
        private string strFormatos;
        private string strNombre;
        private string strProcs;
        private string strRecInfo;
        private string strMedicamentos;
        private string strEquipos;
        private string strTalentoHumano;
        private string strRecFin;
        private int strVersion;

        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntOidGDManual { get => intOidGDManual; set => intOidGDManual = value; }
        public string StrIntroduccion { get => strIntroduccion; set => strIntroduccion = value; }
        public string StrObjetivos { get => strObjetivos; set => strObjetivos = value; }
        public string StrMarcoLegal { get => strMarcoLegal; set => strMarcoLegal = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public string StrGlosario { get => strGlosario; set => strGlosario = value; }
        public string StrAnexos { get => strAnexos; set => strAnexos = value; }
        public string StrFormatos { get => strFormatos; set => strFormatos = value; }
        public string StrDesarrollo { get => strDesarrollo; set => strDesarrollo = value; }
        public int IntOidGDProceso { get => intOidGDProceso; set => intOidGDProceso = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrProcs { get => strProcs; set => strProcs = value; }
        public string StrRecInfo { get => strRecInfo; set => strRecInfo = value; }
        public string StrMedicamentos { get => strMedicamentos; set => strMedicamentos = value; }
        public string StrEquipos { get => strEquipos; set => strEquipos = value; }
        public string StrTalentoHumano { get => strTalentoHumano; set => strTalentoHumano = value; }
        public string StrRecFin { get => strRecFin; set => strRecFin = value; }
        public int StrVersion { get => strVersion; set => strVersion = value; }
    }
}
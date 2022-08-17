using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDDocIndicador
    {

        private int intOidAprovador,
                    intOidGDProceso;

        private string strNomAprovador;

        private DateTime dtmFecha;
        private int intOIdGDDocIndicador;
        private int intOidGDDocumento;
        private int intOidProceso;
        private int intOidRevisor;
        private string strNomDoc;
        private string strJustificacion;
        private string strCodSOGC;
        private string strDescNum;
        private string strOrInfoNum;
        private string strFuentPrimNum;
        private string strDescDen;
        private string strOrInfoDen;
        private string strFuentPrimDen;
        private string strUniMedicion;
        private string strFactor;
        private string strPeriodicidad;
        private string strResponsable;
        private string strFormulaCalc;
        private string strEstandar;
        private string strTendencia;
        private string strTipGrafica;
        private string strInterpretacion;
        private string strResponsableMed;
        private string strResponsableAna;
        private string strActores;
        private string strVigilancia;
        private string strNomRevisor;
        private string strTasa;
        private string strTipo;
        private string strDominio;
        private DateTime dtmFechaRevision;
        private DateTime dtmFechaAprovacion;

        public int IntOIdGDDocIndicador { get => intOIdGDDocIndicador; set => intOIdGDDocIndicador = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntOidProceso { get => intOidProceso; set => intOidProceso = value; }
        public int IntOidRevisor { get => intOidRevisor; set => intOidRevisor = value; }
        public int IntOidAprovador { get => intOidAprovador; set => intOidAprovador = value; }
        public string StrNomDoc { get => strNomDoc; set => strNomDoc = value; }
        public string StrJustificacion { get => strJustificacion; set => strJustificacion = value; }
        public string StrCodSOGC { get => strCodSOGC; set => strCodSOGC = value; }
        public string StrDescNum { get => strDescNum; set => strDescNum = value; }
        public string StrOrInfoNum { get => strOrInfoNum; set => strOrInfoNum = value; }
        public string StrFuentPrimNum { get => strFuentPrimNum; set => strFuentPrimNum = value; }
        public string StrDescDen { get => strDescDen; set => strDescDen = value; }
        public string StrOrInfoDen { get => strOrInfoDen; set => strOrInfoDen = value; }
        public string StrFuentPrimDen { get => strFuentPrimDen; set => strFuentPrimDen = value; }
        public string StrUniMedicion { get => strUniMedicion; set => strUniMedicion = value; }
        public string StrFactor { get => strFactor; set => strFactor = value; }
        public string StrPeriodicidad { get => strPeriodicidad; set => strPeriodicidad = value; }
        public string StrResponsable { get => strResponsable; set => strResponsable = value; }
        public string StrFormulaCalc { get => strFormulaCalc; set => strFormulaCalc = value; }
        public string StrEstandar { get => strEstandar; set => strEstandar = value; }
        public string StrTendencia { get => strTendencia; set => strTendencia = value; }
        public string StrTipGrafica { get => strTipGrafica; set => strTipGrafica = value; }
        public string StrInterpretacion { get => strInterpretacion; set => strInterpretacion = value; }
        public string StrResponsableMed { get => strResponsableMed; set => strResponsableMed = value; }
        public string StrResponsableAna { get => strResponsableAna; set => strResponsableAna = value; }
        public string StrActores { get => strActores; set => strActores = value; }
        public string StrVigilancia { get => strVigilancia; set => strVigilancia = value; }
        public string StrNomRevisor { get => strNomRevisor; set => strNomRevisor = value; }
        public string StrNomAprovador { get => strNomAprovador; set => strNomAprovador = value; }
        public DateTime DtmFechaRevision { get => dtmFechaRevision; set => dtmFechaRevision = value; }
        public DateTime DtmFechaAprovacion { get => dtmFechaAprovacion; set => dtmFechaAprovacion = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }
        public string StrTasa { get => strTasa; set => strTasa = value; }
        public string StrTipo { get => strTipo; set => strTipo = value; }
        public string StrDominio { get => strDominio; set => strDominio = value; }
        public int IntOidGDProceso { get => intOidGDProceso; set => intOidGDProceso = value; }
    }
}
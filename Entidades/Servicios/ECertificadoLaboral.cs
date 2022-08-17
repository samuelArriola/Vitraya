using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Servicios
{
    public class ECertificadoLaboral
    {

        string firmabase64, strEstado, strNombre, strIdentificacion, strCargo, strTipoContrato, strIdHistorico, strAccionHistorico, strAnio, strMes, strTotal;
        float floatSalario;
        DateTime dtFechaVinculacion, dtFechaRetiro, dtFechaHistorico;
        byte[] btFirma;

        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrIdentificacion { get => strIdentificacion; set => strIdentificacion = value; }
        public string StrCargo { get => strCargo; set => strCargo = value; }
        public string StrTipoContrato { get => strTipoContrato; set => strTipoContrato = value; }
        public float FloatSalario { get => floatSalario; set => floatSalario = value; }
        public DateTime DtFechaVinculacion { get => dtFechaVinculacion; set => dtFechaVinculacion = value; }
        public DateTime? DtFechaRetiro { get; set; }
        public DateTime DtFechaHistorico { get => dtFechaHistorico; set => dtFechaHistorico = value; }
        public string StrIdHistorico { get => strIdHistorico; set => strIdHistorico = value; }
        public string StrAccionHistorico { get => strAccionHistorico; set => strAccionHistorico = value; }
        public string StrAnio { get => strAnio; set => strAnio = value; }
        public string StrMes { get => strMes; set => strMes = value; }
        public string StrTotal { get => strTotal; set => strTotal = value; }
        public byte[] BtFirma { get => btFirma; set => btFirma = value; }
        public string Firmabase64 { get => firmabase64; set => firmabase64 = value; }
    }
}
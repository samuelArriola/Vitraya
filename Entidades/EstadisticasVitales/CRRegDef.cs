using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.EstadisticasVitales
{
    public class CRRegDef
    {
        private string strTipDef;
        private DateTime dateFecDef;
        private string strNomPac;
        private double doubleIdPaciente;
        private int intOIdCRCodRuaf;
        private double doubleGNCodUsu;
        private string strNomDoc;
        private string strServicio;
        private DateTime dateFecTimeStart;
        private bool blnEstdoPaciente;

        public string StrTipDef { get => strTipDef; set => strTipDef = value; }
        public DateTime DateFecDef { get => dateFecDef; set => dateFecDef = value; }
        public string StrNomPac { get => strNomPac; set => strNomPac = value; }
        public double DoubleIdPaciente { get => doubleIdPaciente; set => doubleIdPaciente = value; }
        public int IntOIdCRCodRuaf { get => intOIdCRCodRuaf; set => intOIdCRCodRuaf = value; }
        public double DoubleGNCodUsu { get => doubleGNCodUsu; set => doubleGNCodUsu = value; }
        public string StrNomDoc { get => strNomDoc; set => strNomDoc = value; }
        public string StrServicio { get => strServicio; set => strServicio = value; }
        public DateTime DateFecTimeStart { get => dateFecTimeStart; set => dateFecTimeStart = value; }
        public bool BlnEstdoPaciente { get => blnEstdoPaciente; set => blnEstdoPaciente = value; }
    }
}
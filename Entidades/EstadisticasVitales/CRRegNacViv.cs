using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.EstadisticasVitales
{
    public class CRRegNacViv
    {
        private double doubleIdMadre;
        private String strNomMadre;
        private String strTipNac;
        private DateTime dateFecNac;
        private int intCRCodRuaf;
        private int intEdGesNac;
        private double doubleGNCodUsu;
        private string strNomDoc;
        private int intPesoRn;
        private float floatTallaRN;
        private string strSexo;
        private DateTime dateFecTimeStart;

        public double DoubleIdMadre { get => doubleIdMadre; set => doubleIdMadre = value; }
        public string StrNomMadre { get => strNomMadre; set => strNomMadre = value; }
        public string StrTipNac { get => strTipNac; set => strTipNac = value; }
        public DateTime DateFecNac { get => dateFecNac; set => dateFecNac = value; }
        public int IntCRCodRuaf { get => intCRCodRuaf; set => intCRCodRuaf = value; }
        public int IntEdGesNac { get => intEdGesNac; set => intEdGesNac = value; }
        public double DoubleGNCodUsu { get => doubleGNCodUsu; set => doubleGNCodUsu = value; }
        public string StrNomDoc { get => strNomDoc; set => strNomDoc = value; }
        public int IntPesoRn { get => intPesoRn; set => intPesoRn = value; }
        public float FloatTallaRN { get => floatTallaRN; set => floatTallaRN = value; }
        public string StrSexo { get => strSexo; set => strSexo = value; }
        public DateTime DateFecTimeStart { get => dateFecTimeStart; set => dateFecTimeStart = value; }
    }
}
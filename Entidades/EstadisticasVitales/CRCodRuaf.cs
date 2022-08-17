using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.EstadisticasVitales 
{
    public class CRCodRuaf
    {
        private int intOIdCRCodRuaf;
        private DateTime dateFecCod;
        private string strIncidencia;
        private Boolean isEstado;
        private string strTipCodigo;
        private double doubleGNCodUsu;
        private double doubleCRcodRuaf;

        public int IntOIdCRCodRuaf { get => intOIdCRCodRuaf; set => intOIdCRCodRuaf = value; }
        public DateTime DateFecCod { get => dateFecCod; set => dateFecCod = value; }
        public string StrIncidencia { get => strIncidencia; set => strIncidencia = value; }
        public bool IsEstado { get => isEstado; set => isEstado = value; }
        public string StrTipCodigo { get => strTipCodigo; set => strTipCodigo = value; }
        public double DoubleGNCodUsu { get => doubleGNCodUsu; set => doubleGNCodUsu = value; }
        public double DoubleCRcodRuaf { get => doubleCRcodRuaf; set => doubleCRcodRuaf = value; }
    }
}
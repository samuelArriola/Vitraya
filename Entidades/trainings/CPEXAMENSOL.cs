using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPEXAMENSOL
    {
        private int intOidCPEXAMENSOL, intIDMATRICULA, intResultado, intOidPCEXAMEN;
        private List<CPRESPUESTA> respuestas;
        private DateTime dtmFecha;

        public int IntOidCPEXAMENSOL { get => intOidCPEXAMENSOL; set => intOidCPEXAMENSOL = value; }
        public int IntIDMATRICULA { get => intIDMATRICULA; set => intIDMATRICULA = value; }
        public int IntResultado { get => intResultado; set => intResultado = value; }
        public int IntOidPCEXAMEN { get => intOidPCEXAMEN; set => intOidPCEXAMEN = value; }
        public List<CPRESPUESTA> Respuestas { get => respuestas; set => respuestas = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }

        public string StrTitulo { get; set; }
    }
}
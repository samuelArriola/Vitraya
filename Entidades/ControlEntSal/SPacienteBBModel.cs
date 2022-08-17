using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ControlEntSal
{
    public class SPacienteBBModel
    {
        private int oid, adningres1, GnIdUsuSS, GnIdUsuSC, numbebe;
        private string DocPaciente, nomPaciente, docResponsable, eliminado, 
            nomResponsable, tpResponsable, nomBB, registroBB, estado1SS, estado2SC;
        private DateTime fechaSS, fechaSC;

        public int OID { get => oid; set => oid = value; }
        public int ADNINGRES1 { get => adningres1; set => adningres1 = value; }
        public int GNINUSUSS { get => GnIdUsuSS; set => GnIdUsuSS = value; }
        public int GNINUSUSC { get => GnIdUsuSC; set => GnIdUsuSC = value; }
        public int NUMBEBE { get => numbebe; set => numbebe = value; }
        public string DOCPACIENTE { get => DocPaciente; set => DocPaciente = value; }
        public string NOMPACIENTE { get => nomPaciente; set => nomPaciente = value; }
        public string DOCRESPONSABLE { get => docResponsable; set => docResponsable = value; }
        public string ELIMINADO { get => eliminado; set => eliminado = value; }
        public string NOMRESPONSABLE { get => nomResponsable; set => nomResponsable = value; }
        public string TPRESPONSABLE { get => tpResponsable; set => tpResponsable = value; }
        public string NOMBB { get => nomBB; set => nomBB = value; }
        public string REGISTROBB { get => registroBB; set => registroBB = value; }
        public string ESTADO1SS { get => estado1SS; set => estado1SS = value; }
        public string ESTADO2SC { get => estado2SC; set => estado2SC = value; }
        public DateTime FECHASS { get => fechaSS; set => fechaSS = value; }
        public DateTime FECHASC { get => fechaSC; set => fechaSC = value; }
    
    }
}
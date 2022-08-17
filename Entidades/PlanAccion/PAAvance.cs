using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class PAAvance
    {
        private int intOidPAPlanAccion, intOidListaArch, intOidPAAvance;

        private string strAvance, strTitulo;

        private DateTime dtmFecha;

        public int IntOidPAPlanAccion { get => intOidPAPlanAccion; set => intOidPAPlanAccion = value; }
        public int IntOidListaArch { get => intOidListaArch; set => intOidListaArch = value; }
        public string StrAvance { get => strAvance; set => strAvance = value; }
        public int IntOidPAAvance { get => intOidPAAvance; set => intOidPAAvance = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }
        public string StrTitulo { get => strTitulo; set => strTitulo = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPPREGUNTA
    {
        private int intOidCPPREGUNTA, intOidCPEXAMEN;
        private List<CPOPCION> opciones;
        private string strPregunta;

        public int IntOidCPPREGUNTA { get => intOidCPPREGUNTA; set => intOidCPPREGUNTA = value; }
        public int IntOidCPEXAMEN { get => intOidCPEXAMEN; set => intOidCPEXAMEN = value; }
        public string StrPregunta { get => strPregunta; set => strPregunta = value; }
        public List<CPOPCION> Opciones { get => opciones; set => opciones = value; }        
    }
}
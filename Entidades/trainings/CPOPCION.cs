using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPOPCION
    {
        private int     intOidOPCION, 
                        intOidCPPREGUNTA;

        private string  strOpcion;

        private bool    isCorrecta;

        public int IntOidOPCION { get => intOidOPCION; set => intOidOPCION = value; }
        public int IntOidCPPREGUNTA { get => intOidCPPREGUNTA; set => intOidCPPREGUNTA = value; }
        public string StrOpcion { get => strOpcion; set => strOpcion = value; }
        public bool IsCorrecta { get => isCorrecta; set => isCorrecta = value; }
    }

}
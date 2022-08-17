using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPRESPUESTA
    {
        private int intOidCPPREGUNTA, intOidCPOPCION, intOidCPEXAMENSOL, intOidCPRESPUESTA;

        public int IntOidCPPREGUNTA { get => intOidCPPREGUNTA; set => intOidCPPREGUNTA = value; }
        public int IntOidCPOPCION { get => intOidCPOPCION; set => intOidCPOPCION = value; }
        public int IntOidCPEXAMENSOL { get => intOidCPEXAMENSOL; set => intOidCPEXAMENSOL = value; }
        public int IntOidCPRESPUESTA { get => intOidCPRESPUESTA; set => intOidCPRESPUESTA = value; }
    }
}
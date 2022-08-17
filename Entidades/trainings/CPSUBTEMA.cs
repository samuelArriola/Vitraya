using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPSUBTEMA
    {
        private int intOidCPInstacia, intOidCPSUBTEMA, intContexto, intOidCPAgenda;
        private string strSUBTEMA;

        
        public int IntOidCPSUBTEMA { get => intOidCPSUBTEMA; set => intOidCPSUBTEMA = value; }
        public string StrSUBTEMA { get => strSUBTEMA; set => strSUBTEMA = value; }
        public int IntOidCPInstacia { get => intOidCPInstacia; set => intOidCPInstacia = value; }
        public int IntContexto { get => intContexto; set => intContexto = value; }
        public int IntOidCPAgenda { get => intOidCPAgenda; set => intOidCPAgenda = value; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ControlEntSal
{
    public class ControlEntSalModel
    {
        private string pacnumdoc;
        private string pacprinom;
        private string pacsegnom;
        private string pacpriape;
        private string pacsegape;
        private string pacedad;
        private string edad;
        private string ainconsec;
        private string ordenSal;
     
 
        public string PACNUMDOC { get => pacnumdoc; set => pacnumdoc = value; }
        public string PACPRINOM { get => pacprinom; set => pacprinom = value; }
        public string PACSEGNOM { get => pacsegnom; set => pacsegnom = value; }
        public string PACPRIAPE { get => pacpriape; set => pacpriape = value; }
        public string PACSEGAPE { get => pacsegape; set => pacsegape = value; }
        public string PACEDAD { get => pacedad; set => pacedad = value; }
        public string EDAD { get => edad; set => edad = value; }
        public string AINCONSEC { get => ainconsec; set => ainconsec = value; }

        public string ORDENSALIDA { get => ordenSal; set => ordenSal = value; }

    }
}
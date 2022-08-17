using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Vacunacion
{
    public class VCEntradaLote
    {
        public int IntOidVCEntradaLote { get; set; }
        public int IntOidVCDetEntLot { get; set; }
        public int IntOidVCLote { get; set; }
        public float FltCantidad { get; set; }
    }
}
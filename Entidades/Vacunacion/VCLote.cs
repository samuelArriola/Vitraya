using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Vacunacion
{
    public class VCLote
    {
     
        public int IntOidVCLote   {set; get;}
        public int IntExistencias  {set; get;}
        public int IntOidVCInsumo {set; get;}
        public int IntTotalIngresado { set; get;}
        public string StrNumLote     {set; get;}
        public string StrDiluyente { get; set; }
        
        public string StrNombreInsumo { get; set; }
    }
}
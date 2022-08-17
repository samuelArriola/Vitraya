using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNHistorico
    {
		public int intGNCodUsu { get; set; }
		public int intInstancia { get; set; }
		public int intOidGNHistorico { get; set; }
		public string strAccion { get; set; }
		public string strEntidad { get; set; }
		public string strDetalle { get; set; }
		public DateTime dtmFecha { get; set; }
	}
}
using Persistencia.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.EstadisticasVitales
{
    public partial class InformeRepetidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<dynamic> GetDocsRepetidosNV(string codigo, string docMadre, string nomMadre, string nomDoctor)
        {
            return DAOCRCodRuaf.GetDocsRepetidosNV(codigo, docMadre, nomMadre, nomDoctor);
        }
    }
}
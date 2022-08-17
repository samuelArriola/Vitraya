using Entidades.Generales;
using Entidades.Power_BI;
using Persistencia.Generales;
using Persistencia.Power_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Power_BI
{
    public partial class ListaReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ELIstaReportes> getListaReportes()
        {
            return PListaReportes.GetReportes();
        }

        [WebMethod]
        public static List<ELIstaReportes> filtroNombreR(string nombreReporte)
        {
            return PListaReportes.FiltroNombreR(nombreReporte);
        }
    }
}
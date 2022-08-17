
using Entidades.EncuestaCovid;
using Persistencia.EncuestaCovid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.EncuestaCovid
{
    public partial class ReportesCovid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<EEncuestaCovid> getReporte()
        {
            return PEncuestaCovid.GetReporte();
        }

        [WebMethod]
        public static List<EEncuestaCovid> getFiltro(string fechaI, string fechaF)
        {
            return PEncuestaCovid.GetFiltroReporte(fechaI, fechaF);
        }
    }
}
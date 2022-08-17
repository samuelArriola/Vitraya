using Entidades.Facturacion;
using Persistencia.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Facturacion
{
    public partial class HistoricoCierres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<EHistoricoCierres> GetInfoHistorico()
        {
            return PHistoricoCierres.getInfoHistorico();
        }

        [WebMethod]
        public static List<EHistoricoCierres> Getfiltro1(string numeroIngreso, string nombreCierre)
        {
            return PHistoricoCierres.getfiltro1(numeroIngreso, nombreCierre);
        }

        [WebMethod]
        public static List<EHistoricoCierres> Getfiltro2(string fechaI, string fechaF)
        {
            return PHistoricoCierres.getfiltro2(fechaI, fechaF);
        }

    }
}
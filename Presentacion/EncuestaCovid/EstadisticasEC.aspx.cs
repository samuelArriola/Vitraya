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
    public partial class EstadisticasEC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<EEstadisticasEC> ObtenerMeses(string mesI, string mesF)
        {

            return LEEstadisticasEC.GetEstadisticasMes(mesI, mesF);
        }

        [WebMethod]
        public static List<EEstadisticasEC> ObtenerDias(string diaI, string diaF)
        {

            return LEEstadisticasEC.GetEstadisticasDias(diaI, diaF);
        }

        [WebMethod]
        public static List<EEstadisticasEC> ObtenerInfoDetalle(string diaI, string diaF)
        {

            return LEEstadisticasEC.GetInfoDetalle(diaI, diaF);
        }
    }
}
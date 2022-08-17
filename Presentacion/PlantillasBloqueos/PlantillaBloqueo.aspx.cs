using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Generales;
using Persistencia.Generales;

namespace Presentacion.PlantillasBloqueos
{
    public partial class PlantillaCapacitaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ScriptBloqueo> ObtenerPendientes()
        {
            return DAONABloqueos.GetBloqueos();
        }


    }
}
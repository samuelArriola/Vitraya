using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Servicios;
using Persistencia.Servicios;
using Entidades.Generales;
using Persistencia.Generales;

namespace Presentacion.Servicios
{
    public partial class desprendible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<EDesprendibles> ObtenerListaDesprendibles(DateTime fechaI, DateTime fechaF)
        {
            return PDesprendibles.GetListaDesprendibles(fechaI, fechaF);
        }

        [WebMethod]
        public static List<EDesprendibles> ObtenerInfoDesprendible(string fecha)
        {
            return PDesprendibles.GetDesprendibleByFecha(fecha);
        }

        [WebMethod]
        public static List<EDesprendibles> ObtenerValidacionOpcion()
        {
            return PDesprendibles.GetVisiOpcion();
        }

        [WebMethod]
        public static void ActivarOpcion()
        {
            PDesprendibles.SetActivacion();
        }

        [WebMethod]
        public static void DesactivarOpcion()
        {
            PDesprendibles.SetInactivacion();
        }

        [WebMethod]
        public static GNPermisos GetPermisos(string linkOpcion)
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            int IdRol = usuario.CodigoR;

            return DAOGNPermisos.GetPermiso(IdRol, linkOpcion);
        }

    }
}
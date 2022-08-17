using Entidades.Facturacion;
using Entidades.Generales;
using Persistencia.Facturacion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Facturacion
{
    public partial class CensoDiario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static GNPermisos GetPermisos(string linkOpcion)
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            int IdRol = usuario.CodigoR;

            return DAOGNPermisos.GetPermiso(IdRol, linkOpcion);
        }

        [WebMethod]
        public static List<ECensoDiario> getInfoCenso()
        {
            return PCensoDiario.GetInfoCensoDiario();
        }

        [WebMethod]
        public static List<ECensoDiario> GetCensoDetalles(string id)
        {
            return PCensoDiario.GetInfoDetaCenso(id);
        }

        [WebMethod]
        public static List<ECensoDiario> GetfiltroGrupo(string grupo, string subgrupo)
        {
            return PCensoDiario.getfiltroGrupo(grupo, subgrupo);
        }

        [WebMethod]
        public static List<ECensoDiario> GetfiltroNumId(string numID)
        {
            return PCensoDiario.getfiltroNumId(numID);
        }

        [WebMethod]
        public static List<ECensoDiario> GetfiltroNombre(string nombrePaciente)
        {
            return PCensoDiario.getfiltroNombre(nombrePaciente);
        }

        [WebMethod]
        public static List<ECensoDiario> GetfiltroFecha(string filtroFecha)
        {
            return PCensoDiario.getfiltroFecha(filtroFecha);
        }

        [WebMethod]
        public static List<ECensoDiario> GetfiltroIngreso(string numeroIngreso)
        {
            return PCensoDiario.getfiltroIngreso(numeroIngreso);
        }

        [WebMethod]
        public static void SetCierrePaciente(string motivoCierre, string Admision)
        {

            PCensoDiario.setCierrePaciente(motivoCierre, Admision);

        }
    }
}
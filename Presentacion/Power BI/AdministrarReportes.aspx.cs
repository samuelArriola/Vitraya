using Entidades.Power_BI;
using Persistencia.Power_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Power_BI.JS
{
    public partial class AdministrarReprotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ELIstaReportes> getListaReportes()
        {
            return PAdministrarReportes.GetReportes();
        }

        [WebMethod]
        public static List<ELIstaReportes> getDetalleReportes(int idReporte)
        {
            return PAdministrarReportes.GetDetallesReportes(idReporte);
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuarios()
        {
            return PAdministrarReportes.GetUsuarios();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getCargos()
        {
            return PAdministrarReportes.GetCargos();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUnidadesFuncionales()
        {
            return PAdministrarReportes.GetUnidadesFuncionales();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuariosPorCargo(int idCargo)
        {
            return PAdministrarReportes.GetUsuariosPorCargo(idCargo);
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuariosPorUF(int idUnidadF)
        {
            return PAdministrarReportes.GetUsuariosPorUnidadF(idUnidadF);
        }

        [WebMethod]
        public static List<ELIstaReportes> setReporte(string nombre, int estado, string descripcion, string tipo, string enlace)
        {
            return PAdministrarReportes.SetReporte(nombre, estado, descripcion, tipo, enlace);
        }

        [WebMethod]
        public static void InsertarPermisos(int idR, string identificacionU, string nombreU)
        {
            PAdministrarReportes.SetPermisosUsuarios(idR, identificacionU, nombreU);
        }

        [WebMethod]
        public static void updateReporte(int codigo, string nombre, int estado, string descripcion, string tipo, string enlace)
        {
            PAdministrarReportes.UpdateReporte(codigo, nombre, estado, descripcion, tipo, enlace);
        }

        [WebMethod]
        public static void deletePermisos(int codigo)
        {
            PAdministrarReportes.DeletePermisos(codigo);
        }

        [WebMethod]
        public static List<EAdministrarReportes> getPermisosBD(int idReporte)
        {
            return PAdministrarReportes.GetPermisosBD(idReporte);
        }

        [WebMethod]
        public static List<ELIstaReportes> filtroNombreR(string nombreReporte)
        {
            return PAdministrarReportes.FiltroNombreR(nombreReporte);
        }
    }
}
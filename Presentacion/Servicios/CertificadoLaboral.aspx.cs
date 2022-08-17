using Entidades.Generales;
using Entidades.Servicios;
using Persistencia.Generales;
using Persistencia.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Servicios
{
    public partial class certificadoLaboral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidacionPermisos();
        }

        [WebMethod]
        public static List<ECertificadoLaboral> ObtenerInformacion()
        {
            return PCertificadoLaboral.GetDatosCertificado();
        }

        [WebMethod]
        public static List<ECertificadoLaboral> ObtenerInformacionEmpleado(string identificacion)
        {
            return PCertificadoLaboral.GetDatosCertificadoById(identificacion);
        }

        [WebMethod]
        public static List<ECertificadoLaboral> getCoincidencias()
        {
            return PCertificadoLaboral.GetEmpleados();
        }

        [WebMethod]
        public static List<ECertificadoLaboral> getHistorico()
        {
            return PCertificadoLaboral.GetHistorico();
        }

        [WebMethod]
        public static void setHistorico(string accion)
        {
            PCertificadoLaboral.SetHistorico(accion);
        }

        [WebMethod]
        public static List<ECertificadoLaboral> filtro1(string fecha1, string fecha2)
        {
            return PCertificadoLaboral.GetHistoricoFiltro1(fecha1, fecha2);
        }

        [WebMethod]
        public static List<ECertificadoLaboral> filtro2(string fecha1, string fecha2)
        {
            return PCertificadoLaboral.GetHistoricoFiltro2(fecha1, fecha2);
        }

        [WebMethod]
        public static List<ECertificadoLaboral> getDatosFirma()
        {
            return PCertificadoLaboral.getDatosFirma();
        }

        [WebMethod]
        public static List<ECertificadoLaboral> getInfoUsuFirma(string identificacion)
        {
            return PCertificadoLaboral.GetInfoUsuFirma(identificacion);
        }

        [WebMethod]
        public static void ActualizarFirma(string identificacion, string nombre, byte[] firma, string cargo)
        {
            PCertificadoLaboral.UpdateUsuFirma(identificacion, nombre, firma, cargo);
        }

        public void ValidacionPermisos()
        {
            Usuario userLogedIn = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));
            string link = Request.Url.AbsolutePath;

            GNPermisos permisos = DAOGNPermisos.GetPermiso(userLogedIn.CodigoR, link);

            if ( permisos.BlnConfirmar && permisos.BlnCrear && permisos.BlnEliminar && permisos.BlnModificar )
            {
                panelEmpleado.Visible = false;
                panelAdministrador1.Visible = true;
                panelAdministrador2.Visible = true;


            }
            else if (permisos.BlnConfirmar || permisos.BlnCrear || permisos.BlnEliminar || permisos.BlnModificar )
            {
                panelAdministrador1.Visible = false;
                panelAdministrador2.Visible = false;
                panelEmpleado.Visible = true;
                
            }
        }
    }
}
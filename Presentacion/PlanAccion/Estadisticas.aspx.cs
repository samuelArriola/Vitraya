using Entidades.Generales;
using Entidades.Power_BI;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.Power_BI;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlProcesos();
                CargarDdlUsuarios();
            }
        }

        public void CargarDdlProcesos()
        {
            ddlProcesos.Items.Clear();

            ddlProcesos.Items.Add(new ListItem("Todos", ""));

            List<PCProceso> procesos = DAOProceso.listar();
            procesos.ForEach(proceso =>
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro));
            });
        }

        public void CargarDdlUsuarios()
        {
            ddlUsuarios.Items.Clear();
            ddlUsuarios.Items.Add(new ListItem("Todos", ""));

            //List<Usuario> usuarios = DAOUsuario.getUsuarios();
            List<EAdministrarReportes> usuarios = PAdministrarReportes.GetUsuarios();

            usuarios.ForEach(usuario =>
            {
                //ddlUsuarios.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1 + ""));
                ddlUsuarios.Items.Add(new ListItem(usuario.NombrePermisoUsu, usuario.CodigoPermisoUsu + ""));
            });
        }


        [WebMethod]
        public static List<dynamic> GetEstadisticas(string idUsuario, string proceso, DateTime fecha1, DateTime fecha2)
        {
            List<dynamic> datos = DAOPAPlanAccion.GetEstadisticas(idUsuario, proceso, fecha1, fecha2);
            string sDatos = JsonConvert.SerializeObject(datos);
            return datos;
        }
    }
}
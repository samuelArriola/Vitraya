using Entidades.PlanAccion;
using Entidades.Procesos;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class MatrisPlanesAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlProcesos();
            }
        }

        public void CargarDdlProcesos()
        {

            List<PCProceso> procesos = DAOProceso.listar();
            ddlProcesos.Items.Clear();
            ddlProcesos.Items.Add(new ListItem("Todos", ""));
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro));
            }
        }


        [WebMethod]
        public static List<PAPlanAccion> GetPlanesAccion(string nomUsuResp, string nomUsuSeg, string contexto, DateTime fecha1, DateTime fecha2, string proceso, string estado)
        {
            List<PAPlanAccion> planes = DAOPAPlanAccion.GetPlanesAccion(nomUsuResp, nomUsuSeg, contexto, fecha1, fecha2, proceso, estado);
            return planes;
        }

        [WebMethod]
        public static bool EliminarPlanAccion(int idPlanAccion)
        {
            return DAOPAPlanAccion.delete(idPlanAccion);
        }
    }
}
using Entidades.Generales;
using Entidades.PlanAccion;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class EditarPlanAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDrops();
                CargarPlanAccion();
            }
        }

        public void CargarDrops()
        {
            //se obtiene una lista de los usuarios que estan registrados
            List<Usuario> participantes = DAOUsuario.getUsuarios();
            foreach (var participante in participantes)
            {
                //se pasan los datos de la lista de usuarios a los drop correspondientes
                ddlReposnsableSeguimiento.Items.Add(new ListItem(participante.GNNomUsu1, participante.GNCodUsu1.ToString()));
                ddlResponsableActividad.Items.Add(new ListItem(participante.GNNomUsu1, participante.GNCodUsu1.ToString()));
            }

            //se obtiene un a lista de los procesos que han sido creados
            List<PCProceso> procesos = DAOProceso.listar();
            foreach (var proceso in procesos)
            {

                //se pas la informacion de la lista de procesos al drop de los procesos
                ddlProceso.Items.Add(new ListItem(proceso.StrNomPro));
            }
        }

        /// <summary>
        /// Metodo que caga la informacion del plan de accion a editar en el formlario de edicion 
        /// </summary>
        private void CargarPlanAccion()
        {
            int idPlanAccion = Convert.ToInt32(Request["idPlanAccion"]);
            var planAccion = DAOPAPlanAccion.Get(idPlanAccion);

            txtDescriptcion.Text = planAccion.StrDescriptcion;
            txtTitulo.Text = planAccion.StrOrigen;
            ddlFuente.Text = planAccion.StrFuente;
            taActividad.Text = planAccion.StrActividad;
            taComo.Text = planAccion.StrComo;
            taMotivo.Text = planAccion.StrPorQue;
            ddlResponsableActividad.Text = planAccion.IntGNCodUsu + "";
            txtFechaLimiteCompromiso.Text = planAccion.DtmFecIniActa.ToString("yyyy-MM-dd");
            txtLugar.Text = planAccion.StrDonde;
            taCosto.Text = planAccion.StrCuanto;
            taSoporteActividad.Text = planAccion.StrSoporte;
            ddlReposnsableSeguimiento.Text = planAccion.IntcodUsuSegui + "";
            ddlProceso.Text = planAccion.StrProceso;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //se verifica que todos los datos para crear el compromiso esten completos
            if (string.IsNullOrEmpty(taActividad.Text) || string.IsNullOrEmpty(taSoporteActividad.Text)
                || string.IsNullOrEmpty(txtFechaLimiteCompromiso.Text) || ddlProceso.Text == "-1" || ddlReposnsableSeguimiento.Text == "-1"
                || ddlResponsableActividad.Text == "-1"
                || string.IsNullOrEmpty(taMotivo.Text) || string.IsNullOrEmpty(taCosto.Text)
                || string.IsNullOrEmpty(taComo.Text) || string.IsNullOrEmpty(txtLugar.Text))
            {
                //se muetra un mensaje de error en caso de los datos estetn incompetos
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "error(\"Datos Incompletos\",\"Por favor complete todos los datos ante de crear el compromiso\")", true);
                return;
            }

            //Se consulta el plan de acción a editar para presebar los datos que no se van a modicar 
            var planAccionAux = DAOPAPlanAccion.Get(Convert.ToInt32(Request["idPlanAccion"]));

            //se crea una instancia del plan de accion donde se van a guardar los datos del plan de accion a editar
            var planAccion = new PAPlanAccion
            {
                DtmFecFinalActa = Convert.ToDateTime(txtFechaLimiteCompromiso.Text),
                DtmFecIniActa = planAccionAux.DtmFecIniActa,
                IntCodUsuApr = planAccionAux.IntCodUsuApr,
                IntcodUsuSegui = Convert.ToInt32(ddlReposnsableSeguimiento.SelectedItem.Value),
                IntContexto = planAccionAux.IntContexto,
                IntEstAct = planAccionAux.IntEstAct,
                IntGNCodUsu = Convert.ToInt32(ddlResponsableActividad.SelectedItem.Value),
                IntOidARActasC = planAccionAux.IntOidARActasC,
                IntOidInstancia = planAccionAux.IntOidInstancia,
                IntOidPAPlanAccion = planAccionAux.IntOidPAPlanAccion,
                StrActividad = taActividad.Text,
                StrComo = taComo.Text,
                StrCuanto = taCosto.Text,
                StrDescriptcion = txtDescriptcion.Text,
                StrDonde = txtLugar.Text,
                StrFuente = ddlFuente.Text == "-1" ? "" : ddlFuente.Text,
                StrNomEst = "Asignado",
                StrSoporte = taSoporteActividad.Text,
                StrOrigen = planAccionAux.StrOrigen,
                StrPorQue = taMotivo.Text,
                StrProceso = ddlProceso.Text
            };

            // se actualiza el plan de accion en la base de datos
            DAOPAPlanAccion.UpdateCompromiso(planAccion);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = planAccion.IntOidPAPlanAccion,
                strAccion = "Modificar",
                strDetalle = "Se actaliza la información en general del plan de acción desde la opcion de editar plan de acción del mismo modulo",
                strEntidad = "PAPlanAccion"
            });

            //se envia una notificacion indicando que el plan de accion se ha actulizado
            ScriptManager.RegisterStartupScript(this, this.GetType(), "planActualizado", @"exito(""Plan de acción actulizado"",""El Plan de acción se ha actualizado correctamente"")", true);

            //se redirecciona el a la matriz de los planes de accion después de unos segundos 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redireccion", @"setInterval(()=>{window.location.href=""MatrisPlanesAccion.aspx""}, 3000)", true);
        }
    }
}
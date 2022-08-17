using Entidades.Auditorias;
using Entidades.Generales;
using Entidades.PlanAccion;
using Entidades.Procesos;
using Persistencia.Auditorias;
using Persistencia.Generales;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Auditorias
{
    public partial class CargarPlanAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaPlanes();
                ValidarBotones();
                CargarDrops();
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
                ddlProceso.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso.ToString()));
            }
        }

        //Metodo que linpida todos los datos que se encuentran los controles del fromulario
        public void LimpiarDatos()
        {
            taActividad.Text = "";
            taSoporteActividad.Text = "";
            ddlProceso.Text = "-1";
            ddlReposnsableSeguimiento.Text = "-1";
            ddlResponsableActividad.Text = "-1";
            txtFechaLimiteCompromiso.Text = "";
            taComo.Text = "";
            taCosto.Text = "";
            taMotivo.Text = "";
            txtLugar.Text = "";
        }


        protected void btnGuardarCompromiso_Click(object sender, EventArgs e)
        {
            //se verifica que todos los datos para crear el compromiso esten completos
            if (string.IsNullOrEmpty(taActividad.Text) || string.IsNullOrEmpty(taSoporteActividad.Text)
                || string.IsNullOrEmpty(txtFechaLimiteCompromiso.Text) || ddlProceso.Text == "-1" || ddlReposnsableSeguimiento.Text == "-1"
                || ddlResponsableActividad.Text == "-1"
                || string.IsNullOrEmpty(taMotivo.Text) || string.IsNullOrEmpty(taCosto.Text)
                || string.IsNullOrEmpty(taComo.Text) || string.IsNullOrEmpty(txtLugar.Text)
                )
            {
                //se muetra un mensaje de error en caso de los datos estetn incompetos
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "error(\"Datos Incompletos\",\"Por favor complete todos los datos ante de crear el compromiso\")", true);
                return;
            }


            int indexHallazgo = Convert.ToInt32(Request["index"]);
            int idAuditoria = Convert.ToInt32(Request["idAuditoria"]);
            int ContextoPLan = Convert.ToInt32(Request["ContextoPlan"]);
            int ContextoAuditoria = Convert.ToInt32(Request["ContextoAuditoria"]);

            AuditoriaExterna auditoria = DAOAuditoriaExterna.GetAuditoria(idAuditoria);

            List<Hallazgo> hallazgos = DAOHallazgo.GetHallazgosByidAuditoria(idAuditoria, ContextoAuditoria);

            //se crea un plan de accion con los datos suplidos por el usuario
            PAPlanAccion plan = new PAPlanAccion
            {
                StrFuente = "No Conformidad",
                IntContexto = ContextoPLan,
                DtmFecFinalActa = Convert.ToDateTime(txtFechaLimiteCompromiso.Text),
                DtmFecIniActa = DateTime.Now,
                IntCodUsuApr = Convert.ToInt32(Request["Admin"]),
                IntcodUsuSegui = Convert.ToInt32(ddlReposnsableSeguimiento.Text),
                IntEstAct = 1,
                IntGNCodUsu = Convert.ToInt32(ddlResponsableActividad.Text),
                IntOidARActasC = 0,
                IntOidInstancia = hallazgos[indexHallazgo].IntOidHallazgo,
                StrActividad = taActividad.Text,
                StrComo = taComo.Text,
                StrCuanto = taCosto.Text,
                StrDonde = txtLugar.Text,
                StrNomEst = "",
                StrPorQue = taMotivo.Text,
                StrProceso = ddlProceso.SelectedItem.Text,
                StrSoporte = taSoporteActividad.Text,
                StrNombreUsuarioResponsable = ddlResponsableActividad.SelectedItem.Text,
                StrOrigen = $"Auditoria externa realizada por: {auditoria.StrEnteAuditor}, el día {DateTime.Now.ToString("D")}",
                StrDescriptcion = hallazgos[indexHallazgo].StrDescripcion,
            };

            DAOPAPlanAccion.set(plan);

            //se rescata el el plan de accion recien creado para verificar su id y tulizarlos para cada uno de los usuarios
            //que intervienen en el proceso
            plan = DAOPAPlanAccion.GetPlanAccionUlt();

            //se asigna el usuario creador del plan de accion y se guarda en la base de datos
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));
            PAUsuario usuarioAsignaodor = new PAUsuario
            {
                IntOidGNUsuario = usuario.GNCodUsu1,
                IntOidPAPlanAccion = plan.IntOidPAPlanAccion,
                StrCargo = usuario.GnCargo1,
                StrNombre = usuario.GNNomUsu1,
                StrRol = PAUsuario.ASIGNADOR + "",
            };
            DAOPAUsuario.SetPAUSuario(usuarioAsignaodor);


            //se asigna el usuario responsable de la actividad y se guarda en la base de datos 
            usuario = DAOUsuario.getInstance().GetUsuario(plan.IntGNCodUsu);

            PAUsuario usuarioResponsable = new PAUsuario
            {
                IntOidGNUsuario = usuario.GNCodUsu1,
                IntOidPAPlanAccion = plan.IntOidPAPlanAccion,
                StrCargo = usuario.GnCargo1,
                StrNombre = usuario.GNNomUsu1,
                StrRol = PAUsuario.RESPONSABLE + "",
            };

            DAOPAUsuario.SetPAUSuario(usuarioResponsable);

            //se asigna el usuario responsable del seguimiento y se guarda en la base de datos
            usuario = DAOUsuario.getInstance().GetUsuario(plan.IntcodUsuSegui);

            PAUsuario usuarioSeguimiento = new PAUsuario
            {
                IntOidGNUsuario = usuario.GNCodUsu1,
                IntOidPAPlanAccion = plan.IntOidPAPlanAccion,
                StrCargo = usuario.GnCargo1,
                StrNombre = usuario.GNNomUsu1,
                StrRol = PAUsuario.SEGUIMIENTO + "",
            };
            DAOPAUsuario.SetPAUSuario(usuarioSeguimiento);
            CargarTablaPlanes();
            LimpiarDatos();

        }

        //metodo que cara la lista de los planes de accion en la table de los planes de accion
        public void CargarTablaPlanes()
        {

            int indexHallazgo = Convert.ToInt32(Request["index"]);
            int idAuditoria = Convert.ToInt32(Request["idAuditoria"]);
            int ContextoPLan = Convert.ToInt32(Request["ContextoPlan"]);
            int ContextoAuditoria = Convert.ToInt32(Request["ContextoAuditoria"]);


            List<Hallazgo> hallazgos = DAOHallazgo.GetHallazgosByidAuditoria(idAuditoria, ContextoAuditoria);

            //se consulta la lista de los planes de a traveés del contexto y el id del hallazgo
            List<PAPlanAccion> planes = DAOPAPlanAccion.GetPlanAccionByContexto(hallazgos[indexHallazgo].IntOidHallazgo, ContextoPLan);

            //se pasan los datos de la lista de los plenes de accion a la tabla de los planes de accion
            tbCompromisos.DataSource = planes;
            tbCompromisos.DataBind();
        }

        protected void tbCompromisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ValidarBotones()
        {
            int indexHallazgo = Convert.ToInt32(Request["index"]);
            int idAuditoria = Convert.ToInt32(Request["idAuditoria"]);
            int ContextoAuditoria = Convert.ToInt32(Request["ContextoAuditoria"]);

            List<Hallazgo> hallazgos = DAOHallazgo.GetHallazgosByidAuditoria(idAuditoria, ContextoAuditoria);

            if (indexHallazgo == 0)
            {
                btnAnt.Visible = false;
            }
            if (indexHallazgo >= hallazgos.Count - 1)
            {
                btnSig.Visible = false;
                btnFinalizar.Visible = true;
            }

            txtHallazgo.InnerText = hallazgos[indexHallazgo].StrDescripcion;
        }

        protected void btnAnt_Click(object sender, EventArgs e)
        {
            int indexHallazgo = Convert.ToInt32(Request["index"]);
            int idAuditoria = Convert.ToInt32(Request["idAuditoria"]);
            int ContextoPLan = Convert.ToInt32(Request["ContextoPlan"]);
            int ContextoAuditoria = Convert.ToInt32(Request["ContextoAuditoria"]);
            Response.Redirect($"CargarPlanAccion.aspx?idAuditoria={idAuditoria}&index={--indexHallazgo}&ContextoPLan={ContextoPLan}&ContextoAuditoria={ContextoAuditoria}");
        }

        protected void btnSig_Click(object sender, EventArgs e)
        {
            int indexHallazgo = Convert.ToInt32(Request["index"]);
            int idAuditoria = Convert.ToInt32(Request["idAuditoria"]);
            int ContextoPLan = Convert.ToInt32(Request["ContextoPlan"]);
            int ContextoAuditoria = Convert.ToInt32(Request["ContextoAuditoria"]);
            Response.Redirect($"CargarPlanAccion.aspx?idAuditoria={idAuditoria}&index={++indexHallazgo}&ContextoPLan={ContextoPLan}&ContextoAuditoria={ContextoAuditoria}");
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"VerAuditorias");
        }
    }
}
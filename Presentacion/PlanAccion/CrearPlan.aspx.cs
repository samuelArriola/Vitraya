using Entidades.Generales;
using Entidades.PlanAccion;
using Entidades.Power_BI;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.Power_BI;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class CrearPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDrops();
            CargarPlanes();
        }


        public void CargarDrops()
        {
            //se obtiene una lista de los usuarios que estan registrados
            //List<Usuario> participantes = DAOUsuario.getUsuarios();
            List<EAdministrarReportes> participantes = PAdministrarReportes.GetUsuarios();

            foreach (var participante in participantes)
            {
                //se pasan los datos de la lista de usuarios a los drop correspondientes
                ddlReposnsableSeguimiento.Items.Add(new ListItem(participante.NombrePermisoUsu, participante.CodigoPermisoUsu.ToString()));
                ddlResponsableActividad.Items.Add(new ListItem(participante.NombrePermisoUsu, participante.CodigoPermisoUsu.ToString()));
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
            txtDescriptcion.Text = "";
            ddlFuente.Text = "-1";
        }


        protected void btnGuardarCompromiso_Click(object sender, EventArgs e)
        {
            //se verifica que todos los datos para crear el compromiso esten completos
            if (string.IsNullOrEmpty(taActividad.Text) || string.IsNullOrEmpty(taSoporteActividad.Text)
                || string.IsNullOrEmpty(txtFechaLimiteCompromiso.Text) || ddlProceso.Text == "-1" || ddlReposnsableSeguimiento.Text == "-1"
                || ddlResponsableActividad.Text == "-1"
                || string.IsNullOrEmpty(taMotivo.Text) || string.IsNullOrEmpty(taCosto.Text)
                || string.IsNullOrEmpty(taComo.Text) || string.IsNullOrEmpty(txtLugar.Text)
                || ddlFuente.Text == "-1" || string.IsNullOrEmpty(txtDescriptcion.Text))
            {
                //se muetra un mensaje de error en caso de los datos estetn incompetos
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "error(\"Datos Incompletos\",\"Por favor complete todos los datos ante de crear el compromiso\")", true);
                return;
            }


            //se crea un plan de accion con los datos suplidos por el usuario
            PAPlanAccion planAccion = new PAPlanAccion
            {
                StrFuente = ddlFuente.Text,
                StrDescriptcion = txtDescriptcion.Text,
                IntContexto = PAPlanAccion.CONTEXTO.PLANACCION,
                DtmFecFinalActa = Convert.ToDateTime(txtFechaLimiteCompromiso.Text),
                DtmFecIniActa = DateTime.Now,
                IntCodUsuApr = Convert.ToInt32(Session["Admin"]),
                IntcodUsuSegui = Convert.ToInt32(ddlReposnsableSeguimiento.Text),
                IntEstAct = 1,
                IntGNCodUsu = Convert.ToInt32(ddlResponsableActividad.Text),
                IntOidARActasC = 0,
                IntOidInstancia = 0,
                StrActividad = taActividad.Text,
                StrComo = taComo.Text,
                StrCuanto = taCosto.Text,
                StrDonde = txtLugar.Text,
                StrNomEst = "",
                StrPorQue = taMotivo.Text,
                StrProceso = ddlProceso.SelectedItem.Text,
                StrSoporte = taSoporteActividad.Text,
                StrNombreUsuarioResponsable = ddlResponsableActividad.SelectedItem.Text,
                StrOrigen = txtTitulo.Text,
            };


            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));


            //se consulta el listado de los planes de accion guardados previamente el la variable de session
            List<PAPlanAccion> planes = (List<PAPlanAccion>)Session["planes"];

            //en caso de que no exita un listado de los planes de accion en la variable de session se crea un nuevo listado
            if (planes == null)
                planes = new List<PAPlanAccion>();
            //se agrega el plan de accion recien creado a la lista
            planes.Add(planAccion);

            //se actuliza la lista de los planes de accion
            Session["planes"] = planes;
            CargarPlanes();
            LimpiarDatos();
        }

        //metodo que cara la lista de los planes de accion en la table de los planes de accion
        public void CargarPlanes()
        {
            //se consulta la lista de los planes de accion que se encuentra en la variable de session
            List<PAPlanAccion> planes = (List<PAPlanAccion>)Session["planes"];
            if (planes == null)
                planes = new List<PAPlanAccion>();

            //se pasan los datos de la lista de los plenes de accion a la tabla de los planes de accion
            tbCompromisos.DataSource = planes;
            tbCompromisos.DataBind();
        }

        protected void tbCompromisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton boton = (LinkButton)e.CommandSource;
            GridViewRow items = (GridViewRow)boton.NamingContainer;
            int index = items.RowIndex;
            if (e.CommandName == "eliminar")
            {
                List<PAPlanAccion> planes = (List<PAPlanAccion>)Session["planes"];
                planes.RemoveAt(index);

                Session["planes"] = planes;

                tbCompromisos.DataSource = planes;
                tbCompromisos.DataBind();
            }
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<PAPlanAccion> planes = (List<PAPlanAccion>)Session["planes"];
            if (planes == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MesajeError", "error(\"No hay Planes de Acción para guardar\",\"No se ha creado ningún Plan de Acción," +
                    " primero llene todos los datos y luego guarde el Plan de Acción\")", true);
            }

            //se guardan cada uno de los planes de accion creados en la base de datos
            foreach (var Plan in planes)
            {
                DAOPAPlanAccion.set(Plan);

                //se rescata el el plan de accion recien creado para verificar su id y tulizarlos para cada uno de los usuarios
                //que intervienen en el proceso
                PAPlanAccion plan = DAOPAPlanAccion.GetPlanAccionUlt();

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
            };

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MesajeHecho", "exito(\"Hecho\",\"Los Planes de Acción se han asignado correctamente\")", true);

            Session.Remove("planes");
            tbCompromisos.DataSource = null;
            tbCompromisos.DataBind();
        }
    }
}
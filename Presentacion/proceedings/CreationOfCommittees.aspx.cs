using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.Generales;
using Logica.proceedings;
using Newtonsoft.Json;
using Persistencia;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.proceedings
{
    public partial class CreationOfCommittees : System.Web.UI.Page
    {
        Conexion cone = new Conexion();

        Encryption Encriptacion = new Encryption();
        ActasReunionLogica acta = new ActasReunionLogica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                CargarDdlNombreComite();
                cargarDropUsuariosMiembros();
                crearTablas();
                ddlParticipantes.Items[0].Attributes.Add("disabled", "disabled");
            }
           
            txtTAgregarTema.Attributes.Add("placeholder", "Nombre del tema");
            //txtCoordinador.Attributes.Add("autocomplete", "off");
        }

        
        public void cargarDropUsuariosMiembros()
        {
            ddlParticipantes.Items.Clear();
            //ddlCoordinador.Items.Clear();
            //ddlSecretario.Items.Clear();

            ddlParticipantes.Items.Add(new ListItem("Selecione Usuario", "-1"));
            //ddlCoordinador.Items.Add(new ListItem("Selecione un coordinador", "-1"));
            //ddlSecretario.Items.Add(new ListItem("Selecione un secretario", "-1"));


            //se busca una lista de usarios a agregar a la lista de miembros
            List<Usuario> usuarios = acta.getUSuariosAsistentes(Convert.ToInt32(ddlNombreC.SelectedValue));

            //se agregan cada uno de los usuarios consultados a los drops
            foreach (Usuario usuario in usuarios)
            {
                ddlParticipantes.Items.Add(new ListItem(usuario.GNNomUsu1 + HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + usuario.GnCargo1, usuario.GNCodUsu1.ToString()));
                ddlSecretario.Items.Add(new ListItem(usuario.GNNomUsu1 + HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + usuario.GnCargo1, usuario.GNCodUsu1.ToString()));
                ddlCoordinador.Items.Add(new ListItem(usuario.GNNomUsu1 + HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + usuario.GnCargo1, usuario.GNCodUsu1.ToString()));
            }

        }


        public void crearTablas()
        {
            DataTable tablaMiembros = new DataTable();
            tablaMiembros.Columns.Add("codigo");
            tablaMiembros.Columns.Add("nombre");
            tablaMiembros.Columns.Add("tipo");
            Session["tableUsuariosMiembros"] = tablaMiembros;


            DataTable tablaTemas = new DataTable();
            tablaTemas.Columns.Add("tema");
            tablaTemas.Columns.Add("codigo");
            Session["tablaTemas"] = tablaTemas;
        }

        
        //evento que agraga un nuevo registro a la tabla de miembros
        protected void ddlParticipante_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsuariosParticipantes miembro = new UsuariosParticipantes
            {
                GNCodUsu1 = Convert.ToInt32(ddlParticipantes.SelectedValue),
                OidAReunionC1 = Convert.ToInt32(ddlNombreC.SelectedValue),
                TipoUsuario1 = 3,
                TpNomEst1 = "Activo",
                TpNomUsu1 = "Miembro",
                EstUsuario1 = 1
            };

            DAOAReunionD.getInstance().set(miembro);

            List<UsuariosParticipantes> miembros = acta.GetUsuariosParticipantes(Convert.ToInt32(ddlNombreC.SelectedValue));

            cargarTablaMiembros(miembros);
            cargarDropUsuariosMiembros();
            upMiembros.Update();

        }




        //metodo que carga el drop con los no0mbres de los comites
        protected void CargarDdlNombreComite()
        {
            //se crea una lista con los comites en la base de datos
            List<AReunionC> comites = acta.GetAReunionCs(Convert.ToInt32(Session["admin"]));
            ddlComites.Items.Clear();
            ddlComites.Items.Add(new ListItem("Todos", ""));
            foreach (AReunionC comite in comites)
            {
                ListItem item = new ListItem(comite.StrNomReunion, comite.IntOidAReunionC.ToString());

                // se pasan los datos de la lista al drop
                ddlNombreC.Items.Add(item);

                ddlComites.Items.Add(item);
            }


        }


        //evanto que bus los comites ya existentes y muesttra la infomacion en los controles
        protected void ddlNombreC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se busca el comite selecionado
            AReunionC comite = acta.GetAReunionC(Convert.ToInt32(ddlNombreC.SelectedValue));

            //si se encuentra un comite se pasan los datos a los controles
            if (comite != null)
            {
                txtSiglaComite.Text = comite.StrSigla;
                txtTipoComite.Text = comite.StrTipo;

            }
            else //si no se encuentra se borra la informacion de los controles
            {
                txtSiglaComite.Text = "";
                txtTipoComite.Text = "";
            }



            List<ARAgenda> agenda = acta.listarAgenda(Convert.ToInt32(ddlNombreC.SelectedValue));
            List<UsuariosParticipantes> miembros = acta.GetUsuariosParticipantes(Convert.ToInt32(ddlNombreC.SelectedValue));

            UsuariosParticipantes coordinador = null, secretario = null;

            foreach (UsuariosParticipantes miembro in miembros)
            {
                if (miembro.TpNomUsu1 == "Coordinador")
                    coordinador = miembro;
                if (miembro.TpNomUsu1 == "Secretario")
                    secretario = miembro;
            }
            try
            {
                if (coordinador != null)
                    ddlCoordinador.Text = coordinador.GNCodUsu1.ToString();
                else
                    ddlCoordinador.Text = "-1";

                if (secretario != null)
                    ddlSecretario.Text = secretario.GNCodUsu1.ToString();
                else
                    ddlSecretario.Text = "-1";


            }
            catch (Exception ex)
            {
            }
            cargarTablaMiembros(miembros);
            cargarTablaAgenda(agenda);

            upComite.Update();
            upMiembros.Update();
            upTemas.Update();


        }

        //evento que crea un nuevo tema y lo guarda en la tabla de los temas
        protected void btnAgregarTema_Click(object sender, EventArgs e)
        {


            int OidAReunionC = Convert.ToInt32(ddlNombreC.SelectedValue);

            ARAgenda temaAgenda = new ARAgenda
            {
                Nombre = txtTAgregarTema.Text,
                OidARreunionC = OidAReunionC
            };

            acta.SetARAgenda(temaAgenda);

            cargarTablaAgenda(acta.listarAgenda(OidAReunionC));
            txtTAgregarTema.Text = "";

            upTemas.Update();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "exito(\"Nuevo tema\",\"El tema a sido agregado a la agenda\")", true);

        }

        protected void cargarTablaAgenda(List<ARAgenda> agenda)
        {


            tbTemas.DataSource = agenda;
            tbTemas.DataBind();


        }

        public void cargarTablaMiembros(List<UsuariosParticipantes> miembros)
        {
            DataTable TablaMiembros = new DataTable();
            TablaMiembros.Columns.Add("codigo");
            TablaMiembros.Columns.Add("nombre");
            TablaMiembros.Columns.Add("tipo");
            foreach (UsuariosParticipantes miembro in miembros)
            {
                TablaMiembros.Rows.Add(new Object[] { miembro.GNCodUsu1, miembro.NombreUsuario, miembro.TpNomUsu1 });
            }

            Session["tableUsuariosMiembros"] = TablaMiembros;

            tablaMiembros.DataSource = TablaMiembros;
            tablaMiembros.DataBind();
        }

        //evento que sucede cuando se le da click al boton eliminar de la tabla de los temas
        protected void tbTemas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LinkButton buton = (LinkButton)e.CommandSource;
                GridViewRow fila = (GridViewRow)buton.NamingContainer;

                int idtema = Convert.ToInt32(tbTemas.DataKeys[fila.RowIndex].Values[0]);
                // en caso de que el comando enviado se eliminar

                if (e.CommandName == "downTema")
                {
                    int idtemaNext = Convert.ToInt32(tbTemas.DataKeys[fila.RowIndex + 1].Values[0]), aux;


                    ARAgenda agendaActual = DAOARAgenda.GetAgenda(idtema);

                    ARAgenda agendaNext = DAOARAgenda.GetAgenda(idtemaNext);

                    aux = agendaActual.Posicion;

                    agendaActual.Posicion = agendaNext.Posicion;

                    agendaNext.Posicion = aux;

                    DAOARAgenda.update(agendaNext);
                    DAOARAgenda.update(agendaActual);
                    tbTemas.EditIndex = -1;
                    cargarTablaAgenda(DAOARAgenda.GetInstance().listar(Convert.ToInt32(ddlNombreC.Text)));
                }
                if (e.CommandName == "upTema")
                {
                    int idtemaPrev = Convert.ToInt32(tbTemas.DataKeys[fila.RowIndex - 1].Values[0]), aux;


                    ARAgenda agendaActual = DAOARAgenda.GetAgenda(idtema);

                    ARAgenda agendaPrev = DAOARAgenda.GetAgenda(idtemaPrev);

                    aux = agendaActual.Posicion;

                    agendaActual.Posicion = agendaPrev.Posicion;

                    agendaPrev.Posicion = aux;

                    DAOARAgenda.update(agendaPrev);
                    DAOARAgenda.update(agendaActual);
                    tbTemas.EditIndex = -1;
                    cargarTablaAgenda(DAOARAgenda.GetInstance().listar(Convert.ToInt32(ddlNombreC.Text)));
                }
            }
            catch (Exception)
            {

            }

        }

        //evemto que catura los caomados del grid de los miembros
        protected void tablaMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //en caso de que el comando enviado sea eliminar
            if (e.CommandName == "eliminar")
            {
                //se obtiene la fila que envio el comando
                LinkButton buton = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)buton.NamingContainer;

                //se busca la tabla con la informacion de los miembros agregados
                DataTable tablaUsuarios = (DataTable)Session["tableUsuariosMiembros"];

                int idMiembro = Convert.ToInt32(tablaUsuarios.Rows[row.RowIndex]["codigo"].ToString());


                acta.deleteUsuarioMiembro(idMiembro);


                List<UsuariosParticipantes> miembros = acta.GetUsuariosParticipantes(Convert.ToInt32(ddlNombreC.SelectedValue));

                cargarTablaMiembros(miembros);
                cargarDropUsuariosMiembros();
                upMiembros.Update();
            }
        }


        //metodo que selecciona el coordinador del comite
        protected void ddlCoordinador_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se obtiene una lista de miembros del tipo Coordinador
            List<UsuariosParticipantes> miembros = DAOAReunionD.getInstance().GetMiembrosPorTipo(
                Convert.ToInt32(ddlNombreC.SelectedValue),
                "Coordinador"
            );

            //se crea una instacia de de tipo usuarioParticipantes para el usuario coordinador
            UsuariosParticipantes Coordinador = new UsuariosParticipantes();

            //se le dan los datos del usuario coordinador
            Coordinador.EstUsuario1 = Convert.ToInt32(ddlNombreC.SelectedValue);
            Coordinador.GNCodUsu1 = Convert.ToInt32(ddlCoordinador.SelectedValue);
            Coordinador.TipoUsuario1 = 1;
            Coordinador.EstUsuario1 = 1;
            Coordinador.TpNomUsu1 = "Coordinador";
            Coordinador.TpNomEst1 = "Activo";
            Coordinador.NombreUsuario = ddlCoordinador.SelectedItem.Text;
            Coordinador.OidAReunionC1 = Convert.ToInt32(ddlNombreC.SelectedValue);


            if (miembros.Count > 0) //en caso de que exita un usuario coordinador
            {
                //se actualiza el coordinador con los datos ingresados
                DAOAReunionD.getInstance().updateMiembro(
                    Coordinador,
                    "Coordinador"
                );
            }
            else //en caso de que np exista un usuario coordinador
            {
                DAOAReunionD.getInstance().set(Coordinador); // se crea un usuario coordinador
            }

            //se actualizan los datos de la tabla mienbros
            cargarTablaMiembros(miembros);

            //se muesta la notificacion que el miembro coordinador ha sidio actualizado
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exito", "exito(\"Coordinador actualizado\",\"E coordinador ha sido Actualizado\")", true);
        }

        //evento que actualiza el secretario d la runion
        protected void ddlSecretario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se obtine el secretario de la runion
            List<UsuariosParticipantes> miembros = DAOAReunionD.getInstance().GetMiembrosPorTipo(
                Convert.ToInt32(ddlNombreC.SelectedValue),
                "Secretario"
            );
            //se hace una instancia de la clase UsuariosParticipante para el secretario
            UsuariosParticipantes Secretario = new UsuariosParticipantes();

            //se pasan los datos del Usuario  secretario
            Secretario.EstUsuario1 = Convert.ToInt32(ddlNombreC.SelectedValue);
            Secretario.GNCodUsu1 = Convert.ToInt32(ddlSecretario.SelectedValue);
            Secretario.TipoUsuario1 = 2;
            Secretario.EstUsuario1 = 1;
            Secretario.TpNomUsu1 = "Secretario";
            Secretario.TpNomEst1 = "Activo";
            Secretario.NombreUsuario = ddlSecretario.SelectedItem.Text;
            Secretario.OidAReunionC1 = Convert.ToInt32(ddlNombreC.SelectedValue);

            //en caso de que exita un secretario
            if (miembros.Count > 0)
            {
                //se actualiza el secretario
                DAOAReunionD.getInstance().updateMiembro(
                    Secretario,
                    "Secretario"
                );
            }
            //en caso de que no exita el secretario
            else
            {
                //se crea el Miembro secretario
                DAOAReunionD.getInstance().set(Secretario);
            }

            //se actiualizan los datos de la tabla meimbros 
            cargarTablaMiembros(miembros);

            //se muestra la notificacion de la actualizacion del meimbro secretario
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exito", "exito(\"Secretario Actualizado\",\"El Secretario ha sido actualizado\")", true);
        }

        //metodo que pone la tabla temas en modo edicion 
        protected void tbTemas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            //se pone la tabla en modo edicion en el indice de la fila de la tabla selecionada
            tbTemas.EditIndex = index;
            //se realiza la actualizacion de la tabla
            List<ARAgenda> agenda = acta.listarAgenda(Convert.ToInt32(ddlNombreC.SelectedValue));
            cargarTablaAgenda(agenda);
            if (tbTemas.Rows.Count - 1 == index)
            {
                GridViewRow fila = tbTemas.Rows[index];
                LinkButton linkButton = fila.FindControl("downTema") as System.Web.UI.WebControls.LinkButton;
                linkButton.Visible = false;
            }
            if (index == 0)
            {
                GridViewRow fila = tbTemas.Rows[index];
                LinkButton linkButton = fila.FindControl("upTema") as System.Web.UI.WebControls.LinkButton;
                linkButton.Visible = false;
            }

        }

        //mettodo que saca la tabla de temas del modo edicion 
        protected void tbTemas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            tbTemas.EditIndex = -1;
            List<ARAgenda> agenda = acta.listarAgenda(Convert.ToInt32(ddlNombreC.SelectedValue));
            cargarTablaAgenda(agenda);
        }


        //evento que actualiza los datos de la tabla de la ajenda del comite
        protected void tbTemas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = tbTemas.Rows[e.RowIndex];
            TextBox txtNombre = row.FindControl("txtNombre") as TextBox;


            int idTema = Convert.ToInt32(tbTemas.DataKeys[e.RowIndex].Values["OidARAgenda"]);

            ARAgenda tema = DAOARAgenda.GetAgenda(idTema);

            tema.Nombre = txtNombre.Text;

            DAOARAgenda.update(tema);

            tbTemas.EditIndex = -1;

            //se cargan otra vez los datos de la tabla
            List<ARAgenda> agenda = acta.listarAgenda(Convert.ToInt32(ddlNombreC.SelectedValue));
            cargarTablaAgenda(agenda);
        }

        //metodo qe crea un evento al cronograma del comite
        [WebMethod(EnableSession = true)]
        public static string setCronogramaComite(int idGNModulo, DateTime startDate, DateTime endDate, String location, String name, int idCronograma)
        {
            ActasReunionLogica Logica = new ActasReunionLogica();

            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            AReunionC comite = Logica.GetAReunionC(idCronograma);


            UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(comite.IntGnCdArea);

            //se busca la direccion a la que pertenece el comite
            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);
            string sigla;

            sigla = "ACT-" + direccion.StrSiglaDir + "-" + comite.StrSigla + "-";



            ARActasC acta = new ARActasC
            {
                DtmFecFinal = endDate,
                DtmFechEditable = startDate,
                DtmFecInicio = startDate,
                DtmFecSistema = endDate,
                IntEstado = 0,
                IntGNCodUsu = idUsuario,
                IntOidAReunionC = idCronograma,
                StrLink = "",
                StrLugarReun = location,
                StrNombre = comite.StrNomReunion,
                StrObjetivo = "",
                StrSigla = sigla,
                IntCodigo = 0,
            };

            Logica.CargarActa(acta);

            acta = Logica.GetARActasCUltima(comite.IntOidAReunionC);


            GNEventos evento = new GNEventos
            {
                DtmFechaFinal = endDate,
                DtmFechaInicio = startDate,
                IntOidCronograma = idCronograma,
                IntOidGNEvento = acta.IntOidARActas,
                IntOidGNModulo = 3,
                StrContenido = acta.StrNombre,
                StrLugar = acta.StrLugarReun,
            };

            //se devuelve el evento recien creado en formato json
            return Json.Encode(evento);
        }

        //metodo que devuelve una lista de todos los eventos
        [WebMethod]
        public static string GetEventos()
        {
            List<ARActasC> actas = DAOARActasC.ListarActasProgramadas();
            List<GNEventos> eventos = new List<GNEventos>();
            foreach (var acta in actas)
            {
                eventos.Add(new GNEventos
                {
                    DtmFechaFinal = acta.DtmFecFinal,
                    DtmFechaInicio = acta.DtmFecInicio,
                    IntOidCronograma = acta.IntOidAReunionC,
                    IntOidGNEvento = acta.IntOidARActas,
                    IntOidGNModulo = 3,
                    StrContenido = acta.StrNombre,
                    StrLugar = acta.StrLugarReun,
                });
            }
            return Json.Encode(eventos);
        }

        //metodo que devuelve un evento por su id
        [WebMethod]
        public static string GetEvento(int id)
        {
            ActasReunionLogica logica = new ActasReunionLogica();

            ARActasC acta = logica.getActasC(id);

            GNEventos evento = new GNEventos
            {
                DtmFechaFinal = acta.DtmFecFinal,
                DtmFechaInicio = acta.DtmFecInicio,
                IntOidCronograma = acta.IntOidAReunionC,
                IntOidGNEvento = acta.IntOidARActas,
                IntOidGNModulo = 3,
                StrContenido = acta.StrNombre,
                StrLugar = acta.StrLugarReun,
            };

            return Json.Encode(evento);
        }

        //metodo que actuliza un evento por id
        [WebMethod]
        public static string updateEvento(int idGNModulo, DateTime startDate, DateTime endDate, String location, String name, int id, int idCronograma)
        {

            //se cra una instancia de la clase GNEvantos con los datos actulizados

            ActasReunionLogica logica = new ActasReunionLogica();

            ARActasC acta = logica.getActasC(id);

            acta.DtmFecFinal = endDate;
            acta.DtmFecInicio = startDate;
            acta.DtmFechEditable = startDate;
            acta.DtmFecSistema = endDate;
            acta.StrLugarReun = location;

            DAOARActasC.update(acta);
            GNEventos Evento = new GNEventos
            {
                DtmFechaFinal = endDate,
                DtmFechaInicio = startDate,
                IntOidCronograma = idCronograma,
                IntOidGNEvento = id,
                IntOidGNModulo = 3,
                StrContenido = acta.StrNombre,
                StrLugar = location
            };

            //se retorna el evento actualizado en formato Json
            return Json.Encode(Evento);

        }


        //metodo que elimina un evento por su id 
        [ScriptMethod()]
        [WebMethod]
        public static string DeleteEvento(int id)
        {
            try
            {
                ActasReunionLogica logica;

                DAOARActasC.DeleteActa(id);

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        //metodo que devuelve una lista de los usuarios que se encuntran en la base de datos 
        [WebMethod(EnableSession = true)]
        public static string getUsuarios(string nombre)
        {
            try
            {
                List<Usuario> usuarios = DAOUsuario.getUsuarios(nombre);
                return Json.Encode(usuarios);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected void listaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtCoordinador.Text = listaUsuarios.Text;
        }

        protected void tbTemas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idTema = Convert.ToInt32(tbTemas.DataKeys[e.RowIndex].Values["OidARAgenda"]);
            DAOARAgenda.GetInstance().delete(idTema);
            List<ARAgenda> agenda = acta.listarAgenda(Convert.ToInt32(ddlNombreC.SelectedValue));
            cargarTablaAgenda(agenda);
        }

        [WebMethod]
        public static string GetCronograma(string idReunion, DateTime FecInicio, string nombre)
        {
            return JsonConvert.SerializeObject(DAOARActasC.GetActas(idReunion, FecInicio, nombre));
        }
    }
}
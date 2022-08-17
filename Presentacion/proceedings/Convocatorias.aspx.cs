using Comunes;
using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.proceedings
{
    public partial class Convocatorias : System.Web.UI.Page
    {
        ActasReunionLogica acta = new ActasReunionLogica();
        protected void Page_Load(object sender, EventArgs e)
        {
            Verification();
            if (!IsPostBack)
            {
                cargarTbComites();
                cargarTablaComitesConvocados();
                cargarDdlUnidadesFuncionales();
            }
            txtAgregarTema.Attributes.Add("placeholder", "Agregar Tema");
        }


        private void Verification()
        {
            try
            {
                Session["admin"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe iniciar sesion");
            }
        }


        //metodo que carga la tabla comites con su infomacion
        protected void cargarTbComites()
        {
            List<ARActasC> actas = DAOARActasC.ListarActasProgramadas(Convert.ToInt32(Session["Admin"]));
            tbComite.DataSource = actas;
            tbComite.DataBind();
            upComites.Update();
        }

        public void cargarDdlUnidadesFuncionales()
        {
            List<UnidadFuncional> unidades = acta.GetUnidadFuncionales();
            ddlUnidaFuncional.Items.Clear();
            ddlUnidaFuncional.Items.Add(new ListItem("seleccione", "-1"));
            foreach (UnidadFuncional unidad in unidades)
            {
                ddlUnidaFuncional.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }
        }

        // metodo que carga la tabla con la infomacion de los comites que ya aha sido convocados
        public void cargarTablaComitesConvocados()
        {
            //se crea una lista con la informacion de los comites que ya han sido creados
            List<ARActasC> actasConvocadas = DAOARActasC.GetActasConvocadas(Convert.ToInt32(Session["admin"].ToString()));
            //se crea un datatabel donde se guardaran previamente la informacion de los comites convocados
            DataTable dtActasConvocadas = new DataTable();
            dtActasConvocadas.Columns.Add("codigo");
            dtActasConvocadas.Columns.Add("nombre");
            dtActasConvocadas.Columns.Add("lugar");
            dtActasConvocadas.Columns.Add("fecha");

            // se pasan los datos de la lista al datatable
            foreach (ARActasC acta in actasConvocadas)
            {
                dtActasConvocadas.Rows.Add(new object[] { acta.IntOidARActas, acta.StrNombre, acta.StrLugarReun, acta.DtmFechEditable });
            }

            //se pasa lainfomacion del data table a la tabla de los comite convocados
            tbMisConvocatorias.DataSource = dtActasConvocadas;

            tbMisConvocatorias.DataBind();
            upComites.Update();

            //se guarda la infirmacion del databable para su posterior uso
            Session["dtActasConvocadas"] = dtActasConvocadas;
        }

        //evento crea la tablas para la convocatoria del comite
        protected void tbComite_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            LinkButton buton = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)buton.NamingContainer;
            int idActa = Convert.ToInt32(tbComite.DataKeys[row.RowIndex].Values["IntOidARActas"]);
            int idComite = Convert.ToInt32(tbComite.DataKeys[row.RowIndex].Values["IntOidAReunionC"]);
            if (e.CommandName == "convocar")
            {
                List<ARActasC> actas = DAOARActasC.GetActasConvocadas(Convert.ToInt32(Session["Admin"]));
                foreach (var auxActa in actas)
                {
                    if (auxActa.IntOidAReunionC == idComite)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Mess1", "error(\"No se puede convocar reunión\",\"a hay una reunión convocada para este comité\")", true);
                        return;
                    }
                };

                CargarTablaTemas(idComite);
                CargarTablaMiembros(idComite);

                ARActasC acta = DAOARActasC.get(idActa);

                txtLugarReunion.Text = acta.StrLugarReun;
                txtFechaReunion.Text = acta.DtmFecInicio.ToString("yyyy-MM-ddTHH:mm");


                //se muestra el panel que contine la tabla con la ingformacion del comite
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "$(\"#ContentPlaceHolder_upContenidoComite\").show();", true);

                // se guarda el id del comete en una variable de session para su posterior uso
                Session["idComite"] = idComite;
                Session["tipoConvocatoria"] = 0;
                Session["idActa"] = idActa;
                cargarddlUsuarios();
                ddlUnidaFuncional.Visible = false;
                ddlCoordinador.Visible = false;
                lbUnidadFuncional.Visible = false;
                lbCoordinador.Visible = false;
            }
        }

        public void CargarTablaMiembros(int idCominte)
        {
            List<UsuariosParticipantes> miembros = (List<UsuariosParticipantes>)Session["miembros"];
            if (miembros == null)
                miembros = DAOAReunionD.GetUsuariosParticipantes(idCominte);
            tbMiembros.DataSource = miembros;
            tbMiembros.DataBind();
            upContenidoComite.Update();

            Session["miembros"] = miembros;
        }

        public void CargarTablaTemas(int idCominte)
        {

            List<ARAgenda> temas = (List<ARAgenda>)Session["agenda"];

            if (temas == null)
                temas = DAOARAgenda.GetInstance().listar(idCominte);

            Session["agenda"] = temas;

            tbAgenda.DataSource = temas;
            tbAgenda.DataBind();
            upContenidoComite.Update();
        }


        //metodo que agrega un nuevo usuario a la lista de los posibles invitados
        public void cargarddlUsuarios()
        {

            List<UsuariosParticipantes> usuariosConvocados = (List<UsuariosParticipantes>)Session["miembros"] ?? new List<UsuariosParticipantes>();


            int tipoConvocatoria = Convert.ToInt32(Session["tipoConvocatoria"].ToString());
            //se limpia la informacion en la ddlUsuarios
            ddlUsuarios.Items.Clear();
            ddlCoordinador.Items.Clear();

            //se crea indice 1
            ddlUsuarios.Items.Add(new ListItem("Seleccione para agragar un invitado", "-1"));

            // se crea una lista de los usuarios qu pueden ser invitados

            List<Usuario> usuarios = acta.getUSuariosAsistentes((tipoConvocatoria == 1) ? 6017 : Convert.ToInt32(Session["idComite"]));

            foreach (Usuario usuario in usuarios)
            {
                bool isConvocado = false;
                foreach (var usuarioConvocado in usuariosConvocados)
                {
                    if (isConvocado = usuario.GNCodUsu1 == usuarioConvocado.GNCodUsu1)
                        break;

                }
                if (!isConvocado)
                {
                    ddlUsuarios.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1 + ""));
                    ddlCoordinador.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1 + ""));
                }
            }

            if (tipoConvocatoria == 1)
                ddlCoordinador.Text = Session["admin"].ToString();
        }

        //metodo que agreaga un nuvo miembro invitado al comite
        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<UsuariosParticipantes> miembros = (List<UsuariosParticipantes>)Session["miembros"] ?? new List<UsuariosParticipantes>();

            int idComite = Convert.ToInt32(Session["idComite"]);
            miembros.Add(new UsuariosParticipantes
            {
                EstUsuario1 = 1,
                GNCodUsu1 = Convert.ToInt32(ddlUsuarios.Text),
                NombreUsuario = ddlUsuarios.SelectedItem.Text,
                OidAReunionC1 = idComite,
                TipoUsuario1 = 3,
                TpNomEst1 = "Activo",
                TpNomUsu1 = "Invitado",
            });
            Session["miembros"] = miembros;
            CargarTablaMiembros(idComite);
            cargarddlUsuarios();
        }



        public void ConvocarComite()
        {

            int idActa = Convert.ToInt32(Session["idActa"]);
            ARActasC acta = DAOARActasC.get(idActa);
            acta.StrLugarReun = txtLugarReunion.Text;
            acta.DtmFecInicio = Convert.ToDateTime(txtFechaReunion.Text);
            acta.DtmFecFinal = Convert.ToDateTime(txtFechaReunion.Text);
            acta.IntEstado = 1;
            acta.IntCodigo = DAOARActasC.GetCodigo(acta.StrSigla);


            CargarTemaCompromisos(acta);

            DAOARActasC.update(acta);


            CargarMiembros(idActa);
            CargarTemas(idActa);

            cargarTbComites();
            cargarTablaComitesConvocados();
            EnviarNotificacion(idActa);
        }


        public void CargarTemas(int idActa)
        {
            ActasReunionLogica logica = new ActasReunionLogica();
            List<ARAgenda> temas = (List<ARAgenda>)Session["Agenda"];
            foreach (var tema in temas)
            {
                logica.CargarTema(new ARActasTemas
                {
                    IntOidARActasC = idActa,
                    StrAdjuntar = "",
                    StrDesarrollo = "",
                    StrNomTema = tema.Nombre
                });
            }
        }

        /// <summary>
        /// Metodo que crea un tema con la informacion de los comprimisos incumplidos de las actas anteriores  
        /// </summary>
        /// <param name="acta"></param>
        public void CargarTemaCompromisos(ARActasC acta)
        {

            ActasReunionLogica logica = new ActasReunionLogica();

            //se crea un listado de los planes de accion 
            List<PAPlanAccion> planes = new List<PAPlanAccion>();

            //Se consulta un listado de las actas que pertenecen al comite
            List<ARActasC> actas = DAOARActasC.GetActasByIdComite(acta.IntOidAReunionC);

            //se recorre la lista de las actas para consultar los temas 
            foreach (var Acta in actas)
            {
                //se crea un listado de los temas que a cada una las actas consultadas previamente 
                List<ARActasTemas> Temas = logica.GetARActasTemas(Acta.IntOidARActas);

                //por cada tema de cada acata se consulta un listado de los planes de acción
                foreach (var tema in Temas)
                {
                    //se agregan todos los planes de accion a la lista de los planes de acción
                    DAOPAPlanAccion.GetPlanAccionByContexto(tema.IntOidARActasTemas, PAPlanAccion.CONTEXTO.ACTAREUNION).ForEach(plan => planes.Add(plan));
                }
            }

            string tablaCompromisos = "<table>" +
                "<thead>" +
                "	<tr>" +
                "		<th><strong>¿Qué?</strong></th>" +
                "		<th><strong>¿Cómo?</strong></th>" +
                "		<th><strong>¿Quién?</strong></th>" +
                "		<th><strong>¿Cuándo?</strong></th>" +
                "		<th><strong>Estado</strong></th>" +
                "	</tr>" +
                "</thead>" +
                "<tbody>";


            planes.ForEach(plan =>
            {
                string estado;
                switch (plan.IntEstAct)
                {
                    case PAPlanAccion.ESTADO.ASIGNADO: estado = "Asignado"; break;
                    case PAPlanAccion.ESTADO.PROCESO: estado = "Proceso"; break;
                    case PAPlanAccion.ESTADO.EVALUACION: estado = "Evaluación"; break;
                    default: estado = "Asinado"; break;
                }


                tablaCompromisos += "" +
                "<tr>" +
                $"	<td>{plan.StrActividad}</td>" +
                $"	<td>{plan.StrComo}</td>" +
                $"	<td>{plan.StrNombreUsuarioResponsable}</td>" +
                $"	<td>{plan.DtmFecFinalActa}</td>" +
                $"	<td>{estado}</td>" +
                "<tr>";


            });

            tablaCompromisos += "</tbody></table>";

            if (planes.Count > 0)
                logica.CargarTema(new ARActasTemas
                {
                    IntOidARActasC = acta.IntOidARActas,
                    StrAdjuntar = "",
                    StrDesarrollo = tablaCompromisos,
                    StrNomTema = "Compromisos de la(s) Acta(s) anterior(es)"
                });
        }


        public void CargarMiembros(int idActa)
        {

            List<UsuariosParticipantes> usuarios = (List<UsuariosParticipantes>)Session["miembros"];
            foreach (var usuario in usuarios)
            {
                DAOARactasDM.set(new ARActasDM
                {
                    BlnFirmado = false,
                    IntEstUsuario = 1,
                    IntGNCodUsu = usuario.GNCodUsu1,
                    IntOidARActasC = idActa,
                    StrNombre = usuario.NombreUsuario,
                    StrTipoUsuario = usuario.TpNomUsu1,
                });
            }
        }

        public void ConvocarRunionGeneral(int tipoConvocatoria)
        {
            //se requiere seleccionar la unida funcional para el tipo de convocatoria de reunion genral
            if (tipoConvocatoria == 1 && ddlUnidaFuncional.Text == "-1" && ddlCoordinador.Text == "-1" && string.IsNullOrEmpty(txtNombre.Text))
            {
                //en caso de que no se haya dado una unidad funcional se envia el mensaje de error
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Primero llene toda la infomacion requrida\")", true);
                return;
            }

            UnidadFuncional unidadFuncional = DAOUnidadFuncional.GetUnidadFuncional(Convert.ToInt32(ddlUnidaFuncional.Text));
            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidadFuncional.GnCdAra1);

            string sigla = "ACT-" + direccion.StrSiglaDir + "-" + unidadFuncional.GnSiglaUnf1 + "-";

            ActasReunionLogica logica = new ActasReunionLogica();

            AReunionC comite = logica.GetAReunionC(6017);

            ARActasC acta = new ARActasC
            {
                DtmFecFinal = Convert.ToDateTime(txtFechaReunion.Text),
                DtmFechEditable = Convert.ToDateTime(txtFechaReunion.Text),
                DtmFecInicio = Convert.ToDateTime(txtFechaReunion.Text),
                DtmFecSistema = Convert.ToDateTime(txtFechaReunion.Text),
                IntCodigo = DAOARActasC.GetCodigo(sigla),
                IntEstado = 1,
                IntGNCodUsu = Convert.ToInt32(ddlCoordinador.Text),
                IntOidAReunionC = 6017,
                StrLink = txtLink.Text,
                StrLugarReun = txtFechaReunion.Text,
                StrNombre = txtNombre.Text,
                StrObjetivo = "",
                StrSigla = sigla,
            };
            logica.CargarActa(acta);

            acta = logica.GetARActasCUltima(6017);

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(ddlCoordinador.Text));

            ARActasDM miembro = new ARActasDM
            {
                BlnFirmado = false,
                IntEstUsuario = 1,
                IntGNCodUsu = usuario.GNCodUsu1,
                IntOidARActasC = acta.IntOidARActas,
                StrNombre = usuario.GNNomUsu1,
                StrTipoUsuario = "Coordinador",
            };

            DAOARactasDM.set(miembro);

            CargarMiembros(acta.IntOidARActas);
            CargarTemas(acta.IntOidARActas);
            cargarTbComites();
            cargarTablaComitesConvocados();
            EnviarNotificacion(acta.IntOidARActas);
        }


        public void EnviarNotificacion(int idActa)
        {
            List<UsuariosParticipantes> miembros = (List<UsuariosParticipantes>)Session["miembros"];
            List<string> correos = new List<string>();
            ARActasC acta = DAOARActasC.get(idActa);
            foreach (var miembro in miembros)
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(miembro.GNCodUsu1);
                correos.Add(usuario.GNCrusu1);
            }

            List<ARActasTemas> temas = new DAOARActasTemas().Listar(idActa);

            string agenda = "<ul>";

            temas.ForEach(tema =>
            {
                agenda += $@"
					<li>{tema.StrNomTema}</li>
				";
            });

            agenda += "</ul>";
            string mensaje = $@"<!DOCTYPE html>
				<html lang=""en"" xmlns=""https://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
				<head>
					<meta charset=""UTF-8"">
					<meta name=""viewport"" content=""width=device-width,initial-scale=1"">
					<meta name=""x-apple-disable-message-reformatting"">
					<title></title>
					<!--[if mso]>
					<noscript>
						<xml>
							<o:OfficeDocumentSettings>
								<o:PixelsPerInch>96</o:PixelsPerInch>
							</o:OfficeDocumentSettings>
						</xml>
					</noscript>
					<![endif]-->
					<style>
						table,td,div, h1,p {"{font-family: Arial, sans-serif;}"}
					</style>
				</head>

				<body style=""margin: 0;padding: 0;"">
					<p>&nbsp;</p>
   
					<table
						style=""width: 640px; border-collapse: collapse; border: 0px; border-spacing: 0px; text-align: left; margin: 0px auto; font-family: Arial, sans-serif; height: 272px;""
						role=""presentation"">
						<tbody>
							<tr style=""height: 70px;"">
								<td style=""padding: 40px; background: #2874A6; height: 70px;"" align=""center"">
									<table style=""border-collapse: collapse; width: 700px; margin-left: auto; margin-right: auto;""
										role=""presentation"" cellspacing=""0px"">
										<tbody>
											<tr>
												<td style=""padding-right: 30px; width: 434.219px;"" align=""right"">
													<img style=""float: left;"" src=""http://190.242.128.206:81/Images/LogoVitraya2.png"" alt=""""  width=""121"" height=""116"" />
												</td>
												<td style=""padding-left: 30px; padding-top: 21px; text-align: right; width: 264.781px;"">
													<img src=""http://190.242.128.206:81/Images/calendario-blanco.png"" alt="""" width=""47"" height=""50"" /><br />
													<span style=""color: #ffffff;"">Reunión o comité: {acta.StrNombre}</span><br> 
													<span style=""color: #ffffff;""><br />{acta.DtmFechEditable.ToString("MMMM dd, yyyy")} | {acta.DtmFecInicio.ToString("hh:mm tt")}</span><br>
												</td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
							<tr style=""height: 183px;"">
								<td style=""padding: 45px; color: #153643; height: 183px;"">
									<h2 style=""text-align: center;""><strong>Invitaci&oacute;n a reuni&oacute;n</strong></h2>
									<p>Señor(a)</p>
									<p>Cordial saludo</p>
									<p>Se le informa que usted ha sido invitado para la runión o comité: <strong>{acta.StrNombre}</strong>, que se realizara el d&iacute;a {acta.DtmFechEditable.ToString("dd") + " de " + acta.DtmFechEditable.ToString("MMMM yyyy")} a las
										{acta.DtmFecInicio.ToString("HH:mm")}</p>
									<h4>Agenda a tratar:</h4>
									{agenda}
									<table style=""width: 100%; height: 53px;"">
										<tbody>
											<tr>
												<td align=""center"">
													<img src=""http://190.242.128.206:81/Images/BotonReunion.png"" alt="""" width=""244"" height=""55"" />
												</td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
							<tr style=""height: 19px;"">
								<td style=""padding: 40px; background: #f0f0f0;"">
									<table style=""border-collapse: collapse; width: 100%;"">
										<tbody>
											<tr>
												<td style=""width: 49.9359%;"">
													{(acta.StrLink == "" ? "" : $@"<a href=""{acta.StrLink}""><img src=""http://190.242.128.206:81/Images/Logos.png"" alt="""" width=""260"" height=""59"" /></a>")}
												</td>
												<td style=""width: 49.9359%; text-align: right;"">
													notificaiones@vitrayaclinicacrecer.com
												</td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
						</tbody>
					</table>
				</body>
				</html>";

            //Email.SendMail(correos, mensaje, "Invtación a La reunión " + acta.StrNombre);
        }


        //evento que realiza una nueva convocatoria
        protected void bntConvocar_Click(object sender, EventArgs e)
        {

            //se verifica que todos los datos para realizar una comvocatoria esten completos
            if (String.IsNullOrEmpty(txtFechaReunion.Text) && string.IsNullOrEmpty(txtLugarReunion.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Primero llene toda la infomacion requrida\")", true);
                return;
            }

            //se verifica que tipo de convocatoria es si es de reunion general co por comite
            int tipoConvocatoria = Convert.ToInt32(Session["tipoConvocatoria"].ToString());



            if (tipoConvocatoria == 1)
            {
                ConvocarRunionGeneral(tipoConvocatoria);

            }
            else
            {
                ConvocarComite();

            }

            //se limpiantodos los campos
            ddlUnidaFuncional.Visible = false;
            ddlCoordinador.Visible = false;
            lbUnidadFuncional.Visible = false;
            lbCoordinador.Visible = false;
            cargarTablaComitesConvocados();
            cargarTbComites();

            //se oculta el parnel para la realizacion de la convocatoria


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msaaaaa", "$(\".modal\").modal(\"hide\")", true);

            //se muestra el mensaje de que la convocatoria se ha realizado exitosamente 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "exito(\"Hecho\",\"Se ha realizado la convocatoria correctamente\")", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfaa", "$(\"#ContentPlaceHolder_upContenidoComite\").hide()", true);
        }

        //evento de la tabla mis convocatoria que redireciona al pantalla del desarrollo del acta
        protected void tbMisConvocatorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //se obtiene la posicion de la fila que genero el evento
            LinkButton buton = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)buton.NamingContainer;


            DataTable dtActasConvocadas = (DataTable)Session["dtActasConvocadas"];

            int idActa = Convert.ToInt32(dtActasConvocadas.Rows[row.RowIndex]["codigo"].ToString());
            if (e.CommandName == "empezar")
            {
                //se crea una variable de session el id del acta que se va a desarrollar
                Session["idActa"] = idActa;
                //se redirecciona a la pagina del desarrollo del acta
                Response.Redirect("RecordMinutes.aspx");
            }
        }

        //evento que enpieza la realizacion de una convocatoria de reunion general
        protected void btnConvocar_Click(object sender, EventArgs e)
        {
            //se muestra el panel para el desarrollo de la convocatoria
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cargar", "$(\"#event-modal2\").modal(\"hide\")", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", " $(\"#ContentPlaceHolder_upContenidoComite\").show();", true);

            //se muestran los campos necesarios para la reunion general
            ddlUnidaFuncional.Visible = true;
            ddlCoordinador.Visible = true;
            lbUnidadFuncional.Visible = true;
            lbCoordinador.Visible = true;
            txtNombre.Visible = true;
            lbNombre.Visible = true;

            //se establece el tipo de reunion que sera de reunion genral
            Session["tipoConvocatoria"] = 1;
            //se cargan lo dropDownList de los usuarios
            cargarddlUsuarios();
            Session.Remove("dtMiembros");
            Session.Remove("dtAgenda");

            //se limpian los datos de las tablas
            tbMiembros.DataSource = null;
            tbAgenda.DataSource = null;

            tbAgenda.DataSource = null;
            tbAgenda.DataBind();

            tbAgenda.DataBind();
            tbMiembros.DataBind();
            upContenidoComite.Update();
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        protected void btnAgregarTema_Click(object sender, EventArgs e)
        {
            int idComite = Convert.ToInt32(Session["idComite"]);
            List<ARAgenda> temas = (List<ARAgenda>)Session["agenda"];
            if (temas == null)
                temas = new List<ARAgenda>();
            temas.Add(new ARAgenda
            {
                OidARreunionC = idComite,
                Nombre = txtAgregarTema.Text,
            });

            txtAgregarTema.Text = "";

            Session["agenda"] = temas;
            CargarTablaTemas(idComite);
        }


        protected void tbMiembros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<UsuariosParticipantes> miembros = (List<UsuariosParticipantes>)Session["miembros"];
            miembros.RemoveAt(e.RowIndex);
            tbMiembros.DataSource = miembros;
            tbMiembros.DataBind();
            upContenidoComite.Update();
            Session["miembros"] = miembros;
        }

        protected void tbAgenda_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tbAgenda.EditIndex = -1;
            int idComite = Convert.ToInt32(Session["idComite"]);
            CargarTablaTemas(idComite);
        }

        protected void tbAgenda_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<ARAgenda> agendas = (List<ARAgenda>)Session["agenda"];

            agendas.RemoveAt(e.RowIndex);
            Session["agenda"] = agendas;

            tbAgenda.EditIndex = -1;

            int idComite = Convert.ToInt32(Session["idComite"]);
            CargarTablaTemas(idComite);
        }

        protected void tbAgenda_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tbAgenda.EditIndex = e.NewEditIndex;
            int idComite = Convert.ToInt32(Session["idComite"]);
            CargarTablaTemas(idComite);
        }

        protected void tbAgenda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<ARAgenda> agendas = (List<ARAgenda>)Session["agenda"];
            GridViewRow fila = tbAgenda.Rows[e.RowIndex];
            string Tema = (fila.FindControl("txtTema") as System.Web.UI.WebControls.TextBox).Text;

            agendas.ElementAt(e.RowIndex).Nombre = Tema;
            Session["agenda"] = agendas;

            tbAgenda.EditIndex = -1;

            int idComite = Convert.ToInt32(Session["idComite"]);
            CargarTablaTemas(idComite);
        }
    }
}
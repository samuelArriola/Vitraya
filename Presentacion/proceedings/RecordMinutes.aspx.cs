using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.proceedings
{
    public partial class RecordMinutes : System.Web.UI.Page
    {
        ActasReunionLogica ActatasReunionLogica = new ActasReunionLogica();




        #region SECCION DE EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                cargarTablaUsuariosParticipantes();
                cargarDropUsuarios();
                llenarTablaTemas();
                Page.MaintainScrollPositionOnPostBack = true;
                Session["idTema"] = -1;
                txtCodigoActa.Attributes.Add("Disabled", "");
                cargarActa();
                txtNombreTema.Attributes.Add("placeholder", "Nombre del tema");

            }


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }

        public void cargarActa()
        {
            int idActa = Convert.ToInt32(Session["idActa"].ToString());
            ARActasC acta = ActatasReunionLogica.getActasC(idActa);
            txtFechaReunion.Text = acta.DtmFechEditable.ToString("yyyy-MM-dd");
            txtHorainicio.Text = acta.DtmFecInicio.ToString("HH:mm");
            txtLugarReunion.Text = acta.StrLugarReun;
            txtHorafinal.Text = acta.DtmFecFinal.ToString("HH:mm");
            string codigoActa = "";
            for (int i = 0; i < 4 - acta.IntCodigo.ToString().Length; i++)
            {
                codigoActa += "0";
            }

            codigoActa += acta.IntCodigo;

            txtCodigoActa.Text = acta.StrSigla + codigoActa + "  " + acta.StrNombre;
        }

        [System.Web.Http.Authorize]
        [ValidateInput(false)]
        [WebMethod(EnableSession = true)]
        public static string guardarTema(string StrDesarrollo, string StrNomTema)
        {
            try
            {
                ActasReunionLogica actasReunionLogica = new ActasReunionLogica();
                int idTema = Convert.ToInt32(HttpContext.Current.Session["idtema"].ToString());
                ARActasTemas tema = actasReunionLogica.GetActasTema(idTema);
                ARActasC aRActasC = actasReunionLogica.getActasC(Convert.ToInt32(HttpContext.Current.Session["idActa"]));
                actasReunionLogica.updateARActasTemas(StrDesarrollo, "", StrNomTema, idTema, aRActasC.IntOidARActas, tema.IntOidGNListaArchivos);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "1";
        }

        protected void btnAgregarQuorum_Click(object sender, EventArgs e)
        {

            ARActasDM invitado = new ARActasDM
            {
                BlnFirmado = false,
                IntEstUsuario = 1,
                IntGNCodUsu = Convert.ToInt32(DropMiembros.SelectedValue),
                IntOidARActasC = Convert.ToInt32(Session["idActa"].ToString()),
                StrNombre = DropMiembros.SelectedItem.Text,
                StrTipoUsuario = "Invitado"
            };

            DAOARactasDM.set(invitado);
            cargarTablaUsuariosParticipantes();
            cargarDropUsuarios();
            upDdlAsistencia.Update();
        }


        protected void tablaTemas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditarTema")
            {
                LinkButton boton = (LinkButton)e.CommandSource;
                GridViewRow items = (GridViewRow)boton.NamingContainer;

                int idTema = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasTemas"]),
                    idActa = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasC"]);
                Response.Redirect("DesarrollarTemas.aspx?idTema=" + idTema + "&idActa=" + idActa);
            }
            if (e.CommandName == "Delete")  //en caso de que comando enviado se eliminar
            {
                ImageButton boton = (ImageButton)e.CommandSource;
                GridViewRow items = (GridViewRow)boton.NamingContainer;



                int idTema = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasTemas"]),
                    idActa = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasC"]);
                ActatasReunionLogica.deleteTema(idTema);

                //se carga la tabla temas ya que se elimino un tema
                llenarTablaTemas();
                upnListaTemas.Update();

                //se actualiza el editor de texto
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "tinymce.remove('#ContentPlaceHolder_txtEditor')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfe", "crearEditorTextos()", true);
            }
            if (e.CommandName == "upTema")
            {
                LinkButton boton = (LinkButton)e.CommandSource;
                GridViewRow items = (GridViewRow)boton.NamingContainer;
                if (items.RowIndex > 0)
                {
                    int idTema = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasTemas"]);

                    int idActaPrev = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex - 1].Values["IntOidARActasTemas"]);
                    int idTemaAux;

                    ARActasTemas temaAct = ActatasReunionLogica.GetActasTema(idTema);
                    ARActasTemas temaPrev = ActatasReunionLogica.GetActasTema(idActaPrev);

                    idTemaAux = temaAct.IntPosicion;
                    temaAct.IntPosicion = temaPrev.IntPosicion;
                    temaPrev.IntPosicion = idTemaAux;

                    ActatasReunionLogica.updateARActasTemas(temaAct);
                    ActatasReunionLogica.updateARActasTemas(temaPrev);

                    temaPrev.IntOidARActasTemas = idTemaAux;


                    tablaTemas.EditIndex = items.RowIndex - 1;

                    llenarTablaTemas();
                }

            }
            if (e.CommandName == "downTema")
            {
                LinkButton boton = (LinkButton)e.CommandSource;
                GridViewRow items = (GridViewRow)boton.NamingContainer;
                if (items.RowIndex < tablaTemas.Rows.Count - 1)
                {
                    int idTema = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex].Values["IntOidARActasTemas"]);
                    int idActaPrev = Convert.ToInt32(tablaTemas.DataKeys[items.RowIndex + 1].Values["IntOidARActasTemas"]);
                    int idTemaAux;

                    ARActasTemas temaAct = ActatasReunionLogica.GetActasTema(idTema);
                    ARActasTemas temaPrev = ActatasReunionLogica.GetActasTema(idActaPrev);

                    idTemaAux = temaAct.IntPosicion;
                    temaAct.IntPosicion = temaPrev.IntPosicion;
                    temaPrev.IntPosicion = idTemaAux;

                    ActatasReunionLogica.updateARActasTemas(temaAct);
                    ActatasReunionLogica.updateARActasTemas(temaPrev);

                    temaPrev.IntOidARActasTemas = idTemaAux;

                    tablaTemas.EditIndex = items.RowIndex + 1;
                    llenarTablaTemas();
                }
            }
        }


        #endregion

        #region SECCION DE METODOS

        //se carga el dropMembers con la informacion de de los usuarios de la base de datos
        public void cargarDropUsuarios()
        {
            //se optine un listado de todos los usuarios en la base de datos
            List<Usuario> usuarios = DAOUsuario.GetUsuariosAgregarAsitencia(Convert.ToInt32(Session["idActa"].ToString()));

            //se pasa la infomacion de la lista al dropMembers

            DropMiembros.Items.Clear();
            DropMiembros.Items.Add(new ListItem("Agregar nuevo invitado", "-1"));

            foreach (Usuario usuario in usuarios)
            {
                DropMiembros.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1.ToString()));
            }
        }

        // metodo que carga la tabla Participantes con la informacion en la base de datos
        private void cargarTablaUsuariosParticipantes()
        {
            DataTable dataTable = new DataTable();  // se crea un nuevo data table con sus respectibas columnas
            dataTable.Columns.Add("Nombre");
            dataTable.Columns.Add("Tipo");
            dataTable.Columns.Add("codigo");

            //se obtine un lista con los usuarios participante en la reunion
            List<ARActasDM> usuariosParticipantes = DAOARactasDM.getParticipantes(Convert.ToInt32(Session["idActa"].ToString()));
            //se pasan los datos de la lista al datatable
            foreach (ARActasDM usuario in usuariosParticipantes)
            {
                dataTable.Rows.Add(new object[] { usuario.StrNombre, usuario.StrTipoUsuario, usuario.IntGNCodUsu });
            }

            //se pasa el datatable al grid
            GridMembers.DataSource = dataTable;
            GridMembers.DataBind();

            foreach (GridViewRow row in GridMembers.Rows)
            {
                System.Web.UI.WebControls.CheckBox cbAsistencia = ((System.Web.UI.WebControls.CheckBox)row.Cells[2].FindControl("cbAsistencia"));
                cbAsistencia.Checked = usuariosParticipantes[row.RowIndex].IntEstUsuario == 1;
            }
            //se guarda el datatable en una variable de sesion para su posterior edicion
            Session["dtMembers"] = dataTable;
            uplistaAsistentes.Update();

        }

        //metodo que llena el grid tablaTemas
        protected void llenarTablaTemas()
        {
            List<ARActasTemas> temas = ActatasReunionLogica.GetARActasTemas(Convert.ToInt32(Session["idActa"]));

            tablaTemas.DataSource = temas;
            tablaTemas.DataBind();
            upnListaTemas.Update();

        }
        #endregion
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ARActasC acta = ActatasReunionLogica.getActasC(Convert.ToInt32(Session["idActa"].ToString()));
            try
            {
                acta.IntEstado = 2;  //estado,
                acta.StrLugarReun = txtLugarReunion.Text;  //lugarReun,
                acta.DtmFecInicio = DateTime.Parse(txtHorainicio.Text); //fecInicio,
                acta.DtmFecFinal = DateTime.Parse(txtHorafinal.Text);  //fecFinal,
                acta.DtmFechEditable = DateTime.Parse(txtFechaReunion.Text);  //fechEditable,
                acta.DtmFecSistema = DateTime.Now;   //fecSistema
                DAOARActasC.update(acta);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\" Primero llene todos los campos necesarios \")", true);
                return;
            }
            DataTable dtMembers = (DataTable)Session["dtMembers"];
            foreach (GridViewRow row in GridMembers.Rows)
            {
                int idMiembro = Convert.ToInt32(dtMembers.Rows[row.RowIndex]["codigo"].ToString());
                ARActasDM miembro = DAOARactasDM.get(Convert.ToInt32(Session["idActa"].ToString()), idMiembro);
                miembro.IntEstUsuario = (((System.Web.UI.WebControls.CheckBox)row.Cells[2].FindControl("cbAsistencia")).Checked) ? 1 : 0;
                DAOARactasDM.update(miembro);

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = miembro.IntOidARActasDM,
                    strAccion = "Modificar",
                    strDetalle = $"Se marca asistencia al miembro {miembro.StrNombre} en el acta con código {miembro.IntOidARActasC}",
                    strEntidad = "ARActasDM"
                });
            }
        }

        protected void btnGuardarAsistencia_Click(object sender, EventArgs e)
        {
            DataTable dtMembers = (DataTable)Session["dtMembers"];
            foreach (GridViewRow row in GridMembers.Rows)
            {
                int idMiembro = Convert.ToInt32(dtMembers.Rows[row.RowIndex]["codigo"].ToString());
                ARActasDM miembro = DAOARactasDM.get(Convert.ToInt32(Session["idActa"].ToString()), idMiembro);
                miembro.IntEstUsuario = (((System.Web.UI.WebControls.CheckBox)row.Cells[2].FindControl("cbAsistencia")).Checked) ? 1 : 0;
                DAOARactasDM.update(miembro);
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = miembro.IntOidARActasDM,
                    strAccion = "Modificar",
                    strDetalle = $"Se marca asistencia al miembro {miembro.StrNombre} en el acta con código {miembro.IntOidARActasC}",
                    strEntidad = "ARActasDM"
                });
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "exito(\"Hecho\",\"La asitencia ha sido tomada con exito\")", true);
        }

        protected void btnCrearTema_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombreTema.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Error\",\"Primero llene el campo: nombre del tema\")", true);
                return;
            }
            ARActasTemas tema = new ARActasTemas
            {
                StrDesarrollo = "",                  //desarrollo;
                StrAdjuntar = null,                //adjuntar;
                StrNomTema = txtNombreTema.Text,   //nomTema;
                IntOidARActasTemas = 0,                  //oidARActasTemas;
                IntOidARActasC = Convert.ToInt32(Session["idActa"].ToString())                  //oidARActasC;
            };

            ActatasReunionLogica.CargarTema(tema);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "exito(\"Nuevo tema\",\"El tema a sido agregado a la agenda\")", true);
            llenarTablaTemas();
        }

        protected void btnGuardarCabecera_Click(object sender, EventArgs e)
        {
            ARActasC acta = ActatasReunionLogica.getActasC(Convert.ToInt32(Session["idActa"].ToString()));
            upCabeceraActa.DataBind();
            try
            {
                acta.StrLugarReun = txtLugarReunion.Text;  //lugarReun,
                acta.DtmFecInicio = DateTime.Parse(txtHorainicio.Text); //fecInicio,
                acta.DtmFecFinal = DateTime.Parse(txtHorafinal.Text);  //fecFinal,
                acta.DtmFechEditable = DateTime.Parse(txtFechaReunion.Text);  //fechEditable,
                acta.DtmFecSistema = DateTime.Now;   //fecSistema
                DAOARActasC.update(acta);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "exito(\"Hecho\",\" Cabecera del acta guardada\")", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\" Primero llene todos los campos necesarios \")", true);
                return;
            }
        }
        [WebMethod(EnableSession = true)]
        public static string cerrarActa(DateTime DtmfechaReunion, DateTime DtmHorInicio, DateTime DtmHoraFinal, string StrLugarReunion)
        {

            ActasReunionLogica Acta = new ActasReunionLogica();

            List<ARActasTemas> temas = Acta.GetARActasTemas(Convert.ToInt32(HttpContext.Current.Session["idActa"]));
            foreach (ARActasTemas tema in temas)
            {
                if (string.IsNullOrEmpty(tema.StrDesarrollo))
                {
                    return "Hay temas que faltan por desarrollar, primero desarrolle los temas y luego cierre el acta";
                }
            }

            ARActasC acta = Acta.getActasC(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));
            try
            {
                acta.IntEstado = 2;
                acta.StrLugarReun = StrLugarReunion;  //lugarReun,
                acta.DtmFecInicio = DtmHorInicio; //fecInicio,
                acta.DtmFecFinal = DtmHoraFinal;  //fecFinal,
                acta.DtmFechEditable = DtmfechaReunion;  //fechEditable,
                acta.DtmFecSistema = DateTime.Now;   //fecSistema
                DAOARActasC.update(acta);

                acta = DAOARActasC.get(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));
                List<ARActasDM> asistentes = DAOARactasDM.getParticipantes(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));
                foreach (ARActasDM asistente in asistentes)
                {
                    Usuario usuario = DAOUsuario.getInstance().GetUsuario(asistente.IntGNCodUsu);

                    MailMessage email = new MailMessage();
                    try
                    {
                        email.To.Add(new MailAddress(usuario.GNCrusu1));
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    email.From = new MailAddress("administracion@vitrayaclinicacrecer.com");
                    email.Subject = "Solicitud de Firma de acta";
                    email.Body = "<p><img class=\" preload-me\" style=\"display: block; margin-left: auto; margin-right: auto;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" /></p>" +
                                   " <p>Cartagena de Indias D.T. y C &nbsp;" + DateTime.Now.ToString("D") + "</p>" +
                                   " <p>Sr(a): " + System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(usuario.GNNomUsu1.ToLower()) + "</p>" +
                                   " <p>&nbsp;</p>" +
                                   " <p>Cordial saludo,</p>" +
                                   " <p>&nbsp;</p>" +
                                   " <p>Se solicita firmar  acta de la reunión donde usted participo el día " + acta.DtmFechEditable.ToString("D") + " a través de link: <a href=\"http://131.0.169.22:8080/Log%20in/Login.aspx\">Vitrayaclinicacrecer</a>.</p>";
                    List<PAPlanAccion> compromisos = DAOPAPlanAccion.GetCompromisosActa(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()), usuario.GNCodUsu1);
                    if (compromisos.Count > 0)
                    {


                        email.Body += "<p>Recordandole que usted tiene los siguientes compromisos por cumplir, que &nbsp;han sido asignados el d&iacute;a &nbsp;" + acta.DtmFechEditable.ToString("D") + " por: &nbsp;" + acta.StrNombre + ".</p>";


                        string Sconpromisos = "" +
                        "<h5 class=\"text-center\">Compromisos</h5>" +
                        "<table CELLPADDING=5 border>" +
                        "   <thead>" +
                        "       <tr >" +
                        "           <th>Usuario Responsable</th>" +
                        "           <th>Acción de Mejora</td>" +
                        "           <th>Soporte</th>" +
                        "           <th>Usuario de Seguimiento</th>" +
                        "           <th>Fecha Limite</th>" +
                        "       </tr>" +
                        "   </thead>" +
                        "   <tbody>";

                        foreach (PAPlanAccion compromiso in compromisos)
                        {
                            Sconpromisos += "" +
                            "<tr>" +
                            "   <td>" + compromiso.StrNombreUsuarioResponsable + "</td>" +
                            "   <td>" + compromiso.StrActividad + "</td>" +
                            "   <td>" + compromiso.StrSoporte + "</td>" +
                            "   <td>" + compromiso.StrNombreUsuarioSeguimiento + "</td>" +
                            "   <td>" + compromiso.DtmFecFinalActa.ToString("MM/dd/yyyy") + "</td>" +
                            "</tr>";
                        }

                        Sconpromisos += "</tbody></table>";
                        if (compromisos.Count > 0)
                            email.Body += Sconpromisos;
                    }

                    email.Body += " <p>&nbsp;</p>" +
                        "<p>Gracias por su atenci&oacute;n.</p>";
                    email.IsBodyHtml = true;
                    email.Priority = MailPriority.Normal;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.vitrayaclinicacrecer.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("administracion@vitrayaclinicacrecer.com", "Clicrecer@2020");
                    try
                    {
                        smtp.Send(email);
                        email.Dispose();
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                return "0";
            }

            return "1";
        }

        protected void tablaTemas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tablaTemas.EditIndex = -1;
            llenarTablaTemas();
        }

        protected void tablaTemas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
            llenarTablaTemas();
        }

        protected void tablaTemas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = tablaTemas.Rows[e.RowIndex];

            TextBox txtNomTema = fila.FindControl("txtNombre") as TextBox;
            int idTema = Convert.ToInt32(tablaTemas.DataKeys[e.RowIndex].Values["IntOidARActasTemas"]);

            ARActasTemas tema = ActatasReunionLogica.GetActasTema(idTema);
            if (txtNomTema.Text != "")
            {
                tema.StrNomTema = txtNomTema.Text;
                ActatasReunionLogica.updateARActasTemas(tema);
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = tema.IntOidARActasTemas,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza el nombre del tema {tema.StrNomTema} del acta con código {tema.IntOidARActasC}",
                    strEntidad = "ARActasTemas"
                });
            }

            tablaTemas.EditIndex = -1;
            llenarTablaTemas();

        }

        protected void tablaTemas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            int index = e.NewEditIndex;
            //se pone la tabla en modo edicion en el indice de la fila de la tabla selecionada
            tablaTemas.EditIndex = index;

            llenarTablaTemas();
        }
    }

}
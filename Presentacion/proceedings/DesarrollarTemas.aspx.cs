using Comunes;
using Entidades.Generales;
using Entidades.PlanAccion;
using Entidades.Power_BI;
using Entidades.Procesos;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.Power_BI;
using Persistencia.proceedings;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Esta clase esta diseñada para desarrollar cada uno de los temas que que pertenecen a un acta de reunión
/// Por el parametro idTema que se encuentra en el Request se obtiene el tema a desarrollar 
/// </summary>

namespace Presentacion.proceedings
{

    public partial class DesarrollarTemas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDesarrolloTema();
                CargarDrops();
                tbCompromisos.Columns[0].Visible = false;
                cargarTablaCompromisos();
                CargarMenuTemas();
                CargarTablaArchivos();
            }
            if (IsPostBack)
            {
                cargarArchivo();
            }


            ////se recrea el editor  de texto cada vez que se realiza un postback
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "tinymce.remove('#ContentPlaceHolder_txtEditor');", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfe", "crearEditorTextos();", true);
        }

        //metodo que responde a los comandos de la tabla Compromisos
        protected void tbCompromisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //se obtiene la fila de la tabla que genero el comando
            LinkButton boton = (LinkButton)e.CommandSource;
            GridViewRow items = (GridViewRow)boton.NamingContainer;
            int idTema = Convert.ToInt32(tbCompromisos.DataKeys[items.RowIndex].Values["IntOidPAPlanAccion"]);
            //en caso de que el comando enviado se ael de eliminar
            if (e.CommandName == "eliminar")
            {


                //se elimina el compromiso selecionado
                DAOPAPlanAccion.delete(idTema);
                DAOPAUsuario.DeletePAUsuarioByIdPlan(idTema);

                // se vuelve a cargar la tabla de compromisos
            }
            if (e.CommandName == "editar")
            {
                PAPlanAccion compromiso = DAOPAPlanAccion.Get(idTema);
                txtLugar.Text = compromiso.StrDonde;
                txtFechaLimiteCompromiso.Text = compromiso.DtmFecFinalActa.ToString("yyyy-MM-dd");
                taActividad.Text = compromiso.StrActividad;
                taComo.Text = compromiso.StrComo;
                taCosto.Text = compromiso.StrCuanto;
                taMotivo.Text = compromiso.StrPorQue;
                taSoporteActividad.Text = compromiso.StrSoporte;
                ddlProceso.Text = compromiso.StrProceso;
                ddlReposnsableSeguimiento.Text = compromiso.IntcodUsuSegui + "";
                ddlResponsableActividad.Text = compromiso.IntGNCodUsu + "";
                Session["idCompromiso"] = compromiso.IntOidPAPlanAccion;
                upDesarrollo.Update();
            }
        }

        //metodo que carga los drops
        public void CargarDrops()
        {
            //se obtiene una lista de los usuarios que estan registrados
            //List<Usuario> participantes = DAOUsuario.getUsuarios();
            List<EAdministrarReportes> participantes = PAdministrarReportes.GetUsuarios();

            foreach (var participante in participantes)
            {
                //se pasan los datos de la lista de usuarios a los drop correspondientes
                //ddlReposnsableSeguimiento.Items.Add(new ListItem(participante.GNNomUsu1, participante.GNCodUsu1.ToString()));
                //ddlResponsableActividad.Items.Add(new ListItem(participante.GNNomUsu1, participante.GNCodUsu1.ToString()));

                ddlReposnsableSeguimiento.Items.Add(new ListItem(participante.NombrePermisoUsu, participante.CodigoPermisoUsu.ToString()));
                ddlResponsableActividad.Items.Add(new ListItem(participante.NombrePermisoUsu, participante.CodigoPermisoUsu.ToString()));
            }

            //se obtiene un a lista de los procesos que han sido creados
            List<PCProceso> procesos = DAOProceso.listar();
            foreach (var proceso in procesos)
            {

                //se pas la informacion de la lista de procesos al drop de los procesos
                ddlProceso.Items.Add(new ListItem(proceso.StrNomPro, proceso.StrNomPro));
            }
        }


        //metodo que devuelve el indice al que pertenece un tema de una lista de temas
        public static int GetIndexTemaByid(int idTema, List<ARActasTemas> temas)
        {
            //se recorre la lista de los temas
            for (int i = 0; i < temas.Count; i++)
            {
                //en caso de que se encuentre el tema por su id se devuelve el indice del tema encontrado
                if (temas[i].IntOidARActasTemas == idTema)
                    return i;
            }

            //en caso de que no se encuentre el tema se retorna -1
            return -1;
        }


        //evento que se desarrolla al dar clik sobre el boton next
        protected void btnNext_Click(object sender, EventArgs e)
        {

            ActasReunionLogica LogicaActa = new ActasReunionLogica();

            //se obteiene una lista de los temas que pertenecen al acta de ruenión 
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(Convert.ToInt32(Request["idActa"]));

            //se obtiene el indice a tratar y se le suma uno para  desarrollar el proximo tema
            int idNextTema = GetIndexTemaByid(Convert.ToInt32(Request["idTema"]), temas) + 1;
            ARActasTemas tema = temas[idNextTema - 1];
            tema.StrDesarrollo = txtEditor.Text;
            //se guardan los datos de tama desarrollado
            LogicaActa.updateARActasTemas(tema);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = tema.IntOidARActasTemas,
                strAccion = "Modificar",
                strDetalle = $"Se actualiza el desarrollo del tema {tema.StrNomTema} del acta con código {tema.IntOidARActasC}",
                strEntidad = "ARActasTemas"
            });
            //en caso de que el tama a desarrollar no se el ultimo se pasa a desarrollar el siguiente tema
            if (idNextTema < temas.Count)
                Response.Redirect("DesarrollarTemas.aspx?idTema=" + temas[idNextTema].IntOidARActasTemas + "&idActa=" + Request["idActa"]);
        }

        //evento que se desarrolla al dar clik sobre el boton previus
        protected void btnPrevius_Click(object sender, EventArgs e)
        {
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            //se obtiene una lista de los temas a desarrollar en el acta de la reunión
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(Convert.ToInt32(Request["idActa"]));

            //se obtiene el indice del tema a desarrollar y se le resta uno para desarrollar el tema anterior
            int idPrevTema = GetIndexTemaByid(Convert.ToInt32(Request["idTema"]), temas) - 1;
            ARActasTemas tema = temas[idPrevTema + 1];
            tema.StrDesarrollo = txtEditor.Text;
            //se guardan los datos del temadesarrollado
            LogicaActa.updateARActasTemas(tema);

            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = tema.IntOidARActasTemas,
                strAccion = "Modificar",
                strDetalle = $"Se actualiza el desarrollo del tema {tema.StrNomTema} del acta con código {tema.IntOidARActasC}",
                strEntidad = "ARActasTemas"
            });

            //en caso de que el tema desarrollado no sea el promero se retrosede a desarrollar el tema anterior
            if (idPrevTema >= 0)
                Response.Redirect("DesarrollarTemas.aspx?idTema=" + temas[idPrevTema].IntOidARActasTemas + "&idActa=" + Request["idActa"]);
        }

        //metodo que carga los datos del tema 
        public void cargarDesarrolloTema()
        {
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            ARActasTemas tema = LogicaActa.GetActasTema(Convert.ToInt32(Request["idTema"]));
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(Convert.ToInt32(Request["idActa"]));

            //se pasan los datos del desrrollo del tema al editor de texto
            txtEditor.Text = tema.StrDesarrollo;

            //se muestra el nombre del tema 
            nomTema.InnerText = HttpUtility.HtmlDecode(tema.StrNomTema);

            int indiceTema = GetIndexTemaByid(tema.IntOidARActasTemas, temas);

            if (indiceTema == temas.Count - 1)
            {
                btnNext.Visible = false;
                btnGuardarActa.Visible = true;
            }

            if (indiceTema == 0)
            {
                btnPrevius.Visible = false;
                btnRegresar.Visible = true;
                btnInicio.Visible = false;
            }
        }

        //metodo que limpia los campos que se usan para crear un compromiso al tema
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
            upCompromisos.Update();
        }


        //metodo que crea un compromiso para el tema tratado
        protected void btnGuardarCompromiso_Click(object sender, EventArgs e)
        {
            //se verifica que todos los datos para crear el compromiso esten completos
            if (string.IsNullOrEmpty(taActividad.Text) || string.IsNullOrEmpty(taSoporteActividad.Text)
                || string.IsNullOrEmpty(txtFechaLimiteCompromiso.Text) || ddlProceso.Text == "-1" || ddlReposnsableSeguimiento.Text == "-1"
                || string.IsNullOrEmpty(taMotivo.Text)
                || string.IsNullOrEmpty(taComo.Text) || string.IsNullOrEmpty(txtLugar.Text))
            {
                //se muetra un mensaje de error en caso de los datos estetn incompetos
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "error(\"Datos Incompletos\",\"Por favor complete todos los datos ante de crear el compromiso\");", true);
                return;
            }

            ARActasC acta = DAOARActasC.get(Convert.ToInt32(Request["idActa"]));

            string codigoActa = "";
            for (int i = 0; i < 4 - acta.IntCodigo.ToString().Length; i++)
            {
                codigoActa += "0";
            }

            codigoActa += acta.IntCodigo;


            PAPlanAccion compromiso = new PAPlanAccion();

            compromiso.DtmFecFinalActa = Convert.ToDateTime(txtFechaLimiteCompromiso.Text);
            compromiso.DtmFecIniActa = DateTime.Now;
            compromiso.IntCodUsuApr = Convert.ToInt32(Session["Admin"]);
            compromiso.IntcodUsuSegui = Convert.ToInt32(ddlReposnsableSeguimiento.Text);
            compromiso.IntEstAct = 1;
            compromiso.IntGNCodUsu = Convert.ToInt32(ddlResponsableActividad.Text);
            compromiso.IntOidARActasC = Convert.ToInt32(Request["idActa"]);
            compromiso.IntOidInstancia = Convert.ToInt32(Request["idTema"]);
            compromiso.StrActividad = taActividad.Text;
            compromiso.StrNombreUsuarioAprueba = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"])).GNNomUsu1;
            compromiso.StrNombreUsuarioResponsable = ddlResponsableActividad.SelectedItem.Text;
            compromiso.StrNombreUsuarioSeguimiento = ddlReposnsableSeguimiento.SelectedItem.Text;
            compromiso.StrNomEst = "Asignado";
            compromiso.StrSoporte = taSoporteActividad.Text;
            compromiso.StrComo = taComo.Text;
            compromiso.StrDonde = txtLugar.Text;
            compromiso.StrPorQue = taMotivo.Text;
            compromiso.StrCuanto = taCosto.Text;
            compromiso.IntOidPAPlanAccion = Convert.ToInt32(Session["idCompromiso"] ?? "0");
            compromiso.StrProceso = ddlProceso.Text;
            compromiso.IntContexto = PAPlanAccion.CONTEXTO.ACTAREUNION;
            compromiso.StrDescriptcion = "";
            compromiso.StrFuente = "";
            compromiso.StrOrigen = acta.StrSigla + codigoActa;



            PAPlanAccion plan;
            if (Session["idCompromiso"] == null)
            {
                DAOPAPlanAccion.set(compromiso);
                plan = DAOPAPlanAccion.GetPlanAccionUlt();
            }
            else
            {
                int idPlanAccion = (int)Session["idCompromiso"];
                DAOPAPlanAccion.UpdateCompromiso(compromiso);
                DAOPAUsuario.DeletePAUsuarioByIdPlan(idPlanAccion);
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = compromiso.IntOidPAPlanAccion,
                    strAccion = "Modificar",
                    strDetalle = "Se edita el plan de acción en la elaboración de un acta de reunión",
                    strEntidad = "PAPlanAccion"
                });
                Session.Remove("idCompromiso");
                plan = DAOPAPlanAccion.Get(idPlanAccion);
            }

            //se asigna el usuario creador del plan de accion y se guarda en la base de datos
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));


            List<ARActasDM> miembros = DAOARactasDM.GetCoordSec(Convert.ToInt32(Session["idActa"]));

            miembros.ForEach(miembro =>
            {
                usuario = DAOUsuario.getInstance().GetUsuario(miembro.IntGNCodUsu);
                PAUsuario usuarioAsignaodor = new PAUsuario
                {
                    IntOidGNUsuario = usuario.GNCodUsu1,
                    IntOidPAPlanAccion = plan.IntOidPAPlanAccion,
                    StrCargo = usuario.GnCargo1,
                    StrNombre = usuario.GNNomUsu1,
                    StrRol = PAUsuario.ASIGNADOR + "",
                };
                DAOPAUsuario.SetPAUSuario(usuarioAsignaodor);
            });

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

            //se limpian los datos de los controles que se usan para crear un nuevo compromiso
            LimpiarDatos();

            //se carga denuevo la tabla de los compromisos con los datos del compromiso recien creado
            cargarTablaCompromisos();

        }


        //metodo que carga la tabla compromisos con los datos que se encuentran en la base de datos
        public void cargarTablaCompromisos()
        {
            //se cre una lista de los datos de los compromisos
            List<PAPlanAccion> compromisos = DAOPAPlanAccion.Listar(Convert.ToInt32(Request["idTema"]));

            //se pasan los datos de la lista de los compromisos a la tabla de los compromisos
            tbCompromisos.DataSource = compromisos;

            //se actualiza la tabla de compromisos
            tbCompromisos.DataBind();
        }


        //metodo que carga un menú con los tema a tratar
        public void CargarMenuTemas()
        {
            //se obtiene una lista de los temas del acta de reunion 
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(Convert.ToInt32(Request["idActa"]));

            //se pasan los datos de la lista de los temas al menu de los temas con sus corespondientes links
            foreach (var tema in temas)
            {
                content_menu_left.InnerHtml += "<a href=\"javascript:menu(" + tema.IntOidARActasTemas + "," + Convert.ToInt32(Request["idActa"]) + ")\">" +
                        "<h6 " + ((tema.StrDesarrollo != "") ? "style=\"background:#76D7C4\"" : "") + ">" + tema.StrNomTema + "</h6>" +
                    "</a>";
            }
        }

        [WebMethod]
        public static string navMenu(int idTemaD, int idTema, int idActa, String desarrollo)
        {
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            //se obtiene una lista de los temas a desarrollar en el acta de la reunión
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(idActa);

            //se obtiene el indice del tema a desarrollar y se le resta uno para desarrollar el tema anterior
            int indice = GetIndexTemaByid(idTemaD, temas);
            var tema = temas[indice];

            tema.StrDesarrollo = desarrollo;
            //se guardan los datos del tema desarrollado
            LogicaActa.updateARActasTemas(tema);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = tema.IntOidARActasTemas,
                strAccion = "Modificar",
                strDetalle = $"Se actualiza el desarrollo del tema {tema.StrNomTema} del acta con código {tema.IntOidARActasC}",
                strEntidad = "ARActasTemas"
            });
            //en caso de que el tema desarrollado no sea el promero se retrosede a desarrollar el tema anterior
            indice = GetIndexTemaByid(idTema, temas);
            return "DesarrollarTemas.aspx?idTema=" + temas[indice].IntOidARActasTemas + "&idActa=" + idActa;
        }

        protected void cargarArchivo()
        {
            ActasReunionLogica LogicaActa = new ActasReunionLogica();

            ARActasTemas tema = LogicaActa.GetActasTema(Convert.ToInt32(Request["idTema"]));

            //se guardan los datos del temadesarrollado
            LogicaActa.updateARActasTemas(tema);

            // se varifica que el que input contenga un archivo
            if (fuArchivo.HasFile)
            {
                //se obtiene el archivo subido
                HttpPostedFile file = fuArchivo.PostedFile;

                //se obtiene el nombre del archivo
                string nombre = file.FileName.Substring(0, file.FileName.LastIndexOf("."));

                //se obtiene la extencion del archivo
                string ext = fuArchivo.PostedFile.FileName;
                ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();

                // Tipo de contenido
                string contentType = file.ContentType;
                // se obtine el array de bit del archivo
                byte[] imagen = new byte[file.InputStream.Length];


                file.InputStream.Read(imagen, 0, imagen.Length);



                //en caso de que nos exita una lista de los archivos par la capacitacion se crea una nueva
                if (tema.IntOidGNListaArchivos == 0)
                {
                    GNListaArchivos lstArch = new GNListaArchivos
                    {
                        IntOidGNModulo = 3
                    };

                    //se crea una nueva lista de archivos en la base de datos
                    DAOGNListaArchivos.set(lstArch);

                    //se obtiene la lista de archivos recien creada
                    lstArch = DAOGNListaArchivos.GetUltimo();

                    tema.IntOidGNListaArchivos = lstArch.IntOidGNListaArchivos;


                    LogicaActa.updateARActasTemas(tema);

                }

                // se crea un nuevo objeto de tipo GNArchivo con la informacion de del archivo subido por el input file
                GNArchivo archivo = new GNArchivo
                {
                    IntOidGNListaArchivos = tema.IntOidGNListaArchivos,
                    StrContenido = contentType,
                    StrExt = ext,
                    StrNombre = nombre,
                    AbteArchivo = imagen
                };


                // se gurada el archivo en la base de datos
                DAOGNArchivo.set(archivo);


                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri + "#boxUpLoadArch");
            }
        }

        //Metodo que carga una tabla con la informacion de los archivos que han sido subidos al tema en cuestion 
        public void CargarTablaArchivos()
        {
            //se obtiene el tema que se esta tratando
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            ARActasTemas tema = LogicaActa.GetActasTema(Convert.ToInt32(Request["idTema"]));

            //se crea una lista con la informacion de los archivos que pertenecen al tema
            List<GNArchivo> archivos = DAOGNArchivo.Listar(tema.IntOidGNListaArchivos);

            //se pasa la informacion de la lista de los archivos a la tabla
            tableArch.DataSource = archivos;
            tableArch.DataBind();
        }

        //eventos que se ejecutan en la tabla de lso archivos 
        protected void tableArch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //en caso de que el comando enviado sea eliminar 
            if (e.CommandName == "eliminar")
            {
                //se obtiene el boton que genero el evento
                LinkButton boton = (LinkButton)e.CommandSource;

                //la fila en lu que se encuentra el boton que genero el evento
                GridViewRow items = (GridViewRow)boton.NamingContainer;

                //se obtiene el id del archivo que se desea elimnar a traves del indice de la fila que genero el evento
                int id = Convert.ToInt32(items.Cells[0].Text);

                //se elimina el archivo de la base de datos
                DAOGNArchivo.deleteArchivo(id);
                
                CargarTablaArchivos();
            }
        }

        //metodo que envia un correo electrónico a los usuarioas a los cuales se les a asignado un compromiso
        public void EnviarCompromisos()
        {
            //se obtiene el acta que contiene los compromisos
            ARActasC acta = DAOARActasC.get(Convert.ToInt32(Session["idActa"].ToString()));

            //se obtiene una lista de los compromisos que se guardaron en el acta
            List<PAPlanAccion> compromisos = DAOPAPlanAccion.GetCompromisosActa(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));


            foreach (var compromiso in compromisos)
            {
                //se crea una lista de correos de los usuarios a quienes se les va a enviar el correo
                List<string> correos = new List<string>();

                //se obtiene el usuario a quien se le va a enviar el correo
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(compromiso.IntGNCodUsu);

                //se agrega el correo del usuario a la lista de usuarios
                correos.Add(usuario.GNCrusu1);

                //mensaje que se enviara
                string mensaje = "<p>Recordandole que usted tiene los siguientes compromisos por cumplir, que &nbsp;han sido asignados el d&iacute;a &nbsp;" + acta.DtmFechEditable.ToString("D") + " por: &nbsp;" + acta.StrNombre + ".</p>";

                mensaje += "<h5 class=\"text-center\">Compromisos</h5>" +
                        "<table CELLPADDING=5 border>" +
                        "   <thead>" +
                        "       <tr>" +
                        "           <th>Usuario Responsable</th>" +
                        "           <th>Acción de Mejora</td>" +
                        "           <th>Soporte</th>" +
                        "           <th>Usuario de Seguimiento</th>" +
                        "           <th>Fecha Limite</th>" +
                        "       </tr>" +
                        "   </thead>" +
                        "   <tbody>" +
                        "       <tr>" +
                        "           <td>" + compromiso.StrNombreUsuarioResponsable + "</td>" +
                        "           <td>" + compromiso.StrActividad + "</td>" +
                        "           <td>" + compromiso.StrSoporte + "</td>" +
                        "           <td>" + compromiso.StrNombreUsuarioSeguimiento + "</td>" +
                        "           <td>" + compromiso.DtmFecFinalActa.ToString("MM/dd/yyyy") + "</td>" +
                        "       </tr>" +
                        "   </tbody>" +
                        "</table>";

                //se envia el correo
                //Email.SendMail(correos, mensaje, "Compromisos pendientes Por cumplir");
            }
        }


        //metodo que envia un correo a cada uno de los asostentes de la runion
        public void EnviarSolicitudFirma()
        {
            //se obtiene el acta de la reunion 
            ARActasC acta = DAOARActasC.get(Convert.ToInt32(Session["idActa"].ToString()));

            //se obtiene una lista de los asistentes a la reunion del acta
            List<ARActasDM> asistentes = DAOARactasDM.getParticipantes(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));

            //se crea una lista de los coreeos 
            List<string> correos = new List<string>();

            //se agregan los correos de cada uno de los asistentes a la lista de correos 
            foreach (var asistente in asistentes)
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(asistente.IntGNCodUsu);
                correos.Add(usuario.GNCrusu1);
            }

            //mensaje que sera enviado
            string mensaje = "<p><img class=\" preload-me\" style=\"display: block; margin-left: auto; margin-right: auto;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" /></p>" +
                                   " <p>Cartagena de Indias D.T. y C &nbsp;" + DateTime.Now.ToString("D") + "</p>" +
                                   " <p>&nbsp;</p>" +
                                   " <p>Cordial saludo,</p>" +
                                   " <p>&nbsp;</p>" +
                                   " <p>Se solicita firmar  acta de la reunión donde usted participo el día " + acta.DtmFechEditable.ToString("D") + $@" a través de link: <a href=""{Request.Url.GetLeftPart(UriPartial.Authority)}/Index.aspx"">Vitrayaclinicacrecer</a>.</p>";

            //se envia el correo a cada uno de los asistentes
            //Email.SendMail(correos, mensaje, "Solicitud de Firma de Acta de Reunión");
        }

        //evento que guarda el contenido del desarrollo del tema y procede a desarrollar el tema anterior
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            //se obtiene una lista de los temas a desarrollar en el acta de la reunión
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(Convert.ToInt32(Request["idActa"]));

            //se obtiene el indice del tema a desarrollar y se le resta uno para desarrollar el tema anterior
            int indice = GetIndexTemaByid(Convert.ToInt32(Request["idTema"]), temas);
            var tema = temas[indice];

            //se guardan los datos del temadesarrollado
            LogicaActa.updateARActasTemas(tema);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = tema.IntOidARActasTemas,
                strAccion = "Modificar",
                strDetalle = $"Se actualiza el desarrollo del tema {tema.StrNomTema} del acta con código {tema.IntOidARActasC}",
                strEntidad = "ARActasTemas"
            });
            //en caso de que el tema desarrollado no sea el promero se retrosede a desarrollar el tema anterior

            Response.Redirect("RecordMinutes.aspx");
        }

        //metodo que se genera al darle click al boton de guardar y cerrar acta
        [WebMethod]
        public static bool btnGuardarActa_Click(int idTema, string desarrollo)
        {
            //se obtine el ulltimo tema a tratar 
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            ARActasTemas tema = LogicaActa.GetActasTema(idTema);

            tema.StrDesarrollo = desarrollo;
            //se guardan los datos del tema desarrollado
            LogicaActa.updateARActasTemas(tema);

            //se verifica que todos los temas tengan un desarrollo 
            List<ARActasTemas> temas = LogicaActa.GetARActasTemas(tema.IntOidARActasC);
            foreach (var tema1 in temas)
                if (string.IsNullOrEmpty(tema1.StrDesarrollo))
                    return false;

            return true;
        }

        //metodo que cierra el acta
        protected void btnGuardarAct_Click(object sender, EventArgs e)
        {
            //se obtiene el acta que se va a cerrar
            ActasReunionLogica LogicaActa = new ActasReunionLogica();
            ARActasC acta = LogicaActa.getActasC(Convert.ToInt32(HttpContext.Current.Session["idActa"].ToString()));

            //se le asigna el estado 2 que indica que el acta se encuentra cerrada
            acta.IntEstado = 2;


            //se actrualiza el acta en base de datos 
            DAOARActasC.update(acta);

            acta.IntUsuarioCreador = Convert.ToInt32(Session["Admin"]);

            DAOARActasC.UpdateUsuarioCreador(acta.IntUsuarioCreador, acta.IntOidARActas);

            //se envian los correos de los compromisos
            //EnviarCompromisos();

            //se envian los correos con la solicitud de firma del acta
            //EnviarSolicitudFirma();

            //se redirecciona a la opcion donde se visualiza la acta cerradas
            Response.Redirect("MisActas.aspx");
        }


        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecordMinutes.aspx");
        }

        protected void ddlResponsableActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReposnsableSeguimiento.Text == ddlResponsableActividad.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MssRrr2", "error(\"No Valido\",\"El Responsable de Activida no puede ser el mismo Responsable de seguimiento\");", true);
                ddlResponsableActividad.Text = "-1";
                upCompromisos.Update();
            }
        }

        protected void ddlReposnsableSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReposnsableSeguimiento.Text == ddlResponsableActividad.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MssRrr3", "error(\"No Valido\",\"El Responsable de Activida no puede ser el mismo Responsable de seguimiento\");", true);
                ddlReposnsableSeguimiento.Text = "-1";
                upCompromisos.Update();
            }
        }
    }
}
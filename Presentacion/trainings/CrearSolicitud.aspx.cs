using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.trainings
{
    public partial class CrearSolicitud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDropEjesTematicos();
                cargarDropUsuarios();
                CargarDropUnidades();
                CargarDdlUndadM();
                CargarDlCargosM();
            }

            cargarTablaSubtemas();

        }


        //metodo para cargar el drop de Usuarios
        public void cargarDropUsuarios()
        {
            ddlUsuarios.Items.Clear();
            ddlUsuariosM.Items.Clear();
            ddlUsuariosM.Items.Add(new ListItem("Seleccione Usuario", "-1"));
            ddlUsuarios.Items.Add(new ListItem("Seleccione", "-1"));
            List<Usuario> usuarios = DAOUsuario.getUsuarios();
            foreach (var usuario in usuarios)
            {
                ddlUsuarios.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1.ToString()));
                ddlUsuariosM.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNNomUsu1));
            }
        }


        public void CargarDlCargosM()
        {
            List<Cargo> cargos = DAOCargo.GetCargos();
            ddlCargosM.Items.Clear();
            ddlCargosM.Items.Add(new ListItem("Seleccione Cargo", "-1"));
            foreach (var cargo in cargos)
            {
                ddlCargosM.Items.Add(new ListItem(cargo.StrGnNomCgo, cargo.StrGnNomCgo));
            }
        }


        public void CargarDdlUndadM()
        {
            List<UnidadFuncional> unidades = DAOUnidadFuncional.GetInstance().listar();
            ddlUnidadesM.Items.Clear();
            ddlUnidadesM.Items.Add(new ListItem("Selecione Unidad Funcional", "-1"));
            foreach (var unidad in unidades)
            {
                ddlUnidadesM.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnNomDep1));
            }

        }
        //metodo para cargar el drop de ejes tematicos
        public void cargarDropEjesTematicos()
        {
            List<CPEJETEMATICO> ejesTematicos = DAOCPEjeTematico.ListarEjeTem();
            DropEje.Items.Clear();
            DropEje.Items.Add(new ListItem("Selecione", "-1"));
            foreach (var eje in ejesTematicos)
            {
                DropEje.Items.Add(new ListItem(eje.StrEJETEMATICO, eje.IntOidCPEJETEMATICO.ToString()));
            }
        }

        public void CargarDropUnidades()
        {
            List<UnidadFuncional> unidades = DAOUnidadFuncional.GetInstance().listar();
            DropUnidad.Items.Clear();
            DropUnidad.Items.Add(new ListItem("Seleccione", "-1"));
            foreach (var unidad in unidades)
            {
                DropUnidad.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnNomDep1));
            }

        }

        //se carga la tabla de subtemas con una lista de subtemas da la capacitacion
        public void cargarTablaSubtemas()
        {
            List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemas(Convert.ToInt32(Request["idCapacitacion"]));
            GridView2.DataSource = subtemas;
            GridView2.DataBind();
            upSubtemas.Update();
        }



        public bool ValidarDatos()
        {
            return string.IsNullOrEmpty(TextFecha.Text) ||
               string.IsNullOrEmpty(TextHoraFinal.Text) ||
               string.IsNullOrEmpty(TextHoraIni.Text) ||
               DropEje.Text == "-1" ||
               string.IsNullOrEmpty(TextLInk.Text) ||
               DropLugar.Text == "-1" ||
               DropModalidad.Text == "-1" ||
               ddlUsuarios.Text == "-1" ||
               Texttema.Text == "-1" ||
               DropUnidad.Text == "-1" ||
               !fuExamen.HasFile;
        }
        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "errorVal", "error('Datos Incompletos', 'Por favor llene todos los datos antes de continuar con el proceso')", true);
                return;
            }
            CPSolicitud solicitud = new CPSolicitud();

            solicitud.IntEstado = 0;
            solicitud.DtmFecha = Convert.ToDateTime(TextFecha.Text);
            solicitud.DtmHoraFinal = Convert.ToDateTime(TextHoraFinal.Text);
            solicitud.DtmHoraInicial = Convert.ToDateTime(TextHoraIni.Text);
            solicitud.IntGNCodUsu = Convert.ToInt32(Session["Admin"].ToString());
            solicitud.IntOidCPEjeTematico = Convert.ToInt32(DropEje.Text);
            solicitud.IntOidListaArchivos = Convert.ToInt32(Session["idListaArchivo"].ToString());
            solicitud.StrLink = TextLInk.Text;
            solicitud.StrLugar = DropLugar.SelectedItem.Text;
            solicitud.StrModalidad = DropModalidad.SelectedItem.Text;
            solicitud.StrResponsable = ddlUsuarios.SelectedItem.Text;
            solicitud.StrTema = Texttema.Text;
            solicitud.StrUnidadFuncional = DropUnidad.SelectedItem.Text;
            solicitud.IntOidGNAchivo = SetArchExamen().IntOidGNArchivo;
            solicitud.StrInfoMatricula = $"{txtUsuarios.Text} \n {txtUnidades.Text} \n {txtCargos.Text}";

            DAOCPSolicitud.SetCPSolictud(solicitud);

            solicitud = DAOCPSolicitud.GetSolicitudUlt();

            //se guarda la informacion de la tabla de los subtemas en la base datos
            var dtSubtemas = (List<CPSUBTEMA>)Session["dtSubtemas"];
            if (dtSubtemas != null)
                foreach (var subtema in dtSubtemas)
                {
                    subtema.IntOidCPInstacia = solicitud.IntOidCpsolicitud;
                    DAOCPSUBTEMA.setSubtema(subtema);
                }
            Session["idSolicitud"] = solicitud.IntOidCpsolicitud;

            //se rediraciona a la creacion del examen 
            Response.Redirect("AddCapacitacion.aspx", false);



        }


        public GNArchivo SetArchExamen()
        {
            HttpPostedFile file = fuExamen.PostedFile;

            string nombre = file.FileName.Substring(0, file.FileName.LastIndexOf("."));

            string ext = file.FileName;
            ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();

            // Tipo de contenido
            string contentType = file.ContentType;
            // se obtine el array de bit del archivo
            byte[] bteFile = new byte[file.InputStream.Length];


            file.InputStream.Read(bteFile, 0, bteFile.Length);

            GNArchivo archivo = new GNArchivo
            {
                IntOidGNListaArchivos = 0,
                AbteArchivo = bteFile,
                StrContenido = contentType,
                StrExt = ext,
                StrNombre = nombre,
            };

            DAOGNArchivo.set(archivo);

            return DAOGNArchivo.GetArchivoUlt();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static List<GNArchivo> GetArchivos()
        {
            try
            {
                int idListaArchivo = Convert.ToInt32(HttpContext.Current.Session["idListaArchivo"]);
                return DAOGNArchivo.Listar(idListaArchivo);
            }
            catch (Exception ex)
            {
                return new List<GNArchivo>();
            }
        }

        [WebMethod]
        public static void DaleteArchivo(int idArchivo)
        {
            //se elimina el archivo de la base de datos por su id
            DAOGNArchivo.deleteArchivo(idArchivo);
        }

        [WebMethod(EnableSession = true)]
        public static void SetArchivo(int IntOidGNArchivo, string base64StringFile, int IntOidGNListaArchivos, string StrContenido, string StrExt, string StrNombre)
        {

            GNArchivo archivo = new GNArchivo
            {
                AbteArchivo = Convert.FromBase64String(base64StringFile),
                StrContenido = StrContenido,
                StrExt = StrExt,
                StrNombre = StrNombre,
            };

            if (HttpContext.Current.Session["idListaArchivo"] == null)
            {
                GNListaArchivos listaArchivos = new GNListaArchivos
                {
                    IntOidGNModulo = 5
                };
                DAOGNListaArchivos.set(listaArchivos);
                listaArchivos = DAOGNListaArchivos.GetUltimo();
                HttpContext.Current.Session["idListaArchivo"] = listaArchivos.IntOidGNListaArchivos;
            }
            archivo.IntOidGNListaArchivos = Convert.ToInt32(HttpContext.Current.Session["idListaArchivo"]);
            DAOGNArchivo.set(archivo);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //en caso que el TextBox2 este vacio no se permita crear el subtema
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                // se crea una lista de subtemas
                List<CPSUBTEMA> dtSubtemas;

                //en caso de que la lista de no exita se crea una nueva
                if (Session["dtSubtemas"] == null)
                {
                    dtSubtemas = new List<CPSUBTEMA>();
                }
                //en caso contrario se obtiene la lista de guardada en la en la variable de Session
                else
                {
                    dtSubtemas = (List<CPSUBTEMA>)Session["dtSubtemas"];
                }
                //se crea un nuevo subtema y se agrega a la lista de subtemas
                dtSubtemas.Add(new CPSUBTEMA { StrSUBTEMA = TextBox2.Text, IntContexto = 1 });

                //se guarda la lista de subtemas en la variable de Session
                Session["dtSubtemas"] = dtSubtemas;

                //se pasa la informacion de lista  a la tabla de los subtemas
                GridView2.DataSource = dtSubtemas;

                //se actualisa la tabla
                GridView2.DataBind();
                upSubtemas.Update();
            }
        }

        protected void ddlUsuariosM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUsuarios.Text += ddlUsuariosM.Text + "\n";
            ddlUsuariosM.Text = "-1";
        }

        protected void ddlCargosM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCargos.Text += ddlCargosM.Text + "\n";
            ddlCargosM.Text = "-1";
        }

        protected void ddlUnidadesM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUnidades.Text += ddlUnidadesM.Text + "\n";
            ddlUnidadesM.Text = "-1";
        }
    }
}
using Generales_1._0.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using Generales_1._0.Class.DAOGenerales;
using Generales_1._0.Class.DTOGenerales;
using Generales_1._0.Class.DAO;
using Generales_1._0.Class.Objetos;
using Generales_1._0.Class.DTA;
using Entidades.Generales;
using Persistencia.Generales;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class CreateUsuarios : System.Web.UI.Page
    {
        
        Conexion cone = new Conexion();
        
      

        StringBuilder comandos = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                verification();
                cargarDropUnidadFuncional();
                CargarDropCargos();
                CargarDropRoles();
                CargarDropEps();
            }
        }
        private void verification()
        {
            try
            {

                int admin = Convert.ToInt32(Session["adimin"]);
                
            }
            catch (Exception)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
            }
            finally
            {
                cone.CloseConnection();
            }

            
            validacionRoles();
        }

        public void validacionRoles()
        {
            int idUsuario = Convert.ToInt32(Session["admin"]);
            int idRol = Class.DAOGenerales.DAOGNRoles.buscarRolUsuario(idUsuario);

            List<GNPermisos> permisos = DAOGNPermisos.get(idRol);
            foreach (GNPermisos permiso in permisos)
            {
                List<GNOpciones> opciones = DAOGNOpciones.listar();
                foreach (GNOpciones opcion in opciones)
                {
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 1) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        menu_1.Visible = true;
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 2) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        menu_2.Visible = true;
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 3) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        menu_3.Visible = true;
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 4) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        menu_4.Visible = true;
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 5) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        menu_5.Visible = true;
                    if ((opcion.IntOidGNOpcion == permiso.IntOidGNOpcion &&
                        opcion.IntOidGNModulo == 3) && (permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar))
                        Li1.Visible = true;
                }
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 1))
                    sub_menu_1.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 2))
                    sub_menu_2.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 3))
                    sub_menu_3.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 4))
                    sub_menu_4.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 5))
                    sub_menu_5.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 6))
                    sub_menu_6.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 7))
                    sub_menu_7.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 1))
                    sub_menu_1.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 8))
                    sub_menu_8.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 9))
                    sub_menu_9.Visible = true;


                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 10))
                    sub_menu_10.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 11))
                    sub_menu_14.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 12))
                    sub_menu_15.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 13))
                    sub_menu_16.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                   (permiso.IntOidGNOpcion == 14))
                    sub_menu_17.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 15))
                    sub_menu_18.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 16))
                    sub_menu_19.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 17))
                    sub_menu_20.Visible = true;

                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 18))
                    sub_menu_21.Visible = true;
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 19))
                    Li2.Visible = true;
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                    (permiso.IntOidGNOpcion == 20))
                    Li3.Visible = true;
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                   (permiso.IntOidGNOpcion == 21))
                    Li4.Visible = true;
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                   (permiso.IntOidGNOpcion == 22))
                    Li5.Visible = true;
                if ((permiso.BlnConfirmar || permiso.BlnCrear || permiso.BlnEliminar || permiso.BlnModificar) &&
                   (permiso.IntOidGNOpcion == 23))
                    sub_menu_22.Visible = true;

            }
        }
        public void cargarDropUnidadFuncional()
        {
            List<Class.DTO.UnidadFuncional> unidadesFuncionales = DAOUnidadFuncional.GetInstance().listar();
            foreach (var unidad in unidadesFuncionales)
            {
                txtUnidadFuncional.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }
        }

        public void CargarDropRoles()
        {
            List<GNRoles> roles = DAOGNRoles.listar();
            foreach (var rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.StrNombre, rol.IntOidGNRol.ToString()));
            }
        }

        public void CargarDropCargos()
        {
            List<Cargo> cargos = DAOCargo.GetCargos();
            foreach (var cargo in cargos)
            {
                txtCargo.Items.Add(new ListItem(cargo.StrGnNomCgo, cargo.IntGnIdCgo.ToString()));
            }
        }
        public void CargarDropEps()
        {
            List<Eps> listaEps = DAOEps.ListaEps();
            foreach (var eps in listaEps)
            {
                ddlEps.Items.Add(new ListItem(eps.StrGnNomEps, eps.IntGnIdEps.ToString()));
            }
        }

        [WebMethod]
        public static string cargarTablaUsuarios(string documento, string nombre, string cargo, string email, string estado)
        {
            Usuario usuario = new Usuario
            {
                GnUnfun1 = documento,
                GNNomUsu1 = nombre,
                GnCargo1 = cargo,
                GNCrusu1 = email,
                GnEtUsu1 = estado
            };

            List<Usuario> usuarios = DAOUsuario.getUsuarios(usuario);
            return Newtonsoft.Json.JsonConvert.SerializeObject(usuarios);
        }

        protected void btnCrearUsuarios_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"El campo Documento no puede estar vacio\")", true);
                return;
            }
            else if (txtCargo.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Falta por escoger el Cargo\")", true);
                return;
            }
            else if (txtEmail.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"El campo Correo Electrónico no puede estar vacio\")", true);
                return;
            }
            else if (ddlEps.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Hacer falta escoger una EPS\")", true);
                return;
            }
            else if (txtNombre.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"El campo Nombre y Apellido no puede esta vacio\")", true);
                return;
            }
            else if (txtPasssword.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"El campo de Contraseña no puede estar vacio\")", true);
                return;
            }
            else if (txtTelefono.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"El campo Número de Teléfono no puede estar vacio\")", true);
                return;
            }
            else if (txtUnidadFuncional.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Hace falta escoger una Unidad Fincional\")", true);
                return;
            }
            else if (ddlRoles.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dfa", "error(\"Error\",\"Hace falta escoger un Rol\")", true);
                return;
            }

            
            

            HttpPostedFile filefoto = fuImagePerfil.PostedFile;
            HttpPostedFile filefirma = txtFoto.PostedFile;

            byte[] foto = new byte[filefoto.InputStream.Length];
            filefoto.InputStream.Read(foto, 0, foto.Length);

            byte[] firma = new byte[filefirma.InputStream.Length];
            filefirma.InputStream.Read(firma, 0, firma.Length);

            Usuario usuario = new Usuario
            {
                CodigoR = Convert.ToInt32(ddlRoles.Text),
                GnCargo1 = txtCargo.SelectedItem.Text,
                GNCodUsu1 = Convert.ToInt32(txtDocumento.Text),
                GNConUsu1 = txtPasssword.Text,
                GNCrusu1 = txtEmail.Text,
                GnDcCgo1 = Convert.ToInt32(txtCargo.Text),
                GnDcDep1 = Convert.ToInt32(txtUnidadFuncional.Text),
                GnEpsUsu1 = Convert.ToInt32(ddlEps.Text),
                GnEtUsu1 = "Activo",
                GNFhUsu1 = DateTime.Now,
                GNNomUsu1 = txtNombre.Text,
                GnUnfun1 = txtUnidadFuncional.SelectedItem.Text,
                GnTlUsu1 = txtTelefono.Text,
                GNFmUsu1 = firma,
                GNFtUsu1 = foto,
            };
            DAOUsuario.set(usuario);

            ddlRoles.Text = "-1";
            txtCargo.Text = "-1";
            txtDocumento.Text = "";
            txtEmail.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtUnidadFuncional.Text = "-1";
            ddlEps.Text = "-1";

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }
    }
}
    
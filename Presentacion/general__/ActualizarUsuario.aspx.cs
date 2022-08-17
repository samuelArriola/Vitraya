using Entidades.Generales;
using Generales_1._0.Class.DAO;
using Generales_1._0.Class.DAOGenerales;
using Generales_1._0.Class.DTA;
using Generales_1._0.Class.DTO;
using Generales_1._0.Class.DTOGenerales;
using Generales_1._0.Class.Objetos;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class ActualizarUsuario : System.Web.UI.Page
    {
        Conexion cone = new Conexion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                verification();
                cargarDropUnidadFuncional();
                CargarDropCargos();
                CargarDropRoles();
                CargarDropEps();
                CargarUsuario();
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
            List<UnidadFuncional> unidadesFuncionales = DAOUnidadFuncional.GetInstance().listar();
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

        public void CargarUsuario()
        {
            int idUsuario = Convert.ToInt32(Request["idUsuario"]);

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);
            Session["usuarioActualizar"] = usuario;

            txtDocumento.Text = usuario.GNCodUsu1.ToString();
            txtCargo.Text = usuario.GnDcCgo1.ToString();
            txtEmail.Text = usuario.GNCrusu1;
            txtTelefono.Text = usuario.GnTlUsu1;
            txtUnidadFuncional.Text = usuario.GnDcDep1.ToString();
            ddlEps.Text = usuario.GnEpsUsu1.ToString();
            ddlRoles.Text = usuario.CodigoR.ToString();
            txtNombre.Text = usuario.GNNomUsu1;
            txtTelefono.Text = usuario.GnTlUsu1;
        }

        protected void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuarioActualizar"];

            if (!(txtDocumento.Text == ""))
            {
                usuario.GNCodUsu1 = Convert.ToInt32(txtDocumento.Text);
            }
            if (!(txtCargo.Text == "-1"))
            {
                usuario.GnCargo1 = txtCargo.SelectedItem.Text;
                usuario.GnDcCgo1 = Convert.ToInt32(txtCargo.Text);
            }
            if (!(txtEmail.Text == ""))
            {
                usuario.GNCrusu1 = txtEmail.Text;
            }
            if (!(ddlEps.Text == "-1"))
            {
                usuario.GnEpsUsu1 = Convert.ToInt32(ddlEps.Text);
            }
            if (!(txtNombre.Text == ""))
            {
                usuario.GNNomUsu1 = txtNombre.Text;
            }
            if (!(txtPasssword.Text == ""))
            {
                usuario.GNConUsu1 = txtPasssword.Text;
            }
            if (!(txtTelefono.Text == ""))
            {
                usuario.GnTlUsu1 = txtTelefono.Text;
            }
            if (!(txtUnidadFuncional.Text == "-1"))
            {
                usuario.GnDcDep1 = Convert.ToInt32(txtUnidadFuncional.Text);
                usuario.GnUnfun1 = txtUnidadFuncional.SelectedItem.Text;
            }
            if (!(ddlRoles.Text == "-1"))
            {
                usuario.CodigoR = Convert.ToInt32(ddlRoles.Text);
            }

            if (txtFoto.HasFile)
            {
                HttpPostedFile filefirma = txtFoto.PostedFile;
                byte[] firma = new byte[filefirma.InputStream.Length];
                filefirma.InputStream.Read(firma, 0, firma.Length);
                usuario.GNFmUsu1 = firma;
            }
            if (fuImagePerfil.HasFile)
            {
                HttpPostedFile filefoto = fuImagePerfil.PostedFile;
                byte[] foto = new byte[filefoto.InputStream.Length];
                filefoto.InputStream.Read(foto, 0, foto.Length);
                usuario.GNFtUsu1 = foto;
            }

            DAOUsuario.update(usuario);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }
    }
}
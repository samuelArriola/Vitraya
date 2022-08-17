using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class ActualizarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDropUnidadFuncional();
                CargarDropCargos();
                CargarDropRoles();
                CargarDropEps();
                CargarUsuario();
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

        //metodo  que carga la informacion de los roles en un drop
        public void CargarDropRoles()
        {
            //se consuolta un listado de todos los roles en la base de datos 
            List<GNRoles> roles = DAOGNRoles.listar();

            //por cada rol en la lista de los roles se agrega un item al drop
            foreach (var rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.StrNombre, rol.IntOidGNRol.ToString()));
            }
        }

        //metodo que carga los cargos al drop de los cargos
        public void CargarDropCargos()
        {
            //se consulta un listado de los cargos en base de datos
            List<Cargo> cargos = DAOCargo.GetCargos();

            //por cada cargo en el listado se crea un nuevo item en el drop
            foreach (var cargo in cargos)
            {
                txtCargo.Items.Add(new ListItem(cargo.StrGnNomCgo, cargo.IntGnDcCgo.ToString()));
            }
        }

        //metodo que carga el listado de las eps en el drop
        public void CargarDropEps()
        {
            //se consulta un listado de las EPS en base de datos
            List<GNEps> listaEps = DAOGNEps.ListarEps("");
            
            //por cada EPS en la base de datos se creun nuevo item en  el drop
            foreach (var eps in listaEps)
            {
                ddlEps.Items.Add(new ListItem(eps.StrNomEps, eps.IntOidGNEps.ToString()));
            }
        }


        //metodo que carga la informacion de usuario a actualizar en el formulario de actualización de datos 
        public void CargarUsuario()
        {
            //se consulta el id del usuario a traves de los parametos en la url del navegador
            int idUsuario = Convert.ToInt32(Request["idUsuario"]);

            //se conulta la informacion del usuario en la base de datos
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);
            
            //se guarda esta informacion en la variable de sesion 
            Session["usuarioActualizar"] = usuario;

            //se muestra la infomacion de usurio en el formulario de actualizacion de datos 
            txtDocumento.Text = usuario.GNCodUsu1.ToString();
            txtCargo.Text = usuario.GnDcCgo1.ToString();
            txtEmail.Text = usuario.GNCrusu1;
            txtTelefono.Text = usuario.GnTlUsu1;
            txtUnidadFuncional.Text = usuario.GnDcDep1.ToString();
            ddlEps.Text = usuario.GnEpsUsu1.ToString();
            ddlRoles.Text = usuario.CodigoR.ToString();
            txtNombre.Text = usuario.GNNomUsu1;
            txtTelefono.Text = usuario.GnTlUsu1;
            ddlEstadoUsuario.Text = usuario.GnEtUsu1;


        }

        //accion que se cumple al dar clic en el boton de actualizar 
        protected void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            // se consulta el usuario que se desea actualizar en la variable de sesion
            Usuario usuario = (Usuario)Session["usuarioActualizar"];

            // se valida que la información para la actualizadcion de datos este completa 
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
            usuario.GnEtUsu1 = ddlEstadoUsuario.Text;

            //se actualiza el usuario en base de datos con la informacion que se encuentra en base de datos 
            DAOUsuario.update(usuario);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = usuario.GNCodUsu1,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se Modifica el usuario {usuario.GNNomUsu1}",
                dtmFecha = DateTime.Now,
                strEntidad = "Usuario"
            });
        }

        //evento que actualiza es estado del usuario en cuestion 
        protected void ddlEstadoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se consuta el usuario en base de datos con el id que se encuentra como parametro en la url del navegador
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Request["idUsuario"]));
            
            //se cambia el estado del usuario deacuerdo con la iformacion de ddlEstadoUsuario
            usuario.GnEtUsu1 = ddlEstadoUsuario.Text;

            //se acutliza al usuario en base de datos 
            DAOUsuario.update(usuario);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = usuario.GNCodUsu1,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se modifica el estado del usuario a: {usuario.GnEtUsu1}",
                dtmFecha = DateTime.Now,
                strEntidad = "Usuario"
            });


            //se muestra un mensaje que nos indica que el usuario se ha actualizado
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg1", @"exito(""Hecho"",""El estado del usuario ha sido actualzado"")", true);
        }
    }
}
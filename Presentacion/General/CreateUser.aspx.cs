using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDropUnidadFuncional();
            CargarDropCargos();
            CargarDropRoles();
            CargarDropEps();
        }

        //metodo que carga un listado de las unidades fucnionales al drop correspondiente 
        public void cargarDropUnidadFuncional()
        {
            //se consulta un listado de las unidades fucndionales que se encuentran en base de datos
            List<UnidadFuncional> unidadesFuncionales = DAOUnidadFuncional.GetInstance().listar();
            
            //por cada unidad funcional en la lista se crea un nuevo item en el drop
            foreach (var unidad in unidadesFuncionales)
            {
                txtUnidadFuncional.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }
        }

        //metodo que carga un listado de los roles en el drop correspondiente
        public void CargarDropRoles()
        {
            //se consulta un listado de los roles en la base de datos
            List<GNRoles> roles = DAOGNRoles.listar();
            
            //por cada rol en el listado se crea un item en el drop
            foreach (var rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.StrNombre, rol.IntOidGNRol.ToString()));
            }
        }

        //metodo que carga un listado de los cargos en el drop correspondiente
        public void CargarDropCargos()
        {
            //se consulta un listado de los cargos que3 se encuentran en la base de catos
            List<Cargo> cargos = DAOCargo.GetCargos();
            
            //por cada cargo en la lista se crea un nuevo item el el drop
            foreach (var cargo in cargos)
            {
                txtCargo.Items.Add(new ListItem(cargo.StrGnNomCgo, cargo.IntGnDcCgo.ToString()));
            }
        }

        //metodo que carga un listado de las eps en el drop corresponediente 
        public void CargarDropEps()
        {
            //se consulta el listado de las eps en la base de datos 
            List<GNEps> listaEps = DAOGNEps.ListarEps("");
            
            //por cada eps en el listado se crea un nuevo item el drop
            foreach (var eps in listaEps)
            {
                ddlEps.Items.Add(new ListItem(eps.StrNomEps, eps.IntOidGNEps.ToString()));
            }
        }


        //metodo web que retorna un listado de los usuarios
        [WebMethod]
        public static string cargarTablaUsuarios(string documento, string nombre, string cargo, string email, string estado)
        {
            //se crea una instancia de usuario en la que se gurdara la informacion para filtrar la consulta de los usuarios
            Usuario usuario = new Usuario
            {
                GnUnfun1 = documento,
                GNNomUsu1 = nombre,
                GnCargo1 = cargo,
                GNCrusu1 = email,
                GnEtUsu1 = estado
            };

            //se consulta el listado de los usurios deacuerdo a los filtros suministrados por el usuario 
            List<Usuario> usuarios = DAOUsuario.getUsuarios(usuario);
            return Newtonsoft.Json.JsonConvert.SerializeObject(usuarios);
        }

        //evento del boton para la creacion de los usuarios 
        protected void btnCrearUsuarios_Click(object sender, EventArgs e)
        {
            //se valida que la informacion del formulario no se encuentre vacia 
            
            //se octiene la imagen de perfil de usuario
            HttpPostedFile filefoto = fuImagePerfil.PostedFile;

            //se obtiene la imagen de la firma del usuario
            HttpPostedFile filefirma = txtFoto.PostedFile;

            //se obtiene los datos vinarios de las imagenes 
            byte[] foto = new byte[filefoto.InputStream.Length];
            filefoto.InputStream.Read(foto, 0, foto.Length);

            byte[] firma = new byte[filefirma.InputStream.Length];
            filefirma.InputStream.Read(firma, 0, firma.Length);

            //se crea una instacia de usuario con la informacion de para su creacion en base de datos 
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

            //se crea al usuario en base de datos 
            DAOUsuario.set(usuario);


            //se limpian los controles del formulario de creacion de usuario
            ddlRoles.Text = "-1";
            txtCargo.Text = "-1";
            txtDocumento.Text = "";
            txtEmail.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtUnidadFuncional.Text = "-1";
            ddlEps.Text = "-1";

        }
    }
}
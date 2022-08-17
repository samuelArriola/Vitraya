using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuario();
            }
        }

        protected void btnActualizarUsuario_Click(object sender, EventArgs e)
        {

        }

        //Metodo que carga la informacion del usuario en los controles del formulario
        public void CargarUsuario()
        {
            //se consulta el id del usuario en la variable de sesion
            int idUsuario = Convert.ToInt32(Session["Admin"]);

            //se consulta al usuario en la base de datos por su id 
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);

            //se consulta el rol de usuario
            GNRoles rol = DAOGNRoles.GetRol(usuario.CodigoR);

            //se consulta la eps del usuario
            GNEps eps = DAOGNEps.GetGNEps(usuario.GnEpsUsu1);

            //se pasa la informacion consutla en los controles 
            txtDocumento.InnerText = usuario.GNCodUsu1.ToString();
            txtCargo.InnerText = usuario.GnCargo1.ToString();
            txtEmail.InnerText = usuario.GNCrusu1;
            txtTelefono.InnerText = usuario.GnTlUsu1;
            txtUnidadFuncional.InnerText = usuario.GnUnfun1.ToString();
            ddlEps.InnerText = eps == null ? " " :  eps.StrNomEps;
            ddlRoles.InnerText = rol == null ? " " : rol.StrNombre;
            txtNombre.InnerText = usuario.GNNomUsu1;
            txtTelefono.InnerText = usuario.GnTlUsu1;
            txtNombreE.Text = usuario.GNNomUsu1;
            txtTelefonoE.Text = usuario.GnTlUsu1;
            txtEmailE.Text = usuario.GNCrusu1;

            //se carga la innagen de perfil de usuario
            if (usuario.GNFtUsu1.Length > 0)
            {
                imagePerfil1.Attributes["style"] = $@"background-image: url(""data:image/gif;base64,{Convert.ToBase64String(usuario.GNFtUsu1)}"");";
                imagePerfil2.Attributes["style"] = $@"background-image: url(""data:image/gif;base64,{Convert.ToBase64String(usuario.GNFtUsu1)}"");";
            }
            else
            {
                imagePerfil1.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";
                imagePerfil2.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";
            }
        }

        //evento que se generea del boton de actualizar usuario
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //se 
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));
            
            usuario.GNNomUsu1 = txtNombreE.Text;
            usuario.GNCrusu1 = txtEmailE.Text;
            usuario.GnTlUsu1 = txtTelefonoE.Text;

            if (fuFotoPerfil.HasFile)
            {
                byte[] foto = null;
                using (BinaryReader reader = new BinaryReader(fuFotoPerfil.PostedFile.InputStream))
                {
                    foto = reader.ReadBytes(fuFotoPerfil.PostedFile.ContentLength);
                }

                usuario.GNFtUsu1 = foto;
            }

            if(usuario.FechaCambioPass == null)
            {
                usuario.FechaCambioPass = DateTime.Now.AddDays(180);
            }

            DAOUsuario.update(usuario);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = usuario.GNCodUsu1,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"El usuario {usuario.GNNomUsu1} actualiza su informacion de perfil",
                dtmFecha = DateTime.Now,
                strEntidad = "Usuario"
            });


            GNRoles rol = DAOGNRoles.GetRol(usuario.CodigoR);
            GNEps eps = DAOGNEps.GetGNEps(usuario.GnEpsUsu1);

            txtDocumento.InnerText = usuario.GNCodUsu1.ToString();
            txtCargo.InnerText = usuario.GnCargo1.ToString();
            txtEmail.InnerText = usuario.GNCrusu1;
            txtTelefono.InnerText = usuario.GnTlUsu1;
            txtUnidadFuncional.InnerText = usuario.GnUnfun1.ToString();
            ddlEps.InnerText = eps == null ? " " : eps.StrNomEps;
            ddlRoles.InnerText = rol == null ? " " : rol.StrNombre;
            txtNombre.InnerText = usuario.GNNomUsu1;
            txtTelefono.InnerText = usuario.GnTlUsu1;
            txtNombreE.Text = usuario.GNNomUsu1;
            txtTelefonoE.Text = usuario.GnTlUsu1;
            txtEmailE.Text = usuario.GNCrusu1;

            if (usuario.GNFtUsu1.Length > 0)
            {
                imagePerfil1.Attributes["style"] = $@"background-image: url(""data:image/gif;base64,{Convert.ToBase64String(usuario.GNFtUsu1)}"");";
                imagePerfil2.Attributes["style"] = $@"background-image: url(""data:image/gif;base64,{Convert.ToBase64String(usuario.GNFtUsu1)}"");";
            }
            else
            {
                imagePerfil1.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";
                imagePerfil2.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg1", @"$(document).ready(function (e) {exito(""Exito!"", ""Sus datos han sido actualizados correctamente"")});", true);
        }
    }
}
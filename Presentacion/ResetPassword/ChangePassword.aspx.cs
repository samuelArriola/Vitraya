using Logica.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.Generales;
using Persistencia.Generales;
using System.Text.RegularExpressions;

namespace Presentacion.ResetPassword
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var uuid = Encryption.Decrypt(Request["k"].ToString());
            GNCtrlCambioPass cambioPass = DAOGNCtrlCambioPass.GetCtrlCambioPass(uuid);
            if(cambioPass == null || cambioPass.BlnCambiada)
            {
                Response.Redirect("~/page_404.aspx");
            }
        }


        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var uuid = Encryption.Decrypt(Request["k"].ToString());
            GNCtrlCambioPass cambioPass = DAOGNCtrlCambioPass.GetCtrlCambioPass(uuid);

            var exp = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{8,}$");
            if(txtPass.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg1", @"error("""",""Por favor ingrese un usuario valido"");", true);
                return;
            }

            try
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(txtUser.Text));
                if (usuario == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg2", @"error("""",""Por favor ingrese un usuario valido"");", true);
                    return;
                }
                if (!exp.IsMatch(txtPass.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg3", @" error(""Contraseña insegura"", ""La contraseña debe tener mínimo ocho caracteres, al menos una letra mayúscula, una letra minúscula y un número"");", true);
                    return;
                }
                if(txtPass.Text != txtRepeatPass.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg4", @" error(""Contraseñas no coinciden"", ""El campo nueva contraseña y repita contraseña no coinciden"");", true);
                    return;
                }
                if(usuario.GNCodUsu1 != cambioPass.DblGNCodUSu)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg5", @"error("""",""Por favor ingrese un usuario valido"");", true);
                    return;
                }

                usuario.GNConUsu1 = txtPass.Text;
                usuario.FechaCambioPass = DateTime.Now.AddDays(180);
                DAOGNCtrlCambioPass.UpdateCtrlCambioPass(cambioPass.StrOidGNCtrlCambioPass);
                DAOUsuario.update(usuario);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg5",
                    @"exito(""Su contraseña ha sido cambiada"",""Su Contraseña ha sido cambiada, por favor inicie sesión"");
                    setTimeout(function(e){window.location.href = ""../Index.aspx""},3000,false)", 
                    true
                );

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = usuario.GNCodUsu1,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"El usuario {usuario.GNNomUsu1} actualiza su contraseña por olvido de esta",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Usuario"
                });

            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg6", @"error("""",""Por favor ingrese un usuario valido"");", true);
                return;
            }
        }
    }
}
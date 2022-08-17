using Logica.Generales;
using Persistencia;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Generales_1._0.Log_in
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        CloseAllConnections cerrar = new CloseAllConnections();
        Encryption Encriptacion = new Encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ParametersQueryValidation"] == null)
            {
                Response.Redirect("Login.aspx?ParametersQueryValidation=Accion realizada no permitida");
            }
            else
            {
                contraseña.Visible = false;
                solicitar.Visible = true;
                requerido.Visible = false;
                Div2.Visible = false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != "")
            {
                try
                {
                    SqlCommand buscar;
                    SqlDataReader rd;

                    buscar = new SqlCommand("select * from Usuario where Usuario.GNCodUsu = " + TextBox3.Text + "", con.OpenConnection());
                    buscar.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(buscar);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    rd = buscar.ExecuteReader();

                    if (rd.Read())
                    {
                        string correo = rd["GNCrusu"].ToString();
                        string fecahaEcnriptada = HttpUtility.UrlEncode(Encryption.Encrypt("" + DateTime.Now.AddHours(1).ToString() + ""));
                        string usuarioEncript = HttpUtility.UrlEncode(Encryption.Encrypt("" + TextBox3.Text + ""));
                        //new Email().SendMail(correo, "http://localhost:44342/Log%20in/NewPassword.aspx?ParametersQueryValidationNull=" + fecahaEcnriptada+"&Encript="+usuarioEncript+"", "Cambio de contraseña");
                        TextBox3.Enabled = false;
                        Button2.Enabled = false;
                        contraseña.Visible = true;
                    }
                    else
                    {
                        contraseña.Visible = false;
                        requerido.Visible = true;
                        Label4.Text = "El usuario ingresado no se encuentra registrado en el sistema";
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                contraseña.Visible = false;
                requerido.Visible = true;
                Label4.Text = "Favor ingresar el codigo del usuario";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Log in/Login.aspx");
        }
    }
}
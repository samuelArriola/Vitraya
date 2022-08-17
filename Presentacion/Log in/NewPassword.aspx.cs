using Logica.Generales;
using Persistencia;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Generales_1._0.Log_in
{
    public partial class NewPassword : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        CloseAllConnections cerrar = new CloseAllConnections();
        Encryption Encriptacion = new Encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ParametersQueryValidationNull"] == null && Request.QueryString["Encript"] == null)
            {
                Response.Redirect("Login.aspx?ParametersQueryValidation=Accion realizada no permitida");
            }
            else
            {
                contraseña.Visible = false;
                solicitar.Visible = true;
                requerido.Visible = false;
                Div2.Visible = false;
                string numero1 = Encryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ParametersQueryValidationNull"]).ToString());
                string numero2 = Encryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Encript"]).ToString());

                //System.Windows.Forms.MessageBox.Show(numero1 + " " + numero2);
                Label3.Text = numero1;
                Label7.Text = numero2;
                string dato = DateTime.Now.ToString();

                int resultado = DateTime.Compare(DateTime.Parse(dato), DateTime.Parse(Label3.Text));

                if (resultado == 1)
                {
                    Div2.Visible = true;
                    contraseña.Visible = false;
                    solicitar.Visible = false;
                    requerido.Visible = false;
                }
                else
                {
                    contraseña.Visible = false;
                    solicitar.Visible = true;
                    requerido.Visible = false;
                    Div2.Visible = false;
                }
                //System.Windows.Forms.MessageBox.Show(resultado.ToString());
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "" && TextBox3.Text != "")
            {
                if (TextBox3.Text != TextBox1.Text)
                {
                    requerido.Visible = true;
                    Label4.Text = "Las contraseñas no coinciden, verifique!";
                }
                else
                {
                    try
                    {
                        //Response.Redirect("NewPassword.aspx?numero1=4&numero2=6");
                        SqlCommand actualizar;
                        SqlDataReader rd;

                        actualizar = new SqlCommand("update Usuario set GNConUsu =" + int.Parse(TextBox3.Text) + " where GNCodUsu =" + int.Parse(Label7.Text) + "", con.OpenConnection());
                        actualizar.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(actualizar);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        rd = actualizar.ExecuteReader();
                    }
                    catch (Exception)
                    {
                        //
                    }
                    //Response.Write("<script>setTimeout(\"window.location.href='Login.aspx'\", 400)</script>");

                    Thread.Sleep(3000);
                    contraseña.Visible = true;
                    solicitar.Visible = false;
                }
            }
            else
            {
                StringBuilder comando = new StringBuilder();
                comando.Append(@"<script>");
                comando.Append(@"new PNotify({
                                    title: 'Oh No!',
                                    text: 'favor ingresar los datos',
                                    type: 'error',
                                    styling: 'bootstrap3',
                                    delay: 1000
                                });");
                comando.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", comando.ToString(), false);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
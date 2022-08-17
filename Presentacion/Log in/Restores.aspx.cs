using Persistencia;
using System;
using System.Data.SqlClient;

namespace Generales_1._0.Home.Admin.Log_in
{
    public partial class Restore : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        SqlDataReader rd = null;
        SqlCommand encontrar = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label1.Attributes.Add("style", "color: red;");
        }

        public void buscar()
        {
            SqlCommand Actualizar;
            try
            {
                encontrar = new SqlCommand("select GNCodUsu, GNConUsu from Usuario where GNCodUsu = '" + TextBox1.Text + "'", con.OpenConnection());

                rd = encontrar.ExecuteReader();

                try
                {
                    if (rd.Read() == true)
                    {
                        /*TextBox2.Text = rd["GNCodUsu"].ToString();
                        Password.Text = rd["GNConUsu"].ToString();
                        Label1.Visible = false;*/
                        Label1.Visible = true; /*
                        Label1.Text = "encontrado";*/
                        if (Password.Text == TextBox2.Text)
                        {
                            Actualizar = new SqlCommand("update Usuario set GNConUsu = '" + Password.Text + "' where GNCodUsu = '" + TextBox1.Text + "'", con.OpenConnection());
                            Actualizar.ExecuteNonQuery();/*
                            Label1.Text = "Contraseña actualizada con exito";
                            Label1.ForeColor = Color.Green;
                            Response.Write("<script>setTimeout(\"window.location.href='../Log in/Login.aspx'\", 400)</script>");*/
                            Response.Redirect("../Log in/Login.aspx");
                        }
                        else
                        {
                            Label1.Text = "las contraseñas deben ser iguales";
                            Label1.Attributes.Add("style", "color: red;");
                        }
                    }
                    else
                    {/*
                        Label1.Visible = true;
                        Label1.Text = "ERROR usuario no registrado en la base de datos";
                        Response.Write("<script>setTimeout(\"window.location.href='../Log in/Login.aspx'\", 400)</script>");*/
                    }
                }
                catch (Exception er)
                {
                    //MessageBox.Show(er.Message);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            con.CloseConnection();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
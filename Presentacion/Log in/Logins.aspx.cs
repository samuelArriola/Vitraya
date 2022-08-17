using Persistencia;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Generales_1._0.Home.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        SqlDataAdapter sd = new SqlDataAdapter();
        DataSet ds = new DataSet();
        Boolean estaConexion = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Attributes.Add("autocomplete", "off");
            Label1.Visible = false;
            buscar();
        }

        private void buscar()
        {
            try
            {
                string nombres = Session["admin"].ToString();

                Response.Redirect("../index");
            }
            catch (Exception ex)
            {
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usu = "987654";
                string pass = "crecer123";
                if (usu == TextBox1.Text && pass == Password.Text)
                {
                    Session["admin"] = usu;
                    Session["nuevo"] = "9004";

                    Response.Redirect("../Home/Admin/Index/_Index.aspx");
                }
                else
                {
                    LoginSesion(TextBox1.Text, Password.Text);
                }

            }
            catch (Exception)
            {
            }

        }


        public Boolean LoginSesion(string usuario, string contraseña)
        {
            if (TextBox1.Text == "" || Password.Text == "")
            {
            }
            else
            {
                try
                {
                    SqlCommand consulta;
                    consulta = new SqlCommand("select GNCodUsu,GNConUsu,cargo.GnNomCgo,GnEtUsu from Usuario,Cargo where Cargo.GnDcCgo=Usuario.GnDcCgo and GNCodUsu = @usuario and GNConUsu = @contraseña", con.OpenConnection())
                    {
                        CommandType = CommandType.Text
                    };
                    consulta.Parameters.AddWithValue("@usuario", usuario);
                    consulta.Parameters.AddWithValue("@contraseña", contraseña);
                    consulta.ExecuteNonQuery();

                    try
                    {
                        SqlDataAdapter sd = new SqlDataAdapter(consulta);
                        sd.Fill(ds, "Usuario");
                        DataRow dr;
                        dr = ds.Tables["Usuario"].Rows[0];
                        if ((usuario) == dr["GNCodUsu"].ToString() & contraseña == dr["GNConUsu"].ToString() & dr["GnEtUsu"].ToString() == "Activo")
                        {
                            //Session["asa"] = TextBox1.Text;
                            //Response.Redirect("perfil/index.aspx?parametro=" + TextBox1.Text);
                            Session["admin"] = TextBox1.Text;
                            Response.Redirect("../Home/Admin/Index/_Index.aspx");
                            estaConexion = true;
                        }
                        else
                        {
                            if ((usuario) == dr["GNCodUsu"].ToString() & contraseña == dr["GNConUsu"].ToString() & dr["GnEtUsu"].ToString() == "Inactivo")
                            {
                                /* Session["admin"] = dr["Gncod"].ToString();
                                 //Response.Redirect("perfil/index.aspx?parametro=" + TextBox1.Text);
                                 Response.Redirect("../Home/Admin/Index/_Index.aspx");
                                 estaConexion = true;*/
                                TextBox1.Text = "";
                                Password.Text = "";
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error " + ex.Message);
                        Label1.Visible = true;
                        Label1.Attributes.Add("style", "color: red");
                        Label1.Text = "Usuario Invalido";
                        //MessageBox.Show(ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error " + ex.Message);
                    Label1.Visible = true;
                    Label1.Attributes.Add("style", "color: red");
                    Label1.Text = "Usuario Invalido";
                    //MessageBox.Show(ex.Message);
                }
            }
            con.CloseConnection();
            return estaConexion;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //string text = TextBox2.Text;
            /*if (CheckBox1.Checked)
            {
                TextBox2.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                TextBox2.TextMode = TextBoxMode.Password;
            }*/
        }
    }
}
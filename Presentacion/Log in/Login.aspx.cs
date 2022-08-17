using Entidades.Generales;
using Logica.Generales;
using Persistencia;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Generales_1._0.Log_in
{
    public partial class Login : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        CloseAllConnections cerrar = new CloseAllConnections();
        Encryption encript = new Encryption();
        SqlDataAdapter sd = new SqlDataAdapter();
        DataSet ds = new DataSet();
        Boolean estaConexion = false;
        int His = 20000;
        string estadoactualquetenia = null;

        int contar = 0;
        int contar2 = 0; private static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarFecha();
                buscar();
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                //TextBox3.Attributes.Add("autocomplete", "off");
                Label5.Text = DateTime.Now.ToString();
                CodeHistorial();
                requerido.Visible = false;

                //Random r = new Random();
                //int n = r.Next(1000,500000);
                //MessageBox.Show(n.ToString());
                /*string correoencript = HttpUtility.UrlEncode(encript.Encrypt("jhonivizcaino20405@gmail.com"));
                new Email().SendMail("jhonivizcaino20405@gmail.com", "http://192.168.0.205/Log%20in/?Validation="+correoencript+"", "Cambio de contraseña");*/
            }
        }
        public void GetUniqueKey(int size)
        {
            try
            {
                char[] chars =
                "jhonivizcaino20405@gmail.com".ToCharArray();
                byte[] data = new byte[size];
                using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetBytes(data);
                }
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ValidarFecha()//leer el codigo del area e incrementarle uno
        {
            try
            {
                SqlCommand conn;
                conn = new SqlCommand("select GETDATE() as FechaActual", con.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    string Fecha = reader["FechaActual"].ToString();
                    SqlCommand buscar;
                    buscar = new SqlCommand("select GnFclpl,PlanesDeAccion.GnCodPl,GnUsuRs,GnRstPl,PlanesDeAccion.GnEspl from PlanesDeAccion left join RespuestaPlanes on PlanesDeAccion.GnCodPl = RespuestaPlanes.GnCodPl", con.OpenConnection());
                    SqlDataReader rd = buscar.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            estadoactualquetenia = rd["GnEspl"].ToString();
                            string cod = rd["GnRstPl"].ToString();
                            if (cod == "")
                            {
                                if (estadoactualquetenia == "7")
                                {
                                    string rechazado = "este fue rechazado";
                                }
                                else
                                {
                                    string prueba = rd["GnFclpl"].ToString();
                                    //string Fecha2 = Convert.ToDateTime(rd["GnFclpl"]).ToString("yyy-MM-dd");
                                    string codigo = rd["GnCodPl"].ToString();
                                    string ident = rd["GnUsuRs"].ToString();
                                    int resul = DateTime.Compare(DateTime.Parse(Fecha), DateTime.Parse(prueba));

                                    //System.Windows.Forms.MessageBox.Show(Convert.ToString(resul));
                                    if (resul == 1)
                                    {
                                        string var = "Expirado se acabo tu tiempo";
                                        try
                                        {
                                            SqlCommand update;
                                            update = new SqlCommand("update PlanesDeAccion set GnEspl = 5 where GnCodPl = " + int.Parse(codigo) + " and GnUsuRs = " + int.Parse(ident) + "", con.OpenConnection());
                                            SqlDataAdapter da = new SqlDataAdapter(update);
                                            update.ExecuteNonQuery();
                                            DataSet ds = new DataSet();
                                            da.Fill(ds);
                                            //select * from HistorialPlanes where GnCusPl = 123 and GnCdPl = 9004 and GnSthpl = 3
                                            try
                                            {
                                                SqlCommand update2;
                                                update2 = new SqlCommand("select GnIshPl, GnCdhPl, GnFchpl, GnCusPl, GnCdPl, GnSthpl, GnEspl from HistorialPlanes join PlanesDeAccion on (PlanesDeAccion.GnCodPl = HistorialPlanes.GnCdPl and HistorialPlanes.GnCdPl = " + int.Parse(codigo) + ") " +
                                                    "where HistorialPlanes.GnCusPl = " + int.Parse(ident) + "  and HistorialPlanes.GnSthpl = 5 or HistorialPlanes.GnSthpl = 6", con.OpenConnection());
                                                SqlDataReader rd2 = update2.ExecuteReader();
                                                if (rd2.Read())
                                                {
                                                    if (estadoactualquetenia == "6" || estadoactualquetenia == "1")
                                                    {
                                                        Label5.Text = DateTime.Now.ToString();
                                                        CodeHistorial();
                                                        AgregarHistorial(int.Parse(Label6.Text), DateTime.Parse(Label5.Text), int.Parse(ident), int.Parse(codigo), 5);
                                                    }
                                                    else
                                                    {
                                                    }
                                                }
                                                else
                                                {
                                                    if (estadoactualquetenia == "6" || estadoactualquetenia == "1")
                                                    {
                                                        Label5.Text = DateTime.Now.ToString();
                                                        CodeHistorial();
                                                        AgregarHistorial(int.Parse(Label6.Text), DateTime.Parse(Label5.Text), int.Parse(ident), int.Parse(codigo), 5);
                                                    }
                                                    else
                                                    {
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                //System.Windows.Forms.MessageBox.Show(e.Message);
                                            }
                                            finally
                                            {
                                                con.CloseConnection();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            //System.Windows.Forms.MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            con.CloseConnection();
                                        }
                                    }
                                    else
                                    {
                                        string var3 = "No Expirado aun tienes tiempo";
                                    }
                                }
                            }
                            else
                            {
                                string var2 = "Ya se encuentra respondido";
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            /* if (reader.HasRows)
                {
                while (reader.Read())
                {

                }
            }*/
            catch (Exception es)
            {
                //System.Windows.Forms.MessageBox.Show(es.Message);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodeHistorial()//leer el codigo del area e incrementarle uno
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
                conn = new SqlCommand("select top 1 GnCdhPl from HistorialPlanes order by GnCdhPl desc", con.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["GnCdhPl"].ToString();

                    lol = int.Parse(numero);
                    sg = lol + 1;
                    Label6.Text = sg.ToString();
                }
                else
                {
                    Label6.Text = His.ToString();
                }
            }

            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();

        }
        private void AgregarHistorial(int GnCdhPl, DateTime GnFchpl, int GnCusPl, int GnCdPl, int GnSthpl)
        {
            try
            {
                SqlCommand guardar;
                guardar = new SqlCommand("insert into HistorialPlanes(GnCdhPl,GnFchpl,GnCusPl,GnCdPl,GnSthpl)" +
                    "values (@GnCdhPl,@GnFchpl,@GnCusPl,@GnCdPl,@GnSthpl)", con.OpenConnection());
                guardar.Parameters.Add("@GnCdhPl", SqlDbType.Int).Value = GnCdhPl;
                guardar.Parameters.Add("@GnFchpl", SqlDbType.DateTime).Value = GnFchpl;
                guardar.Parameters.Add("@GnCusPl", SqlDbType.Int).Value = GnCusPl;
                guardar.Parameters.Add("@GnCdPl", SqlDbType.Int).Value = GnCdPl;
                guardar.Parameters.Add("@GnSthpl", SqlDbType.Int).Value = GnSthpl;
                SqlDataAdapter da = new SqlDataAdapter(guardar);
                guardar.ExecuteNonQuery();
                DataSet ds = new DataSet();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                //string javaScript = "OcultarMensaje();";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", javaScript, true);
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                con.CloseConnection();
            }

            
            con.CloseConnection();
            cerrar.closeallconnections();
        }
        private void buscar()
        {
            try
            {
                string nombres = Session["admin"].ToString();

                Response.Redirect("../index.aspx");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();
        }
        public Boolean LoginSesion(string usuario, string contraseña)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Complete los campos requeridos');", true);
                //ClientScript.RegisterStartupScript(GetType(), "vermensaje", "error2();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Completar", "error2()", true);
            }
            else
            {
                try
                {
                    SqlCommand consulta;
                    consulta = new SqlCommand("select GNCodUsu,GNConUsu,GnEtUsu from Usuario where GNCodUsu = @usuario and GNConUsu = @contraseña", con.OpenConnection())
                    {
                        CommandType = CommandType.Text
                    };
                    consulta.Parameters.AddWithValue("@usuario", Convert.ToInt32(usuario));
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
                            //if (!ValidarEncuesta(TextBox1.Text))
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", $@"alertaEncuesta(""{Session["admin"].ToString()}"");", true);
                            //    return false;
                            //}

                            Session["admin"] = TextBox1.Text;
                            Response.Redirect("../index.aspx");
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
                                //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                requerido.Visible = true;
                                Label4.Text = "este usuario se encuentra inactivo, por motivos de seguridad comuniquese con su jefe para configurar su estado";
                                TextBox1.Text = "";
                                TextBox2.Text = "";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Completar", "error()", true);
                    }
                    finally
                    {
                        con.CloseConnection();
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error " + ex.Message);
                    /*Label1.Visible = true;
                    Label1.Attributes.Add("style", "color: red");
                    Label1.Text = "Usuario Invalido";
                    TextBox1.Enabled = true;
                    TextBox2.Enabled = true;*/
                    //MessageBox.Show(ex.Message + "abajo");
                    //ClientScript.RegisterStartupScript(GetType(), "vermensaje", "error();", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Completar", "error()", true);
                    //MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.CloseConnection();
                }
            }
            con.CloseConnection();
            cerrar.closeallconnections();
            return estaConexion;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            LoginSesion(TextBox1.Text, TextBox2.Text);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string fecahaEcnriptada = HttpUtility.UrlEncode(Encryption.Encrypt("" + DateTime.Now.ToString() + ""));
            Response.Redirect("ResetPassword.aspx?ParametersQueryValidation=" + fecahaEcnriptada + "");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string fecahaEcnriptada = HttpUtility.UrlEncode(Encryption.Encrypt(DateTime.Now.AddHours(1).ToString()));
            //new Email().SendMail("jhonivizcaino20405@gmail.com", "http://192.168.0.205/Log%20in/?Validation=" + fecahaEcnriptada + "", "Cambio de contraseña");
        }


        [WebMethod]
        public static string ValidarUsuario(int idUsuario)
        {
            Usuario usuaro = DAOUsuario.getInstance().GetUsuario(idUsuario);
            if (usuaro == null)
            {
                return "Usuario invalido, por favor intente nuevamente";
            }
            else
            {
                return "";
            }
        }

        protected void btnEnviarPeticion_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(txtChangePass.Text));
                if(usuario != null)
                {
                    var uuid = DAOGNCtrlCambioPass.SetCtrlCambioPass(new GNCtrlCambioPass
                    {
                        DtmFecha = DateTime.Now,
                        BlnCambiada = false,
                        DblGNCodUSu = usuario.GNCodUsu1
                    });

                    var uuidEncripted = HttpUtility.UrlEncode(Encryption.Encrypt(uuid));

                    var r = new Regex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/", RegexOptions.None, TimeSpan.FromMilliseconds(150));

                    string puerto = r.Match(Request.Url.AbsoluteUri).Result("${port}");
                    string protocolo = r.Match(Request.Url.AbsoluteUri).Result("${proto}");

                    string correoPart1 = usuario.GNCrusu1.Split('@')[0];
                    string correoPart2 = usuario.GNCrusu1.Split('@')[1];

                    for(int i = 2, j = correoPart1.Length -2; i < j; i++)
                    {
                        correoPart1 = correoPart1.Remove(i, 1);
                        correoPart1 = correoPart1.Insert(i, "*");
                    }

                    var mensaje = $@"
                           <p style=""text-align:center"">Ingrese al siguiente enlace para cambiar su contraseña: <a href=""{protocolo}://{Request.Url.Host + puerto}/ResetPassword/ChangePassword.aspx?k={uuidEncripted}"">aquí</a> </p>
                    ";

                    Comunes.Email.SendMail(new List<string> {usuario.GNCrusu1}, mensaje, "Solicitud de coambio de contraseña Vitraya");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2",
                      $@"
                            $(""#mdChPass"").modal(""hide"");
                            exito("""",`Se ha enviado un mensaje al correo electrónico: ""{correoPart1}@{correoPart2}"" para el cambio de contraseña`);
                       ",
                      true
                  );
                }
                else
                {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "msg3",
                       $@"
                           error(""Usuario incorrecto"",""Por favor ingrese un usuario valido"");
                       ",
                       true
                   );
                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1",
                    $@"
                         error(""Usuario incorrecto"",""Por favor ingrese un usuario valido"");
                    ",
                    true
                );
            }
        }
    }
}
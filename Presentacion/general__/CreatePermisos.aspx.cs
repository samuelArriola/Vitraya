using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Generales_1._0.Class;
using System.Text;
using System.Web.Services;
using System.Drawing;
using Generales_1._0.Class.DTOGenerales;
using Generales_1._0.Class.DAOGenerales;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class GestionarPermisos : System.Web.UI.Page
    {
        private object codigo = 30001;
        private object codigo2 = 20001;
        Conexion con = new Conexion();
        Encryption Encriptacion = new Encryption();
        CloseAllConnections cerrar = new CloseAllConnections();

        int codigoModulo = 10008;
        int CodigoCrear = 20022;
        int CodigoAsignar = 20023;

        int codigo4;
        string itemsR;
        int codigoOperacion;
        int codigoRool;

        StringBuilder comandos = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                ConsultarUsuarios();
                CodePerfil();
                TextBox6.Attributes.Add("autocomplete", "off");
                MaintainScrollPositionOnPostBack = true;
                con.CloseConnection();
                cerrar.closeallconnections();
                Button10.Enabled = false;
                Button9.Enabled = false;
                //Div1.Visible = false;
                buscarDrop(); 
                //dt = (DataTable)Session["datos"];

               
            }
        }
        private void verification()
        {
            try
            {
                Label1.Text = Session["admin"].ToString();
                //buscar();
                //buscarCodigos();
                foto();
            }
            catch (Exception)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
            }
            finally
            {
                con.CloseConnection();
            }
            //if (Label9.Text == "00000")
            //{
            //    string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label1.Text));
            //    Response.Redirect(string.Format("../WebError.aspx?ParametersQuery=" + id_lista));
            //}
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
        private void foto()
        {
            string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label1.Text));

            Image1.ImageUrl = string.Format("BuscarImagenes.aspx?id=" + id_lista);
            Image2.ImageUrl = string.Format("BuscarImagenes.aspx?id=" + id_lista);
            SqlCommand buscar;
            SqlDataReader rd;

            buscar = new SqlCommand("select GNNomUsu from Usuario where GNCodUsu =  '" + int.Parse(Label1.Text) + "'", con.OpenConnection());
            rd = buscar.ExecuteReader();

            if (rd.Read())
            {
                Label12.Text = rd["GNNomUsu"].ToString();
                Label13.Text = rd["GNNomUsu"].ToString();
            }
            con.CloseConnection();
            cerrar.closeallconnections();
        }
        //private void buscar()
        //{
        //    SqlCommand buscar;
        //    SqlDataReader rd;

        //    buscar = new SqlCommand("select Rol.codigoR, operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Rol, Perfil_operacion, operaciones, modulos, Usuario " +
        //        "where Rol.codigoR = Perfil_operacion.codigoR " +
        //        "and modulos.codigoM = operaciones.codigoM " +
        //        "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //        "and Rol.codigoR = Usuario.codigoR " +
        //        "and Usuario.GNCodUsu =  '" + int.Parse(Label1.Text) + "'", con.OpenConnection());
        //    rd = buscar.ExecuteReader();

        //    if (rd.Read())
        //    {
        //        Label11.Text = rd["codigoR"].ToString();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
        private void buscarCodigos()
        {
            try
            {
                SqlCommand asignar;
                SqlDataReader asigna;

                asignar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                    "and modulos.codigoM = " + codigoModulo + " " +
                    "and operaciones.codigoP = " + CodigoAsignar + " ", con.OpenConnection());
                asigna = asignar.ExecuteReader();

                if (asigna.Read())
                {
                    Label9.Text = asigna["codigoP"].ToString();
                }
                else
                {
                    Label9.Text = "00000";
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            con.CloseConnection();
            cerrar.closeallconnections();
        }
        public void ConsultarUsuarios()
        {
            SqlDataReader leer2;
            SqlCommand consult2;


            try
            {
                consult2 = new SqlCommand("select GNCodUsu,GNNomUsu from Usuario where GnEtUsu = 'Activo'", con.OpenConnection());
                consult2.ExecuteNonQuery();
                SqlDataAdapter da2 = new SqlDataAdapter(consult2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                leer2 = consult2.ExecuteReader();
                if (leer2.Read())
                {
                    GridView2.DataSource = ds2;
                    GridView2.DataBind();
                    GridView1.DataSource = ds2;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodePerfil()
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
                conn = new SqlCommand("select codigoPO from Perfil_operacion where codigoPO = (select MAX(codigoPO) from Perfil_operacion) group by codigoPO", con.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["codigoPO"].ToString();

                    lol = int.Parse(numero);
                    sg = lol + 1;

                    Label50.Text = sg.ToString();
                    //TextBox5.Text = sg.ToString();
                }
                else
                {
                    Label50.Text = codigo2.ToString();
                    //TextBox5.Text = codigo2.ToString();
                }
            }

            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();

        }
        public void AddPerfil(int codigoPO, int codigoP, int GNCodUsu)//metodo para agregar el area
        {
            try
            {
                SqlDataReader leer;
                SqlCommand consult;
                DataTable dt = new DataTable();
                consult = new SqlCommand("select * from Perfil_operacion, operaciones, Usuario, modulos " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and operaciones.codigoM = modulos.codigoM " +
                    "and Usuario.GNCodUsu = " + int.Parse(Label22.Text) + " and modulos.codigoM =" + int.Parse(Label18.Text) + "" +
                    "and operaciones.codigoP = " + codigoOperacion + "", con.OpenConnection());
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    //System.Windows.Forms.MessageBox.Show("Ya se encuntra con ese permiso", "Clinica Crecer", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.ServiceNotification);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ya se encuntra con ese permiso');", true);
                    //ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol8();", true);
                    //ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol8();", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "errorRol8()", true);
                    //Label7.ForeColor = Color.Red;
                    //Label7.Text = "Ya se encuntra con ese permiso";
                }
                else
                {
                    SqlCommand insert;//variable de comando 
                    SqlDataReader rd;
                    insert = new SqlCommand("insert into Perfil_operacion (codigoPO,codigoP,GNCodUsu)  values (@codigoPO,@codigoP,@GNCodUsu)", con.OpenConnection());//cadena de insert para la base de datos
                    insert.CommandType = CommandType.Text;//tipo de comando = texto
                    insert.Parameters.AddWithValue("@codigoPO", SqlDbType.Int).Value = codigoPO;//paso de parametros
                    insert.Parameters.AddWithValue("@codigoP", SqlDbType.VarChar).Value = codigoP;//paso de parametros
                    insert.Parameters.AddWithValue("@GNCodUsu", SqlDbType.VarChar).Value = GNCodUsu;//paso de parametros
                    insert.ExecuteNonQuery();
                    /*MessageBox.Show("Exito");*/
                    //ListBox1.Items.Clear();
                    //ListBox2.Items.Clear();
                    //ConsultarPermisos();
                    if (ListBox3.SelectedIndex == -1)
                    {
                        ListBox4.Items.Add(itemsR);
                    }
                    else
                    {
                        ListBox4.Items.Add(ListBox3.SelectedValue);
                    }
                    ListBox3.SelectedIndex = -1;
                    //Label7.ForeColor = Color.Green;
                    //Label7.Text = "Agregado con exito";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "exitoRol()", true);
                    Button10.Enabled = false;
                }
            }
            catch (Exception ex)//excepcion
            {
                System.Windows.Forms.MessageBox.Show("Permiso " + ex.Message);
            }
            finally//cerrar la conexion
            {
                con.CloseConnection();
            }
            /*
             */
            cerrar.closeallconnections();
        }
        public void codigoOp(string nombreOp, string Modulo)
        {
            SqlCommand codigo;
            codigo = new SqlCommand("select codigoP from operaciones where nombreOp = @nombreOp and codigoM = @codigoM", con.OpenConnection());
            codigo.Parameters.AddWithValue("@nombreOp", nombreOp);
            codigo.Parameters.AddWithValue("@codigoM", Modulo);
            codigo.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(codigo);
            DataSet ds = new DataSet();
            da.Fill(ds);
            SqlDataReader rd = codigo.ExecuteReader();
            if (rd.Read())
            {
                codigoOperacion = Convert.ToInt32(rd["codigoP"]);
            }
        }
        private void Roles()
        {
            SqlCommand encontrar8;
            SqlDataReader rd8;
            try
            {
                DataTable dt = new DataTable();
                encontrar8 = new SqlCommand("select GNCodUsu, GNNomUsu from Usuario where not GnEtUsu = 'Inactivo' and GNNomUsu like '%"+TextBox6.Text+"%'", con.OpenConnection());
                encontrar8.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(encontrar8);
                DataSet ds = new DataSet();
                da.Fill(dt);

                rd8 = encontrar8.ExecuteReader();
                try
                {
                    if (rd8.Read() == true)
                    {
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                    }
                    else
                    {
                    }
                }
                catch (Exception er)
                {
                    //   MessageBox.Show(er.Message);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
                }
                finally
                {
                    con.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                ///Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                con.CloseConnection();
            }
            con.CloseConnection();
            cerrar.closeallconnections();
        }

        [WebMethod]
        public static List<string> GetEmployeeName(string empName)
        {
            Conexion con = new Conexion();
            CloseAllConnections cerrar = new CloseAllConnections();
            List<string> empResult = new List<string>();
            /*using (SqlConnection con = new SqlConnection(@"data source=DESKTOP-LOFE9GQ\SQLEXPRESS;Min Pool Size=0;Max Pool Size=10024;Pooling=true;Initial catalog=otra; integrated security=true"))
            {*/
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Top 10 GNCodUsu from Usuario where GnEtUsu = 'Activo' and GNCodUsu LIKE ''+@SearchEmpName+'%'";
                cmd.Connection = con.OpenConnection();
                cmd.Parameters.AddWithValue("@SearchEmpName", empName);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        empResult.Add(dr["GNCodUsu"].ToString());
                    }
                }
                else
                {
                    empResult.Add("sin resultados");
                }
                con.CloseConnection();
                cerrar.closeallconnections();
                return empResult;
            }
            //}
        }

        [WebMethod]
        public static List<string> GetEmployeRol(string empName)
        {
            Conexion con = new Conexion();
            CloseAllConnections cerrar = new CloseAllConnections();
            List<string> empResult = new List<string>();
            /*using (SqlConnection con = new SqlConnection(@"data source=DESKTOP-LOFE9GQ\SQLEXPRESS;Min Pool Size=0;Max Pool Size=10024;Pooling=true;Initial catalog=otra; integrated security=true"))
            {*/
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Top 10 nombreRol from Rol where EstRol = 1 and nombreRol LIKE ''+@SearchEmpName+'%'";
                cmd.Connection = con.OpenConnection();
                cmd.Parameters.AddWithValue("@SearchEmpName", empName);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        empResult.Add(dr["nombreRol"].ToString());
                    }
                }
                else
                {
                    empResult.Add("sin resultados");
                }
                con.CloseConnection();
                cerrar.closeallconnections();
                return empResult;
            }
            //}
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }
        void Popup(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                /*builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());*/
                builder.Append(@"<script>");
                builder.Append(@"$('#mask').fadeIn();
                                 $('#pnlpopup').fadeIn();");
                builder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "POPUP", builder.ToString(), false);
            }
            else
            {
                /*builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());*/
                builder.Append(@"<script>");
                builder.Append(@"$('#mask').fadeOut();
                                 $('#pnlpopup').fadeOut();");
                builder.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "POPUP", builder.ToString(), false);
            }
        }
        public void buscar(string nombre)
        {
            try
            {
                SqlCommand buscar;
                buscar = new SqlCommand("select * from operaciones join modulos on (modulos.codigoM = operaciones.codigoM) where modulos.nombreMod = @nombre", con.OpenConnection());
                buscar.Parameters.AddWithValue("@nombre", nombre);
                buscar.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(buscar);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlDataReader rd = buscar.ExecuteReader();

                if (rd.HasRows)
                {
                    if(DropDownList1.SelectedIndex != -1)
                    {
                        while (rd.Read())
                        {
                            this.ListBox1.Items.Add(rd.GetString(2));
                        }
                    }
                    else if(DropDownList2.SelectedIndex != -1)
                    {
                        while (rd.Read())
                        {
                            this.ListBox3.Items.Add(rd.GetString(2));
                        }
                        try
                        {
                            SqlCommand consult;
                            SqlDataReader leer;
                            DataTable dt = new DataTable();
                            consult = new SqlCommand("select * from Perfil_operacion  join operaciones on(operaciones.codigoP = Perfil_operacion.codigoP) join Usuario on(Usuario.GNCodUsu = Perfil_operacion.GNCodUsu) join modulos on(modulos.codigoM = operaciones.codigoM) " +
                               " where Perfil_operacion.GNCodUsu = " + int.Parse(Label22.Text) + " and modulos.nombreMod = @nombre", con.OpenConnection());
                            consult.Parameters.AddWithValue("@nombre", nombre);
                            consult.ExecuteNonQuery();
                            SqlDataAdapter da2 = new SqlDataAdapter(consult);
                            DataSet ds2 = new DataSet();
                            da2.Fill(ds);

                            leer = consult.ExecuteReader();

                            if (leer.HasRows)
                            {
                                while (leer.Read())
                                {
                                    this.ListBox4.Items.Add(leer["nombreOp"].ToString()); //.GetString(6)
                                }
                            }
                        }
                        catch (Exception es)
                        {
                            System.Windows.Forms.MessageBox.Show(es.Message);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            if (ListBox3.SelectedIndex == -1)
            {
                //System.Windows.Forms.MessageBox.Show("debe seleccionar una opcion");
                //Label7.ForeColor = Color.Red;
                //Label7.Text = "debe seleccionar una opcion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "errorRol7()", true);
            }
            else
            {
                //ListBox4.Items.Add(ListBox3.SelectedValue);
                CodePerfil();
                codigoOp(ListBox3.SelectedValue, HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedItem.Value)));
                AddPerfil(int.Parse(Label50.Text), codigoOperacion, int.Parse(Label22.Text));
                //System.Windows.Forms.MessageBox.Show(Label17.Text + " " + Label18.Text + " " + ListBox3.SelectedValue + " " + codigoOperacion);
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            int numero = ListBox3.Items.Count;
            if (numero <= 0)
            {
                //Label7.ForeColor = Color.Red;
                //Label7.Text = "Debe seleccionar una opcion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "errorRol7()", true);
            }
            else
            {
                //ListBox4.Items.Clear();
                foreach (ListItem item in ListBox3.Items)
                {
                    itemsR = item.ToString();
                    CodePerfil();
                    codigoOp(item.ToString(), HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedItem.Value)));//HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedValue))
                    AddPerfil(int.Parse(Label50.Text), codigoOperacion, int.Parse(Label22.Text));
                    itemsR = "";
                    //ListBox4.Items.Add(item);
                }
            }
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            int numero = ListBox4.Items.Count;
            if (numero <= 0)
            {
                //Label7.ForeColor = Color.Red;
                //Label7.Text = "Debe seleccionar una opcion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "errorRol7()", true);
            }
            else
            {
                foreach (ListItem item in ListBox4.Items)
                {
                    CodePerfil();
                    codigoOp(item.ToString(), HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedItem.Value)));
                    borrar(int.Parse(Label22.Text), codigoOperacion);
                }
                ListBox4.Items.Clear();
            }
        }

        protected void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Text = "";
            ListBox4.SelectedIndex = -1;
            Button10.Enabled = true;
            Button9.Enabled = false;
        }

        protected void ListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Text = "";
            ListBox3.SelectedIndex = -1;
            Button10.Enabled = false;
            Button9.Enabled = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ListBox3.Items.Clear();
            ListBox4.Items.Clear();
            Label7.Text = "";
            Label17.Text = "";
            Label18.Text = "";
            UpdatePanel2.Update();
            ClientScript.RegisterStartupScript(GetType(), "exito", "exitoRol2();", true);
            ConsultarUsuarios();
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (TextBox6.Text != "")
            {
                GridView2.SelectedIndex = -1;
                Roles();
                Label7.Text = "";
                //TextBox4.Text = "";
                //TextBox3.Text = "";
            }
            else
            {
                GridView2.SelectedIndex = -1;
                ConsultarUsuarios();
                Label7.Text = "";
                //TextBox4.Text = "";
                //TextBox3.Text = "";
            }
        }
        public void borrar(int codigo1, int codigo2)
        {
            try
            {
                SqlCommand Inactivar;
                Inactivar = new SqlCommand("delete from Perfil_operacion where GNCodUsu = @codigo1 and codigoP = @codigo2", con.OpenConnection());
                Inactivar.CommandType = CommandType.Text;
                Inactivar.Parameters.AddWithValue("@codigo1", SqlDbType.Int).Value = codigo1;
                Inactivar.Parameters.AddWithValue("@codigo2", SqlDbType.Int).Value = codigo2;
                Inactivar.ExecuteNonQuery();
                ListBox4.Items.Remove(ListBox4.SelectedValue);
                ListBox3.SelectedIndex = -1;
                //Label7.ForeColor = Color.Green;
                //Label7.Text = "Borrado con exito";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "exitoRol4()", true);
                Button9.Enabled = false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                Roles();
                con.CloseConnection();
            }
            cerrar.closeallconnections();
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            if (ListBox4.SelectedIndex == -1)
            {
                //Label7.ForeColor = Color.Red;
                //Label7.Text = "debe seleccionar una opcion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exitpo", "errorRol7()", true);
            }
            else
            {
                CodePerfil();
                codigoOp(ListBox4.SelectedValue, HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedItem.Value)));
                borrar(int.Parse(Label22.Text), codigoOperacion);
            }
        } 
        public void buscarDrop()
        {
            try
            {
                SqlCommand buscarD;
                buscarD = new SqlCommand("select codigoM, nombreMod from modulos", con.OpenConnection());
                buscarD.ExecuteNonQuery();
                SqlDataAdapter daD = new SqlDataAdapter(buscarD);
                DataSet dsD = new DataSet();
                daD.Fill(dsD);
                DropDownList1.DataSource = dsD;
                DropDownList1.DataTextField = "nombreMod";
                DropDownList1.DataValueField = "codigoM";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Seleccione");


                DropDownList2.DataSource = dsD;
                DropDownList2.DataTextField = "nombreMod";
                DropDownList2.DataValueField = "codigoM";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "Seleccione");
            }
            catch (Exception ecx)
            {
                System.Windows.Forms.MessageBox.Show("Test" + ecx.Message);
            }

            foreach (ListItem item in DropDownList1.Items)
            {
                item.Value = HttpUtility.UrlDecode(Encriptacion.Encrypt(item.Value.ToString()));
            }
            foreach (ListItem ite2 in DropDownList2.Items)
            {
                ite2.Value = HttpUtility.UrlDecode(Encriptacion.Encrypt(ite2.Value.ToString()));
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox3.Items.Clear();
            ListBox4.Items.Clear();
            string item = DropDownList1.SelectedItem.Text; //HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedValue));
            buscar(item);
            Label18.Text = HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedItem.Value)); //HttpUtility.HtmlDecode(Encriptacion.Decrypt());
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (TextBox6.Text != "")
            {
                //DropDownList1.Items.Clear();
                //buscarDrop();
                DropDownList1.SelectedIndex = -1;
                ListBox3.Items.Clear();
                ListBox4.Items.Clear();
                GridView2.SelectedIndex = -1;
                Roles();
                Label7.Text = "";
                if (e.CommandName == "Filtrar")
                {
                    DropDownList1.SelectedIndex = -1;
                    ListBox3.Items.Clear();
                    ListBox4.Items.Clear();
                    LinkButton btndetails = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                    codigo4 = Convert.ToInt32(GridView2.DataKeys[gvrow.RowIndex].Value.ToString());
                    Label22.Text = codigo4.ToString();
                    Popup(true);
                    if (Label17.Text == "30000")
                    {
                        DropDownList1.Enabled = false;
                        Button11.Enabled = false;
                        Button12.Enabled = false;
                    }
                    else
                    {
                        DropDownList1.Enabled = true;
                        Button11.Enabled = true;
                        Button12.Enabled = true;
                    }
                    Label20.Text = "Permiso de: " + gvrow.Cells[1].Text;
                }
            }
            else
            {
                //DropDownList1.Items.Clear();
                //buscarDrop();
                DropDownList1.SelectedIndex = -1;
                ListBox3.Items.Clear();
                ListBox4.Items.Clear();
                GridView2.SelectedIndex = -1;
                ConsultarUsuarios();
                Label7.Text = "";
                if (e.CommandName == "Filtrar")
                {
                    DropDownList1.SelectedIndex = -1;
                    ListBox3.Items.Clear();
                    ListBox4.Items.Clear();
                    LinkButton btndetails = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                    codigo4 = Convert.ToInt32(GridView2.DataKeys[gvrow.RowIndex].Value.ToString());
                    //codigo4 = Convert.ToInt32(e.CommandArgument.ToString());
                    Label22.Text = codigo4.ToString();
                    Popup(true);
                    if (Label17.Text == "30000")
                    {
                        DropDownList1.Enabled = false;
                        Button11.Enabled = false;
                        Button12.Enabled = false;
                    }
                    else
                    {
                        DropDownList1.Enabled = true;
                        Button11.Enabled = true;
                        Button12.Enabled = true;
                    }
                    Label20.Text = "Permiso de: " + gvrow.Cells[1].Text;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder comandos = new StringBuilder();
            comandos.Append(@"<script>");
            comandos.Append(@"$('#mask').fadeIn();
                                $('#Panel1').fadeIn();");
            comandos.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Show", comandos.ToString(), false);

            //DataTable dt = new DataTable();
            //Session["Tablas"] = dt;

            //dt.Columns.Add("Id");
            //dt.Columns.Add("Nombre");
            //dt.Columns.Add("Ciudad");

            //dt.Rows.Add(1, "Jhonier", "Cartagena");
            //dt.Rows.Add(2, "Juan", "Peru");
            //dt.Rows.Add(3, "Maria", "EE.UU");

            //GridView1.DataSource = Session["Tablas"];
            //GridView1.DataBind();

            /*DataTable dt = new DataTable();
                    //Session["dt"] = dt;
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("GNCodUsu");
                        dt.Columns.Add("GNNomUsu");
                    }
                    DataRow row = dt.NewRow();
                    row[0] = gvr.Cells[1].Text;
                    row[1] = gvr.Cells[2].Text;
                    //dt.Rows.Add(gvr.Cells[1].Text, gvr.Cells[2].Text);
                    dt.Rows.Add(row);
                    Session["dt"] = dt;
                    GridView3.DataSource = Session["dt"];
                    GridView3.DataBind();*/
        }
        public void Panel()
        {
            StringBuilder comandos = new StringBuilder();
            comandos.Append(@"<script>");
            comandos.Append(@"$('#mask').show();
                                $('#Panel1').show();");
            comandos.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Show", comandos.ToString(), false);
        }
        public DataTable llenar()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GNCodUsu");
            dt.Columns.Add("GNNomUsu");
            return dt;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "check")
            {
                LinkButton btn = (LinkButton)e.CommandSource;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                //System.Windows.Forms.MessageBox.Show(gvr.Cells[1].Text);
                if (btn.Text == "Seleccionar")
                {
                    gvr.Cells[1].Attributes.Add("style", "background: #0077e9; colo: #fff");
                    gvr.Cells[2].Attributes.Add("style", "background: #0077e9; colo: #fff");
                    btn.Text = "Deseleccionar";

                    if(Session["datos"] == null)
                    {
                        DataTable dt = llenar();
                        DataRow dr = dt.NewRow();
                        dr[0] = gvr.Cells[1].Text;
                        dr[1] = gvr.Cells[2].Text;
                        dt.Rows.Add(dr);
                        GridView3.DataSource = dt;
                        GridView3.DataBind();

                        Session["datos"] = dt;
                    }
                    else
                    {
                        DataTable dt = (Session["datos"]) as DataTable;
                        DataRow row = dt.NewRow();
                        row[0] = gvr.Cells[1].Text;
                        row[1] = gvr.Cells[2].Text;
                        dt.Rows.Add(row);
                        GridView3.DataSource = dt;
                        GridView3.DataBind();

                        Session["datos"] = dt;
                    }
                }
                else if (btn.Text == "Deseleccionar")
                {
                    gvr.Cells[1].Attributes.Remove("style");
                    gvr.Cells[2].Attributes.Remove("style");
                    btn.Text = "Seleccionar";
                }
            }
            Panel();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    LinkButton boton = gvr.FindControl("LinkButton3") as LinkButton;
                    gvr.Cells[1].Attributes.Add("style", "background: #0077e9; colo: #fff");
                    gvr.Cells[2].Attributes.Add("style", "background: #0077e9; colo: #fff");
                    boton.Text = "Deseleccionar";
                    if (Session["datos"] == null)
                    {
                        DataTable dt = llenar();
                        DataRow dr = dt.NewRow();
                        dr[0] = gvr.Cells[1].Text;
                        dr[1] = gvr.Cells[2].Text;
                        dt.Rows.Add(dr);
                        GridView3.DataSource = dt;
                        GridView3.DataBind();

                        Session["datos"] = dt;
                    }
                    else
                    {
                        foreach (GridViewRow item in GridView3.Rows)
                        {
                            if (item.Cells[1].Text != gvr.Cells[1].Text)
                            {
                                DataTable dt = (Session["datos"]) as DataTable;
                                DataRow row = dt.NewRow();
                                row[0] = gvr.Cells[1].Text;
                                row[1] = gvr.Cells[2].Text;
                                dt.Rows.Add(row);
                                GridView3.DataSource = dt;
                                GridView3.DataBind();

                                Session["datos"] = dt;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    LinkButton boton = gvr.FindControl("LinkButton3") as LinkButton;
                    gvr.Cells[1].Attributes.Remove("style");
                    gvr.Cells[2].Attributes.Remove("style");
                    boton.Text = "Seleccionar";
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                }
            }
            Panel();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            string item = DropDownList2.SelectedItem.Text; //HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList1.SelectedValue));
            buscar(item);
            Label6.Text = HttpUtility.HtmlDecode(Encriptacion.Decrypt(DropDownList2.SelectedItem.Value)); //HttpUtility.HtmlDecode(Encriptacion.Decrypt());
            Panel();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel();
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }
    }
}
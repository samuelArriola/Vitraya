using Generales_1._0.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class CreatePermisos : System.Web.UI.Page
    {
        private object codigo = 30001;
        private object codigo2 = 20001;
        Conexion con = new Conexion();
        Encryption Encriptacion = new Encryption();
        CloseAllConnections cerrar = new CloseAllConnections();

        int codigoModulo = 10008;
        int CodigoCrear = 20022;
        int CodigoAsignar = 20023;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////verification();
            ////CodeRol();
            ////ConsultarOperaciones();
            ////ConsultarRoles();
            ////CodePerfil();
            //TextBox3.Attributes.Add("autocomplete", "off");
            //TextBox2.Attributes.Add("autocomplete", "off");
            //TextBox8.Attributes.Add("autocomplete", "off");/*
            //TextBox1.Attributes.Add("disabled", "disabled");
            //TextBox4.Attributes.Add("disabled", "disabled");*/
            //TextBox1.Enabled = false;
            //TextBox4.Enabled = false;
            //TextBox5.Enabled = false;
            //TextBox6.Enabled = false;
            //TextBox7.Enabled = false;/*
            //TextBox5.Attributes.Add("disabled", "disabled");
            //TextBox6.Attributes.Add("disabled", "disabled");
            //TextBox7.Attributes.Add("disabled", "disabled");*/
            //DropDownList1.Items[0].Attributes.Add("disabled", "disabled");/*
            //TextBox1.Visible = false;
            //TextBox3.Visible = false;*/
            //Button2.Visible = false;
            //guardado.Visible = false;
            //info.Visible = false;
            //Error.Visible = false;
            //con.CloseConnection();
            //cerrar.closeallconnections();
            ////MaintainScrollPositionOnPostBack = true;
        }
        //private void verification()
        //{
        //    try
        //    {
        //        Label1.Text = Session["admin"].ToString();
        //        Response.Redirect("../Index.aspx");
        //        buscar();
        //        buscarCodigos();
        //        foto();
        //    }
        //    catch (Exception)
        //    {
        //        Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    if (Label8.Text == "20022")//Crear
        //    {
        //        crearRol.Visible = true;
        //    }
        //    else
        //    {
        //        crearRol.Visible = false;
        //    }

        //    if (Label9.Text == "20023")//asignar 
        //    {
        //        AsignarRol.Visible = true;
        //        AsignarRol2.Visible = true;
        //        AsignarRol3.Visible = true;
        //    }
        //    else
        //    {
        //        AsignarRol.Visible = false;
        //        AsignarRol2.Visible = false;
        //        AsignarRol3.Visible = false;
        //    }

        //    if (Label8.Text == "00000" && Label9.Text == "00000")
        //    {
        //        string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label1.Text));
        //        Response.Redirect(string.Format("../WebError.aspx?ParametersQuery=" + id_lista));
        //    }
        //}
        //private void foto()
        //{

        //    Image1.ImageUrl = "BuscarImagenes.aspx?id=" + int.Parse(Label1.Text);
        //    Image2.ImageUrl = "BuscarImagenes.aspx?id=" + int.Parse(Label1.Text);
        //    SqlCommand buscar;
        //    SqlDataReader rd;

        //    buscar = new SqlCommand("select GNNomUsu from Usuario where GNCodUsu =  '" + int.Parse(Label1.Text) + "'", con.OpenConnection());
        //    rd = buscar.ExecuteReader();

        //    if (rd.Read())
        //    {
        //        Label12.Text = rd["GNNomUsu"].ToString();
        //        Label13.Text = rd["GNNomUsu"].ToString();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
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
        //private void buscarCodigos()
        //{
        //    SqlCommand Crear;
        //    SqlDataReader Crearrd;

        //    Crear = new SqlCommand("select Rol.codigoR, operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Rol, Perfil_operacion, operaciones, modulos, Usuario " +
        //        "where Rol.codigoR = Perfil_operacion.codigoR " +
        //        "and modulos.codigoM = operaciones.codigoM " +
        //        "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //        "and Rol.codigoR = Usuario.codigoR " +
        //        "and Usuario.GNCodUsu =  '" + int.Parse(Label1.Text) + "'" +
        //        "and Rol.codigoR = " + int.Parse(Label11.Text) + "" +
        //        "and modulos.codigoM = " + codigoModulo + "" +
        //        "and operaciones.codigoP = " + CodigoCrear + "", con.OpenConnection());
        //    Crearrd = Crear.ExecuteReader();

        //    if (Crearrd.Read())
        //    {
        //        Label8.Text = Crearrd["codigoP"].ToString();
        //    }
        //    else
        //    {
        //        Label8.Text = "00000";
        //    }
        //    SqlCommand asignar;
        //    SqlDataReader asigna;

        //    asignar = new SqlCommand("select Rol.codigoR, operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Rol, Perfil_operacion, operaciones, modulos, Usuario " +
        //        "where Rol.codigoR = Perfil_operacion.codigoR " +
        //        "and modulos.codigoM = operaciones.codigoM " +
        //        "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //        "and Rol.codigoR = Usuario.codigoR " +
        //        "and Usuario.GNCodUsu =  '" + int.Parse(Label1.Text) + "'" +
        //        "and Rol.codigoR = " + int.Parse(Label11.Text) + "" +
        //        "and modulos.codigoM = " + codigoModulo + "" +
        //        "and operaciones.codigoP = " + CodigoAsignar + "", con.OpenConnection());
        //    asigna = asignar.ExecuteReader();

        //    if (asigna.Read())
        //    {
        //        Label9.Text = asigna["codigoP"].ToString();
        //    }
        //    else
        //    {
        //        Label9.Text = "00000";
        //    }

        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}

        //public void CodeRol()
        //{
        //    try
        //    {
        //        int lol, sg;
        //        string numero = "";
        //        SqlCommand conn;
        //        conn = new SqlCommand("select top 1 codigoR from Rol order by codigoR desc", con.OpenConnection());
        //        SqlDataReader reader = conn.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            numero = reader["codigoR"].ToString();

        //            lol = int.Parse(numero);
        //            sg = lol + 1;

        //            TextBox1.Text = sg.ToString();
        //        }
        //        else
        //        {
        //            TextBox1.Text = codigo.ToString();
        //        }
        //    }

        //    catch (Exception es)
        //    {
        //        //MessageBox.Show(es.Message);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();

        //}
        //public void AddRol(int codigoR, string nombre)//metodo para agregar el area
        //{
        //    try
        //    {
        //        SqlCommand insert;//variable de comando 
        //        insert = new SqlCommand("insert into Rol (codigoR,nombreRol,EstRol)  values (@codigoR,@nombreRol,1)", con.OpenConnection());//cadena de insert para la base de datos
        //        insert.CommandType = CommandType.Text;//tipo de comando = texto
        //        insert.Parameters.AddWithValue("@codigoR", SqlDbType.Int).Value = codigoR;//paso de parametros
        //        insert.Parameters.AddWithValue("@nombreRol", SqlDbType.VarChar).Value = nombre;//paso de parametros
        //        insert.ExecuteNonQuery();
        //        guardado.Visible = true;
        //        guardado.Visible = false;
        //        //MessageBox.Show("Rol guardado con exito", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        TextBox2.Text = "";
        //        ConsultarRoles();
        //    }
        //    catch (Exception ex)//excepcion
        //    {
        //        //MessageBox.Show("No se puede crear el area" + ex.Message);
        //    }
        //    finally//cerrar la conexion
        //    {
        //        con.CloseConnection();
        //    }
        //    cerrar.closeallconnections();
        //}
        //public void ConsultarOperaciones()
        //{
        //    SqlDataReader leer;
        //    SqlCommand consult;


        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        consult = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos " +
        //            "where modulos.codigoM = operaciones.codigoM", con.OpenConnection());
        //        consult.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(consult);
        //        DataSet ds = new DataSet();
        //        da.Fill(dt);

        //        leer = consult.ExecuteReader();
        //        if (leer.Read())
        //        {
        //            GridView2.DataSource = dt;
        //            GridView2.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
        //public void AddPerfil(int codigoPO, int codigoR, int codigoP)//metodo para agregar el area
        //{
        //    try
        //    {
        //        SqlDataReader leer;
        //        SqlCommand consult;
        //        DataTable dt = new DataTable();
        //        consult = new SqlCommand("select * from Perfil_operacion, operaciones, Rol, modulos " +
        //            "where Rol.codigoR = Perfil_operacion.codigoR " +
        //            "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //            "and operaciones.codigoM = modulos.codigoM " +
        //            "and Rol.codigoR = " + int.Parse(TextBox4.Text) + " and modulos.codigoM =" + int.Parse(Label7.Text) + "" +
        //            "and operaciones.codigoP = " + int.Parse(TextBox5.Text) + "", con.OpenConnection());
        //        consult.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(consult);
        //        DataSet ds = new DataSet();
        //        da.Fill(dt);

        //        leer = consult.ExecuteReader();
        //        if (leer.Read())
        //        {
        //            //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ya se encuntra con ese permiso');", true);
        //            ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol8();", true);
        //        }
        //        else
        //        {
        //            SqlCommand insert;//variable de comando 
        //            SqlDataReader rd;
        //            insert = new SqlCommand("insert into Perfil_operacion (codigoPO,codigoR,codigoP)  values (@codigoPO,@codigoR,@codigoP)", con.OpenConnection());//cadena de insert para la base de datos
        //            insert.CommandType = CommandType.Text;//tipo de comando = texto
        //            insert.Parameters.AddWithValue("@codigoPO", SqlDbType.Int).Value = codigoPO;//paso de parametros
        //            insert.Parameters.AddWithValue("@codigoR", SqlDbType.VarChar).Value = codigoR;//paso de parametros
        //            insert.Parameters.AddWithValue("@codigoP", SqlDbType.VarChar).Value = codigoP;//paso de parametros
        //            insert.ExecuteNonQuery();
        //            /*MessageBox.Show("Exito");*/
        //            ListBox1.Items.Clear();
        //            ListBox2.Items.Clear();
        //            ConsultarPermisos();
        //        }
        //    }
        //    catch (Exception ex)//excepcion
        //    {
        //        //MessageBox.Show("No se puede crear el area" + ex.Message);
        //    }
        //    finally//cerrar la conexion
        //    {
        //        con.CloseConnection();
        //    }
        //    /*
        //     */
        //    cerrar.closeallconnections();
        //}
        //public void CodePerfil()
        //{
        //    try
        //    {
        //        int lol, sg;
        //        string numero = "";
        //        SqlCommand conn;
        //        conn = new SqlCommand("select top 1 codigoPO from Perfil_operacion order by codigoPO desc", con.OpenConnection());
        //        SqlDataReader reader = conn.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            numero = reader["codigoPO"].ToString();

        //            lol = int.Parse(numero);
        //            sg = lol + 1;

        //            TextBox5.Text = sg.ToString();
        //        }
        //        else
        //        {
        //            TextBox5.Text = codigo2.ToString();
        //        }
        //    }

        //    catch (Exception es)
        //    {
        //        //MessageBox.Show(es.Message);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();

        //}
        //public void ConsultarPermisos()
        //{
        //    SqlDataReader leer;
        //    SqlCommand consult;

        //    int contar = 0;


        //    Roles();
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        consult = new SqlCommand("select * from Perfil_operacion, operaciones, Rol, modulos " +
        //            "where Rol.codigoR = Perfil_operacion.codigoR " +
        //            "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //            "and operaciones.codigoM = modulos.codigoM " +
        //            "and Rol.codigoR = " + int.Parse(GridView1.SelectedRow.Cells[1].Text.ToString()) + "", con.OpenConnection());
        //        consult.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(consult);
        //        DataSet ds = new DataSet();
        //        da.Fill(dt);

        //        leer = consult.ExecuteReader();
        //        if (leer.HasRows)
        //        {
        //            while (leer.Read())
        //            {
        //                contar = contar + 1;
        //                this.ListBox1.Items.Add(contar + " " + leer["nombreMod"].ToString());
        //                this.ListBox2.Items.Add(contar + " " + leer["nombreOp"].ToString());
        //            }
        //        }
        //        else
        //        {
        //            ListBox1.Items.Clear();
        //            ListBox2.Items.Clear();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //     //   MessageBox.Show(ex.Message);
        //       //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    Roles();
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
        //public void borrar(int codigo1, int codigo2)
        //{
        //    try
        //    {
        //        Roles();
        //        SqlCommand Inactivar;
        //        Inactivar = new SqlCommand("delete from Perfil_operacion where codigoR = @codigo1 and codigoP = @codigo2", con.OpenConnection());
        //        Inactivar.CommandType = CommandType.Text;
        //        Inactivar.Parameters.AddWithValue("@codigo1", SqlDbType.Int).Value = codigo1;
        //        Inactivar.Parameters.AddWithValue("@codigo2", SqlDbType.Int).Value = codigo2;
        //        Inactivar.ExecuteNonQuery();
        //        //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Eliminacion Exitosa');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "exitoRol4();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Eliminacion Exitosa').set('label', 'Aceptar');", true);
        //        ListBox2.Items.Clear();
        //        ListBox1.Items.Clear();
        //        ConsultarPermisos();
        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede crear el perfil intente mas tarde');", true);
        //    }
        //    finally
        //    {
        //        Roles();
        //        con.CloseConnection();
        //    }
        //    cerrar.closeallconnections();
        //}
        //public void perfil()
        //{
        //    SqlCommand encontrar;
        //    SqlDataReader rd;
        //    try
        //    {
        //        encontrar = new SqlCommand("select Usuario.GNCodUsu, Usuario.GNNomUsu, Rol.nombreRol, Rol.codigoR " +
        //            "from Usuario, Area, Cargo, Departamento, Rol where Area.GnCdAra = Usuario.GnCdAra " +
        //            "and Cargo.GnDcCgo = Usuario.GnDcCgo and Departamento.GnDcDep = Usuario.GnDcDep " +
        //            "and Rol.codigoR = Usuario.codigoR " +
        //            "and Usuario.GNCodUsu = '" + int.Parse(TextBox3.Text) + "'", con.OpenConnection());

        //        rd = encontrar.ExecuteReader();

        //        try
        //        {
        //            if (rd.Read() == true)
        //            {
        //                /*MessageBox.Show("Codigo: " + rd["GNCodUsu"].ToString() + "\n" + "Nombre: " + rd["GNNomUsu"].ToString() + "\n" + "Rol: " + rd["nombreRol"].ToString(), "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);*/
        //                /*rd["GNCodUsu"].ToString();
        //                rd["GNNomUsu"].ToString();
        //               rd["GNConUsu"].ToString();
        //                rd["GnNomDep"].ToString();
        //                rd["GNFhUsu"].ToString();
        //                rd["GnEtUsu"].ToString();
        //               rd["GnNomAra"].ToString();
        //                rd["GnNomCgo"].ToString();
        //                rd["GNCrusu"].ToString();
        //               rd["GnTlUsu"].ToString();
        //                rd["GnEpsUsu"].ToString();*/
        //                //Label20.Text = rd["GnCod"].ToString();
        //                TextBox4.Text = rd["codigoR"].ToString();
        //                TextBox9.Text = rd["GNCodUsu"].ToString();
        //                TextBox10.Text = rd["GNNomUsu"].ToString();
        //                TextBox11.Text = rd["nombreRol"].ToString();
        //                info.Visible = true;
        //            }
        //            else
        //            {
        //                TextBox3.Text = "";
        //                Label2.Visible = true;
        //                Label2.ForeColor = Color.Red;
        //                Label2.Text = "Usuario Incorrecto";
        //                info.Visible = false;
        //            }
        //        }
        //        catch (Exception er)
        //        {
        //            //MessageBox.Show(er.Message);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
        //private void Cambiar(int codigo1, int codigo2)
        //{
        //    try
        //    {
        //        SqlCommand Inactivar;
        //        Inactivar = new SqlCommand("update Usuario set codigoR = @codigo1 where GNCodUsu = @codigo2", con.OpenConnection());
        //        Inactivar.CommandType = CommandType.Text;
        //        Inactivar.Parameters.AddWithValue("@codigo1", SqlDbType.Int).Value = codigo1;
        //        Inactivar.Parameters.AddWithValue("@codigo2", SqlDbType.Int).Value = codigo2;
        //        Inactivar.ExecuteNonQuery();
        //        // MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Cambio Exitoso');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "exitoRol3();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Cambio Exitoso').set('label', 'Aceptar');", true);
        //        perfil();
        //        GridView1.SelectedIndex = -1;
        //        Label6.Text = "";
        //        TextBox4.Text = "";
        //        TextBox6.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede borrar intente mas tarde');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    cerrar.closeallconnections();
        //}

        //[WebMethod]
        //public static List<string> GetEmployeeName(string empName)
        //{
        //    Conexion con = new Conexion();
        //    CloseAllConnections cerrar = new CloseAllConnections();
        //    List<string> empResult = new List<string>();
        //    /*using (SqlConnection con = new SqlConnection(@"data source=DESKTOP-LOFE9GQ\SQLEXPRESS;Min Pool Size=0;Max Pool Size=10024;Pooling=true;Initial catalog=otra; integrated security=true"))
        //    {*/
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.CommandText = "select Top 10 GNCodUsu from Usuario where GNCodUsu LIKE ''+@SearchEmpName+'%'";
        //        cmd.Connection = con.OpenConnection();
        //        cmd.Parameters.AddWithValue("@SearchEmpName", empName);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            empResult.Add(dr["GNCodUsu"].ToString());
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //        return empResult;
        //    }
        //    //}
        //}

        //[WebMethod]
        //public static List<string> GetEmployeRol(string empName)
        //{
        //    Conexion con = new Conexion();
        //    CloseAllConnections cerrar = new CloseAllConnections();
        //    List<string> empResult = new List<string>();
        //    /*using (SqlConnection con = new SqlConnection(@"data source=DESKTOP-LOFE9GQ\SQLEXPRESS;Min Pool Size=0;Max Pool Size=10024;Pooling=true;Initial catalog=otra; integrated security=true"))
        //    {*/
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.CommandText = "select Top 10 nombreRol from Rol where EstRol = 1 and nombreRol LIKE ''+@SearchEmpName+'%'";
        //        cmd.Connection = con.OpenConnection();
        //        cmd.Parameters.AddWithValue("@SearchEmpName", empName);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            empResult.Add(dr["nombreRol"].ToString());
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //        return empResult;
        //    }
        //    //}
        //}

        //private void BuscarOperaciones()
        //{
        //    if (DropDownList1.SelectedValue == "Areas")
        //    {
        //        SqlCommand areas;
        //        SqlDataReader area;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            areas = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'areas'", con.OpenConnection());
        //            areas.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(areas);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            area = areas.ExecuteReader();
        //            try
        //            {
        //                if (area.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //              //  MessageBox.Show(er.Message + "area");
        //                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show(ex.Message + "area");
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Cargos")
        //    {
        //        SqlCommand encontrar3;
        //        SqlDataReader rd3;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar3 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'cargos'", con.OpenConnection());
        //            encontrar3.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar3);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd3 = encontrar3.ExecuteReader();
        //            try
        //            {
        //                if (rd3.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //                //MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //          //  MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Departamentos")
        //    {
        //        SqlCommand encontrar4;
        //        SqlDataReader rd4;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar4 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'departamentos'", con.OpenConnection());
        //            encontrar4.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar4);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd4 = encontrar4.ExecuteReader();
        //            try
        //            {
        //                if (rd4.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //               // MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //           // MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Eps")
        //    {
        //        SqlCommand encontrar5;
        //        SqlDataReader rd5;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar5 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'eps'", con.OpenConnection());
        //            encontrar5.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar5);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd5 = encontrar5.ExecuteReader();
        //            try
        //            {
        //                if (rd5.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //               // MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Usuarios")
        //    {
        //        SqlCommand encontrar6;
        //        SqlDataReader rd6;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar6 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'usuario'", con.OpenConnection());
        //            encontrar6.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar6);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd6 = encontrar6.ExecuteReader();
        //            try
        //            {
        //                if (rd6.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //                //MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Listas de chequeo")
        //    {
        //        SqlCommand encontrar7;
        //        SqlDataReader rd7;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar7 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'lista de chequeo'", con.OpenConnection());
        //            encontrar7.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar7);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd7 = encontrar7.ExecuteReader();
        //            try
        //            {
        //                if (rd7.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //              //  MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //          //  MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Reportes")
        //    {
        //        SqlCommand encontrar8;
        //        SqlDataReader rd8;
        //        try
        //        {

        //            DataTable dt = new DataTable();
        //            encontrar8 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod from operaciones, modulos where modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'reportes'", con.OpenConnection());
        //            encontrar8.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar8);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd8 = encontrar8.ExecuteReader();
        //            try
        //            {
        //                if (rd8.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //             ///   MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //           /// MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Permisos")
        //    {
        //        SqlCommand encontrar9;
        //        SqlDataReader rd9;
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            encontrar9 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod  " +
        //                "from operaciones, modulos where not nombreOp = 'actualizar' and modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'permisos'", con.OpenConnection());
        //            encontrar9.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar9);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd9 = encontrar9.ExecuteReader();
        //            try
        //            {
        //                if (rd9.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //               // MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //           // MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Reuniones")
        //    {
        //        SqlCommand encontrar9;
        //        SqlDataReader rd9;
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            encontrar9 = new SqlCommand("select codigoP,operaciones.nombreOp, operaciones.codigoM,modulos.nombreMod  " +
        //                "from operaciones, modulos where not nombreOp = 'actualizar' and modulos.codigoM = operaciones.codigoM and modulos.nombreMod = 'reuniones'", con.OpenConnection());
        //            encontrar9.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(encontrar9);
        //            DataSet ds = new DataSet();
        //            da.Fill(dt);

        //            rd9 = encontrar9.ExecuteReader();
        //            try
        //            {
        //                if (rd9.Read() == true)
        //                {
        //                    GridView2.DataSource = dt;
        //                    GridView2.DataBind();
        //                }
        //                else
        //                {
        //                }
        //            }
        //            catch (Exception er)
        //            {
        //               // MessageBox.Show(er.Message);
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show(ex.Message);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //        con.CloseConnection();
        //        cerrar.closeallconnections();
        //    }
        //    else if (DropDownList1.SelectedValue == "Todos")
        //    {
        //        ConsultarOperaciones();
        //    }
        //    Roles();
        //    cerrar.closeallconnections();
        //}
        //private void Roles()
        //{

        //    SqlCommand encontrar8;
        //    SqlDataReader rd8;
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        encontrar8 = new SqlCommand("select * from Rol where Rol.nombreRol = '" + TextBox8.Text + "'", con.OpenConnection());
        //        encontrar8.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(encontrar8);
        //        DataSet ds = new DataSet();
        //        da.Fill(dt);

        //        rd8 = encontrar8.ExecuteReader();
        //        try
        //        {
        //            if (rd8.Read() == true)
        //            {
        //                GridView1.DataSource = dt;
        //                GridView1.DataBind();
        //            }
        //            else
        //            {
        //            }
        //        }
        //        catch (Exception er)
        //        {
        //         //   MessageBox.Show(er.Message);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //        }
        //        finally
        //        {
        //            con.CloseConnection();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        ///Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}
        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    Session.Remove("admin");
        //    Response.Redirect("~/Log%20in/Login.aspx");
        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if (TextBox2.Text != "")
        //    {
        //        AddRol(int.Parse(TextBox1.Text), TextBox2.Text);
        //        ConsultarRoles();
        //    }
        //    else
        //    {
        //        guardado.Visible = false;
        //        Error.Visible = true;
        //        //MessageBox.Show("Ingrese el nombre del rol", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //}
        //public void ConsultarRoles()
        //{
        //    SqlDataReader leer;
        //    SqlCommand consult;


        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        consult = new SqlCommand("select codigoR,nombreRol from Rol where Rol.EstRol = 1", con.OpenConnection());
        //        consult.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(consult);
        //        DataSet ds = new DataSet();
        //        da.Fill(dt);

        //        leer = consult.ExecuteReader();
        //        if (leer.Read())
        //        {
        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //        }


        //        /*foreach (GridViewRow row in GridView1.Rows)
        //        {
        //            String cellText = row.Cells[1].Text;
        //            //MessageBox.Show(cellText);
        //            if (cellText == "30000")
        //            {
        //                row.Cells[0].Enabled = false;
        //            }
        //        }*/
        //    }
        //    catch (Exception ex)
        //    {
        //      //  MessageBox.Show(ex.Message);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
        //    }
        //    finally
        //    {
        //        con.CloseConnection();
        //    }
        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ConsultarPermisos();
        //        Roles();
        //        Button4.Attributes.Remove("disabled");
        //        Button5.Attributes.Remove("disabled");
        //        Label6.Text = "";
        //        ListBox1.Items.Clear();
        //        ListBox2.Items.Clear();
        //        Roles();
        //        Label6.Text = GridView1.SelectedRow.Cells[1].Text.ToString();
        //        //TextBox2.Text = GridView1.SelectedRow.Cells[2].Text.ToString();
        //        TextBox4.Text = GridView1.SelectedRow.Cells[1].Text.ToString();
        //        TextBox6.Text = GridView1.SelectedRow.Cells[1].Text.ToString();
        //        ConsultarPermisos();
        //        Roles();
        //        guardado.Visible = false;
        //        if (Label6.Text == "30000")
        //        {
        //            Button4.Attributes.Add("disabled", "disabled");
        //            Button5.Attributes.Add("disabled", "disabled");
        //            //Button3.Enabled = false;
        //            //Button2.Enabled = false;
        //            //Button5.Enabled = false;
        //            guardado.Visible = false;
        //        }
        //        else
        //        {
        //            Button4.Attributes.Remove("disabled");
        //            Button5.Attributes.Remove("disabled");
        //            //Button3.Enabled = true;
        //            //Button2.Enabled = true;
        //            //Button5.Enabled = true;
        //            guardado.Visible = false;
        //            guardado.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //     //   Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('"+ex.Message+"');", true);
        //    }
        //}

        //protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BuscarOperaciones();
        //    TextBox7.Text = GridView2.SelectedRow.Cells[1].Text.ToString();
        //    Label7.Text = GridView2.SelectedRow.Cells[3].Text.ToString();
        //    BuscarOperaciones();
        //    guardado.Visible = false;
        //    guardado.Visible = false;
        //}

        //protected void Button4_Click(object sender, EventArgs e)
        //{
        //    if (TextBox6.Text != "" && TextBox7.Text != "")
        //    {
        //        Roles();
        //        AddPerfil(int.Parse(TextBox5.Text), int.Parse(TextBox6.Text), int.Parse(TextBox7.Text));
        //        Roles();
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //    else
        //    {
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Seleccione el rol, las operaciones y los modulos');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Seleccione el rol, las operaciones y los modulos')", true);
        //        //MessageBox.Show("Seleccione el rol, las operaciones y los modulos", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //}

        //protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var strin1 = ListBox1.SelectedIndex;
        //    ListBox2.SelectedIndex = int.Parse(strin1.ToString());
        //}

        //protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var strin2 = ListBox2.SelectedIndex.ToString();
        //    ListBox1.SelectedIndex = int.Parse(strin2.ToString());
        //    var seleccionar1 = ListBox2.SelectedValue;
        //    var string1 = seleccionar1.Substring(2);
        //    var seleccionar2 = ListBox1.SelectedValue;
        //    var string2 = seleccionar2.Substring(2);

        //    SqlCommand encontrar;
        //    SqlDataReader rd;

        //    encontrar = new SqlCommand("select operaciones.codigoP,  operaciones.nombreOp, modulos.codigoM, modulos.nombreMod " +
        //        "from operaciones, modulos " +
        //        "where modulos.codigoM = operaciones.codigoM " +
        //        "and operaciones.nombreOp = '" + string1 + "' " +
        //        "and modulos.nombreMod = '" + string2 + "'", con.OpenConnection());

        //    rd = encontrar.ExecuteReader();
        //    if (rd.Read())
        //    {
        //        Label3.Text = rd["codigoP"].ToString();
        //        Label4.Text = rd["codigoM"].ToString();

        //        SqlCommand encontrar2;
        //        SqlDataReader rd2;

        //        encontrar2 = new SqlCommand("select Perfil_operacion.codigoPO " +
        //            "from Perfil_operacion, Rol, operaciones " +
        //            "where Rol.codigoR = Perfil_operacion.codigoR " +
        //            "and operaciones.codigoP = Perfil_operacion.codigoP " +
        //            "and Rol.codigoR = " + int.Parse(TextBox4.Text) + " and operaciones.codigoP = " + int.Parse(Label3.Text) + "", con.OpenConnection());

        //        rd2 = encontrar2.ExecuteReader();
        //        if (rd2.Read())
        //        {
        //            Label5.Text = rd2["codigoPO"].ToString();
        //        }
        //        else
        //        {
        //            Label5.Text = "no hay datos";
        //        }
        //    }
        //    else
        //    {
        //        Label3.Text = "no hay datos";
        //    }

        //    con.CloseConnection();
        //    cerrar.closeallconnections();
        //}

        //protected void Button5_Click(object sender, EventArgs e)
        //{
        //    if (ListBox2.SelectedItem == null)
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol2();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Seleccione el rol y la operacion que desea Quitar');", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Seleccione el rol y la operacion que desea Quitar').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("Seleccione el rol y la operacion que desea Quitar", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //    else
        //    {
        //        Roles();
        //        borrar(int.Parse(TextBox4.Text), int.Parse(Label3.Text));
        //        Roles();
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //}

        //protected void TextBox3_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBox3.Text != "")
        //    {
        //        perfil();
        //        Roles();
        //        GridView1.SelectedIndex = -1;
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //    else
        //    {
        //        ConsultarRoles();
        //        GridView1.SelectedIndex = -1;
        //        TextBox3.Text = "";
        //        TextBox4.Text = "";
        //        Label2.Visible = false;
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    if (TextBox3.Text != "" && TextBox4.Text != "")
        //    {

        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol3();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ingrese el nombre del usuario y Seleccione el rol');", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Ingrese el nombre del usuario y Seleccione el rol').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("Ingrese el nombre del usuario y Seleccione el rol", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //}

        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    if (TextBox3.Text != "" && TextBox4.Text != "")
        //    {
        //        Cambiar(int.Parse(TextBox4.Text), int.Parse(TextBox3.Text));
        //        guardado.Visible = false;
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol3();", true);
        //        // Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ingrese el nombre del usuario y Seleccione el rol');", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Ingrese el nombre del usuario y Seleccione el rol').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("Ingrese el nombre del usuario y Seleccione el rol", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        guardado.Visible = false;
        //        guardado.Visible = false;
        //    }
        //}

        //protected void Button7_Click(object sender, EventArgs e)
        //{
        //    if (Label6.Text != "Label")
        //    {
        //        if (Label6.Text == "30000")
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol4();", true);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar este Rol');", true);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','No se puede eliminar este Rol').set('label', 'Aceptar');", true);
        //            // MessageBox.Show("No se puede eliminar este Rol", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        }
        //        else
        //        {
        //            try
        //            {
        //                SqlCommand Inactivar;
        //                Inactivar = new SqlCommand("update Rol set EstRol = 0 where Rol.codigoR = @codigo", con.OpenConnection());
        //                Inactivar.CommandType = CommandType.Text;
        //                Inactivar.Parameters.AddWithValue("@codigo", SqlDbType.Int).Value = int.Parse(Label6.Text);
        //                Inactivar.ExecuteNonQuery();
        //                ClientScript.RegisterStartupScript(GetType(), "vermensaje", "exitoRol();", true);
        //                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Rol eliminado con exito');", true);
        //                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Rol eliminado con exito').set('label', 'Aceptar');", true);
        //                //MessageBox.Show("Rol eliminado con exito", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //                ConsultarRoles();
        //                ListBox1.Items.Clear();
        //                ListBox2.Items.Clear();
        //                TextBox4.Text = "";
        //            }
        //            catch (Exception ex)
        //            {
        //                //MessageBox.Show("No se puede eliminar el rol", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //            }
        //            finally
        //            {
        //                con.CloseConnection();
        //            }
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Exito');", true);
        //            ClientScript.RegisterStartupScript(GetType(), "vermensaje", "exitoRol2();", true);
        //            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Exito').set('label', 'Aceptar');", true);
        //            //MessageBox.Show("Exito", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //        }
        //    }
        //    else
        //    {
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Seleccione el Rol que desea eliminar');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol5();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Seleccione el Rol que desea eliminar').set('label', 'Aceptar');", true);
        //        // MessageBox.Show("Seleccione el Rol que desea eliminar", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //}

        //protected void Button8_Click(object sender, EventArgs e)
        //{
        //    if (GridView2.SelectedIndex != -1)
        //    {
        //        GridView2.SelectedIndex = -1;
        //        TextBox5.Text = "";
        //        Label7.Text = "";
        //        BuscarOperaciones();
        //    }
        //    else
        //    {
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No hay filas seleccionadas');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol6();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','No hay filas seleccionadas').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("No hay filas seleccionadas", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //}

        //protected void Button6_Click(object sender, EventArgs e)
        //{
        //    if (GridView1.SelectedIndex != -1)
        //    {
        //        GridView1.SelectedIndex = -1;
        //        if (TextBox4.Text == "30000")
        //        {
        //            Button4.Attributes.Remove("disabled");
        //            Button5.Attributes.Remove("disabled");
        //            TextBox6.Text = "";
        //        }
        //        TextBox4.Text = "";
        //        Label6.Text = "";
        //        TextBox7.Text = "";
        //        TextBox2.Text = "";
        //        TextBox6.Text = "";
        //        ListBox1.Items.Clear();
        //        ListBox2.Items.Clear();
        //        Roles();
        //        Button4.Attributes.Remove("disabled");
        //        Button5.Attributes.Remove("disabled");
        //    }
        //    else
        //    {
        //        // Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No hay filas seleccionadas');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol6();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','No hay filas seleccionadas').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("No hay filas seleccionadas", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //    MaintainScrollPositionOnPostBack = true;
        //}

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList1.SelectedValue != "Filtrar por")
        //    {
        //        GridView2.SelectedIndex = -1;
        //        TextBox5.Text = "";
        //        Label7.Text = "";
        //        BuscarOperaciones();
        //    }
        //    else
        //    {
        //        // Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Selccione una opcion');", true);
        //        ClientScript.RegisterStartupScript(GetType(), "vermensaje", "errorRol7();", true);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alertify.alert('Clinica Crecer','Selccione una opcion').set('label', 'Aceptar');", true);
        //        //MessageBox.Show("Selccione una opcion", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        //    }
        //}

        //protected void TextBox8_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBox8.Text != "")
        //    {
        //        GridView1.SelectedIndex = -1;
        //        Roles();
        //        ListBox1.Items.Clear();
        //        ListBox2.Items.Clear();
        //        Label6.Text = "";
        //        TextBox4.Text = "";
        //        TextBox7.Text = "";
        //    }
        //    else
        //    {
        //        GridView1.SelectedIndex = -1;
        //        ConsultarRoles();
        //        ListBox1.Items.Clear();
        //        ListBox2.Items.Clear();
        //        Label6.Text = "";
        //        TextBox4.Text = "";
        //        TextBox7.Text = "";
        //    }
        //}

        //protected void Button9_Click(object sender, EventArgs e)
        //{
        //}
    }
}
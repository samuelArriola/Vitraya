using Generales_1._0.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Generales_1._0.Class.DTOGenerales;
using Generales_1._0.Class.DAOGenerales;

namespace Generales_1._0.Home.dashboard.production.screens.proceedings
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Conexion cone = new Conexion();
        Processes Processes = new Processes();
        CloseAllConnections cerrar = new CloseAllConnections();
        Encryption Encriptacion = new Encryption();
        int e = 30000;
        StringBuilder Comandos = new StringBuilder();
        int codigoModulo = 10011;
        int CodigoCrear = 20027;
        int CodigoActualizar = 20028;
        int CodigoEliminar = 20029;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                CodeProcesos();
                ListarProcesos();
                GridProcesos.Columns[0].Visible = false;
                foto();
                cone.CloseConnection();
                cerrar.closeallconnections();
            }
        }

        private void verification()
        {
            try
            {
                Label1.Text = Session["admin"].ToString();
                foto();
                //buscar();
                //buscarCodigosArea();
            }
            catch (Exception)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
            }

            //if (Label3.Text == "20027")
            //{
            //    Button1.Visible = true;
            //}
            //else
            //{
            //    Button1.Visible = false;
            //}

            //if (Label10.Text == "20028")
            //{
            //    GridProcesos.Columns[2].Visible = true;
            //}
            //else
            //{
            //    GridProcesos.Columns[2].Visible = false;
            //}

            //if (Label6.Text == "20029")
            //{
            //    GridProcesos.Columns[3].Visible = true;
            //}
            //else
            //{
            //    GridProcesos.Columns[3].Visible = false;
            //}

            //if(Label3.Text == "00000" && Label10.Text == "00000" && Label6.Text == "00000")
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
        private void buscarCodigosArea()
        {
            SqlCommand Crear;
            SqlDataReader Crearrd;

            Crear = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulo + "" +
                "and operaciones.codigoP = " + CodigoCrear + "", cone.OpenConnection());
            Crearrd = Crear.ExecuteReader();

            if (Crearrd.Read())
            {
                Label3.Text = Crearrd["codigoP"].ToString();
            }
            else
            {
                Label3.Text = "00000";
            }
            SqlCommand Actualizar;
            SqlDataReader Actualizarrd;

            Actualizar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulo + "" +
                "and operaciones.codigoP = " + CodigoActualizar + "", cone.OpenConnection());
            Actualizarrd = Actualizar.ExecuteReader();

            if (Actualizarrd.Read())
            {
                Label10.Text = Actualizarrd["codigoP"].ToString();
            }
            else
            {
                Label10.Text = "00000";
            }
            SqlCommand Eliminar;
            SqlDataReader Eliminarrd;

            Eliminar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulo + "" +
                "and operaciones.codigoP = " + CodigoEliminar + "", cone.OpenConnection());
            Eliminarrd = Eliminar.ExecuteReader();

            if (Eliminarrd.Read())
            {
                Label6.Text = Eliminarrd["codigoP"].ToString();
            }
            else
            {
                Label6.Text = "00000";
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void foto()
        {
            string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label1.Text));

            Image1.ImageUrl = string.Format("../general/BuscarImagenes.aspx?id=" + id_lista);
            Image2.ImageUrl = string.Format("../general/BuscarImagenes.aspx?id=" + id_lista);
            SqlCommand buscar;
            SqlDataReader rd;

            buscar = new SqlCommand("select GNNomUsu from Usuario where GNCodUsu =  '" + int.Parse(Label1.Text) + "'", cone.OpenConnection());
            rd = buscar.ExecuteReader();

            if (rd.Read())
            {
                Label2.Text = rd["GNNomUsu"].ToString();
                Label9.Text = rd["GNNomUsu"].ToString();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodeProcesos()//leer el codigo del departamento e incrementarle uno
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
               // conn = new SqlCommand("select top 1 GnOIdPrs from Procesos where Procesos.GnstPrs='Activo' order by GnOIdPrs desc", cone.OpenConnection());
                conn = new SqlCommand("select GnOIdPrs from Procesos where GnOIdPrs = (select MAX(GnOIdPrs) from Procesos) group by GnOIdPrs", cone.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["GnOIdPrs"].ToString();

                    lol = int.Parse(numero);
                    sg = lol + 1;

                    Label4.Text = sg.ToString();
                }
                else
                {
                    Label4.Text = e.ToString();
                }
            }

            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void ListarProcesos()//metodo para llenar el dropdownlist desde la base de datos
        {

            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                DataTable dt = new DataTable();
                consult = new SqlCommand("select GnOIdPrs,GnNmPrs from Procesos where GnstPrs='Activo' order by GnNmPrs asc", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    GridProcesos.DataSource = dt;
                    GridProcesos.DataBind();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                cone.CloseConnection();
            }

            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void GuardarProcesos(string eps2)
        {
            SqlDataReader leer;
            SqlCommand consult;


            try
            {
                consult = new SqlCommand("select Procesos.GnNmPrs from Procesos where GnNmPrs=@GnNmPrs", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@GnNmPrs", eps2);
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(ds);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    string carg = leer["GnNmPrs"].ToString();
                    if (TextBox1.Text == carg)
                    {
                        //Label8.Text = "Este Proceso ya se encuentra registrado";
                        //Label8.ForeColor = Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "errore", "error()", true);
                    }
                }
                else
                {
                    CodeProcesos();
                    Processes.AddProceso(int.Parse(Label4.Text), TextBox1.Text);
                    //Label8.Text = "Registro exitoso";
                    //Label8.ForeColor = Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "exito", "Exito()", true);
                    TextBox1.Text = "";
                    CodeProcesos();
                    ListarProcesos();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + "Eps");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);

            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void filtrarProcesos()
        {
            if (TextBox2.Text != "")
            {
                SqlDataReader leer;
                SqlCommand consult;

                try
                {
                    DataTable dt = new DataTable();
                    consult = new SqlCommand("select GnOIdPrs,GnNmPrs from Procesos where GnstPrs='Activo' and GnNmPrs like '%" + TextBox2.Text + "%'", cone.OpenConnection());
                    consult.CommandType = CommandType.Text;
                    consult.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(consult);
                    DataSet ds = new DataSet();
                    da.Fill(dt);

                    leer = consult.ExecuteReader();
                    if (leer.Read())
                    {
                        GridProcesos.DataSource = dt;
                        GridProcesos.DataBind();
                        Label8.Text = "";
                    }
                    else
                    {
                        Label8.Text = "El Proceso ingresada no se encuentra registrado";
                        Label8.ForeColor = Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
                }
                finally
                {
                    cone.CloseConnection();
                }
            }
            else
            {
                Label2.Text = "";
                ListarProcesos();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    //Label8.Text = "Ingrese el nombre de la Eps";
                    //Label8.ForeColor = Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "eror", "Error2()", true);
                }
                else
                {
                    GuardarProcesos(TextBox1.Text);
                }

                ListarProcesos();
                CodeProcesos();
                TextBox1.Text = "";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                filtrarProcesos();
            }
            else
            {
                CodeProcesos();
                ListarProcesos();
                Label8.Text = "";
            }
        }

        protected void GridArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           if(TextBox2.Text == "")
            {
                GridProcesos.EditIndex = -1;
                CodeProcesos();
                ListarProcesos();
                //TextBox1.Text = "";
                Label8.Text = "";
            }
            else
            {
                GridProcesos.EditIndex = -1;
                filtrarProcesos();
                Label8.Text = "";
            }
        }

        protected void GridArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlCommand query;

            try
            {
                int codigo = Convert.ToInt32(GridProcesos.DataKeys[e.RowIndex].Values[0]);
                query = new SqlCommand("DELETE FROM Procesos  WHERE Procesos.GnOIdPrs = @GnOIdPrs", cone.OpenConnection());
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@GnOIdPrs", SqlDbType.Int).Value = codigo;
                query.ExecuteReader();
                GridProcesos.EditIndex = -1;
                CodeProcesos();
                ListarProcesos();
                Label8.Text = "";
            }
            catch (Exception ex)
            {
                int codigo = Convert.ToInt32(GridProcesos.DataKeys[e.RowIndex].Values[0]);
                string estado = "Inactivo";
                Processes.DisableProceso(codigo, estado);
                CodeProcesos();
                ListarProcesos();
                Label8.Text = "";
            }
            finally//cerrar la conexion
            {
                cone.CloseConnection();
            }
            cerrar.closeallconnections();
        }

        protected void GridArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (TextBox2.Text != "")
            {
                GridProcesos.EditIndex = e.NewEditIndex;
                filtrarProcesos();
                Label8.Text = "";

            }
            else
            {
                GridProcesos.EditIndex = e.NewEditIndex;
                CodeProcesos();
                ListarProcesos();
                Label8.Text = "";
            }
        }

        protected void GridArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridProcesos.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridProcesos.DataKeys[e.RowIndex].Values["GnOIdPrs"]);

            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            Processes.UpdateProceso(codigo, nombre);


            GridProcesos.EditIndex = -1;
            CodeProcesos();
            ListarProcesos();
            TextBox2.Text = "";
            Label8.Text = "";
        }
    }
}
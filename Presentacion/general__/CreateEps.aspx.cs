using Generales_1._0.Class;
using Generales_1._0.Class.DAOGenerales;
using Generales_1._0.Class.DTOGenerales;
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
using System.Windows.Forms;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class CreateEps : System.Web.UI.Page
    {
        Conexion cone = new Conexion();
        CrudEps eps = new CrudEps();
        Encryption Encriptacion = new Encryption();
        CloseAllConnections cerrar = new CloseAllConnections();
        int e = 4000;
        int codigoModulo = 10004;
        int CodigoCrear = 20010;
        int CodigoActualizar = 20011;
        int CodigoEliminar = 20012;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                CodeEps();
                ListarEps();
                GridEps.Columns[0].Visible = false;
                cone.CloseConnection();
                cerrar.closeallconnections();
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
                cone.CloseConnection();
            }

            //if (Label16.Text == "20010")//Crear
            //{
            //    agregar.Visible = true;
            //}
            //else
            //{
            //    agregar.Visible = false;
            //}

            //if (Label3.Text == "20011")//Actualizar
            //{
            //    GridEps.Columns[2].Visible = true;
            //}
            //else
            //{
            //    GridEps.Columns[2].Visible = false;
            //}

            //if (Label5.Text == "20012")//Eliminar
            //{
            //    GridEps.Columns[3].Visible = true;
            //}
            //else
            //{
            //    GridEps.Columns[3].Visible = false;
            //}

            //if (Label16.Text == "00000" && Label3.Text == "00000" && Label5.Text == "00000")
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

            buscar = new SqlCommand("select GNNomUsu from Usuario where GNCodUsu =  '" + int.Parse(Label1.Text) + "'", cone.OpenConnection());
            rd = buscar.ExecuteReader();

            if (rd.Read())
            {
                Label6.Text = rd["GNNomUsu"].ToString();
                Label9.Text = rd["GNNomUsu"].ToString();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void buscarCodigos()
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
                Label16.Text = Crearrd["codigoP"].ToString();
            }
            else
            {
                Label16.Text = "00000";
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
                Label3.Text = Actualizarrd["codigoP"].ToString();
            }
            else
            {
                Label3.Text = "00000";
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
                Label5.Text = Eliminarrd["codigoP"].ToString();
            }
            else
            {
                Label5.Text = "00000";
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodeEps()//leer el codigo del departamento e incrementarle uno
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
                //conn = new SqlCommand("select top 1 GnCodEps from Eps where Eps.GnEstEps='Activo' order by GnCodEps desc", cone.OpenConnection());
                conn = new SqlCommand("select GnCodEps from Eps where GnCodEps = (select MAX(GnCodEps) from Eps) group by GnCodEps", cone.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["GnCodEps"].ToString();

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
        public void ListarEps()//metodo para llenar el dropdownlist desde la base de datos
        {

            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                DataTable dt = new DataTable();
                consult = new SqlCommand("select GnCodEps,GnNomEps from Eps where GnEstEps='Activo' order by GnNomEps asc", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    GridEps.DataSource = dt;
                    GridEps.DataBind();
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
        public void GuardarEps(string eps2)
        {
            SqlDataReader leer;
            SqlCommand consult;


            try
            {
                consult = new SqlCommand("select Eps.GnNomEps from Eps where GnNomEps=@GnNomEps", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@GnNomEps", eps2);
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(ds, "Eps");

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    string carg = leer["GnNomEps"].ToString();
                    if (TextBox1.Text == carg)
                    {
                        //Label2.Text = "Esta Eps ya se encuentra registrada";
                        //Label2.ForeColor = Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "error()", true);
                    }
                }
                else
                {
                    CodeEps();
                    eps.AddEps(float.Parse(Label4.Text), TextBox1.Text);
                    //Label2.Text = "Registro exitoso";
                    //Label2.ForeColor = Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "Exito()", true);
                    TextBox1.Text = "";
                    CodeEps();
                    ListarEps();
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
        private void filtrarEpss()
        {
            if (TextBox2.Text != "")
            {
                SqlDataReader leer;
                SqlCommand consult;

                try
                {
                    DataTable dt = new DataTable();
                    consult = new SqlCommand("select GnCodEps,GnNomEps from Eps where GnEstEps='Activo' and GnNomEps like '%" + TextBox2.Text + "%'", cone.OpenConnection());
                    consult.CommandType = CommandType.Text;
                    consult.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(consult);
                    DataSet ds = new DataSet();
                    da.Fill(dt);

                    leer = consult.ExecuteReader();
                    if (leer.Read())
                    {
                        GridEps.DataSource = dt;
                        GridEps.DataBind();
                    }
                    else
                    {
                        Label2.Text = "La eps ingresada no se encuentra registrada";
                        Label2.ForeColor = Color.Red;
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
                ListarEps();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }

        protected void GridEps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEps.EditIndex = -1;
            GridEps.PageIndex = e.NewPageIndex;
            Label2.Text = "";
        }

        protected void GridEps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           if(TextBox2.Text == "")
            {
                GridEps.EditIndex = -1;
                CodeEps();
                ListarEps();
                //TextBox1.Text = "";
            }
            else
            {
                GridEps.EditIndex = -1;
                filtrarEpss();
            }
        }

        protected void GridEps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlCommand query;

            try
            {
                int codigo = Convert.ToInt32(GridEps.DataKeys[e.RowIndex].Values[0]);
                query = new SqlCommand("DELETE FROM Eps  WHERE Eps.GnCodEps = @GnCodEps", cone.OpenConnection());
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@GnCodEps", SqlDbType.Int).Value = codigo;
                query.ExecuteReader();
                GridEps.EditIndex = -1;
                CodeEps();
                ListarEps();
            }
            catch (Exception ex)
            {
                int codigo = Convert.ToInt32(GridEps.DataKeys[e.RowIndex].Values[0]);
                string estado = "Inactivo";
                eps.DisableEps(codigo, estado);
                CodeEps();
                ListarEps();
            }
            finally//cerrar la conexion
            {
                cone.CloseConnection();
            }
            cerrar.closeallconnections();
        }

        protected void GridEps_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (TextBox2.Text != "")
            {
                GridEps.EditIndex = e.NewEditIndex;
                filtrarEpss();

            }
            else
            {
                GridEps.EditIndex = e.NewEditIndex;
                CodeEps();
                ListarEps();
            }
        }

        protected void GridEps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridEps.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridEps.DataKeys[e.RowIndex].Values["GnCodEps"]);

            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            eps.UpdateEps(codigo, nombre);


            GridEps.EditIndex = -1;
            CodeEps();
            ListarEps();
            TextBox2.Text = "";
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if(TextBox2.Text != "")
            {
                filtrarEpss();
            }
            else
            {
                CodeEps();
                ListarEps();
                Label2.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    //Label2.Text = "Ingrese el nombre de la Eps";
                    //Label2.ForeColor = Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "Error2()", true);
                }
                else
                {
                    GuardarEps(TextBox1.Text);
                }

                ListarEps();
                CodeEps();
                TextBox1.Text = "";
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
        }
    }
}
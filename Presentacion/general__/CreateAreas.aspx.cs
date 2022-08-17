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
    public partial class CreateAreas : System.Web.UI.Page
    {
        int are = 1000;
        Conexion cone = new Conexion();
        CrudArea area = new CrudArea();
        Encryption Encriptacion = new Encryption();
        CloseAllConnections cerrar = new CloseAllConnections();
        //areas
        int codigoModulo = 10001;
        int CodigoCrear = 20001;
        int CodigoActualizar = 20002;
        int CodigoEliminar = 20003;

        string NombreArea;
        string SiglaArea;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                CodeArea();
                Area();
                GridArea.Columns[0].Visible = false;
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                TextBox4.Attributes.Add("autocomplete", "off");
                cone.CloseConnection();
                cerrar.closeallconnections();
                TextBox4.Enabled = false;
                Button1.Enabled = false;
            }
            validacionRoles();
        }
        private void verification()
        {
            try
            {
                Label1.Text = Session["admin"].ToString();
                //buscar();
                //buscarCodigosArea();
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

            //if (Label3.Text == "20001")//Crear
            //{
            //    Agregar.Visible = true;
            //}
            //else
            //{
            //    Agregar.Visible = false;
            //}

            //if (Label4.Text == "20002")//Actualizar
            //{
            //    GridArea.Columns[2].Visible = true;
            //}
            //else
            //{
            //    GridArea.Columns[2].Visible = false;
            //}

            //if (Label6.Text == "20003")//Eliminar
            //{
            //    GridArea.Columns[3].Visible = true;
            //}
            //else
            //{
            //    GridArea.Columns[3].Visible = false;
            //}

            //if (Label3.Text == "00000" && Label4.Text == "00000" && Label6.Text == "00000")
            //{
            //    string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label1.Text));
            //    Response.Redirect(string.Format("../WebError.aspx?ParametersQuery=" + id_lista));
            //}
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
                Label2.Text = rd["GNNomUsu"].ToString();
                Label9.Text = rd["GNNomUsu"].ToString();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
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
                Label4.Text = Actualizarrd["codigoP"].ToString();
            }
            else
            {
                Label4.Text = "00000";
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
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }
        public void CodeArea()//leer el codigo del area e incrementarle uno
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
                conn = new SqlCommand("select GnCdAra from Area where GnCdAra = (select MAX(GnCdAra) from Area) group by GnCdAra", cone.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["GnCdAra"].ToString();

                    lol = int.Parse(numero);
                    sg = lol + 1;
                    Label5.Text = sg.ToString();
                }
                else
                {
                    Label5.Text = are.ToString();
                }
            }

            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();

        }
        public void Area()//metodo para llenar el dropdownlist desde la base de datos
        {

            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                DataTable dt = new DataTable();
                consult = new SqlCommand("select GnCdAra,GnNomAra,GnSiglaDr from Area where GnEsAre='Activo' order by GnNomAra asc", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    GridArea.DataSource = dt;
                    GridArea.DataBind();
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

            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void FiltrarAreas()
        {
            if (TextBox2.Text != "")
            {
                SqlDataReader leer;
                SqlCommand consult;


                try
                {
                    DataTable dt = new DataTable();
                    consult = new SqlCommand("select GnCdAra,GnNomAra,GnSiglaDr from Area where GnEsAre='Activo' and GnNomAra like '%" + TextBox2.Text + "%'", cone.OpenConnection());
                    consult.CommandType = CommandType.Text;
                    consult.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(consult);
                    DataSet ds = new DataSet();
                    da.Fill(dt);

                    leer = consult.ExecuteReader();
                    if (leer.Read())
                    {
                        GridArea.DataSource = dt;
                        GridArea.DataBind();
                    }
                    else
                    {
                        Label8.Visible = true;
                        Label8.Text = "El area ingresado no se encuentra registrado";
                        Label8.ForeColor = Color.Red;
                        Area();
                        CodeArea();
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
            }
            else
            {
                Label8.Text = "";
                Area();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void GuardarAreas(string Area2, string GnSiglaDr)
        {
            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                consult = new SqlCommand("select area.GnNomAra from Area where GnNomAra=@GnNomAra", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@GnNomAra", Area2);
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(ds, "Area");

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    NombreArea = leer["GnNomAra"].ToString();
                    if (TextBox1.Text == NombreArea)
                    {
                        //Label8.Visible = true;
                        //Label8.Text = "Esta Area ya se encuentra registrada ";
                        //Label8.ForeColor = Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "Direeccio", "Area()", true);
                    }
                }

                try
                {
                    SqlCommand buscar;
                    SqlDataReader reader;

                    buscar = new SqlCommand("select area.GnSiglaDr  from Area where GnSiglaDr =@GnSiglaDr", cone.OpenConnection());
                    buscar.Parameters.AddWithValue("@GnSiglaDr", GnSiglaDr);
                    buscar.ExecuteNonQuery();
                    SqlDataAdapter data = new SqlDataAdapter(buscar);
                    DataSet dataSet = new DataSet();
                    data.Fill(dataSet);
                    reader = buscar.ExecuteReader();
                    if (reader.Read())
                    {
                        SiglaArea = reader["GnSiglaDr"].ToString();
                        if(TextBox4.Text == SiglaArea)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Direeccio", "Sigla()", true);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                if(TextBox1.Text != NombreArea && TextBox4.Text != SiglaArea)
                {
                    CrudArea ar = new CrudArea();
                    ar.AddArea(TextBox1.Text, float.Parse(Label5.Text), TextBox4.Text);
                    //Label8.Text = "Registro exitoso";
                    //Label8.ForeColor = Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "Exito", "Exito()", true);
                    TextBox1.Text = "";
                    TextBox4.Text = "";
                    TextBox4.Enabled = false;
                    Button1.Enabled = false;
                    Area();
                    CodeArea();
                }

            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message + "area");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);

            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        protected void GridArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if(TextBox2.Text == "")
            {
                GridArea.EditIndex = -1;
                CodeArea();
                Area();
                //TextBox1.Text = "";
                Label8.Text = "";
            }
            else
            {
                GridArea.EditIndex = -1;
                FiltrarAreas();
                Label8.Text = "";
            }
        }

        protected void GridArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlCommand query;

            try
            {
                int codigo = Convert.ToInt32(GridArea.DataKeys[e.RowIndex].Values[0]);
                query = new SqlCommand("DELETE FROM Area  WHERE Area.GnCdAra = @GnCdAra", cone.OpenConnection());
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@GnCdAra", SqlDbType.Int).Value = codigo;
                query.ExecuteReader();
                GridArea.EditIndex = -1;
                CodeArea();
                Area();
            }
            catch (Exception ex)
            {
                int codigo = Convert.ToInt32(GridArea.DataKeys[e.RowIndex].Values[0]);
                string estado = "Inactivo";
                area.DisableArea(codigo, estado);
                CodeArea();
                Area();
            }
            finally//cerrar la conexion
            {
                cone.CloseConnection();
            }
            Label8.Text = "";
            cerrar.closeallconnections();
        }

        protected void GridArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridArea.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridArea.DataKeys[e.RowIndex].Values[0]);

            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string Sigla = (fila.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;
            Sigla = (CultureInfo.InvariantCulture.TextInfo.ToUpper(Sigla.ToUpper()));
            area.UpdateArea(codigo, nombre, Sigla);


            GridArea.EditIndex = -1;
            CodeArea();
            Area();
            //TextBox1.Text = "";
            Label8.Text = "";
        }

        protected void GridArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (TextBox2.Text != "")
            {
                GridArea.EditIndex = e.NewEditIndex;
                FiltrarAreas();
                Label8.Text = "";
            }
            else
            {
                GridArea.EditIndex = e.NewEditIndex;
                CodeArea();
                Area();
                Label8.Text = "";
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                FiltrarAreas();
            }
            else
            {
                Label8.Text = "";
                Area();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                GuardarAreas(TextBox1.Text, TextBox4.Text);
            }
            else
            {
                Label8.ForeColor = Color.Red;
                Label8.Text = "Ingrese el nombre del area";
                //MessageBox.Show("", "Clinica Crecer", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if(TextBox1.Text != "")
            {
                TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
                TextBox4.Enabled = true;
            }
            else
            {
                TextBox4.Enabled = false;
            }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (TextBox4.Text != "")
            {
                TextBox4.Text = (CultureInfo.InvariantCulture.TextInfo.ToUpper(TextBox4.Text.ToUpper()));
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }
}
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
    public partial class CreateCargos : System.Web.UI.Page
    {
        Conexion cone = new Conexion();
        CrudRole rol = new CrudRole();
        Encryption Encriptacion = new Encryption();
        int car = 2000;
        CloseAllConnections cerrar = new CloseAllConnections();

        //Cargos
        int codigoModuloc = 10002;
        int CodigoCrearc = 20004;
        int CodigoActualizarc = 20005;
        int CodigoEliminarc = 20006;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                Cargo();
                CodeCargo();
                GridCargo.Columns[0].Visible = false;
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                cone.CloseConnection();
                cerrar.closeallconnections();
                DropDownList1.Enabled = false;
                UnidadFuncional_Departamento();
                Button1.Enabled = false;
            }
        }
        private void verification()
        {
            try
            {
                Label1.Text = Session["admin"].ToString();
                //buscar();
                //buscarCodigoCargo();
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
            //*********************************************************************//
            //if (Label20.Text == "20004")//Crear
            //{
            //    guardar.Visible = true;
            //}
            //else
            //{
            //    guardar.Visible = false;
            //}

            //if (Label21.Text == "20005")//Actualizar
            //{
            //    GridCargo.Columns[2].Visible = true;
            //}
            //else
            //{
            //    GridCargo.Columns[2].Visible = false;
            //}

            //if (Label22.Text == "20006")//Eliminar
            //{
            //    GridCargo.Columns[3].Visible = true;
            //}
            //else
            //{
            //    GridCargo.Columns[3].Visible = false;
            //}

            //if (Label20.Text == "00000" && Label21.Text == "00000" && Label22.Text == "00000")
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
        public void UnidadFuncional_Departamento()
        {
            SqlCommand buscar;
            SqlDataReader rd;
            buscar = new SqlCommand("SELECT [GnNomDep],[GnDcDep] FROM [Departamento] order by GnNomDep asc", cone.OpenConnection());
            buscar.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "GnNomDep";
            DropDownList1.DataValueField = "GnDcDep";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Seleccione...");
            cone.CloseConnection();
            cerrar.closeallconnections();
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
        private void buscarCodigoCargo()
        {
            SqlCommand Crear;
            SqlDataReader Crearrd;

            Crear = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModuloc + "" +
                "and operaciones.codigoP = " + CodigoCrearc + "", cone.OpenConnection());
            Crearrd = Crear.ExecuteReader();

            if (Crearrd.Read())
            {
                Label20.Text = Crearrd["codigoP"].ToString();
            }
            else
            {
                Label20.Text = "00000";
            }
            SqlCommand Actualizar;
            SqlDataReader Actualizarrd;

            Actualizar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModuloc + "" +
                "and operaciones.codigoP = " + CodigoActualizarc + "", cone.OpenConnection());
            Actualizarrd = Actualizar.ExecuteReader();

            if (Actualizarrd.Read())
            {
                Label21.Text = Actualizarrd["codigoP"].ToString();
            }
            else
            {
                Label21.Text = "00000";
            }
            SqlCommand Eliminar;
            SqlDataReader Eliminarrd;

            Eliminar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModuloc + "" +
                "and operaciones.codigoP = " + CodigoEliminarc + "", cone.OpenConnection());
            Eliminarrd = Eliminar.ExecuteReader();

            if (Eliminarrd.Read())
            {
                Label22.Text = Eliminarrd["codigoP"].ToString();
            }
            else
            {
                Label22.Text = "00000";
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void Cargo()
        {
            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                DataTable dt = new DataTable();
                consult = new SqlCommand("select  GnDcCgo,GnNomCgo,Cargo.GnDcDep, Departamento.GnNomDep, Departamento.GnSiglaUnf "+
                                            "from Cargo left join Departamento on Cargo.GnDcDep = Departamento.GnDcDep "+
                                            "where GnEsCgo = 'Activo' order by GnNomCgo asc", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    GridCargo.DataSource = dt;
                    GridCargo.DataBind();
                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodeCargo()//leer el codigo del Cargo e incrementarle uno
        {

            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
               // conn = new SqlCommand("select top 1 GnDcCgo from Cargo order by GnDcCgo desc", cone.OpenConnection()); // where Cargo.GnEsCgo='Activo'
                conn = new SqlCommand("select GnDcCgo from Cargo where GnDcCgo = (select MAX(GnDcCgo) from Cargo) group by GnDcCgo", cone.OpenConnection()); // where Cargo.GnEsCgo='Activo'
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read() == true)
                {
                    numero = reader["GnDcCgo"].ToString();
                    lol = int.Parse(numero);
                    sg = lol + 1;
                    Label4.Text = sg.ToString();

                }
                else
                {
                    Label4.Text = car.ToString();

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
        public void guardarCArgo(string cargo2)
        {
            SqlDataReader leer;
            SqlCommand consult;
            try
            {
                consult = new SqlCommand("select Cargo.GnNomCgo from Cargo where Cargo.GnEsCgo='Activo' and  GnNomCgo=@GnNomCgo", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@GnNomCgo", cargo2);
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(ds, "Cargo");

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    string carg = leer["GnNomCgo"].ToString();
                    if (TextBox1.Text == carg)
                    {
                        //Label7.Text = "Este Cargo ya se encuentra registrado";
                        //Label7.ForeColor = Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "error1()", true);
                    }
                }
                else
                {
                    rol.AddRole(TextBox1.Text, float.Parse(Label4.Text), int.Parse(DropDownList1.SelectedValue));
                    //Label7.Text = "Registro exitoso";
                    //Label7.ForeColor = Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "exito", "Exito1()", true);
                    TextBox1.Text = "";
                    DropDownList1.Items.Clear();
                    UnidadFuncional_Departamento();
                    CodeCargo();
                    Cargo();
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message + "Cargo");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void filterCargos()
        {
            if (TextBox2.Text != "")
            {
                SqlDataReader leer;
                SqlCommand consult;

                Conexion co = new Conexion();

                try
                {
                    DataTable dt = new DataTable();
                    consult = new SqlCommand("select  GnDcCgo,GnNomCgo,Cargo.GnDcDep, Departamento.GnNomDep, Departamento.GnSiglaUnf "+
                                                "from Cargo left join Departamento on Cargo.GnDcDep = Departamento.GnDcDep "+
                                                " where GnEsCgo = 'Activo' and  GnNomCgo like '%" + TextBox2.Text + "%'", cone.OpenConnection());
                    consult.CommandType = CommandType.Text;
                    consult.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(consult);
                    DataSet ds = new DataSet();
                    da.Fill(dt);

                    leer = consult.ExecuteReader();
                    if (leer.Read())
                    {
                        GridCargo.DataSource = dt;
                        GridCargo.DataBind();
                    }
                    else
                    {
                        Label7.Text = "El cargo ingresado no se encuentra registrado";
                        Label7.ForeColor = Color.Red;
                        Cargo();
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
                Label7.Text = "";
                Cargo();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }

        protected void GridCargo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridCargo.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridCargo.DataKeys[e.RowIndex].Values[0]);

            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string selec = (fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList).SelectedValue;
            rol.UpdateRole(codigo, nombre, int.Parse(selec));
            GridCargo.EditIndex = -1;
            CodeCargo();
            Cargo();
            TextBox2.Text = "";
            Label7.Text = "";
        }

        protected void GridCargo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (TextBox2.Text != "")
            {
                GridCargo.EditIndex = e.NewEditIndex;
                filterCargos();
            }
            else
            {
                GridCargo.EditIndex = e.NewEditIndex;
                CodeCargo();
                Cargo();
            }
            Label7.Text = "";
        }

        protected void GridCargo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlCommand query;

            try
            {
                int codigo = Convert.ToInt32(GridCargo.DataKeys[e.RowIndex].Values[0]);
                query = new SqlCommand("DELETE FROM Cargo  WHERE Cargo.GnDcCgo = @GnDcCgo", cone.OpenConnection());
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@GnDcCgo", SqlDbType.Int).Value = codigo;
                query.ExecuteReader();
                GridCargo.EditIndex = -1;
                CodeCargo();
                Cargo();
            }
            catch (Exception ex)
            {
                int codigo = Convert.ToInt32(GridCargo.DataKeys[e.RowIndex].Values[0]);
                string estado = "Inactivo";
                rol.DisableRol(codigo, estado);
                CodeCargo();
                Cargo();
            }
            finally//cerrar la conexion
            {
                cone.CloseConnection();
            }
            Label7.Text = "";
            cerrar.closeallconnections();
        }

        protected void GridCargo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if(TextBox2.Text == "")
            {
                GridCargo.EditIndex = -1;
                CodeCargo();
                Cargo();
                //TextBox1.Text = "";
                Label7.Text = "";
            }
            else
            {
                GridCargo.EditIndex = -1;
                filterCargos();
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if(TextBox2.Text != "")
            {
                filterCargos();
            }
            else
            {
                Cargo();
                Label7.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    //Label7.Text = "Ingrese el nombre del Cargo";
                    //Label7.ForeColor = Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "error2()", true);
                }
                else
                {
                    guardarCArgo(TextBox1.Text);
                }

                Cargo();
                CodeCargo();
                //TextBox1.Text = "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);

            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if(TextBox1.Text != "")
            {
                TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
                DropDownList1.Enabled = true;
            }
            else
            {
                DropDownList1.Enabled = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedValue != "Seleccione...")
            {
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }
}
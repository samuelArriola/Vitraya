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
using Generales_1._0.Class;
using Generales_1._0.Class.DAOGenerales;
using Generales_1._0.Class.DTOGenerales;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class CreateDepartamentos : System.Web.UI.Page
    {
        Conexion cone = new Conexion();
        CrudDepartment depar = new CrudDepartment();
        Encryption Encriptacion = new Encryption();
        CloseAllConnections cerrar = new CloseAllConnections();
        int depa = 3000;

        //departamento
        int codigoModulod = 10003;
        int CodigoCreard = 20007;
        int CodigoActualizard = 20008;
        int CodigoEliminard = 20009;

        string depart;
        string Sigla;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                CodeDepartment();
                Department();
                GridDepartamento.Columns[0].Visible = false;
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                TextBox3.Attributes.Add("autocomplete", "off");
                cone.CloseConnection();
                cerrar.closeallconnections();
                Button1.Enabled = false;
                TextBox3.Enabled = false;
                DropDownList1.Enabled = false;
                Direcciones_areas();
                //Direcciones_areasDrop();
            }
        }
        private void verification()
        {
            try
            {
                Label1.Text = Session["admin"].ToString();
                //buscar();
                //buscarCodidepa();
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
            //if (Label18.Text == "20007")//Crear
            //{
            //    guardar.Visible = true;
            //}
            //else
            //{
            //    guardar.Visible = false;
            //}

            //if (Label19.Text == "20008")//Actualizar
            //{
            //    GridDepartamento.Columns[2].Visible = true;
            //}
            //else
            //{
            //    GridDepartamento.Columns[2].Visible = false;
            //}

            //if (Label23.Text == "20009")//Eliminar
            //{
            //    GridDepartamento.Columns[3].Visible = true;
            //}
            //else
            //{
            //    GridDepartamento.Columns[3].Visible = false;
            //}

            //if (Label18.Text == "00000" && Label19.Text == "00000" && Label23.Text == "00000")
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
                Label2.Text = rd["GNNomUsu"].ToString();
                Label3.Text = rd["GNNomUsu"].ToString();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        private void buscarCodidepa()
        {
            SqlCommand Crear;
            SqlDataReader Crearrd;

            Crear = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulod + "" +
                "and operaciones.codigoP = " + CodigoCreard + "", cone.OpenConnection());
            Crearrd = Crear.ExecuteReader();

            if (Crearrd.Read())
            {
                Label18.Text = Crearrd["codigoP"].ToString();
            }
            else
            {
                Label18.Text = "00000";
            }
            SqlCommand Actualizar;
            SqlDataReader Actualizarrd;

            Actualizar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulod + "" +
                "and operaciones.codigoP = " + CodigoActualizard + "", cone.OpenConnection());
            Actualizarrd = Actualizar.ExecuteReader();

            if (Actualizarrd.Read())
            {
                Label19.Text = Actualizarrd["codigoP"].ToString();
            }
            else
            {
                Label19.Text = "00000";
            }
            SqlCommand Eliminar;
            SqlDataReader Eliminarrd;

            Eliminar = new SqlCommand("select operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Perfil_operacion, operaciones, modulos, Usuario " +
                    "where Usuario.GNCodUsu = Perfil_operacion.GNCodUsu " +
                    "and modulos.codigoM = operaciones.codigoM " +
                    "and operaciones.codigoP = Perfil_operacion.codigoP " +
                    "and Usuario.GNCodUsu =  " + int.Parse(Label1.Text) + " " +
                "and modulos.codigoM = " + codigoModulod + "" +
                "and operaciones.codigoP = " + CodigoEliminard + "", cone.OpenConnection());
            Eliminarrd = Eliminar.ExecuteReader();

            if (Eliminarrd.Read())
            {
                Label23.Text = Eliminarrd["codigoP"].ToString();
            }
            else
            {
                Label23.Text = "00000";
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void CodeDepartment()//leer el codigo del departamento e incrementarle uno
        {
            try
            {
                int lol, sg;
                string numero = "";
                SqlCommand conn;
                //conn = new SqlCommand("select top 1 GnDcDep from Departamento  order by GnDcDep desc", cone.OpenConnection());
                conn = new SqlCommand("select GnDcDep from Departamento where GnDcDep = (select MAX(GnDcDep) from Departamento) group by GnDcDep", cone.OpenConnection());
                SqlDataReader reader = conn.ExecuteReader();

                if (reader.Read())
                {
                    numero = reader["GnDcDep"].ToString();
                    lol = int.Parse(numero);
                    sg = lol + 1;
                    Label6.Text = sg.ToString();
                }
                else
                {
                    Label6.Text = depa.ToString();

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
        public void Direcciones_areas()
        {
            SqlCommand buscar;
            SqlDataReader rd;
            buscar = new SqlCommand("select GnCdAra,GnNomAra from Area order by GnNomAra asc", cone.OpenConnection());
            buscar.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "GnNomAra";
            DropDownList1.DataValueField = "GnCdAra";
            //foreach (GridViewRow item in GridDepartamento.Rows)
            //{
            //    DropDownList drop = item.FindControl("DropDownList2") as DropDownList;
            //    drop.DataSource = ds;
            //    drop.DataTextField = "GnNomAra";
            //    drop.DataValueField = "GnCdAra";
            //    drop.DataBind();
            //    drop.Items.Insert(0, "Seleccione...");
            //}
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Seleccione...");
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void Direcciones_areasDrop()
        {
            try
            {
                if(GridDepartamento.EditIndex == -1 || GridDepartamento.EditIndex != -1 )
                {
                    foreach (GridViewRow item in GridDepartamento.Rows)
                    {
                        System.Web.UI.WebControls.DropDownList drop = item.Cells[3].FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList;

                        SqlCommand buscar2;
                        SqlDataReader rd2;
                        buscar2 = new SqlCommand("select GnCdAra,GnNomAra from Area order by GnNomAra asc", cone.OpenConnection());
                        buscar2.ExecuteNonQuery();
                        SqlDataAdapter da2 = new SqlDataAdapter(buscar2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        drop.DataSource = ds2;
                        //drop.Text = "GnNomAra";
                        drop.DataTextField = "GnNomAra";
                        drop.DataValueField = "GnCdAra";
                        drop.DataBind();
                        drop.Items.Insert(0, "Seleccione...");
                        UpdatePanel1.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Test ." + ex.Message);
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void Department()//metodo para llenar el dropdownlist desde la base de datos
        {
            SqlDataReader leer;
            SqlCommand consult;

            try
            {
                DataTable dt = new DataTable();
                consult = new SqlCommand("select GnDcDep,GnNomDep, Departamento.GnSiglaUnf,Area.GnNomAra, Area.GnSiglaDr from Departamento, Area where Area.GnCdAra=Departamento.GnCdAra and GnEsDep='Activo' order by GnNomDep asc", cone.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(dt);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    GridDepartamento.DataSource = dt;
                    GridDepartamento.DataBind();
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
        private void filtrarDepartamentos()
        {
            if (TextBox2.Text != "")
            {
                SqlDataReader leer;
                SqlCommand consult;

                try
                {
                    DataTable dt = new DataTable();
                    consult = new SqlCommand("select GnDcDep,GnNomDep, Departamento.GnSiglaUnf,Area.GnNomAra, Area.GnSiglaDr from Departamento, Area " +
                        "where Area.GnCdAra=Departamento.GnCdAra and GnEsDep='Activo' and GnNomDep like '%" + TextBox2.Text + "%'", cone.OpenConnection());
                    consult.CommandType = CommandType.Text;
                    consult.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(consult);
                    DataSet ds = new DataSet();
                    da.Fill(dt);

                    leer = consult.ExecuteReader();
                    if (leer.Read())
                    {
                        GridDepartamento.DataSource = dt;
                        GridDepartamento.DataBind();
                        Label9.Text = "";
                    }
                    else
                    {
                        Label9.Text = "El departemento ingresado no se encuentra registrado";
                        Label9.ForeColor = Color.Red;
                        Department();
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
                Label9.Text = "";
                Department();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        public void GuardarDepartamento(string depar2, string GnSiglaUnf)
        {
            SqlDataReader leer;
            SqlDataReader leer2;
            SqlCommand consult;
            SqlCommand consult2;


            try
            {
                consult = new SqlCommand("select Departamento.GnNomDep,Departamento.GnSiglaUnf from Departamento where GnNomDep=@GnNomDep", cone.OpenConnection()); //Departamento.GnEsDep='Activo' or Departamento.GnEsDep='Inactivo' 
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@GnNomDep", depar2);
                consult.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(consult);
                DataSet ds = new DataSet();
                da.Fill(ds);

                leer = consult.ExecuteReader();
                if (leer.Read())
                {
                    depart = leer["GnNomDep"].ToString();

                    if (TextBox1.Text == depart)
                    {
                        //Label9.Text = "Este Departamento ya se encuentra registrado";
                        //Label9.ForeColor = Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "Error()", true);
                    }

                }

                consult2 = new SqlCommand("select Departamento.GnNomDep,Departamento.GnSiglaUnf from Departamento where GnSiglaUnf=@GnSiglaUnf", cone.OpenConnection()); // Departamento.GnEsDep='Activo' or Departamento.GnEsDep='Inactivo' and
                consult2.CommandType = CommandType.Text;
                consult2.Parameters.AddWithValue("@GnSiglaUnf", GnSiglaUnf);
                consult2.ExecuteNonQuery();
                SqlDataAdapter da2 = new SqlDataAdapter(consult2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                leer2 = consult2.ExecuteReader();
                if (leer2.Read())
                {
                    Sigla = leer2["GnSiglaUnf"].ToString();

                    if (TextBox3.Text == Sigla)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "Error2()", true);
                    }
                }


                if (TextBox1.Text != depart && TextBox3.Text != Sigla)
                {
                    try
                    {
                        CrudDepartment dep = new CrudDepartment();
                        dep.AddDepartment(TextBox1.Text, float.Parse(Label6.Text), TextBox3.Text, Convert.ToInt32(DropDownList1.SelectedValue));
                        //Label9.Text = "";
                        //Label9.ForeColor = Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "exito", "Exito()", true);
                        TextBox1.Text = "";
                        TextBox3.Text = "";
                        DropDownList1.Items.Clear();
                        Direcciones_areas();
                        Button1.Enabled = false;
                        TextBox3.Enabled = false;
                        DropDownList1.Enabled = false;
                        CodeDepartment();
                        Department();
                    }
                    catch (Exception)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "Error()", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "Error2()", true);
                //MessageBox.Show(ex.Message + "Departamento");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error');", true);
            }
            finally
            {
                cone.CloseConnection();
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }

        protected void GridDepartamento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if(TextBox2.Text == "")
            {
                GridDepartamento.EditIndex = -1;
                CodeDepartment();
                Department();
                //TextBox2.Text = "";
                Label9.Text = "";
            }
            else
            {
                GridDepartamento.EditIndex = -1;
                //xtBox2.Text = "";
                Label9.Text = "";
                filtrarDepartamentos();
            }
        }

        protected void GridDepartamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlCommand query;

            try
            {
                int codigo = Convert.ToInt32(GridDepartamento.DataKeys[e.RowIndex].Values[0]);
                query = new SqlCommand("DELETE FROM Departamento  WHERE Departamento.GnDcDep = @GnDcDep", cone.OpenConnection());
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@GnDcDep", SqlDbType.Int).Value = codigo;
                query.ExecuteReader();
                GridDepartamento.EditIndex = -1;
                CodeDepartment();
                Department();
            }
            catch (Exception ex)
            {
                string estado = "Inactivo";
                int codigo = Convert.ToInt32(GridDepartamento.DataKeys[e.RowIndex].Values[0]);
                depar.DisableDepartment(codigo, estado);
                CodeDepartment();
                Department();
            }
            finally//cerrar la conexion
            {
                cone.CloseConnection();
            }
            Label9.Text = "";
            cerrar.closeallconnections();
        }

        protected void GridDepartamento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (TextBox2.Text != "")
            {
                GridDepartamento.EditIndex = e.NewEditIndex;
                filtrarDepartamentos();
                //Direcciones_areasDrop();
                Label9.Text = "";
            }
            else
            {
                GridDepartamento.EditIndex = e.NewEditIndex;
                CodeDepartment();
                Department();
                //Direcciones_areasDrop();
                Label9.Text = "";
            }
        }

        protected void GridDepartamento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridDepartamento.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridDepartamento.DataKeys[e.RowIndex].Values[0]);

            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string sigla = (fila.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;
            string selec = (fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList).SelectedValue;
            depar.UpdateDepartment(codigo, nombre, sigla, int.Parse(selec));


            GridDepartamento.EditIndex = -1;
            CodeDepartment();
            Department();
            TextBox2.Text = "";
            Label9.Text = "";
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                filtrarDepartamentos();
            }
            else
            {
                Label9.Text = "";
                Department();
                CodeDepartment();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "")
                {
                    //Label9.Text = "Ingrese el nombre del departamento";
                    //Label9.ForeColor = Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "Error3()", true);
                }
                else
                {
                    GuardarDepartamento(TextBox1.Text, TextBox3.Text);
                }

                Department();
                CodeDepartment();
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
                TextBox3.Enabled = true;
            }
            else
            {
                TextBox3.Enabled = false;
                DropDownList1.Enabled = false;
                Button1.Enabled = false;
            }
        }

        public void CodigoDireccion(string GnNomAra)
        {
            SqlCommand buscar;
            SqlDataReader rd;
            buscar = new SqlCommand("select GnCdAra from Area where GnNomAra = @GnNomAra", cone.OpenConnection());
            buscar.Parameters.AddWithValue("@GnNomAra", GnNomAra);
            buscar.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            DataSet ds = new DataSet();
            da.Fill(ds);
            rd = buscar.ExecuteReader();
            if (rd.Read())
            {
                Session["Codigo"] = rd["GnCdAra"];
            }
            cone.CloseConnection();
            cerrar.closeallconnections();
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedIndex != -1)
            {
                CodigoDireccion(DropDownList1.SelectedItem.Text);
                //Session["Codigo"] = DropDownList1.SelectedValue;
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if(TextBox3.Text != "")
            {
                TextBox3.Text = (CultureInfo.InvariantCulture.TextInfo.ToUpper(TextBox3.Text.ToUpper()));
                DropDownList1.Enabled = true;
            }
            else
            {
                DropDownList1.Enabled = false;
                Button1.Enabled = false;
            }
        }
    }
}
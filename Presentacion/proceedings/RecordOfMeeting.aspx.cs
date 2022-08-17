using Persistencia;
using System;
using System.Data.SqlClient;

namespace Generales_1._0.Home.dashboard.production.screens.proceedings
{
    public partial class RecordOfMeeting : System.Web.UI.Page
    {
        Conexion cone = new Conexion();
        //CrudCommittees acta = new CrudCommittees();
        int code = 1;
        int codigoModulo = 10009;
        int CodigoCrear = 20024;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Verification();
                //TextBox3.Text = DateTime.Now.ToShortDateString();
                //Reuniones(Label14.Text);
                //OIDCommiteeHeadboard();
                Label4.Text = DateTime.Now.ToString();
                //tema.Visible = false;
                //TextBox4.Text = DateTime.Now.ToLocalTime().ToString();
                // TextBox5.Text = DateTime.Now.ToLocalTime().ToString();
            }
        }
        private void Verification()
        {
            try
            {
                Label14.Text = Session["admin"].ToString();

                buscar();
                //CodigoReunion();
                foto();
            }
            catch (Exception ex)
            {
                Response.Redirect("../../Log%20in/Login.aspx?Sesion=Debe iniciar sesion");
            }

            //if (Label5.Text == "00000")
            //{
            //    string id_lista = HttpUtility.UrlEncode(Encriptacion.Encrypt(Label14.Text));
            //    Response.Redirect(string.Format("../WebError.aspx?ParametersQuery=" + id_lista));
            //}
        }


        private void foto()
        {

            Image1.ImageUrl = "../general/BuscarImagenes.aspx?id=" + int.Parse(Label14.Text);
            Image2.ImageUrl = "../general/BuscarImagenes.aspx?id=" + int.Parse(Label14.Text);
            SqlCommand buscar;
            SqlDataReader rd;

            buscar = new SqlCommand("select GNNomUsu from Usuario where GNCodUsu =  '" + int.Parse(Label14.Text) + "'", cone.OpenConnection());
            rd = buscar.ExecuteReader();

            if (rd.Read())
            {
                Label2.Text = rd["GNNomUsu"].ToString();
                Label6.Text = rd["GNNomUsu"].ToString();
            }
            cone.CloseConnection();
        }
        private void buscar()
        {
            SqlCommand buscar;
            SqlDataReader rd;

            buscar = new SqlCommand("select Rol.codigoR, operaciones.codigoP, operaciones.nombreOp, modulos.codigoM, modulos.nombreMod, Usuario.GNNomUsu from Rol, Perfil_operacion, operaciones, modulos, Usuario " +
                "where Rol.codigoR = Perfil_operacion.codigoR " +
                "and modulos.codigoM = operaciones.codigoM " +
                "and operaciones.codigoP = Perfil_operacion.codigoP " +
                "and Rol.codigoR = Usuario.codigoR " +
                "and Usuario.GNCodUsu =  '" + int.Parse(Label14.Text) + "'", cone.OpenConnection());
            rd = buscar.ExecuteReader();

            if (rd.Read())
            {
                Label8.Text = rd["codigoR"].ToString();
            }
            cone.CloseConnection();
        }

    }
}
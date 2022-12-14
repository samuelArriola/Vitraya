using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Generales_1._0.Class;

namespace Generales_1._0.Home.dashboard.production.screens.general
{
    public partial class BuscarHuella : System.Web.UI.Page
    {
        Conexion con = new Conexion();
        CloseAllConnections cerrar = new CloseAllConnections();
        Encryption Encriptacion = new Encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string nombres = Session["admin"].ToString();

                SqlCommand buscar;

                buscar = new SqlCommand("select GnFtHull from Usuario where GNCodUsu=@id3", con.OpenConnection());
                buscar.Parameters.Add("@id3", SqlDbType.VarChar).Value = Encriptacion.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id3"]).ToString());//Request.QueryString["id3"];
                SqlDataReader rd = buscar.ExecuteReader();
                if (rd.Read())
                {
                    byte[] imagen = (byte[])rd["GnFtHull"];
                    Response.BinaryWrite(imagen);
                }
                else
                {
                    byte[] imagen = null;
                    Response.BinaryWrite(imagen);
                }
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                Response.Redirect("../../../Log%20in/Login.aspx?Sesion=Debe iniciar sesion");
            }
            finally
            {
                con.CloseConnection();
            }
            cerrar.closeallconnections();
        }
    }
}
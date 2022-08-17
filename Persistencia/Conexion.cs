
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace Persistencia
{



    public class Conexion
    {
        private class Config
        {
            public string password { get; set; }
            public string user { get; set; }
            public string server { get; set; }
            public string database { get; set; }
        }

        SqlConnection con;//variable de conexion
        

        private Config SetConfig()
        {
            string path;
            try
            {
                path = HttpContext.Current.Server.MapPath("..\\Config.json");
            }
            catch (Exception)
            {

                path = HttpContext.Current.Server.MapPath("~/Config.json");
            }

            StreamReader r = new StreamReader(path);
            string jsonString = r.ReadToEnd();
            r.Close();
            return JsonConvert.DeserializeObject<Config>(jsonString);

        }
        public SqlConnection OpenConnection()//metodo para abrir la conexion
        {
            //Config config = SetConfig();
            try
            {
                //con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Vitraya;Trusted_Connection=True;");//establecer conexion con el servidor, la base de datos, el usuario y la contraseña

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                con = new SqlConnection(connectionString);//establecer conexion con el servidor, la base de datos, el usuario y la contraseña
                con.Open();
            }
            catch (SqlException ex)//crear expecion cuando la conexion no este abierta 
            {
                con.Open();
            }
            return con;
        }
        public void CloseConnection()//modo para cerrar la conexion 
        {
            if (con != null)//metodo que valide que la variable conexion no este vacia o nula
            {
                con.Close();//cerrar conexion
            }
        }

        public static string GetRuta()
        {
            return HttpContext.Current.Server.MapPath("../Config.json");
        }
    }
}
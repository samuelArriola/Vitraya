using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;

namespace Persistencia.Generales
{
    public class DAONABloqueos
    {

        public static List<ScriptBloqueo> GetBloqueos()
        {
            
            List<ScriptBloqueo> bloqueos = new List<ScriptBloqueo>();

            SqlCommand command, command2;
            SqlDataReader reader, reader2;
            SqlConnection conexion = new Conexion().OpenConnection();
            

            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            int identificacion = Convert.ToInt32(HttpContext.Current.Session["Admin"]); ;

            try
            {

                command = new SqlCommand("SELECT * FROM GNScriptBloqueo ", conexion);
                command2 = new SqlCommand();
                
                command2.Parameters.AddWithValue("identificacion", identificacion);
                command2.Parameters.AddWithValue("fechaActual", fechaActual);

                reader = command.ExecuteReader();
                while ( reader.Read())
                {
                    SqlConnection conexion2 = new Conexion().OpenConnection();
                    command2.Connection = conexion2;
                    try
                    {
                        command2.CommandText = reader["Consulta"].ToString();

                        reader2 = command2.ExecuteReader();

                        while (reader2.Read())
                        {
                            ScriptBloqueo bloqueo = new ScriptBloqueo
                            {
                                intOidGnScriptsBloqueos = (reader["OidGnScriptsBloqueos"].ToString() == "") ? 0 : Convert.ToInt32(reader["OidGnScriptsBloqueos"].ToString()),
                                strNombre = reader["Nombre"].ToString(),
                                strEstado = reader["Estado"].ToString(),
                                strResultConsulta = Convert.ToString(reader2["resultado"].ToString()),
                                IntValidacion = Convert.ToInt32(reader2["validacion"])
                            };
                            bloqueos.Add(bloqueo);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conexion2.Close();
                    }
                    
                }

            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return bloqueos;
        }

        public static void UpdateBloqueos(int id, string estado)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GNScriptBloqueo] SET [Estado] = @strEstado WHERE OidGNScriptsBloqueos = @intOidGNScriptsBloqueos ", conexion.OpenConnection());

                command.Parameters.AddWithValue("strEstado", estado);
                command.Parameters.AddWithValue("intOidGNScriptsBloqueos", id);

                command.ExecuteNonQuery();
             
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

    }
}
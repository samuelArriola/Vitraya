using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNOpcionesBloqueadas
    {
        public static List<OpcionesBloqueadas> GetOpcionesBloqueadas()
        {
            List<OpcionesBloqueadas> Opcionesbloqueadas = new List<OpcionesBloqueadas>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {

                command = new SqlCommand("SELECT a.OidGNOpcionBloqueada, a.OidGNOpcion, Nombre, Estado " +
                "FROM GNOpcionBloqueada as a INNER JOIN GNOpciones ON a.OidGNOpcion = GNOpciones.OidGNOpcion", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OpcionesBloqueadas OpcionBloqueada = new OpcionesBloqueadas
                    {
                        intOidGNOpcionBloqueada = (reader["OidGNOpcionBloqueada"].ToString() == "") ? 0 : Convert.ToInt32(reader["OidGNOpcionBloqueada"].ToString()),
                        intOidGNOpcion = (reader["OidGNOpcion"].ToString() == "") ? 0 : Convert.ToInt32(reader["OidGNOpcion"].ToString()),
                        intEstado = (reader["Estado"].ToString() == "") ? 0 : Convert.ToInt32(reader["Estado"].ToString()),
                        stringNombre = reader["Nombre"].ToString()
                    };

                    Opcionesbloqueadas.Add(OpcionBloqueada);

                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return Opcionesbloqueadas;
        }

        public static void UpdateOpcionesBloqueadas(int id, string estado)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE GNOpcionBloqueada SET Estado = @strEstado WHERE OidGNOpcionBloqueada = @intOidGNOpcionBloqueada", conexion.OpenConnection());

                command.Parameters.AddWithValue("strEstado", estado);
                command.Parameters.AddWithValue("intOidGNOpcionBloqueada", id);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static List<OpcionesBloqueadas> GetByIdOpcionesBloqueadas(int id)
        {
            List<OpcionesBloqueadas> Opcionesbloqueadas = new List<OpcionesBloqueadas>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {

                command = new SqlCommand("SELECT a.OidGNOpcionBloqueada, a.OidGNOpcion, Nombre, Estado, Prefijo " +
                "FROM GNOpcionBloqueada as a INNER JOIN GNOpciones ON a.OidGNOpcion = GNOpciones.OidGNOpcion " +
                "WHERE GNOpciones.OidGNOpcion = @idOpcion ", conexion.OpenConnection());

                command.Parameters.AddWithValue("idOpcion", id);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OpcionesBloqueadas OpcionBloqueada = new OpcionesBloqueadas
                    {
                        intOidGNOpcionBloqueada = (reader["OidGNOpcionBloqueada"].ToString() == "") ? 0 : Convert.ToInt32(reader["OidGNOpcionBloqueada"].ToString()),
                        intOidGNOpcion = (reader["OidGNOpcion"].ToString() == "") ? 0 : Convert.ToInt32(reader["OidGNOpcion"].ToString()),
                        intEstado = (reader["Estado"].ToString() == "") ? 0 : Convert.ToInt32(reader["Estado"].ToString()),
                        stringNombre = reader["Nombre"].ToString(),
                        stringPrefijo = reader["Prefijo"].ToString()
                    };

                    Opcionesbloqueadas.Add(OpcionBloqueada);

                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return Opcionesbloqueadas;
        }

    }
}
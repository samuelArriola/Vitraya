using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNModulos
    {

        public static GNModulos get(int id)
        {
            GNModulos modulo = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT *  FROM GNModulos where OidGNModulo = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    modulo = new GNModulos
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString()),
                        StrIcono = reader["Icono"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
            }

            return modulo;
        }

        public static List<GNModulos> GetModulos()
        {
            List<GNModulos> modulos = new List<GNModulos>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT *  FROM GNModulos", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNModulos modulo = new GNModulos
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString()),
                        StrIcono = reader["Icono"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString()
                    };
                    modulos.Add(modulo);
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
            return modulos;
        }
        public static List<GNModulos> GetModulosByRol(int idRol, string nombre = "")
        {
            List<GNModulos> modulos = new List<GNModulos>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select mods.* from GNModulos  mods"+
                                         "   left join GNOpciones opcs on opcs.OidGNModulo = mods.OidGNModulo"+
                                         "   left join GNPermisos prms on prms.OidGNOpcion = opcs.OidGNOpcion"+
                                         "   where(prms.Confirmar = 1 or prms.Crear = 1 or prms.Eliminar = 1 or prms.Modificar = 1) and prms.OidRol = @OidRol" +
                                         "   and mods.Nombre like '%' + @Nombre + '%'" +
                                         "   group by mods.Nombre, mods.Icono, mods.OidGNModulo, mods.Prefijo", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidRol", idRol);
                command.Parameters.AddWithValue("Nombre", nombre);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNModulos modulo = new GNModulos
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString()),
                        StrIcono = reader["Icono"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString()
                    };
                    modulos.Add(modulo);
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
            return modulos;
        }
    }
}
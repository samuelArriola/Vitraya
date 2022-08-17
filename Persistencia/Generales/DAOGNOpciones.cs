using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNOpciones
    {
        public static List<GNOpciones> listar()
        {
            List<GNOpciones> opciones = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("SELECT OidGNOpcion, opcs.Nombre as nombreO, mods.Nombre as nombreM, opcs.OidGNModulo, opcs.EstadoBloqueo " +
                                           " from GNOpciones as opcs"+
                                           " left join GNModulos as mods"+
                                           " on mods.OidGNModulo = opcs.OidGNModulo order by mods.Nombre", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                opciones = new List<GNOpciones>();
                
                while (reader.Read())
                {
                    GNOpciones opcion = new GNOpciones();

                    opcion.StrNombre = reader["nombreO"].ToString();
                    opcion.IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString());
                    opcion.IntOidGNOpcion = Convert.ToInt32(reader["OidGNOpcion"].ToString());
                    opcion.StrNombreModulo = reader["nombreM"].ToString();
                    opcion.IntEstadoBloqueo = Convert.ToInt32(reader["EstadoBloqueo"]);
                   
                    opciones.Add(opcion);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return opciones;
        }

        public static List<GNOpciones> GetGNOpcionesByIdModulo(int idModulo, int idRol)
        {
            List<GNOpciones> opciones = new List<GNOpciones>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select opcs.* from GNOpciones opcs "+
                                         "   left join GNPermisos prms on prms.OidGNOpcion = opcs.OidGNOpcion"+
                                         "   where(prms.Confirmar = 1 or prms.Crear = 1 or prms.Eliminar = 1 or prms.Modificar = 1) and opcs.OidGNModulo = @OidGNModulo and OidRol = @OidRol", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidGNModulo", idModulo);
                command.Parameters.AddWithValue("OidRol", idRol);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNOpciones opcion = new GNOpciones();

                    opcion.StrNombre = reader["nombre"].ToString();
                    opcion.IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString());
                    opcion.IntOidGNOpcion = Convert.ToInt32(reader["OidGNOpcion"].ToString());
                    opcion.StrPrefijo = reader["Prefijo"].ToString();
                    opciones.Add(opcion);
                    opcion.IntEstadoBloqueo = Convert.ToInt32(reader["EstadoBloqueo"]);

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
            return opciones;
        }

        public static void UpdateEstadoOpcion(int estado, int idOpcion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update GNOpciones set EstadoBloqueo = @EstadoBloqueo where OidGNOpcion =  @OidGNOpcion", conexion.OpenConnection());

                command.Parameters.AddWithValue("EstadoBloqueo", estado);
                command.Parameters.AddWithValue("OidGNOpcion", idOpcion);
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;
namespace Persistencia.Generales
{
    public class DAOGNArchivo
    {

        public static GNArchivo GetArchivoUlt() {

            GNArchivo archivo = new GNArchivo();


            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select top(1) OidGNArchivo, OidGNListaArchivos, Contenido, Ext, Nombre from GNArchivos order by OidGNArchivo desc", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    archivo = new GNArchivo
                    {
                        AbteArchivo = (byte[])reader["Archivo"],
                        IntOidGNArchivo = Convert.ToInt32(reader["OidGNArchivo"].ToString()),
                        IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"].ToString()),
                        StrContenido = reader["Contenido"].ToString(),
                        StrExt = reader["Ext"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }


            return archivo;
        }

        public static void set(GNArchivo archivo)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNArchivos]"+
                                               "([Nombre]"+
                                               ",[Ext]"+
                                               ",[Contenido]"+
                                               ",[Archivo]" +
                                               ",[OidGNListaArchivos])" +
                                        " VALUES"+
                                               "(@Nombre,"+
                                               "@Ext,"+
                                               "@Contenido,"+
                                               "@Archivo," +
                                               "@OdiGNListaArchivos)", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Nombre", archivo.StrNombre);
                command.Parameters.AddWithValue("@Ext", archivo.StrExt);
                command.Parameters.AddWithValue("@Contenido", archivo.StrContenido);
                command.Parameters.AddWithValue("@Archivo", archivo.AbteArchivo);
                command.Parameters.AddWithValue("@OdiGNListaArchivos", archivo.IntOidGNListaArchivos);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        /// <summary>
        /// Metodo que retorna  una lista de los archivos
        /// </summary>
        /// <param name="id">Oid de la de la clase GNListaArchivos</param>
        /// <returns></returns>
        public static List<GNArchivo> Listar(int id)
        {
            List<GNArchivo> archivos = new List<GNArchivo>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT OidGNArchivo, OidGNListaArchivos, Contenido, Ext, Nombre  FROM [dbo].[GNArchivos] where OidGNListaArchivos = @OidGNArchivo", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGNArchivo", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNArchivo archivo = new GNArchivo
                    {
                        IntOidGNArchivo = Convert.ToInt32(reader["OidGNArchivo"].ToString()),
                        IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"].ToString()),
                        StrContenido = reader["Contenido"].ToString(),
                        StrExt = reader["Ext"].ToString(),
                        StrNombre   = reader["Nombre"].ToString(),
                    };

                    archivos.Add(archivo);
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return archivos;
        }
        /// <summary>
        /// Metodo que retorna un una Instancia la clase GNArchivo de la base de datos
        /// </summary>
        /// <param name="id">id del Archivo en la base de datos que se desea consultar</param>
        /// <returns></returns>
        public static GNArchivo get(int id)
        {
            GNArchivo archivo = new GNArchivo();


            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GNArchivos where OidGNArchivo = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id",id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    archivo = new GNArchivo
                    {
                        AbteArchivo = (byte[])reader["Archivo"],
                        IntOidGNArchivo = Convert.ToInt32(reader["OidGNArchivo"].ToString()),
                        IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"].ToString()),
                        StrContenido = reader["Contenido"].ToString(),
                        StrExt = reader["Ext"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }


            return archivo;
        }


        public static bool deleteArchivo(int idArchivo)
        {
            bool isDeleted = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM [dbo].[GNArchivos] WHERE OidGNArchivo = @OidGNArchivo", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGNArchivo", idArchivo);
               
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }

            return isDeleted;
        }
        public static int GetGNArchivoFromAuditoria(int idListaArchivo)
        {
            int? idArchivo = null;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select  OidGNArchivo from GNArchivos where  OidGNListaArchivos = @OidGNListaArchivos",conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGNListaArchivos", idListaArchivo);

                idArchivo = (int)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return idArchivo ?? -1;
        }

        public static void DeleteArchivoByIdListaArch(int idListaArchivos, string condiciones)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand($@"DELETE FROM [dbo].[GNArchivos] WHERE OidGNListaArchivos = @OidGNListaArchivos 
                                            {(condiciones.Length > 0 ? $" and OidGNArchivo not in ({condiciones})" : "")}" , conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGNListaArchivos", idListaArchivos);

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
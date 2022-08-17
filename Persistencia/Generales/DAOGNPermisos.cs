
using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNPermisos
    {

        public static GNPermisos get(int idRol, int idOpcion)
        {
            GNPermisos permiso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT [OidGNPermiso]"+
                                          ",[OidGNOpcion]"+
                                          ",[Crear]"+
                                          ",[Modificar]"+
                                          ",[Eliminar]"+
                                          ",[Confirmar]"+
                                          ",[OidRol]"+
                                       " FROM[dbo].[GNPermisos] where OidRol = @idRol and OidGNOpcion = @idOpcion ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@idRol",idRol);
                command.Parameters.AddWithValue("@idOpcion",idOpcion);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    permiso = new GNPermisos();
                    permiso.BlnConfirmar = Convert.ToBoolean(reader["Confirmar"].ToString());
                    permiso.BlnCrear = Convert.ToBoolean(reader["Crear"].ToString());
                    permiso.BlnEliminar = Convert.ToBoolean(reader["Eliminar"].ToString());
                    permiso.BlnModificar = Convert.ToBoolean(reader["Modificar"].ToString());
                    permiso.ItnOidRol = Convert.ToInt32(reader["OidRol"].ToString());
                    permiso.IntOidGNOpcion = Convert.ToInt32(reader["OidGNOpcion"].ToString());
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return permiso;
        }

        public static List<GNPermisos> get(int idRol)
        {
            List<GNPermisos> permisos = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT [OidGNPermiso]" +
                                          ",[OidGNOpcion]" +
                                          ",[Crear]" +
                                          ",[Modificar]" +
                                          ",[Eliminar]" +
                                          ",[Confirmar]" +
                                          ",[OidRol]" +
                                       " FROM[dbo].[GNPermisos] where OidRol = @idRol", conexion.OpenConnection());
                command.Parameters.AddWithValue("@idRol", idRol);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                permisos = new List<GNPermisos>();
                while (reader.Read())
                {
                    GNPermisos permiso = new GNPermisos();
                    permiso = new GNPermisos();
                    permiso.BlnConfirmar = Convert.ToBoolean(reader["Confirmar"].ToString());
                    permiso.BlnCrear = Convert.ToBoolean(reader["Crear"].ToString());
                    permiso.BlnEliminar = Convert.ToBoolean(reader["Eliminar"].ToString());
                    permiso.BlnModificar = Convert.ToBoolean(reader["Modificar"].ToString());
                    permiso.ItnOidRol = Convert.ToInt32(reader["OidRol"].ToString());
                    permiso.IntOidGNOpcion = Convert.ToInt32(reader["OidGNOpcion"].ToString());
                    permisos.Add(permiso);
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return permisos;
        }

        public static void upadate(GNPermisos datos)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GNPermisos]"+
                                           " SET [Crear] = @Crear" +
                                              ",[Modificar] = @Modificar" +
                                              ",[Eliminar] = @Eliminar" +
                                              ",[Confirmar] = @Confirmar" +
                                        " WHERE OidRol = @OidRol and  " +
                                        "OidGNOpcion = @OidGNOpcion", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("@Crear", (datos.BlnCrear)? 1:0);
                command.Parameters.AddWithValue("@Modificar", (datos.BlnModificar) ? 1 : 0);
                command.Parameters.AddWithValue("@Eliminar", (datos.BlnEliminar) ? 1 : 0);
                command.Parameters.AddWithValue("@Confirmar", (datos.BlnConfirmar) ? 1 : 0);
                command.Parameters.AddWithValue("@OidRol", datos.ItnOidRol);
                command.Parameters.AddWithValue("@OidGNOpcion", datos.IntOidGNOpcion);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = datos.IntOidGNPermiso,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $@"Se dan al rol permisos: {(datos.BlnConfirmar ? "Confirmar,":"")} {(datos.BlnCrear ? "Crear,": "")} {(datos.BlnEliminar ? "Eliminar," : "")} {(datos.BlnModificar ? "Modificar,": "")} al rol {datos.ItnOidRol}, a la opcion {datos.IntOidGNOpcion}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNPermisos"
                });


            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
        public static void set(GNPermisos datos)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNPermisos]"+
                                               "([OidGNOpcion]"+
                                               ",[Crear]"+
                                               ",[Modificar]"+
                                               ",[Eliminar]"+
                                               ",[Confirmar]"+
                                               ",[OidRol])"+
                                         " VALUES"+
                                               "(@OidGNOpcion,"+
                                               "@Crear,"+
                                               "@Modificar,"+
                                               "@Eliminar,"+
                                               "@Confirmar,"+
                                               "@OidRol) select SCOPE_IDENTITY()", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("@Crear", (datos.BlnCrear) ? 1 : 0);
                command.Parameters.AddWithValue("@Modificar", (datos.BlnModificar) ? 1 : 0);
                command.Parameters.AddWithValue("@Eliminar", (datos.BlnEliminar) ? 1 : 0);
                command.Parameters.AddWithValue("@Confirmar", (datos.BlnConfirmar) ? 1 : 0);
                command.Parameters.AddWithValue("@OidRol", datos.ItnOidRol);
                command.Parameters.AddWithValue("@OidGNOpcion", datos.IntOidGNOpcion);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crean permisos para el rol con oid {datos.ItnOidRol} para la opcion {datos.IntOidGNOpcion}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNPermisos"
                });

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static GNPermisos GetPermiso(int idRol, string linkOpcion)
        {
            GNPermisos permiso = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select prm.* from GNPermisos prm
	                                            inner join GNRoles rol on rol.OidGNRol = prm.OidRol
	                                            inner join GNOpciones opc on opc.OidGNOpcion = prm.OidGNOpcion
                                            where rol.OidGNRol = @idRol and opc.Prefijo like '%'+ @Prefijo +'%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("idRol", idRol);
                command.Parameters.AddWithValue("Prefijo", linkOpcion);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    permiso = new GNPermisos();
                    permiso.BlnConfirmar = Convert.ToBoolean(reader["Confirmar"].ToString());
                    permiso.BlnCrear = Convert.ToBoolean(reader["Crear"].ToString());
                    permiso.BlnEliminar = Convert.ToBoolean(reader["Eliminar"].ToString());
                    permiso.BlnModificar = Convert.ToBoolean(reader["Modificar"].ToString());
                    permiso.ItnOidRol = Convert.ToInt32(reader["OidRol"].ToString());
                    permiso.IntOidGNOpcion = Convert.ToInt32(reader["OidGNOpcion"].ToString());
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

            return permiso;
        }
    }
}
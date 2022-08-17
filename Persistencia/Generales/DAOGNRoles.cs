
using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNRoles
    {
        public static List<GNRoles> listar()
        {
            List<GNRoles> roles = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("SELECT * FROM GNRoles", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                roles = new List<GNRoles>();
                while (reader.Read())
                {
                    GNRoles rol = new GNRoles();
                    rol.IntOidGNRol = Convert.ToInt32(reader["OidGNRol"].ToString());
                    rol.StrNombre = reader["Nombre"].ToString();
                    roles.Add(rol);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return roles;
        }

        public static void set(string nombre)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNRoles]"+
                                                   "([Nombre])"+
                                             "VALUES"+
                                                   "(@Nombre) select SCOPE_IDENTITY()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Nombre", nombre);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el rol {nombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNRoles"
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

        public static int buscarRolUsuario(int id)
        {
            int idRol = -1;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select codigoR from Usuario  where GNCodUsu = @GNCodUsu", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    idRol = Convert.ToInt32(reader["codigoR"].ToString());
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return idRol;
        }

        public static GNRoles GetRol(int idRol)
        {
            GNRoles rol = null;

            Conexion conexion = new Conexion();
            try
            {
                using (var command = new SqlCommand("", conexion.OpenConnection()))
                {
                    command.CommandText = @"select * from GNRoles where OidGNRol = @OidGNRol";

                    command.Parameters.AddWithValue("OidGNRol", idRol);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rol = new GNRoles
                            {
                                IntOidGNRol = Convert.ToInt32(reader["OidGNRol"]),
                                StrNombre = reader["Nombre"].ToString()
                            };
                        }
                    }
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

            return rol;
        }
    }
}
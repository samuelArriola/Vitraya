using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.proceedings
{
    public class DAOPAUsuario
    {

        
        public static string SetPAUSuario(PAUsuario usuario)
        {
            string result = "";

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[PAUsuario]" +
                                         "          ([OidPAPlanAccion]" +
                                         "          ,[OidGNUsuario]" +
                                         "          ,[Rol]" +
                                         "          ,[Nombre]" +
                                         "          ,[Cargo])" +
                                         "    values(" +
                                         "            @OidPAPlanAccion" +
                                         "          , @OidGNUsuario" +
                                         "          , @Rol" +
                                         "          , @GNNomUsu" +
                                         "          , @GnCargo) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidPAPlanAccion", usuario.IntOidPAPlanAccion);
                command.Parameters.AddWithValue("OidGNUsuario", usuario.IntOidGNUsuario);
                command.Parameters.AddWithValue("Rol", usuario.StrRol);
                command.Parameters.AddWithValue("GNNomUsu", usuario.StrNombre);
                command.Parameters.AddWithValue("GnCargo", usuario.StrCargo);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = "",
                    strEntidad = "PAUsuario"
                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                result = ex.Message;
            }
            finally
            {
                conexion.OpenConnection();
            }

            return result;
        }


        public static Mensaje<PAUsuario> GetPAUsuarioByIdPlan(int idPlanAccion)
        {
            Mensaje<PAUsuario> mensaje = new Mensaje<PAUsuario>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PAUsuario where OidPAPlanAccion = @OidPAPlanAccion order by Rol", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidPAPlanAccion", idPlanAccion);
                reader = command.ExecuteReader();

                mensaje.Messaje = "";
                List<PAUsuario> usuarios = new List<PAUsuario>();

                while (reader.Read())
                {
                    try
                    {
                        usuarios.Add(new PAUsuario { 
                            IntOidGNUsuario = Convert.ToInt32(reader["OidGNUsuario"]),
                            IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"]),
                            IntOidPAUsuario = Convert.ToInt32(reader["OidPAUsuario"]),
                            StrCargo = reader["Cargo"].ToString(),
                            StrNombre = reader["Nombre"].ToString(),
                            StrRol   = reader["Rol"].ToString(),
                        });
                    }
                    catch (Exception ex)
                    {
                        mensaje.Messaje += ";\n" + ex.Message;
                    }
                }

                mensaje.Data = usuarios;
            }
            catch (Exception ex)
            {
                mensaje.Messaje = ex.Message;
            }
            finally
            {
                conexion.CloseConnection();
            }

            return mensaje;
        }

        public static PAUsuario GetPAUsuarioByIdPlan(int idPlan, int Rol)
        {
            PAUsuario usuario = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PAUsuario where OidPAPlanAccion = @OidPAPlanAccion and Rol = @Rol", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidPAPlanAccion", idPlan);
                command.Parameters.AddWithValue("Rol", Rol);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new PAUsuario
                    {
                        IntOidGNUsuario = Convert.ToInt32(reader["OidGNUsuario"]),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"]),
                        IntOidPAUsuario = Convert.ToInt32(reader["OidPAUsuario"]),
                        StrCargo = reader["Cargo"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrRol = reader["Rol"].ToString(),
                    };
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

            return usuario;
        }


        public static void DeletePAUsuarioByIdPlan(int idPlanAccion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from PAUsuario where OidPAPlanAccion = @OidPAPlanAccion",conexion.OpenConnection());
                command.Parameters.AddWithValue("OidPAPlanAccion", idPlanAccion);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;

namespace Persistencia.proceedings
{
    public class DAOAReunionD
    {
        private static DAOAReunionD usuariosParticipantes;
        
        private DAOAReunionD() { }

        public static DAOAReunionD getInstance()
        {
            if(usuariosParticipantes == null)
            {
                usuariosParticipantes = new DAOAReunionD();
            }
            return usuariosParticipantes;

        }

        public static List<UsuariosParticipantes> GetUsuariosParticipantes(int id)
        {
            Conexion conexion = new Conexion();
            SqlCommand consult;
            SqlDataReader reader;
            List<UsuariosParticipantes> usuariosParticipantes = null;
            try
            {
                consult = new SqlCommand("SELECT OidAReunionD\n"+
                                              ", OidAReunionC\n"+
                                              ", AReunionD.GNCodUsu\n"+
                                              ", TipoUsuario\n"+
                                              ", TpNomUsu\n"+
                                              ", TpNomEst\n"+
                                              ", Usuario.GNNomUsu\n"+
                                              "FROM AReunionD LEFT JOIN  Usuario ON AReunionD.GNCodUsu = Usuario.GNCodUsu\n" +
                                              "where OidAReunionC = @id ", conexion.OpenConnection());
                consult.Parameters.AddWithValue("@id", id);
                reader = consult.ExecuteReader();
                usuariosParticipantes = new List<UsuariosParticipantes>();
                while (reader.Read())
                {
                    UsuariosParticipantes usuario = new UsuariosParticipantes();
                    usuario.OidAReunionC1 = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    usuario.OidAReunionD1 = Convert.ToInt32(reader["OidAReunionD"].ToString());
                    usuario.GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    usuario.TipoUsuario1 = Convert.ToInt32(reader["TipoUsuario"].ToString());
                    //usuario.EstUsuario1 = Convert.ToInt32(reader["TpNomEst"].ToString());
                    usuario.TpNomUsu1 = reader["TpNomUsu"].ToString();
                    usuario.TpNomEst1 = reader["TpNomEst"].ToString();
                    usuario.NombreUsuario = reader["GNNomUsu"].ToString();
                    usuariosParticipantes.Add(usuario);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }
            return usuariosParticipantes;
        }

        public void set(UsuariosParticipantes data)
        {
            Conexion conexion = new Conexion();
            SqlCommand consult;
            SqlDataReader reader;
            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[AReunionD]"+
                                               "([OidAReunionC]"+
                                               ",[GNCodUsu]"+
                                               ",[TipoUsuario]"+
                                               ",[EstUsuario]"+
                                               ",[TpNomUsu]"+
                                               ",[Eliminado]"+
                                               ",[TpNomEst])"+
                                         "VALUES"+
                                               "(@OidAReunionC,"+
                                               "@GNCodUsu,"+
                                               "@TipoUsuario,"+
                                               "@EstUsuario,"+
                                               "@TpNomUsu,"+
                                               "0,"+
                                               "@TpNomEst) " +
                                               " select SCOPE_IDENTITY() instancia, NomReunion from AReunionC where OidAReunionC = @OidAReunionC", conexion.OpenConnection());
                
                consult.Parameters.AddWithValue("@OidAReunionC", data.OidAReunionC1);
                consult.Parameters.AddWithValue("@GNCodUsu", data.GNCodUsu1);
                consult.Parameters.AddWithValue("@TipoUsuario", data.TipoUsuario1);
                consult.Parameters.AddWithValue("@EstUsuario", data.EstUsuario1);
                consult.Parameters.AddWithValue("@TpNomUsu", data.TpNomUsu1);
                consult.Parameters.AddWithValue("@TpNomEst", data.TpNomEst1);
                reader = consult.ExecuteReader();
                reader.Read();


                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(reader["instancia"]),
                    strAccion = "Crar",
                    strDetalle = $"Se asigna El miembro {data.NombreUsuario} a comité  {reader["NomReunion"].ToString()}",
                    strEntidad = "AReunionD"
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

        public void delete(int id)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("update [dbo].[AReunionD] set Eliminado = 1  WHERE GNCodUsu = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = id,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "AReunionD"
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

        public List<UsuariosParticipantes> GetMiembrosPorTipo(int id, string tipoMiembro)
        {
            List<UsuariosParticipantes> miembros = null;
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;

            try
            {
                command = new SqlCommand("SELECT OidAReunionD\n"+
                                              ", OidAReunionC\n" +
                                              ", AReunionD.GNCodUsu\n" +
                                              ", TipoUsuario\n" +
                                              ", TpNomUsu\n" +
                                              ", TpNomEst\n" +
                                              ", Usuario.GNNomUsu\n" +
                                              "FROM AReunionD  LEFT JOIN  Usuario ON AReunionD.GNCodUsu = Usuario.GNCodUsu\n" +
                                              "where OidAReunionC = @id and TpNomUsu = @TpNomUsu and isnull(AReunionD.Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@TpNomUsu", tipoMiembro);

                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                miembros = new List<UsuariosParticipantes>();
                while (reader.Read()) 
                {
                    UsuariosParticipantes usuario = new UsuariosParticipantes();
                    usuario.OidAReunionC1 = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    usuario.OidAReunionD1 = Convert.ToInt32(reader["OidAReunionD"].ToString());
                    usuario.GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    usuario.TipoUsuario1 = Convert.ToInt32(reader["TipoUsuario"].ToString());
                    //usuario.EstUsuario1 = Convert.ToInt32(reader["TpNomEst"].ToString());
                    usuario.TpNomUsu1 = reader["TpNomUsu"].ToString();
                    usuario.TpNomEst1 = reader["TpNomEst"].ToString();
                    usuario.NombreUsuario = reader["GNNomUsu"].ToString();
                    miembros.Add(usuario);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }

            return miembros;
        }
        public void updateMiembro(UsuariosParticipantes data, string tipo )
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;

            try
            {
                command = new SqlCommand("UPDATE [dbo].[AReunionD]" +
                                               " SET [GNCodUsu] = @GNCodUsu" +
                                                  ",[TipoUsuario] = @TipoUsuario" +
                                                  ",[EstUsuario] =  @EstUsuario" +
                                                  ",[TpNomUsu] =  @TpNomUsu " +
                                                  ",[TpNomEst] =  @TpNomEst " +
                                             "WHERE OidAReunionC = @OidAReunionC " +
                                             "and TpNomUsu = @tipo and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", data.GNCodUsu1);
                command.Parameters.AddWithValue("@TipoUsuario", data.TipoUsuario1);
                command.Parameters.AddWithValue("@EstUsuario", data.EstUsuario1);
                command.Parameters.AddWithValue("@TpNomUsu", data.TpNomUsu1);
                command.Parameters.AddWithValue("@TpNomEst", data.TpNomEst1);
                command.Parameters.AddWithValue("@OidAReunionC", data.OidAReunionC1);
                command.Parameters.AddWithValue("@tipo", tipo);


                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = data.OidAReunionD1,
                    strAccion = "Modificar",
                    strDetalle = $"",
                    strEntidad = "AReunionD"
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

        private void SetHistorico(GNHistorico gNHistorico)
        {
            throw new NotImplementedException();
        }
    }
}
using Entidades.Auditorias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Auditorias
{
    public class DAOAuditoriaInterna
    {
        public static int SetAuditoriaInterna(AuditoriaInterna auditoriaInterna)
        {
            int idAuditoriaInterna = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[AUAuditoriaInterna]
                                                   ([Fecha]                    
                                                   ,[Responsable]              
                                                   ,[Objetivo]                 
                                                   ,[Alcance]                  
                                                   ,[Procesos]                 
                                                   ,[OidUsuarioCreador]        
                                                   ,[OidListaArchivos])        
                                             VALUES                            
                                                   ( @Fecha                    
                                                   , @Responsable              
                                                   , @Objetivo                 
                                                   , @Alcance                  
                                                   , @Procesos                 
                                                   , @OidUsuarioCreador        
                                                   , @OidListaArchivos);       
                                                       select cast(SCOPE_IDENTITY() as int)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Fecha", auditoriaInterna.DtmFecha);
                command.Parameters.AddWithValue("Responsable", auditoriaInterna.StrResponsable);
                command.Parameters.AddWithValue("Objetivo", auditoriaInterna.StrObjetivo);
                command.Parameters.AddWithValue("Alcance", auditoriaInterna.StrAlcance);
                command.Parameters.AddWithValue("Procesos", auditoriaInterna.StrProcesos);
                command.Parameters.AddWithValue("OidListaArchivos", auditoriaInterna.IntOidListaArchivos);
                command.Parameters.AddWithValue("OidUsuarioCreador", auditoriaInterna.IntOidUsuarioCreador);

                idAuditoriaInterna = (int)command.ExecuteScalar();

                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idAuditoriaInterna,
                    strAccion = "Crear",
                    strDetalle = $"Se Crea una auditoria Interna con los objetivos: {auditoriaInterna.StrObjetivo}",
                    strEntidad = "AUAuditoriaInterna"
                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return idAuditoriaInterna;
        }

        public static AuditoriaInterna GetAuditoriaInterna(int idAuditoria)
        {
            AuditoriaInterna auditoria = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from AUAuditoriaInterna where OidAuditoriaInterna = @OidAuditoriaInterna", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidAuditoriaInterna", idAuditoria);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    auditoria = new AuditoriaInterna
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntOidAuditoriaInterna = Convert.ToInt32(reader["OidAuditoriaInterna"]), 
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrProcesos = reader["Procesos"].ToString(),
                        StrResponsable  = reader["Responsable"].ToString(),
                        IntOidUsuarioCreador  = Convert.ToInt32(reader["OidUsuarioCreador"].ToString())
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

            return auditoria;
        }

        public static List<AuditoriaInterna> GetAuditoriaInternasByIdRespHall(int idResp)
        {
            List<AuditoriaInterna> auditorias = new List<AuditoriaInterna>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand(@"select DISTINCT AU.* from AUAuditoriaInterna AU
	                                            inner join AUHallazgo H on H.Instancia = AU.OidAuditoriaInterna 
                                            where H.Contexto = 1 and (H.Responsable = @Responsable or AU.OidUsuarioCreador = @Responsable)", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("Responsable", idResp);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    auditorias.Add(new AuditoriaInterna
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntOidAuditoriaInterna = Convert.ToInt32(reader["OidAuditoriaInterna"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrProcesos = reader["Procesos"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        IntOidUsuarioCreador = Convert.ToInt32(reader["OidUsuarioCreador"].ToString())
                    });
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

            return auditorias;
        }

        public static List<AuditoriaInterna> GetAuditorias()
        {
            List<AuditoriaInterna> auditorias = new List<AuditoriaInterna>();

            Conexion conexion = new Conexion();

            try
            {
                using(var command = new SqlCommand())
                {
                    command.Connection = conexion.OpenConnection();
                    command.CommandText = @"select DISTINCT AU.* from AUAuditoriaInterna AU
	                                            inner join AUHallazgo H on H.Instancia = AU.OidAuditoriaInterna 
                                            where H.Contexto = 1";

                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            auditorias.Add(new AuditoriaInterna {
                                DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                                IntOidAuditoriaInterna = Convert.ToInt32(reader["OidAuditoriaInterna"]),
                                IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                                StrAlcance = reader["Alcance"].ToString(),
                                StrObjetivo = reader["Objetivo"].ToString(),
                                StrProcesos = reader["Procesos"].ToString(),
                                StrResponsable = reader["Responsable"].ToString(),
                                IntOidUsuarioCreador = Convert.ToInt32(reader["OidUsuarioCreador"].ToString())
                            });
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

            return auditorias;
        }
    }
}
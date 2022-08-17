using Entidades.Auditorias;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Auditorias
{
    public class DAOAuditoriaExterna
    {
        public static List<AuditoriaExterna> GetAuditoriasExternas()
        {
            List<AuditoriaExterna> auditorias = new List<AuditoriaExterna>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from AUAuditoriaExterna", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    auditorias.Add(new AuditoriaExterna { 
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        IntOIdAuditoriaExterna = Convert.ToInt32(reader["OIdAuditoriaExterna"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrAuditores = reader["Auditores"].ToString(),
                        StrEnteAuditor = reader["EnteAuditor"].ToString(),
                        StrHallasgos = reader["Hallasgos"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrProcesos  = reader["Procesos"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        IntOidUsuarioCreador = Convert.ToInt32(reader["OidUsuarioCreador"])

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

        public static int SetAuditoriaExterna(AuditoriaExterna auditoriaExterna)
        {
            int oidAuditoria = 0;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[AUAuditoriaExterna]"+
                                         "          ([Fecha]                    "+
                                         "          ,[EnteAuditor]              "+
                                         "          ,[Auditores]                "+
                                         "          ,[Objetivo]                 "+
                                         "          ,[Procesos]                 "+
                                         "          ,[OidListaArchivos]         "+
                                         "          ,[Alcance]                  "+
                                         "          ,[OidUsuarioCreador]        " +
                                         "          ,[Hallasgos])               " +
                                         "    VALUES                            "+
                                         "          (@Fecha                     "+
                                         "          , @EnteAuditor              "+
                                         "          , @Auditores                "+
                                         "          , @Objetivo                 "+
                                         "          , @Procesos                 "+
                                         "          , @OidListaArchivos         "+
                                         "          , @Alcance                  " +
                                         "          , @OidUsuarioCreador        " +
                                         "          , @Hallasgos);" +
                                         " SELECT CAST(scope_identity() AS int)", conexion.OpenConnection());
                command.Parameters.AddWithValue("Fecha", auditoriaExterna.DtmFecha);
                command.Parameters.AddWithValue("EnteAuditor", auditoriaExterna.StrEnteAuditor);
                command.Parameters.AddWithValue("Auditores", auditoriaExterna.StrAuditores);
                command.Parameters.AddWithValue("Objetivo", auditoriaExterna.StrObjetivo);
                command.Parameters.AddWithValue("Procesos", auditoriaExterna.StrProcesos);
                command.Parameters.AddWithValue("OidListaArchivos", auditoriaExterna.IntOidListaArchivos);
                command.Parameters.AddWithValue("Hallasgos", auditoriaExterna.StrHallasgos);
                command.Parameters.AddWithValue("Alcance", auditoriaExterna.StrAlcance);
                command.Parameters.AddWithValue("OidUsuarioCreador", auditoriaExterna.IntOidUsuarioCreador);

                oidAuditoria =  (int) command.ExecuteScalar();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = oidAuditoria,
                    strAccion = "Crear",
                    strDetalle = $"Se Crea una auditoria externa con los objetivos: {auditoriaExterna.StrObjetivo}",
                    strEntidad = "AUAuditoriaExterna"
                }) ;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return oidAuditoria;
        }

        public static AuditoriaExterna GetAuditoria(int idAuditoriaExterna)
        {
            AuditoriaExterna auditoria = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from  AUAuditoriaExterna where OIdAuditoriaExterna = @OIdAuditoriaExterna",conexion.OpenConnection());
                command.Parameters.AddWithValue("OIdAuditoriaExterna", idAuditoriaExterna);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    auditoria = new AuditoriaExterna { };
                    auditoria.DtmFecha = Convert.ToDateTime(reader["Fecha"]);
                    auditoria.IntOIdAuditoriaExterna = Convert.ToInt32(reader["OIdAuditoriaExterna"]);
                    auditoria.IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]);
                    auditoria.StrAuditores = reader["Auditores"].ToString();
                    auditoria.StrEnteAuditor = reader["EnteAuditor"].ToString();
                    auditoria.StrHallasgos = reader["Hallasgos"].ToString();
                    auditoria.StrObjetivo = reader["Objetivo"].ToString();
                    auditoria.StrProcesos = reader["Procesos"].ToString();
                    auditoria.StrAlcance = reader["Alcance"].ToString();
                    auditoria.IntOidUsuarioCreador = reader["OidUsuarioCreador"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OidUsuarioCreador"]);
                    
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

        public static List<AuditoriaExterna> GetAuditoriaExternasbyRespHall(int idReponsable)
        {
            List<AuditoriaExterna> auditoriasExternas = new List<AuditoriaExterna>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select DISTINCT AU.* from AUAuditoriaExterna AU
	                                            inner join AUHallazgo H on H.Instancia = AU.OIdAuditoriaExterna 
                                            where  H.Contexto = 2 and (H.Responsable = @Responsable or AU.OidUsuarioCreador = @Responsable)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Responsable", idReponsable);
                

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    auditoriasExternas.Add(new AuditoriaExterna {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        IntOIdAuditoriaExterna = Convert.ToInt32(reader["OIdAuditoriaExterna"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrAuditores = reader["Auditores"].ToString(),
                        StrEnteAuditor = reader["EnteAuditor"].ToString(),
                        StrHallasgos = reader["Hallasgos"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrProcesos = reader["Procesos"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        IntOidUsuarioCreador = Convert.ToInt32(reader["OidUsuarioCreador"])

                    });
                }
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
            }

            finally
            {
                conexion.CloseConnection();
            } 

            return auditoriasExternas;
        }

        public static List<AuditoriaExterna> GetAuditorias()
        {
            List<AuditoriaExterna> auditorias = new List<AuditoriaExterna>();

            Conexion conexion = new Conexion();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = conexion.OpenConnection();
                    command.CommandText = @"Select DISTINCT AU.* from AUAuditoriaExterna AU
	                                            inner join AUHallazgo H on H.Instancia = AU.OIdAuditoriaExterna 
                                            where  H.Contexto = 2";

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            auditorias.Add(new AuditoriaExterna {
                                DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                                IntOIdAuditoriaExterna = Convert.ToInt32(reader["OIdAuditoriaExterna"]),
                                IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                                StrAuditores = reader["Auditores"].ToString(),
                                StrEnteAuditor = reader["EnteAuditor"].ToString(),
                                StrHallasgos = reader["Hallasgos"].ToString(),
                                StrObjetivo = reader["Objetivo"].ToString(),
                                StrProcesos = reader["Procesos"].ToString(),
                                StrAlcance = reader["Alcance"].ToString(),
                                IntOidUsuarioCreador = Convert.ToInt32(reader["OidUsuarioCreador"])
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
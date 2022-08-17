using Entidades.Auditorias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Auditorias
{
    public class DAOEnteAuditor
    {

        public static void SetEnteAuditor(EnteAuditor enteAuditor)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[AUEnteAuditor]"+
                                        "          ([Nombre]               "+
                                        "          ,[Sigla]                "+
                                        "          ,[Codigo])              "+
                                        "    VALUES                        "+
                                        "          (@Nombre                "+
                                        "          , @Sigla                "+
                                        "          , @Codigo)", conexion.OpenConnection());
                command.Parameters.AddWithValue("Nombre", enteAuditor.StrNombre);
                command.Parameters.AddWithValue("Sigla", enteAuditor.StrSigla);
                command.Parameters.AddWithValue("Codigo", enteAuditor.StrCodigo);
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

        public static List<EnteAuditor> GetEntesAuditores()
        {
            List<EnteAuditor> entes = new List<EnteAuditor>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from AUEnteAuditor", conexion.OpenConnection());
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entes.Add(new EnteAuditor
                    {
                        IntOidAUEnteAuditor = Convert.ToInt32(reader["OidAUEnteAuditor"]),
                        StrCodigo = reader["Codigo"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
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

            return entes;
        }

        public static EnteAuditor GetEnteAuditorById(int idEnteAuditor)
        {
            EnteAuditor ente = null;
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from AUEnteAuditor where OidAUEnteAuditor = @OidAUEnteAuditor", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidAUEnteAuditor", idEnteAuditor);

                reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    ente = new EnteAuditor
                    {
                        IntOidAUEnteAuditor = Convert.ToInt32(reader["OidAUEnteAuditor"]),
                        StrCodigo = reader["Codigo"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
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

            return ente;
        }

        public static void DeleteEnteAuditor(int idEnteAuditor)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM AUEnteAuditor WHERE OidAUEnteAuditor = @OidAUEnteAuditor",conexion.OpenConnection());
                command.Parameters.AddWithValue("OidAUEnteAuditor", idEnteAuditor);
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

        public static void UpdateEnteAuditor(EnteAuditor ente)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[AUEnteAuditor]"+
                                         "      SET[Nombre] = @Nombre  "+
                                         "         ,[Sigla] = @Sigla   "+
                                         "         ,[Codigo] = @Codigo "+
                                         "    WHERE OidAUEnteAuditor = @OidAUEnteAuditor", conexion.OpenConnection());
                command.Parameters.AddWithValue("Nombre", ente.StrNombre);
                command.Parameters.AddWithValue("Sigla",ente.StrSigla);
                command.Parameters.AddWithValue("Codigo", ente.StrCodigo);
                command.Parameters.AddWithValue("OidAUEnteAuditor", ente.IntOidAUEnteAuditor);

                command.ExecuteReader();
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
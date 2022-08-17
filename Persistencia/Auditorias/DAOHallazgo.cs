using Entidades.Auditorias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Auditorias
{
    public class DAOHallazgo
    {
        public static int SetHallazgo(Hallazgo hallazgo)
        {
            int OidHallazgo = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[AUHallazgo]"+
                                         "          ([Descripcion]      "+
                                         "          ,[Contexto]         "+
                                         "          ,[Responsable]      "+
                                         "          ,[Instancia])       "+
                                         "    VALUES                    "+
                                         "          (@Descripcion       "+
                                         "          , @Contexto         "+
                                         "          , @Responsable      "+
                                         "          , @Instancia);" +
                                         " SELECT CAST(scope_identity() AS int)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Descripcion", hallazgo.StrDescripcion);
                command.Parameters.AddWithValue("Contexto", hallazgo.IntContexto);
                command.Parameters.AddWithValue("Instancia", hallazgo.IntInstancia);
                command.Parameters.AddWithValue("Responsable", hallazgo.IntResponsable);

                OidHallazgo = (int)command.ExecuteScalar();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return OidHallazgo;
        }

        public static Hallazgo GetHallasgo(int idHallazgo)
        {
            Hallazgo hallazgo = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from AUHallazgo where OidHallazgo = @OidHallazgo", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidHallazgo", idHallazgo);
                reader = command.ExecuteReader();
                if (reader.Read())
                    hallazgo = new Hallazgo
                    {
                        IntContexto = Convert.ToInt32(reader["Contexto"]),
                        IntInstancia = Convert.ToInt32(reader["Instancia"]),
                        IntOidHallazgo = Convert.ToInt32(reader["OidHallazgo"]),
                        IntResponsable = Convert.ToInt32(reader["Responsable"]),
                        StrDescripcion = reader["Descripcion"].ToString()
                    };
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return hallazgo;
        }

        public static List<Hallazgo> GetHallazgosByidAuditoria(int idAuditoria, int contexto)
        {
            int idResponsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            List<Hallazgo> hallazgos = new List<Hallazgo>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            

            try
            {
                command = new SqlCommand(@" if(@contexto = 1)
	                                            select H.* from AUHallazgo H
		                                            inner join AUAuditoriaInterna A on A.OidAuditoriaInterna = H.Instancia and Contexto = 1
	                                            where Instancia = @Instancia and (H.Responsable = @Responsable or A.OidUsuarioCreador = @Responsable)
                                            else
	                                            select H.* from AUHallazgo H
		                                            inner join AUAuditoriaExterna A on A.OIdAuditoriaExterna = H.Instancia and Contexto = 2
	                                            where Instancia = @Instancia and (H.Responsable = @Responsable or A.OidUsuarioCreador = @Responsable)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Instancia", idAuditoria);
                command.Parameters.AddWithValue("Contexto", contexto);
                command.Parameters.AddWithValue("Responsable", idResponsable);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    hallazgos.Add(new Hallazgo {
                        IntContexto = Convert.ToInt32(reader["Contexto"]),
                        IntInstancia = Convert.ToInt32(reader["Instancia"]),
                        IntOidHallazgo = Convert.ToInt32(reader["OidHallazgo"]),
                        IntResponsable = Convert.ToInt32(reader["Responsable"]),
                        StrDescripcion = reader["Descripcion"].ToString()
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
            return hallazgos;
        }
    }
}
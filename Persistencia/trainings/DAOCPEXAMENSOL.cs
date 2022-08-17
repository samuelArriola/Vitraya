using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.trainings
{
    public class DAOCPEXAMENSOL
    {
        public static void setExamenSol(CPEXAMENSOL examenSol)
        {
            SqlCommand command = new SqlCommand();
            Conexion conexion = new Conexion();
            SqlDataReader reader;
            try
            {
                command = new SqlCommand(@" INSERT INTO [dbo].[CPEXAMENSOL]
                                                   ([IDMATRICULA]
                                                   ,[Resultado]
                                                   ,[OidPCEXAMEN]
                                                   ,[Fecha])
                                             VALUES
                                                   (@IDMATRICULA
                                                   ,@Resultado
                                                   ,@OidPCEXAMEN
                                                   ,@Fecha)
                                            select SCOPE_IDENTITY() instancia, TEMA from CPCAPACITACION  C
	                                            inner join CPEXAMEN E on E.OidInstancia = C.OidCPCAPACITACION and Contexto = 1
                                            where E.OidCPEXAMEN = @OidCPEXAMEN", conexion.OpenConnection());

                command.Parameters.AddWithValue("@IDMATRICULA", examenSol.IntIDMATRICULA);
                command.Parameters.AddWithValue("@Resultado", examenSol.IntResultado);
                command.Parameters.AddWithValue("@OidPCEXAMEN", examenSol.IntOidPCEXAMEN);
                command.Parameters.AddWithValue("@Fecha", examenSol.DtmFecha);
                command.Parameters.AddWithValue("OidCPEXAMEN", examenSol.IntOidPCEXAMEN);

                reader = command.ExecuteReader();
                if(reader.Read())
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        dtmFecha = DateTime.Now,
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = Convert.ToInt32(reader["instancia"]),
                        strAccion = "Crear",
                        strDetalle = $"Se da respuesta para un examen de la capacitacion con tema {reader["TEMA"]}",
                        strEntidad = "CPEXAMENSOL"
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
        }

        public static CPEXAMENSOL getExamenSolUlt()
        {
            CPEXAMENSOL examen = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT TOP(1) * FROM CPEXAMENSOL ORDER BY OidCPEXAMENSOL DESC", conexion.OpenConnection());
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    examen = new CPEXAMENSOL
                    {
                        IntIDMATRICULA = Convert.ToInt32(reader["IDMATRICULA"]),
                        IntOidCPEXAMENSOL = Convert.ToInt32(reader["OidCPEXAMENSOL"]),
                        IntOidPCEXAMEN = Convert.ToInt32(reader["OidPCEXAMEN"]),
                        IntResultado = Convert.ToInt32(reader["Resultado"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
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

            return examen;
        }

        public static CPEXAMENSOL GetExamenApr(int idUsuairo, int idCapacitacion)
        {
            CPEXAMENSOL examen = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPEXAMENSOL as ex "+
                                          "  left join CPMATRICULA on CPMATRICULA.OidCPMATRICULA = ex.IDMATRICULA"+
                                          "  where Resultado >= "+
                                          "  (select  CPEXAMEN.NumApro from CPEXAMEN where OidCPEXAMEN = ex.OidPCEXAMEN and OidCPCapacitacion = @idCapacitacion)"+
                                          "  and CPMATRICULA.GNCodUsu = @idUsuairo", conexion.OpenConnection());

                command.Parameters.AddWithValue("@idUsuairo", idUsuairo);
                command.Parameters.AddWithValue("@idCapacitacion", idCapacitacion);


                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    examen = new CPEXAMENSOL
                    {
                        IntIDMATRICULA = Convert.ToInt32(reader["IDMATRICULA"]),
                        IntOidCPEXAMENSOL = Convert.ToInt32(reader["OidCPEXAMENSOL"]),
                        IntOidPCEXAMEN = Convert.ToInt32(reader["OidPCEXAMEN"]),
                        IntResultado = Convert.ToInt32(reader["Resultado"]),
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
            return examen;
        }

        public static List<CPEXAMENSOL> ListarExamSol(int idUsuario, int idAgenda)
        {
            List<CPEXAMENSOL> examenes = new List<CPEXAMENSOL>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" SELECT es.*, E.Nombre FROM CPEXAMENSOL es
                                               inner join   CPMATRICULA as m on es.IDMATRICULA = m.OidCPMATRICULA
                                               inner join   CPEXAMEN E on E.OidCPEXAMEN = es.OidPCEXAMEN 
                                            where GNCodUsu = @GNCodUsu and OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", idUsuario);
                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CPEXAMENSOL examen = new CPEXAMENSOL
                    {
                        IntIDMATRICULA = Convert.ToInt32(reader["IDMATRICULA"]),
                        IntOidCPEXAMENSOL = Convert.ToInt32(reader["OidCPEXAMENSOL"]),
                        IntOidPCEXAMEN = Convert.ToInt32(reader["OidPCEXAMEN"]),
                        IntResultado = Convert.ToInt32(reader["Resultado"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]).Date,
                        StrTitulo = reader["Nombre"].ToString()
                    };
                    examenes.Add(examen);
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

            return examenes;
        }

        public static CPEXAMENSOL getExamenSol(int idExamensol)
        {
            CPEXAMENSOL examen = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT *  FROM [dbo].[CPEXAMENSOL]  WHERE OidCPEXAMENSOL = @idExamenSol", conexion.OpenConnection());
                command.Parameters.AddWithValue("idExamenSol", idExamensol);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    examen = new CPEXAMENSOL
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        IntIDMATRICULA = Convert.ToInt32(reader["IDMATRICULA"]),
                        IntOidCPEXAMENSOL = Convert.ToInt32(reader["OidCPEXAMENSOL"]),
                        IntOidPCEXAMEN = Convert.ToInt32(reader["OidPCEXAMEN"]),
                        IntResultado = Convert.ToInt32(reader["Resultado"]),
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

            return examen;
        }
    }
}
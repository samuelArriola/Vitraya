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
    public class DAOCPSUBTEMA
    {

        public static void setSubtema(CPSUBTEMA subtema)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPSUBTEMA]"+
                                         "          ([OidCPInstacia]"+
                                         "          ,[SUBTEMA]" +
                                         "          ,[OidCPAgenda]" +
                                         "          ,Contexto)" +
                                         "    VALUES"+
                                         "          (@OidCPInstacia"+
                                         "          ,@SUBTEMA" +
                                         "          ,@OidCPAgenda" +
                                         "          ,@Contexto) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidCPInstacia", subtema.IntOidCPInstacia);
                command.Parameters.AddWithValue("@SUBTEMA", subtema.StrSUBTEMA);
                command.Parameters.AddWithValue("@Contexto", subtema.IntContexto);
                command.Parameters.AddWithValue("@OidCPAgenda", subtema.IntOidCPAgenda);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "CPSUBTEMA"
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


        /// <summary>
        /// Metodos que devuelve una lista de los subtemas que pertenecen a una capacitacion o  a una solicitud
        /// </summary>
        /// <param name="idInstancia">Id que puede ser tanto de una capacitacion como de una solicitud </param>
        /// <returns></returns>
        public static List<CPSUBTEMA> GetSubtemas(int idInstancia)
        {
            List<CPSUBTEMA> temas = new List<CPSUBTEMA>();
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;

            try
            {
                command = new SqlCommand("SELECT * FROM[dbo].[CPSubtema] where OidCPInstacia = @idInstancia", conexion.OpenConnection());
                command.Parameters.AddWithValue("@idInstancia", idInstancia);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPSUBTEMA subtema = new CPSUBTEMA
                    {
                        IntOidCPInstacia = Convert.ToInt32(reader["OidCPInstacia"].ToString()),
                        IntOidCPSUBTEMA = Convert.ToInt32(reader["OidCPSUBTEMA"].ToString()),
                        StrSUBTEMA = reader["SUBTEMA"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"]),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"])
                    };
                    temas.Add(subtema);
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
            return temas;
        }


        public static List<CPSUBTEMA> GetSubtemasByAgenda(int idAgenda)
        {
            List<CPSUBTEMA> subtemas = new List<CPSUBTEMA>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPSubtema where OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    subtemas.Add(new CPSUBTEMA {
                        IntOidCPInstacia = Convert.ToInt32(reader["OidCPInstacia"].ToString()),
                        IntOidCPSUBTEMA = Convert.ToInt32(reader["OidCPSUBTEMA"].ToString()),
                        StrSUBTEMA = reader["SUBTEMA"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"]),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"])
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

            return subtemas;
        }
        public static bool ELiminarSubtema(int idSubtema)
        {
            bool isDeleted = true;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM [dbo].[CPSUBTEMA] WHERE OidCPSUBTEMA = @OidCPSUBTEMA", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPSUBTEMA", idSubtema);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idSubtema,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPSUBTEMA"
                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }

            return isDeleted;
        }

        public static void DeleteSubtemaByIdAgenda(int idAgenda)
        {
            Conexion conexion = new Conexion();
            SqlCommand command;
            try
            {
                command = new SqlCommand("DELETE FROM [dbo].[CPSUBTEMA] WHERE OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
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
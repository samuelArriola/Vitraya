using Entidades.Generales;
using Entidades.trainings;
using Persistencia;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.trainings
{
    public class DAOCPREGUNTA
    {
        public static void setPreguntaExamen(CPPREGUNTA pregunta)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPPREGUNTA]"+
                                          "            ([Pregunta]"+
                                          "            ,[OidCPEXAMEN])"+
                                          "VALUES"+
                                          "            (@Pregunta"+
                                          "           ,@OidCPEXAMEN) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Pregunta", pregunta.StrPregunta);
                command.Parameters.AddWithValue("@OidCPEXAMEN", pregunta.IntOidCPEXAMEN);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar()); //ejecutar consulta.

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "CPPREGUNTA"
                });
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }


        public static CPPREGUNTA getPreguntaExamenUlt()
        {
            SqlCommand command;
            SqlDataReader reader;
            CPPREGUNTA pregunta = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(" select top(1) * from CPPREGUNTA order by OidCPPREGUNTA desc ", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pregunta = new CPPREGUNTA
                    {
                        IntOidCPEXAMEN =  Convert.ToInt32(reader["OidCPEXAMEN"].ToString()),
                        StrPregunta = reader["Pregunta"].ToString(), 
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                    };
                }
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return pregunta;
        }

        public static List<CPPREGUNTA> getPreguntaSExamen(int OidCPExamen)
        {
            SqlCommand command;
            SqlDataReader reader;
            List<CPPREGUNTA> preguntas = new List<CPPREGUNTA>();
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(" select  * from CPPREGUNTA where OidCPEXAMEN = @OidCPEXAMEN ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPEXAMEN", OidCPExamen);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPPREGUNTA pregunta = new CPPREGUNTA
                    {
                        IntOidCPEXAMEN = Convert.ToInt32(reader["OidCPEXAMEN"].ToString()),
                        StrPregunta = reader["Pregunta"].ToString(),
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                    };
                    preguntas.Add(pregunta);

                }
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return preguntas;
        }
        /// <summary>
        /// metodo que elimina una pregunta por su id 
        /// </summary>
        /// <param name="idPregunta"> id de la pregunta</param>
        public static void DeletePregunta(int idPregunta)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from CPPREGUNTA where OidCPPREGUNTA = @OidCPPREGUNTA", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPPREGUNTA", idPregunta);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idPregunta,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPPREGUNTA"
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
    }
}
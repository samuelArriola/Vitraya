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
    public class DAOCPRESPUESTA
    {
        public static void setRespuestaExamCapa(CPRESPUESTA respuesta)
        {
            SqlCommand commad;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("INSERT INTO [dbo].[CPRESPUESTA]"+
                                        "       ([OidCPOPCION]"+
                                        "       ,[OidCPEXAMENSOL]"+
                                        "       ,[OidCPPREGUNTA])" +
                                        " VALUES"+
                                        "       (@OidCPOPCION"+
                                        "       ,@OidCPEXAMENSOL"+
                                        "       ,@OidCPPREGUNTA) select scope_identity()", conexion.OpenConnection());

                commad.Parameters.AddWithValue("@OidCPOPCION", respuesta.IntOidCPOPCION);
                commad.Parameters.AddWithValue("@OidCPEXAMENSOL", respuesta.IntOidCPEXAMENSOL);
                commad.Parameters.AddWithValue("@OidCPPREGUNTA", respuesta.IntOidCPPREGUNTA);
                int OidInstancia = Convert.ToInt32(commad.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "CPRESPUESTA"
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

        public static List<CPRESPUESTA> GetRespExaSol(int idExamenSol)
        {
            List<CPRESPUESTA> respuestas = new List<CPRESPUESTA>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPRESPUESTA WHERE OidCPEXAMENSOL = @OidCPEXAMENSOL", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPEXAMENSOL", idExamenSol);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CPRESPUESTA respuesta = new CPRESPUESTA
                    {
                        IntOidCPEXAMENSOL = Convert.ToInt32(reader["OidCPEXAMENSOL"].ToString()),
                        IntOidCPOPCION = Convert.ToInt32(reader["OidCPOPCION"].ToString()),
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                        IntOidCPRESPUESTA = Convert.ToInt32(reader["OidCPRESPUESTA"].ToString()),
                    };
                    respuestas.Add(respuesta);
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

            return respuestas;
        }
    }
}
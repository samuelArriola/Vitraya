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
    public class DAOCPOPCION
    {
        public static void setOpcionExamen(CPOPCION opcion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPOPCION]"+
                                         "          ([Opcion]"+
                                         "          ,[OidCPPREGUNTA]"+
                                         "          ,[Correcta])"+
                                         "    VALUES"+
                                         "          (@Opcion"+
                                         "          ,@OidCPPREGUNTA"+
                                         "          ,@Correcta) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Opcion", opcion.StrOpcion);
                command.Parameters.AddWithValue("@OidCPPREGUNTA", opcion.IntOidCPPREGUNTA);
                command.Parameters.AddWithValue("@Correcta", opcion.IsCorrecta);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Modificar",
                    strDetalle = $"",
                    strEntidad = "CPMATRICULA"
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


        public static CPOPCION getOpcionExamenUlt()
        {
            SqlCommand command;
            SqlDataReader reader;
            CPOPCION opcion = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(" select top(1) * from CPOPCION order by OidCPPREGUNTA desc ", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    opcion = new CPOPCION
                    {
                        StrOpcion = reader["Opcion"].ToString(),
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                        IsCorrecta = Convert.ToBoolean(reader["Correcta"].ToString()),
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
            return opcion;
        }

        public static List<CPOPCION> getOpcionExamenes(int OidPregunta)
        {
            SqlCommand command;
            SqlDataReader reader;
            List<CPOPCION> opciones = new List<CPOPCION>();
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select  * from CPOPCION where OidCPPREGUNTA = @OidCPPREGUNTA ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPPREGUNTA", OidPregunta);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPOPCION opcion = new CPOPCION
                    {
                        StrOpcion = reader["Opcion"].ToString(),
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                        IsCorrecta = Convert.ToBoolean(reader["Correcta"].ToString()),
                        IntOidOPCION = Convert.ToInt32(reader["OidOPCION"]),
                    };
                    opciones.Add(opcion);
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
            return opciones;
        }

        public static CPOPCION getOpcion(int OidOpcion)
        {
            SqlCommand command;
            SqlDataReader reader;
            CPOPCION opcion = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select  * from CPOPCION where OidOPCION = @OidOPCION ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidOPCION", OidOpcion);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    opcion = new CPOPCION
                    {
                        StrOpcion = reader["Opcion"].ToString(),
                        IntOidCPPREGUNTA = Convert.ToInt32(reader["OidCPPREGUNTA"].ToString()),
                        IsCorrecta = Convert.ToBoolean(reader["Correcta"].ToString()),
                        IntOidOPCION = Convert.ToInt32(reader["OidOPCION"]),
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
            return opcion;
        }

        /// <summary>
        /// Metodo que elimina todas las opciones de una pregunta por su id
        /// </summary>
        /// <param name="idPregunta">id de la pregunta</param>
        public static void DeleteOpcionByIdPre(int idPregunta)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from CPOPCION where OidCPPREGUNTA = @OidCPPREGUNTA", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPPREGUNTA", idPregunta);

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
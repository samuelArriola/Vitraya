using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.proceedings
{
    public class DAOPAAvance
    {

        public static void SetPAAvance(PAAvance avance)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[PAAvance]"+
                                         "          ([Avance]"+
                                         "          ,[OidListaArch]" +
                                         "          ,[OidPAPlanAccion]" +
                                         "          ,[Titulo]" +
                                         "          ,[Fecha])" +
                                         "    VALUES"+
                                         "          (@Avance"+
                                         "          ,@OidListaArch" +
                                         "          ,@OidPAPlanAccion" +
                                         "          ,@Titulo" +
                                         "          ,@Fecha) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("Avance", avance.StrAvance);
                command.Parameters.AddWithValue("OidListaArch", avance.IntOidListaArch);
                command.Parameters.AddWithValue("OidPAPlanAccion", avance.IntOidPAPlanAccion);
                command.Parameters.AddWithValue("Fecha", avance.DtmFecha);
                command.Parameters.AddWithValue("Titulo", avance.StrTitulo);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se crea un avance para el plan de acción con el identificador: {avance.IntOidPAPlanAccion}",
                    strEntidad = "PAAvance"
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

        public static PAAvance GetAvance(int idAvance)
        {
            PAAvance avance = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PAAvance where OidPAAvance = @OidPAAvance", conexion.OpenConnection());

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    avance = new PAAvance
                    {
                        IntOidListaArch = Convert.ToInt32(reader["OidListaArch"]),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"]),
                        StrAvance = reader["Avance"].ToString(),
                        IntOidPAAvance = Convert.ToInt32(reader["OidPAAvance"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        StrTitulo = reader["Titulo"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }

            return avance;
        }

        public static List<PAAvance> GetAAvances()
        {
            List<PAAvance> avances = new List<PAAvance>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from PAAvance", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PAAvance avance = new PAAvance
                    {
                        IntOidListaArch = Convert.ToInt32(reader["OidListaArch"]),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"]),
                        StrAvance = reader["Avance"].ToString(),
                        IntOidPAAvance = Convert.ToInt32(reader["OidPAAvance"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        StrTitulo = reader["Titulo"].ToString()
                    };
                    avances.Add(avance);
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

            return avances;
        }

        public static void UpdateAvance(PAAvance avance)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[PAAvance]"+
                                         "      SET[Avance] = @Avance"+
                                         "         ,[OidListaArch] = @OidListaArch"+
                                         "         ,[OidPAPlanAccion] = @OidPAPlanAccion" +
                                         "         ,[Titulo] = @Titulo" +
                                         "         ,[Fecha] = @Fecha" +
                                         "    WHERE OidPAAvance = @OidPAAvance", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidListaArch", avance.IntOidListaArch);
                command.Parameters.AddWithValue("OidPAPlanAccion", avance.IntOidPAPlanAccion);
                command.Parameters.AddWithValue("Avance", avance.StrAvance);
                command.Parameters.AddWithValue("OidPAAvance", avance.IntOidPAAvance);
                command.Parameters.AddWithValue("Fecha", avance.DtmFecha);
                command.Parameters.AddWithValue("Titulo", avance.StrTitulo);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = avance.IntOidPAAvance,
                    strAccion = "Modificar",
                    strDetalle = "Se realiza modificacion al avance",
                    strEntidad = "PAAvance"
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
        /// Metodo que retorna una lista de avances que pertenecen a un plan de acción por el id del plan de acción 
        /// </summary>
        /// <param name="idPlanAccion">Oid del plan de accion que se desea consultar</param>
        /// <returns></returns>
        public static List<PAAvance> GetAvencesByIdPlan(int idPlanAccion)
        {
            List<PAAvance> avances = new List<PAAvance>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from PAAvance where OidPAPlanAccion = @OidPAPlanAccion", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidPAPlanAccion", idPlanAccion);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PAAvance avance = new PAAvance
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntOidListaArch = Convert.ToInt32(reader["OidListaArch"]),
                        IntOidPAAvance = Convert.ToInt32(reader["OidPAAvance"]),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"]),
                        StrAvance = reader["Avance"].ToString(),
                        StrTitulo = reader["Titulo"].ToString(),
                    };
                    avances.Add(avance);
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

            return avances;
        }
    }
}
using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNEps
    {

        public static GNEps GetGNEps(int id)
        {
            GNEps eps = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNEps where OidGNEps = @OidGNEps and isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNEps", id);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    eps = new GNEps
                    {
                        IntOidGNEps = Convert.ToInt32(reader["OidGNEps"].ToString()),
                        StrEstado = reader["Estado"].ToString(),
                        StrNomEps = reader["NomEps"].ToString(),
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

            return eps;
        }

        public static List<GNEps> ListarEps(string nombreEps)
        {
            List<GNEps> ListaEps = new List<GNEps>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNEps where NomEps like '%'+@NomEps+'%' and isnull(Eliminado, 0) = 0 ORDER BY NomEps", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomEps", nombreEps);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNEps eps = new GNEps
                    {
                        IntOidGNEps = Convert.ToInt32(reader["OidGNEps"].ToString()),
                        StrEstado = reader["Estado"].ToString(),
                        StrNomEps  = reader["NomEps"].ToString(),
                    };
                    ListaEps.Add(eps);
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

            return ListaEps;
        }

        public static void SetGNEps(GNEps eps)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNEps]"+
                                         "          ([NomEps]"+
                                         "          ,[Estado])"+
                                         "    VALUES"+
                                         "          (@NomEps"+
                                         "          ,@Estado) select SCOPE_IDENTITY()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomEps", eps.StrNomEps);
                command.Parameters.AddWithValue("@Estado", eps.StrEstado);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea la EPS {eps.StrNomEps}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNEps"
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

        public static bool DeleteEps(int id)
        {
            bool isDeleted = true;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNEps where OidGNEps = @OidGNEps " +
                    "Delete from GNEps where OidGNEps = @OidGNEps", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNEps", id);
                reader =  command.ExecuteReader();

                if (reader.Read())
                {
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = id,
                        intOidGNHistorico = 0,
                        strAccion = "Eliminar",
                        strDetalle = $"Se elimina la EPS {reader["NomEps"].ToString()}",
                        dtmFecha = DateTime.Now,
                        strEntidad = "GNEps"
                    });
                }

            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isDeleted;
        }

        public static bool UpdateEps(GNEps eps)
        {
            bool isUpdated = true;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Update GNEps set NomEps = @NomEps, Estado = @Estado where OidGNEps = @OidGNEps", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomEps", eps.StrNomEps);
                command.Parameters.AddWithValue("@Estado", eps.StrEstado);
                command.Parameters.AddWithValue("@OidGNEps", eps.IntOidGNEps);

                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = eps.IntOidGNEps,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se Modifica la eps  {eps.StrNomEps}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNEps"
                });
            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isUpdated;
        }
    }
}
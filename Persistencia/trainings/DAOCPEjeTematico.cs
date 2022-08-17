
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
    public class DAOCPEjeTematico
    {
        public static CPEJETEMATICO GetEjeTematico(int idEjeTEmatico)
        {
            CPEJETEMATICO ejetematico = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPEJETEMATICO where OidCPEJETEMATICO = @OidCPEJETEMATICO and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPEJETEMATICO", idEjeTEmatico);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ejetematico = new CPEJETEMATICO
                    {
                        IntOidCPEJETEMATICO = Convert.ToInt32(reader["OidCPEJETEMATICO"]),
                        StrEJETEMATICO = reader["EJETEMATICO"].ToString(),
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

            return  ejetematico;
        }

        public static List<CPEJETEMATICO> ListarEjeTem()
        {
            List<CPEJETEMATICO> ejesTematicos = new List<CPEJETEMATICO>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPEJETEMATICO where isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CPEJETEMATICO eje = new CPEJETEMATICO
                    {
                        IntOidCPEJETEMATICO = Convert.ToInt32(reader["OidCPEJETEMATICO"]),
                        StrEJETEMATICO = reader["EJETEMATICO"].ToString()
                    };
                    ejesTematicos.Add(eje);
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

            return ejesTematicos;
        }

        public static bool updateEjeTem(CPEJETEMATICO eje)
        {
            bool isUpdated = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[CPEJETEMATICO] SET[EJETEMATICO] = @EJETEMATICO WHERE OidCPEJETEMATICO = @OidCPEJETEMATICO ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPEJETEMATICO", eje.IntOidCPEJETEMATICO);
                command.Parameters.AddWithValue("@EJETEMATICO", eje.StrEJETEMATICO);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = eje.IntOidCPEJETEMATICO,
                    strAccion = "Modificar",
                    strDetalle = $"Se modifica el nombre del eje temático {eje.StrEJETEMATICO}",
                    strEntidad = "CPEJETEMATICO"
                });

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                isUpdated = false;
            }
            finally
            {
                conexion.CloseConnection();
            }


            return isUpdated;
        } 

        public static bool deleteEjeTem(int id)
        {
            bool isDeleted = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[CPEJETEMATICO] SET Eliminado = 1  WHERE OidCPEJETEMATICO = @OidCPEJETEMATICO ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPEJETEMATICO", id);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = id,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPEJETEMATICO"
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

        public static void setEjeTem(CPEJETEMATICO eje)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPEJETEMATICO] (EJETEMATICO, Eliminado) VALUES (@EJETEMATICO, 0) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@EJETEMATICO", eje.StrEJETEMATICO);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se  crea el eje temático {eje.StrEJETEMATICO}",
                    strEntidad = "CPEJETEMATICO"
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

        public static List<CPEJETEMATICO> GetEjesTematicos(string nombre)
        {
            List<CPEJETEMATICO> ejesTematicos = new List<CPEJETEMATICO>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPEJETEMATICO WHERE EJETEMATICO LIKE '%' + @EJETEMATICO + '%' AND isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("EJETEMATICO", nombre);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    ejesTematicos.Add(new CPEJETEMATICO
                    {
                        IntOidCPEJETEMATICO = Convert.ToInt32(reader["OidCPEJETEMATICO"]),
                        StrEJETEMATICO = reader["EJETEMATICO"].ToString()
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
            return ejesTematicos;
        }
    }
}
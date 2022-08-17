using Entidades.Generales;
using Entidades.GestionDocumental;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.GestionDocumental
{
    public class DAOGDActividad
    {
        public static void SetActividad(GDActividad Actividad)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [GDActividad]" +
                                           "([OidGDDocumento]" +
                                           ",[Responsable]" +
                                           ",[Descripcion]" +
                                           ",[NomActividad])" +
                                     "VALUES" +
                                           "(@OidGDDocumento," +
                                           "@Responsable," +
                                           "@Descripcion," +
                                           "@NomActividad) SELECT scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", Actividad.IntOidGDDocumento);
                command.Parameters.AddWithValue("@Responsable", Actividad.StrResponsable);
                command.Parameters.AddWithValue("@Descripcion", Actividad.StrDescripcion);
                command.Parameters.AddWithValue("@NomActividad", Actividad.StrNomActividad);
                int OidInstancia =  Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDActividad"
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

       

        public static void Setupdate(GDActividad Actividad)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update [GDActividad] SET " +
                                           " [Responsable] = @Responsable " +
                                           " ,[Descripcion] = @Descripcion " +
                                            " ,[NomActividad] = @NomActividad " +
                                           " where OidActividad = @OidActividad", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Responsable", Actividad.StrResponsable);
                command.Parameters.AddWithValue("@Descripcion", Actividad.StrDescripcion);
                command.Parameters.AddWithValue("@NomActividad", Actividad.StrNomActividad);
                command.Parameters.AddWithValue("@OidActividad", Actividad.IntOidActividad);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {   
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Actividad.IntOidActividad,
                    intOidGNHistorico = 0,
                    strAccion = "Midificar",
                    strDetalle = $"",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDActividad"
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


        public static List<GDActividad> GetActividadesByDoc(int idDocumento)
        {

            List<GDActividad> actividades = new List<GDActividad>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDActividad where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GDActividad actividad = new GDActividad
                    {
                        IntOidActividad = Convert.ToInt32(reader["OidActividad"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        StrDescripcion = reader["Descripcion"].ToString(),
                        StrNomActividad = reader["NomActividad"].ToString(),
                        StrResponsable   = reader["Responsable"].ToString(),
                    };
                    actividades.Add(actividad);
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


            return actividades;
        }

        public static GDActividad GetActividad(int idActividad)
        {
            GDActividad actividad = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDActividad where OidActividad = @OidActividad", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidActividad",idActividad);
                
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    actividad = new GDActividad
                    {
                        IntOidActividad = Convert.ToInt32(reader["OidActividad"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        StrDescripcion = reader["Descripcion"].ToString(),
                        StrNomActividad = reader["NomActividad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
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
            return actividad;
        }

        public static void DeleteAtividadByIdDoc(int idDocumento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"delete from GDActividad where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDDocumento", idDocumento);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idDocumento,
                    intOidGNHistorico = 0,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDActividad"
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
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
    public class DAOGDEvaluacion
    {

        public static void SetEvaluacion(GDEvaluacion evalucion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDEvaluacion]"+
                                           "([Estado]"+
                                           ",[Insidencia]"+
                                           ",[OidGDSolicitud]"+
                                           ",[Tipo])"+
                                     "VALUES"+
                                           "(@Estado,"+
                                           "@Insidencia," +
                                           "@OidGDSolicitud,"+
                                           "@Tipo)" +
                                           " select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Estado", evalucion.StrEstado);
                command.Parameters.AddWithValue("@Insidencia", evalucion.SrtInsidencia);
                command.Parameters.AddWithValue("@OidGDSolicitud", evalucion.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@Tipo",evalucion.StrTipo);
                int OidInstancia = Convert.ToInt32( command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se Crea una evaluacion de documento",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDEvaluacion"
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

        public static GDEvaluacion GetUltEvalucion(int idSolicitud)
        {
            GDEvaluacion evaluacion = null;
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select top(1)* from GDEvaluacion where OidGDSolicitud = @OidGDSolicitud order by OidGDEvaluacion desc", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDSolicitud", idSolicitud);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    evaluacion = new GDEvaluacion
                    {
                        IntOIdGDEvaluacion = Convert.ToInt32(reader["OIdGDEvaluacion"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        SrtInsidencia = reader["Insidencia"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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

            return evaluacion;
        }

        public static void Setupdate(GDEvaluacion evalucion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update [dbo].[GDEvaluacion] SET " +
                                           " [Estado] = @Estado " +
                                           " ,[Insidencia] = @Insidencia " +
                                           " where OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Estado", evalucion.StrEstado);
                command.Parameters.AddWithValue("@Insidencia", evalucion.SrtInsidencia);
                command.Parameters.AddWithValue("@OidGDSolicitud", evalucion.IntOidGDSolicitud);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = evalucion.IntOIdGDEvaluacion,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza la información de la evaluación de un documento",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDEvaluacion"
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
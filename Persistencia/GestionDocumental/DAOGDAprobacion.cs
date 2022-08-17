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
    public class DAOGDAprobacion
    {
        public static void SetAprobacion(GDAprobacion Aprobacion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDAprobacion]" +
                                         "          ([OidGDDocumento]" +
                                         "          ,[OidRevisor]" +
                                         "          ,[Estado]" +
                                         "          ,[Fecha]" +
                                         "          ,[Cargo]" +
                                         "          ,[Detalles])" +
                                         "    VALUES" +
                                         "          (@OidGDDocumento" +
                                         "          , @OidRevisor" +
                                         "          , @Estado" +
                                         "          , @Fecha" +
                                         "          , @Cargo" +
                                         "          , @Detalles) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", Aprobacion.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidRevisor", Aprobacion.IntOidRevisor);
                command.Parameters.AddWithValue("@Estado", Aprobacion.IntEstado);
                command.Parameters.AddWithValue("@Detalles", Aprobacion.StrDetalles);
                command.Parameters.AddWithValue("@Fecha", Aprobacion.DtmFecha);
                command.Parameters.AddWithValue("@Cargo", Aprobacion.StrCargo);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se asigna Aprobador el {Aprobacion.IntOidRevisor} para el documeto con código {Aprobacion.IntOidGDDocumento}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDAprobacion"
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

        public static List<GDAprobacion> GetGDAprobacionesPorIdRevisor(int idRevisor)
        {
            List<GDAprobacion> Aprobaciones = new List<GDAprobacion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDAprobacion where OidRevisor = @OidRevisor", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidRevisor", idRevisor);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDAprobacion Aprobacion = new GDAprobacion
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDAprobacion = Convert.ToInt32(reader["OidGDAprobacion"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    };

                    Aprobaciones.Add(Aprobacion);
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
            return Aprobaciones;
        }

        public static List<GDAprobacion> GetGDAprobaciones()
        {
            List<GDAprobacion> Aprobaciones = new List<GDAprobacion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDAprobacion", conexion.OpenConnection());


                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDAprobacion Aprobacion = new GDAprobacion
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDAprobacion = Convert.ToInt32(reader["OidGDAprobacion"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrCargo = reader["Cargo"].ToString(),
                        StrDetalles = reader["Detalles"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())

                    };

                    Aprobaciones.Add(Aprobacion);
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
            return Aprobaciones;
        }

        public static List<GDAprobacion> GetGDAprobaciones(int idDocumento)
        {
            List<GDAprobacion> Aprobaciones = new List<GDAprobacion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDAprobacion where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDAprobacion Aprobacion = new GDAprobacion
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDAprobacion = Convert.ToInt32(reader["OidGDAprobacion"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    };

                    Aprobaciones.Add(Aprobacion);
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
            return Aprobaciones;
        }


        public static GDAprobacion GetGDAprobacion(int idAprobacion)
        {
            GDAprobacion Aprobacion = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDAprobacion where OidGDAprobacion = @OidGDAprobacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDAprobacion", idAprobacion);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Aprobacion = new GDAprobacion
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDAprobacion = Convert.ToInt32(reader["OidGDAprobacion"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())

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
            return Aprobacion;
        }

        public static GDAprobacion GetGDAprobacion(int idRevisor, int  idDocumento)
        {
            GDAprobacion Aprobacion = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDAprobacion where OidGDDocumento = @OidGDDocumento and OidRevisor = @OidRevisor", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);
                command.Parameters.AddWithValue("@OidRevisor", idRevisor);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Aprobacion = new GDAprobacion
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDAprobacion = Convert.ToInt32(reader["OidGDAprobacion"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())

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
            return Aprobacion;
        }



        public static  void UpdateAprobacion(GDAprobacion Aprobacion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"UPDATE [dbo].[GDAprobacion]
                                               SET[OidGDDocumento] = @OidGDDocumento
                                                  ,[OidRevisor] = @OidRevisor
                                                  ,[Estado] = @Estado
                                                  ,[Fecha] = @Fecha
                                                  ,[Cargo] = @Cargo
                                                  ,[Detalles] = @Detalles
                                             WHERE  OidGDAprobacion = @OidGDAprobacion
                                             Select NomDoc from GDDocumento where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", Aprobacion.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidRevisor", Aprobacion.IntOidRevisor);
                command.Parameters.AddWithValue("@Estado", Aprobacion.IntEstado);
                command.Parameters.AddWithValue("@Detalles", Aprobacion.StrDetalles);
                command.Parameters.AddWithValue("@OidGDAprobacion", Aprobacion.IntOidGDAprobacion);
                command.Parameters.AddWithValue("@Fecha", Aprobacion.DtmFecha);
                command.Parameters.AddWithValue("@Cargo", Aprobacion.StrCargo);

                string nombre = command.ExecuteScalar().ToString();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Aprobacion.IntOidGDAprobacion,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"El usuario {Aprobacion.IntOidRevisor} {(Aprobacion.IntEstado == GDRevision.APROBADO ? "Aceptó":"Rechazó")} la aprobación para el documento {nombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDAprobacion"
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
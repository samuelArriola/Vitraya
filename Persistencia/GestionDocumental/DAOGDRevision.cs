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
    public class DAOGDRevision
    {
        public static void SetRevision(GDRevision revision)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDRevision]"+
                                         "          ([OidGDDocumento]"+
                                         "          ,[OidRevisor]"+
                                         "          ,[Estado]"+
                                         "          ,[Fecha]" +
                                         "          ,[Cargo]" +
                                         "          ,[Detalles])" +
                                         "    VALUES"+
                                         "          (@OidGDDocumento"+
                                         "          ,@OidRevisor"+
                                         "          ,@Estado"+
                                         "          ,@Fecha" +
                                         "          ,@Cargo" +
                                         "          ,@Detalles) select spcope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", revision.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidRevisor", revision.IntOidRevisor);
                command.Parameters.AddWithValue("@Estado", revision.IntEstado);
                command.Parameters.AddWithValue("@Detalles", revision.StrDetalles);
                command.Parameters.AddWithValue("@Fecha", revision.DtmFecha);
                command.Parameters.AddWithValue("@Cargo", revision.StrCargo);

                int OidInstancia  = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = "Se crea una revision para documento",
                    strEntidad = "GDRevision"
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

        public static List<GDRevision> GetGDRevisionesPorIdRevisor(int idRevisor)
        {
            List<GDRevision> revisiones = new List<GDRevision>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDRevision where OidRevisor = @OidRevisor", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidRevisor", idRevisor);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDRevision revision = new GDRevision
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDRevision = Convert.ToInt32(reader["OidGDRevision"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    };

                    revisiones.Add(revision);
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
            return revisiones;
        }

        public static List<GDRevision> GetGDRevisiones()
        {
            List<GDRevision> revisiones = new List<GDRevision>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDRevision", conexion.OpenConnection());


                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDRevision revision = new GDRevision
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDRevision = Convert.ToInt32(reader["OidGDRevision"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrDetalles = reader["Detalles"].ToString(),
                        StrCargo = reader["Cargo"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    };

                    revisiones.Add(revision);
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
            return revisiones;
        }

        public static GDRevision GetGDRevision(int idRevision)
        {
            GDRevision revision = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDRevision where OidGDRevision = @OidGDRevision", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDRevision", idRevision);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    revision = new GDRevision
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDRevision = Convert.ToInt32(reader["OidGDRevision"]),
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
            return revision;
        }
        public static GDRevision GetGDRevision(int idRevisor, int idDocumento)
        {
            GDRevision revision = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDRevision where OidRevisor = @OidRevisor and OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidRevisor", idRevisor);
                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    revision = new GDRevision
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDRevision = Convert.ToInt32(reader["OidGDRevision"]),
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
            return revision;
        }

        public static void UpdateRvision(GDRevision revision)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GDRevision]"+
                                         "      SET[OidGDDocumento] = @OidGDDocumento"+
                                         "         ,[OidRevisor] = @OidRevisor"+
                                         "         ,[Estado] = @Estado"+
                                         "         ,[Fecha] = @Fecha" +
                                         "         ,[Cargo] = @Cargo" +
                                         "         ,[Detalles] = @Detalles" +
                                         "    WHERE  OidGDRevision = @OidGDRevision", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", revision.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidRevisor", revision.IntOidRevisor);
                command.Parameters.AddWithValue("@Estado", revision.IntEstado);
                command.Parameters.AddWithValue("@Detalles", revision.StrDetalles);
                command.Parameters.AddWithValue("@OidGDRevision", revision.IntOidGDRevision);
                command.Parameters.AddWithValue("@Fecha", revision.DtmFecha);
                command.Parameters.AddWithValue("@Cargo", revision.StrCargo);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = revision.IntOidGDRevision,
                    strAccion = "Modoficar",
                    strDetalle = "el revisor realizo revision de un documento",
                    strEntidad = "GDRevision"
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

        public static List<GDRevision> GetGDRevisiones(int idDocumento)
        {
            List<GDRevision> revisiones = new List<GDRevision>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("Select * from GDRevision where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDRevision revision = new GDRevision
                    {
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDRevision = Convert.ToInt32(reader["OidGDRevision"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        StrCargo = reader["Cargo"].ToString(),
                        StrDetalles = reader["Detalles"].ToString(),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())       
                    };

                    revisiones.Add(revision);
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
            return revisiones;
        }

    }
}
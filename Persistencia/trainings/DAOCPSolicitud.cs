using Entidades.trainings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.trainings
{
    public class DAOCPSolicitud
    {
        public static void SetCPSolictud(CPSolicitud solicitud)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPSolicitud]" +
                                         "          ([Fecha]" +
                                         "          ,[HoraInicial]" +
                                         "          ,[HoraFinal]" +
                                         "          ,[Lugar]" +
                                         "          ,[UnidadFuncional]" +
                                         "          ,[OidCPEjeTematico]" +
                                         "          ,[Tema]" +
                                         "          ,[Estado]" +
                                         "          ,[Modalidad]" +
                                         "          ,[Responsable]" +
                                         "          ,[GNCodUsu]" +
                                         "          ,[Link]" +
                                         "          ,[OidListaArchivos]" +
                                         "          ,[InfoMatricula]" +
                                         "          ,[OidGNAchivo])" +
                                         "    VALUES" +
                                         "          ( @Fecha" +
                                         "          , @HoraInicial" +
                                         "          , @HoraFinal" +
                                         "          , @Lugar" +
                                         "          , @UnidadFuncional" +
                                         "          , @OidCPEjeTematico" +
                                         "          , @Tema" +
                                         "          , @Estado" +
                                         "          , @Modalidad" +
                                         "          , @Responsable" +
                                         "          , @GNCodUsu" +
                                         "          , @Link" +
                                         "          , @OidListaArchivos" +
                                         "          , @InfoMatricula" +
                                         "          , @OidGNAchivo)", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Fecha", solicitud.DtmFecha);
                command.Parameters.AddWithValue("@HoraInicial", solicitud.DtmHoraInicial);
                command.Parameters.AddWithValue("@HoraFinal", solicitud.DtmHoraFinal);
                command.Parameters.AddWithValue("@Lugar", solicitud.StrLugar);
                command.Parameters.AddWithValue("@UnidadFuncional", solicitud.StrUnidadFuncional);
                command.Parameters.AddWithValue("@OidCPEjeTematico", solicitud.IntOidCPEjeTematico);
                command.Parameters.AddWithValue("@Tema", solicitud.StrTema);
                command.Parameters.AddWithValue("@Estado", solicitud.IntEstado);
                command.Parameters.AddWithValue("@Modalidad", solicitud.StrModalidad);
                command.Parameters.AddWithValue("@Responsable", solicitud.StrResponsable);
                command.Parameters.AddWithValue("@GNCodUsu", solicitud.IntGNCodUsu);
                command.Parameters.AddWithValue("@Link", solicitud.StrLink);
                command.Parameters.AddWithValue("@OidListaArchivos", solicitud.IntOidListaArchivos);
                command.Parameters.AddWithValue("@InfoMatricula", solicitud.StrInfoMatricula);
                command.Parameters.AddWithValue("@OidGNAchivo", solicitud.IntOidGNAchivo);


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

        public static List<CPSolicitud> GetCPSolicitudes()
        {
            List<CPSolicitud> solictitudes = new List<CPSolicitud>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPSolicitud", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    CPSolicitud solicitud = new CPSolicitud
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"]),
                        DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"]),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidCpsolicitud = Convert.ToInt32(reader["OidCpsolicitud"]),
                        IntOidGNAchivo = Convert.ToInt32(reader["OidGNAchivo"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrInfoMatricula = reader["InfoMatricula"].ToString(),
                        StrLink = reader["Link"].ToString(),
                        StrLugar = reader["Lugar"].ToString(),
                        StrModalidad = reader["Modalidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrTema = reader["Tema"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                    };
                    solictitudes.Add(solicitud);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Test");
            }
            finally
            {
                conexion.CloseConnection();
            }

            return solictitudes;
        }

        public static List<CPSolicitud> GetCPSolicitudes(string tema, string fecha1, string fecha2, string lugar)
        {
            List<CPSolicitud> solictitudes = new List<CPSolicitud>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPSolicitud " +
                    " where Fecha >= @fecha1 and Fecha <= @fecha2 and " +
                    "Tema like '%' + @Tema + '%' and  Lugar like '%' + @Lugar + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("Tema", tema);
                command.Parameters.AddWithValue("fecha1", fecha1);
                command.Parameters.AddWithValue("fecha2", fecha2);
                command.Parameters.AddWithValue("Lugar", lugar);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPSolicitud solicitud = new CPSolicitud();
                   

                    solicitud.DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    solicitud.DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"].ToString());
                    solicitud.DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"].ToString());
                    solicitud.IntEstado = Convert.ToInt32(reader["Estado"]);
                    solicitud.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]);
                    solicitud.IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]);
                    solicitud.IntOidCpsolicitud = Convert.ToInt32(reader["OidCpsolicitud"]);
                    solicitud.IntOidGNAchivo = Convert.ToInt32(reader["OidGNAchivo"]);
                    solicitud.IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]);
                    solicitud.StrInfoMatricula = reader["InfoMatricula"].ToString();
                    solicitud.StrLink = reader["Link"].ToString();
                    solicitud.StrLugar = reader["Lugar"].ToString();
                    solicitud.StrModalidad = reader["Modalidad"].ToString();
                    solicitud.StrResponsable = reader["Responsable"].ToString();
                    solicitud.StrTema = reader["Tema"].ToString();
                    solicitud.StrUnidadFuncional = reader["UnidadFuncional"].ToString();
                    
                    solictitudes.Add(solicitud);

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message );
            }
            finally
            {
                conexion.CloseConnection();
            }

            return solictitudes;
        }


        public static CPSolicitud GetSolicitud(int idSolicitud)
        {
            CPSolicitud solicitud = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPSolicitud where OidCpsolicitud = @OidCpsolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidCpsolicitud", idSolicitud);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    solicitud = new CPSolicitud
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"].ToString()),
                        DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidCpsolicitud = Convert.ToInt32(reader["OidCpsolicitud"]),
                        IntOidGNAchivo = Convert.ToInt32(reader["OidGNAchivo"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrInfoMatricula = reader["InfoMatricula"].ToString(),
                        StrLink = reader["Link"].ToString(),
                        StrLugar = reader["Lugar"].ToString(),
                        StrModalidad = reader["Modalidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrTema = reader["Tema"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "DAOCPSolcitud.GetSolicitud");
            }
            finally
            {
                conexion.CloseConnection();
            }

            return solicitud;
        }

        public static CPSolicitud GetSolicitudUlt()
        {
            CPSolicitud solicitud = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select top(1) * from CPSolicitud order by OidCpsolicitud desc", conexion.OpenConnection());

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    solicitud = new CPSolicitud
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"]),
                        DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"]),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidCpsolicitud = Convert.ToInt32(reader["OidCpsolicitud"]),
                        IntOidGNAchivo = Convert.ToInt32(reader["OidGNAchivo"]),
                        IntOidListaArchivos = Convert.ToInt32(reader["OidListaArchivos"]),
                        StrInfoMatricula = reader["InfoMatricula"].ToString(),
                        StrLink = reader["Link"].ToString(),
                        StrLugar = reader["Lugar"].ToString(),
                        StrModalidad = reader["Modalidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrTema = reader["Tema"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
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

            return solicitud;
        }

        public static void UpdateSolicitud(CPSolicitud solicitud)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[CPSolicitud]" +
                                         "      SET[Fecha] = @Fecha"+
                                         "         ,[HoraInicial] = @HoraInicial"+
                                         "         ,[HoraFinal] = @HoraFinal"+
                                         "         ,[Lugar] = @Lugar"+
                                         "         ,[UnidadFuncional] = @UnidadFuncional"+
                                         "         ,[OidCPEjeTematico] = @OidCPEjeTematico"+
                                         "         ,[Tema] = @Tema"+
                                         "         ,[Estado] = @Estado"+
                                         "         ,[Modalidad] = @Modalidad"+
                                         "         ,[Responsable] = @Responsable"+
                                         "         ,[GNCodUsu] = @GNCodUsu"+
                                         "         ,[Link] = @Link"+
                                         "         ,[OidListaArchivos] = @OidListaArchivos"+
                                         "         ,[InfoMatricula] = @InfoMatricula"+
                                         "         ,[OidGNAchivo] = @OidGNAchivo"+
                                         "    WHERE OidCpsolicitud = @OidCpsolicitud", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Fecha", solicitud.DtmFecha);
                command.Parameters.AddWithValue("@HoraInicial", solicitud.DtmHoraInicial);
                command.Parameters.AddWithValue("@HoraFinal", solicitud.DtmHoraFinal);
                command.Parameters.AddWithValue("@Lugar", solicitud.StrLugar);
                command.Parameters.AddWithValue("@UnidadFuncional", solicitud.StrUnidadFuncional);
                command.Parameters.AddWithValue("@OidCPEjeTematico", solicitud.IntOidCPEjeTematico);
                command.Parameters.AddWithValue("@Tema", solicitud.StrTema);
                command.Parameters.AddWithValue("@Estado", solicitud.IntEstado);
                command.Parameters.AddWithValue("@Modalidad", solicitud.StrModalidad);
                command.Parameters.AddWithValue("@Responsable", solicitud.StrResponsable);
                command.Parameters.AddWithValue("@GNCodUsu", solicitud.IntGNCodUsu);
                command.Parameters.AddWithValue("@Link", solicitud.StrLink);
                command.Parameters.AddWithValue("@OidListaArchivos", solicitud.IntOidListaArchivos);
                command.Parameters.AddWithValue("@InfoMatricula", solicitud.StrInfoMatricula);
                command.Parameters.AddWithValue("@OidGNAchivo", solicitud.IntOidGNAchivo);
                command.Parameters.AddWithValue("@OidCpsolicitud", solicitud.IntOidCpsolicitud);

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
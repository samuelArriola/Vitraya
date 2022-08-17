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
    public class DAOCPAgenda
    {

        public static int SetCPAgenda(CPAgenda agenda)
        {
            int idAgenda = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[CPAgenda]
                                               ([Fecha]
                                               ,[FechaFirma]
                                               ,[HoraInicial]
                                               ,[HoraFinal]
                                               ,[OidCPCapacitacion]
                                               ,[LinkReunion]
                                               ,[LinkAnexo]
                                               ,[OidGNListaArchivos]
                                               ,[TiempoFirma]
                                               ,[Estado]
                                               ,[Modalidad]
                                               ,[Lugar]
                                               ,[FechaFinal]
                                               ,[OidUsuarioResponsable]
                                               ,[Facilitador]
                                               ,[Responsable])
                                         VALUES
                                               (@Fecha
                                               ,@FechaFirma
                                               ,@HoraInicial
                                               ,@HoraFinal
                                               ,@OidCPCapacitacion
                                               ,@LinkReunion
                                               ,@LinkAnexo
                                               ,@OidGNListaArchivos
                                               ,@TiempoFirma
                                               ,@Estado
                                               ,@Modalidad
                                               ,@Lugar
                                               ,@FechaFinal
                                               ,@OidUsuarioResponsable
                                               ,@Facilitador
                                               ,@Responsable)
                                        select CAST(SCOPE_IDENTITY() as int)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Fecha", agenda.DtmFecha);
                command.Parameters.AddWithValue("FechaFirma", agenda.DtmFechaFirma);
                command.Parameters.AddWithValue("HoraInicial", agenda.DtmHoraInicial);
                command.Parameters.AddWithValue("HoraFinal", agenda.DtmHoraFinal);
                command.Parameters.AddWithValue("OidCPCapacitacion", agenda.IntOidCPCapacitacion);
                command.Parameters.AddWithValue("LinkReunion", agenda.StrLinkReunion);
                command.Parameters.AddWithValue("LinkAnexo", agenda.StrLinkAnexo);
                command.Parameters.AddWithValue("OidGNListaArchivos", agenda.IntOidGNListaArchivos);
                command.Parameters.AddWithValue("TiempoFirma", agenda.IntTiempoFirma);
                command.Parameters.AddWithValue("Estado", agenda.IntEstado);
                command.Parameters.AddWithValue("Modalidad", agenda.StrModalidad);
                command.Parameters.AddWithValue("Lugar", agenda.StrLugar);
                command.Parameters.AddWithValue("Responsable", agenda.StrResponsable);
                command.Parameters.AddWithValue("FechaFinal", agenda.DtmFechaFinal);
                command.Parameters.AddWithValue("OidUsuarioResponsable", agenda.IntOidUsuarioResponsable);
                command.Parameters.AddWithValue("Facilitador", agenda.StrFacilitador);

                idAgenda = (int)(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idAgenda,
                    strAccion = "Crear",
                    strDetalle = $"Se crea una nueva agenda para la capacitación con el codigo {agenda.IntOidCPCapacitacion}",
                    strEntidad = "CPAgenda"
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

            return idAgenda;
        }
        public static CPAgenda GetAgenda(int idAgenda)
        {
            CPAgenda agenda = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPAgenda where OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    agenda = new CPAgenda();
                    agenda.DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    agenda.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"] == DBNull.Value ? "01-01-3000" : reader["FechaFirma"].ToString());
                    agenda.DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"].ToString());
                    agenda.DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"].ToString());
                    agenda.IntEstado = Convert.ToInt32(reader["Estado"]);
                    agenda.IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]);
                    agenda.IntOidCPCapacitacion = Convert.ToInt32(reader["OidCPCapacitacion"]);
                    agenda.IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"] == DBNull.Value ? "0": reader["OidGNListaArchivos"]);
                    agenda.IntTiempoFirma = Convert.ToInt32(reader["TiempoFirma"]);
                    agenda.StrLinkAnexo = reader["LinkAnexo"].ToString();
                    agenda.StrLinkReunion = reader["LinkReunion"].ToString();
                    agenda.StrLugar = reader["Lugar"].ToString();
                    agenda.StrModalidad = reader["Modalidad"].ToString();
                    agenda.StrResponsable = reader["Responsable"].ToString();
                    agenda.StrFacilitador = reader["Facilitador"].ToString();
                    agenda.IntOidUsuarioResponsable = Convert.ToInt32(reader["OidUsuarioResponsable"]);
                    agenda.DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"] == DBNull.Value ? "01/01/1800" : reader["FechaFinal"].ToString());
                    agenda.IntOidCPExamen = Convert.ToInt32(reader["OidCPExamen"] == DBNull.Value ? "0" : reader["OidCPExamen"]);
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

            return agenda;
        }

        public static void UpdataCPAgenda(CPAgenda agenda)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"UPDATE [dbo].[CPAgenda]
                                           SET [Fecha] = @Fecha
                                              ,[FechaFirma] = @FechaFirma
                                              ,[HoraInicial] = @HoraInicial
                                              ,[HoraFinal] = @HoraFinal
                                              ,[OidCPCapacitacion] = @OidCPCapacitacion
                                              ,[LinkReunion] = @LinkReunion
                                              ,[LinkAnexo] = @LinkAnexo
                                              ,[OidGNListaArchivos] = @OidGNListaArchivos
                                              ,[TiempoFirma] = @TiempoFirma
                                              ,[Estado] = @Estado
                                              ,[Modalidad] = @Modalidad
                                              ,[Lugar] = @Lugar
                                              ,[Responsable] = @Responsable
                                              ,[OidUsuarioResponsable] = @OidUsuarioResponsable
                                              ,[FechaFinal] = @FechaFinal
                                              ,[OidCPExamen] = @OidCPExamen
                                              ,[Facilitador] = @Facilitador
                                         WHERE OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("Fecha", agenda.DtmFecha);
                command.Parameters.AddWithValue("FechaFirma", agenda.DtmFechaFirma);
                command.Parameters.AddWithValue("HoraInicial", agenda.DtmHoraInicial);
                command.Parameters.AddWithValue("HoraFinal", agenda.DtmHoraFinal);
                command.Parameters.AddWithValue("OidCPCapacitacion", agenda.IntOidCPCapacitacion);
                command.Parameters.AddWithValue("LinkReunion", agenda.StrLinkReunion);
                command.Parameters.AddWithValue("LinkAnexo", agenda.StrLinkAnexo);
                command.Parameters.AddWithValue("OidGNListaArchivos", agenda.IntOidGNListaArchivos);
                command.Parameters.AddWithValue("TiempoFirma", agenda.IntTiempoFirma);
                command.Parameters.AddWithValue("Estado", agenda.IntEstado);
                command.Parameters.AddWithValue("Modalidad", agenda.StrModalidad);
                command.Parameters.AddWithValue("Lugar", agenda.StrLugar);
                command.Parameters.AddWithValue("Responsable", agenda.StrResponsable);
                command.Parameters.AddWithValue("OidCPAgenda", agenda.IntOidCPAgenda);
                command.Parameters.AddWithValue("OidUsuarioResponsable", agenda.IntOidUsuarioResponsable);
                command.Parameters.AddWithValue("FechaFinal", agenda.DtmFechaFinal);
                command.Parameters.AddWithValue("OidCPExamen", agenda.IntOidCPExamen);
                command.Parameters.AddWithValue("Facilitador", agenda.StrFacilitador);
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

        public static List<CPAgenda> GetAgendasByCapacitacion(int idCapacitacion)
        {
            List<CPAgenda> agendas = new List<CPAgenda>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPAgenda where OidCPCapacitacion = @OidCPCapacitacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPCapacitacion", idCapacitacion);

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    CPAgenda agenda = new CPAgenda();

                    agenda.DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    agenda.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"] == DBNull.Value ? "01-01-3000" : reader["FechaFirma"].ToString());
                    agenda.DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"].ToString());
                    agenda.DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"].ToString());
                    agenda.IntEstado = Convert.ToInt32(reader["Estado"]);
                    agenda.IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]);
                    agenda.IntOidCPCapacitacion = Convert.ToInt32(reader["OidCPCapacitacion"] == DBNull.Value ? "0" : reader["OidCPCapacitacion"]);
                    agenda.IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"] == DBNull.Value ? "0": reader["OidGNListaArchivos"]);
                    agenda.IntTiempoFirma = Convert.ToInt32(reader["TiempoFirma"]);
                    agenda.StrLinkAnexo = reader["LinkAnexo"].ToString();
                    agenda.StrLinkReunion = reader["LinkReunion"].ToString();
                    agenda.StrLugar = reader["Lugar"].ToString();
                    agenda.StrModalidad = reader["Modalidad"].ToString();
                    agenda.StrResponsable = reader["Responsable"].ToString();
                    agenda.IntOidUsuarioResponsable = Convert.ToInt32(reader["OidUsuarioResponsable"]);
                    agenda.DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"] == DBNull.Value ? "01/01/1800" : reader["FechaFinal"].ToString());
                    agenda.IntOidCPExamen = Convert.ToInt32(reader["OidCPExamen"] == DBNull.Value ? "0" : reader["OidCPExamen"]);
                    agenda.StrFacilitador = reader["Facilitador"].ToString();
                    agendas.Add(agenda);
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
            return agendas;
        }
        public static List<dynamic> GEtAgendasByUsaurio(int idUsuario, bool firmado, bool asistido, string tema )
        {
            List<dynamic> datos = new List<dynamic>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select distinct M.OidCPMATRICULA, C.TEMA, A.Fecha, M.Asistido, M.Firmado, A.OidCPAgenda, A.LinkAnexo, A.OidGNListaArchivos, isnull(ES.Resultado,0) Examen
                                            from CPCAPACITACION C 
	                                            inner join CPAgenda A on A.OidCPCapacitacion = C.OidCPCAPACITACION
	                                            inner join CPMATRICULA M on M.OidCPAgenda = A.OidCPAgenda
	                                            left join CPEXAMEN E on E.OidCPEXAMEN = A.OidCPExamen
	                                            left join CPEXAMENSOL ES on ES.IDMATRICULA = M.OidCPMATRICULA and ES.Resultado >= E.NumApro
                                             where M.GNCodUsu = @GNCodUsu and M.Firmado =  @Firmado 
	                                            and M.Asistido = @Asistido and A.Estado = 3 and C.TEMA like '%' + @tema + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", idUsuario);
                command.Parameters.AddWithValue("Firmado", firmado);
                command.Parameters.AddWithValue("Asistido", asistido);
                command.Parameters.AddWithValue("tema", tema);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    datos.Add(new
                    {
                        StrTEMA = reader["TEMA"].ToString(),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Firmado = reader["Firmado"] == DBNull.Value ? false : Convert.ToBoolean(reader["Firmado"]),
                        Asistido = reader["Asistido"] == DBNull.Value ? false : Convert.ToBoolean(reader["Asistido"]),
                        IdAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
                        IdListaArchivos = reader["OidGNListaArchivos"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OidGNListaArchivos"]),
                        Examen = reader["Examen"].ToString(),
                        LinkAnexo = reader["LinkAnexo"].ToString()
                    }); ;
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
            return datos;
        }
        public static CPAgenda GetAgendaUlt(int IdCapacitacion)
        {
            CPAgenda agenda = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select top 1 * from CPAgenda where OidCPCapacitacion = @OidCPCapacitacion order by OidCPAgenda desc", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPCapacitacion", IdCapacitacion);

                reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    agenda = new CPAgenda
                    {
                        DtmFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"] == DBNull.Value ? "01-01-3000" : reader["FechaFirma"].ToString()),
                        DtmHoraFinal = Convert.ToDateTime(reader["HoraFinal"].ToString()),
                        DtmHoraInicial = Convert.ToDateTime(reader["HoraInicial"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
                        IntOidCPCapacitacion = Convert.ToInt32(reader["OidCPCapacitacion"]),
                        IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"] == DBNull.Value ? "0" : reader["OidGNListaArchivos"]),
                        IntTiempoFirma = Convert.ToInt32(reader["TiempoFirma"]),
                        StrLinkAnexo = reader["LinkAnexo"].ToString(),
                        StrLinkReunion = reader["LinkReunion"].ToString(),
                        StrLugar = reader["Lugar"].ToString(),
                        StrModalidad = reader["Modalidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        IntOidUsuarioResponsable = Convert.ToInt32(reader["OidUsuarioResponsable"]),
                        DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"] == DBNull.Value ? "01/01/1800" : reader["FechaFinal"].ToString()),
                        IntOidCPExamen = Convert.ToInt32(reader["FechaFinal"] == DBNull.Value ? "0" : reader["OidCPExamen"].ToString()),
                        StrFacilitador = reader["Facilitador"].ToString()
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

            return agenda;
        }
    }
}
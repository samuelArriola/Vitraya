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
    public class DAOGDDivulgacion
    {
        /// <summary>
        /// metodo que crea un formato para la divulgacion del documento en la base de datos
        /// </summary>
        /// <param name="divulgacion">intancia de tipo Divulgacion que se va a crear en base de datos</param>
        public static void SetGDDivulgacion(GDDivulgacion divulgacion)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[GDDivulgacion]
                                                   ([OidCPEJETEMATICO]
                                                   ,[OidGDDocumento]
                                                   ,[Cargos]
                                                   ,[TempFirma]
                                                   ,[Subtemas])
                                             VALUES
                                                   ( @OidCPEJETEMATICO
                                                   , @OidGDDocumento
                                                   , @Cargos
                                                   , @TempFirma
                                                   , @Subtemas) 
                                                    SELECT SCOPE_IDENTITY() id, NomDoc from GDDocumento where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidCPEJETEMATICO", divulgacion.IntOidCPEjeTematico);
                command.Parameters.AddWithValue("Cargos", divulgacion.StrCargos);
                command.Parameters.AddWithValue("Subtemas", divulgacion.StrSubtemas);
                command.Parameters.AddWithValue("OidGDDocumento", divulgacion.IntOidGDDocumento);
                command.Parameters.AddWithValue("TempFirma", divulgacion.IntTempFirma);
                reader = command.ExecuteReader();
                reader.Read();
                int OidInstancia = Convert.ToInt32(reader["id"]);
                string nomDoc = reader["NomDoc"].ToString();


                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Parametrizacion para la divulgacion del documento {nomDoc}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDivulgacion"
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
        /// Meto que retorna un listado de la divulgacines que se encuentran en la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<GDDivulgacion> GetGDDivulgaciones()
        {
            List<GDDivulgacion> divulgaciones = new List<GDDivulgacion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from  GDDivulgacion",conexion.OpenConnection());

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    divulgaciones.Add(new GDDivulgacion { 
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidGDDivulgacion = Convert.ToInt32(reader["OidGDDivulgacion"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        StrCargos = reader["Cargos"].ToString(),
                        StrSubtemas  = reader["Subtemas"].ToString(),
                        IntTempFirma = Convert.ToInt32(reader["TempFirma"])
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

            return divulgaciones;
        }

        /// <summary>
        /// metodo que consulta un divulgacion por el id que se encuentra en la base de datos
        /// </summary>
        /// <param name="idDivulgacion"></param>
        /// <returns></returns>
        public static GDDivulgacion GetDivulgacion(int idDivulgacion)
        {
            GDDivulgacion divulgacion = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDDivulgacion where OidGDDivulgacion = @OidGDDivulgacion", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDivulgacion", idDivulgacion);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    divulgacion = new GDDivulgacion
                    {
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidGDDivulgacion = Convert.ToInt32(reader["OidGDDivulgacion"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        StrCargos = reader["Cargos"].ToString(),
                        StrSubtemas = reader["Subtemas"].ToString(),
                        IntTempFirma = Convert.ToInt32(reader["TempFirma"])
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

            return divulgacion;
        }
        /// <summary>
        /// Metodo que retorna un Instacia de tipo Divulgacion consultandola en la base de datos por el id del Documento a divulgar
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        public static GDDivulgacion GetDivulgacionByIdDoc(int idDocumento)
        {
            GDDivulgacion divulgacion = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GDDivulgacion where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    divulgacion = new GDDivulgacion
                    {
                        IntOidCPEjeTematico = Convert.ToInt32(reader["OidCPEjeTematico"]),
                        IntOidGDDivulgacion = Convert.ToInt32(reader["OidGDDivulgacion"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        StrCargos = reader["Cargos"].ToString(),
                        StrSubtemas = reader["Subtemas"].ToString(),
                        IntTempFirma = Convert.ToInt32(reader["TempFirma"])

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
            return divulgacion;
        }

        public static List<GdDocProcedimiento> SetCapacitacion(string nombreC, string idSolicitud)
        {
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud);

            string nomResponsable = solicitud.StrNomUsu;
            Double idResponsable = solicitud.DblCodUsu;
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

            List<GdDocProcedimiento> infoG = new List<GdDocProcedimiento>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO CPCAPACITACION (CODIGO, FECHA, HORAINICIAL, HORAFINAL, OidCPEJETEMA, TEMA, ESTADO, OidGDDocumento, LUGAR, UNIDADFUNCIONAL, MODALIDAD, RESPONSABLE, GNCodUsu, LINK, TempFirma, FechaFirma) 
                                VALUES((select CONCAT('ACT-GEC-', ISNULL(max(Convert(int, REPLACE(CODIGO, 'ACT-GEC-', ''))) + 1, 1))  AS CODIGO from CPCAPACITACION), @fechaA, '07:00:00.0000000', '07:00:00.0000000', 2012,
                                @nombreC, 1, 1, '', '', '', @nomResponsable, @idResponsable, '', 0, @fechaA)
                                SELECT SCOPE_IDENTITY() AS 'ID'", conexion.OpenConnection());

                command.Parameters.AddWithValue("nombreC", nombreC);
                command.Parameters.AddWithValue("fechaA", fechaActual);
                command.Parameters.AddWithValue("nomResponsable", nomResponsable);
                command.Parameters.AddWithValue("idResponsable", idResponsable);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GdDocProcedimiento infoI = new GdDocProcedimiento
                    {
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["ID"].ToString())
                    };
                    infoG.Add(infoI);
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
            return infoG;
        }

        public static List<GdDocProcedimiento> SetAgenda(int idCapacitacion, string idSolicitud)
        {
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud);

            string nomResponsable = solicitud.StrNomUsu;
            Double idResponsable = solicitud.DblCodUsu;
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

            List<GdDocProcedimiento> infoG = new List<GdDocProcedimiento>();
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO CPAgenda (OidCPCapacitacion, Estado, HoraInicial, HoraFinal, Fecha, FechaFirma, FechaFinal, OidGNListaArchivos, TiempoFirma, Modalidad, Lugar, Responsable, OidCPExamen, OidUsuarioResponsable) 
                                            VALUES (@idCapacitacion, 1, '07:00:00.0000000', '07:00:00.0000000', '1900-01-01', '1900-01-01', '1900-01-01', 0, 30, 'Virtual Documental', 'Virtual', @nomResponsable, 0, @idResponsable) 
                                            SELECT SCOPE_IDENTITY() AS 'ID'", conexion.OpenConnection());

                command.Parameters.AddWithValue("idCapacitacion", idCapacitacion);
                command.Parameters.AddWithValue("nomResponsable", nomResponsable);
                command.Parameters.AddWithValue("idResponsable", idResponsable);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GdDocProcedimiento infoI = new GdDocProcedimiento
                    {
                        OidCPAgenda1 = Convert.ToInt32(reader["ID"].ToString())
                    };
                    infoG.Add(infoI);
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
            return infoG;
        }
    }
}
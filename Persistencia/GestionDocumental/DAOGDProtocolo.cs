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
    public class DAOGDProtocolo
    {
        public static void setProtocolo(GDProtocolo protocolo)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[GDProtocolo]
                                                   ([OidGDDocumento]
                                                   ,[Nombre]
                                                   ,[Alcance]
                                                   ,[Objetivo]
                                                   ,[Recursos]
                                                   ,[Definiciones]
                                                   ,[Recomendaciones]
                                                   ,[RefNorm]
                                                   ,[Responsable]
                                                   ,[Anexos] 
                                                   ,[RecHumanos] 
                                                   ,[RecEquiposBiomedicos] 
                                                   ,[RecInformaticos] 
                                                   ,[RecMedicamentos] 
                                                   ,[Actividad] 
                                                   ,[Indicadores] 
                                                   ,[OidGDProceso]) 
                                             VALUES
                                                   ( @OidGDDocumento
                                                   , @Nombre
                                                   , @Alcance
                                                   , @Objetivo
                                                   , @Recursos
                                                   , @Definiciones
                                                   , @Recomendaciones
                                                   , @RefNorm
                                                   , @Responsable
                                                   , @Anexos 
                                                   , @RecHumanos
                                                   , @RecEquiposBiomedicos 
                                                   , @RecInformaticos
                                                   , @RecMedicamentos
                                                   , @Actividad
                                                   , @Indicadores
                                                   , @OidGDProceso)
                                                     Select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", protocolo.IntOidGDDocumento);
                command.Parameters.AddWithValue("@Nombre", protocolo.StrNombre);
                command.Parameters.AddWithValue("@Alcance", protocolo.StrAlcance);
                command.Parameters.AddWithValue("@Objetivo", protocolo.StrObjetivo);
                command.Parameters.AddWithValue("@Recursos", protocolo.StrRecursos);
                command.Parameters.AddWithValue("@Definiciones", protocolo.StrDefiniciones);
                command.Parameters.AddWithValue("@Recomendaciones", protocolo.StrRecomendaciones);
                command.Parameters.AddWithValue("@RefNorm", protocolo.StrRefNorm);
                command.Parameters.AddWithValue("@Responsable", protocolo.StrResponsable);
                command.Parameters.AddWithValue("@Anexos",protocolo.StrAnexos);
                command.Parameters.AddWithValue("@RecHumanos", protocolo.StrRecHumanos);
                command.Parameters.AddWithValue("@RecEquiposBiomedicos", protocolo.StrRecEquiposBiomedicos);
                command.Parameters.AddWithValue("@RecInformaticos", protocolo.StrRecInformaticos);
                command.Parameters.AddWithValue("@RecMedicamentos", protocolo.StrRecMedicamentos);
                command.Parameters.AddWithValue("@OidGDProceso", protocolo.IntOidGDProceso);
                command.Parameters.AddWithValue("@Actividad", protocolo.StrActividad);
                command.Parameters.AddWithValue("@Indicadores", protocolo.StrIndicadores);

                int OidInstancia = Convert.ToInt32( command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se cra el documento tipo Protocolo con el nombre: {protocolo.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDProtocolo"
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

        public static List<GDProtocolo> GetGDProtocolos()
        {
            List<GDProtocolo> protocolos = new List<GDProtocolo>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDProtocolo", conexion.OpenConnection());
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDProtocolo protocolo = new GDProtocolo
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDProtocolo = Convert.ToInt32(reader["OidGDProtocolo"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrRecomendaciones = reader["Recomendaciones"].ToString(),
                        StrRecursos = reader["Recursos"].ToString(),
                        StrRefNorm = reader["RefNorm"].ToString(),
                        StrResponsable   = reader["Responsable"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrRecMedicamentos = reader["RecMedicamentos"].ToString(),
                        StrRecInformaticos = reader["RecInformaticos"].ToString(),
                        StrRecEquiposBiomedicos = reader["RecEquiposBiomedicos"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrRecHumanos   = reader ["RecHumanos"].ToString()
                    };
                    protocolos.Add(protocolo);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " Getprotocolos");
            }

            return protocolos;
        }

        public static GDProtocolo getProtocolo(int idProtocolo)
        {
            GDProtocolo protocolo = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDProtocolo where OidGDProtocolo = @OidGDProtocolo", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDProtocolo", idProtocolo);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    protocolo = new GDProtocolo
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDProtocolo = Convert.ToInt32(reader["OidGDProtocolo"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrRecomendaciones = reader["Recomendaciones"].ToString(),
                        StrRecursos = reader["Recursos"].ToString(),
                        StrRefNorm = reader["RefNorm"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrRecMedicamentos = reader["RecMedicamentos"].ToString(),
                        StrRecInformaticos = reader["RecInformaticos"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrRecEquiposBiomedicos = reader["RecEquiposBiomedicos"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString()

                    };
                   
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " Getprotocolos");
            }

            return protocolo;
        }

        /// <summary>
        /// Obtiene un Protocolo de Oid de la solicitud relacionada con el protocolo
        /// </summary>
        /// <param name="idSolicitud">id de la solicitu por la cual se desea ralizar la consulta</param>
        /// <returns></returns>
        public static GDProtocolo GetProtocoloByIdSol(int idSolicitud)
        {
            GDProtocolo protocolo = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDProtocolo P "+
                                         "   left join GDDocumento D on  D.OidGDDocumento = P.OidGDDocumento"+
                                         "   left join GDSolicitud S on D.OidGDSolicitud = S.OidGDSolicitud"+
                                         "   where S.OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDSolicitud", idSolicitud);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    protocolo = new GDProtocolo
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDProtocolo = Convert.ToInt32(reader["OidGDProtocolo"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrRecomendaciones = reader["Recomendaciones"].ToString(),
                        StrRecursos = reader["Recursos"].ToString(),
                        StrRefNorm = reader["RefNorm"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrRecMedicamentos = reader["RecMedicamentos"].ToString(),
                        StrRecInformaticos = reader["RecInformaticos"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrRecEquiposBiomedicos = reader["RecEquiposBiomedicos"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString()
                    };

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " Getprotocolos");
            }

            return protocolo;
        }

        public static GDProtocolo getProtocolobyidDoc(int idDocumento)
        {
            GDProtocolo protocolo = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDProtocolo where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDDocumento", idDocumento);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    protocolo = new GDProtocolo
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGDProtocolo = Convert.ToInt32(reader["OidGDProtocolo"]),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrRecomendaciones = reader["Recomendaciones"].ToString(),
                        StrRecursos = reader["Recursos"].ToString(),
                        StrRefNorm = reader["RefNorm"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrRecMedicamentos = reader["RecMedicamentos"].ToString(),
                        StrRecInformaticos = reader["RecInformaticos"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrRecEquiposBiomedicos = reader["RecEquiposBiomedicos"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString()

                    };

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " Getprotocolos");
            }

            return protocolo;
        }

        public static void UpdateProtocolo(GDProtocolo protocolo)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[GDProtocolo]"+
                                         "      SET[OidGDDocumento] = @OidGDDocumento"+
                                         "         ,[Nombre] = @Nombre"+
                                         "         ,[Alcance] = @Alcance"+
                                         "         ,[Objetivo] = @Objetivo"+
                                         "         ,[Recursos] = @Recursos"+
                                         "         ,[Definiciones] = @Definiciones"+
                                         "         ,[Recomendaciones] = @Recomendaciones"+
                                         "         ,[RefNorm] = @RefNorm"+
                                         "         ,[Responsable] = @Responsable"+
                                         "         ,[Anexos] = @Anexos"+
                                         "         ,[RecHumanos] = @RecHumanos" +
                                         "         ,[RecEquiposBiomedicos] = @RecEquiposBiomedicos" +
                                         "         ,[RecInformaticos] = @RecInformaticos" +
                                         "         ,[RecMedicamentos] = @RecMedicamentos" +
                                         "         ,[OidGDProceso] = @OidGDProceso" +
                                         "         ,[Actividad] = @Actividad" +
                                         "         ,[Indicadores] = @Indicadores" +
                                         "    WHERE OidGDProtocolo = @OidGDProtocolo", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", protocolo.IntOidGDDocumento);
                command.Parameters.AddWithValue("@Nombre", protocolo.StrNombre);
                command.Parameters.AddWithValue("@Alcance", protocolo.StrAlcance);
                command.Parameters.AddWithValue("@Objetivo", protocolo.StrObjetivo);
                command.Parameters.AddWithValue("@Recursos", protocolo.StrRecursos);
                command.Parameters.AddWithValue("@Definiciones", protocolo.StrDefiniciones);
                command.Parameters.AddWithValue("@Recomendaciones", protocolo.StrRecomendaciones);
                command.Parameters.AddWithValue("@RefNorm", protocolo.StrRefNorm);
                command.Parameters.AddWithValue("@Responsable", protocolo.StrResponsable);
                command.Parameters.AddWithValue("@Anexos", protocolo.StrAnexos);
                command.Parameters.AddWithValue("@OidGDProtocolo", protocolo.IntOidGDProtocolo);
                command.Parameters.AddWithValue("@OidGDProceso", protocolo.IntOidGDProceso);
                command.Parameters.AddWithValue("@RecHumanos", protocolo.StrRecHumanos);
                command.Parameters.AddWithValue("@RecEquiposBiomedicos", protocolo.StrRecEquiposBiomedicos);
                command.Parameters.AddWithValue("@RecInformaticos", protocolo.StrRecInformaticos);
                command.Parameters.AddWithValue("@RecMedicamentos", protocolo.StrRecMedicamentos);
                command.Parameters.AddWithValue("@Actividad", protocolo.StrActividad);
                command.Parameters.AddWithValue("@Indicadores", protocolo.StrIndicadores);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {  
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = protocolo.IntOidGDProtocolo,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza la infomacion del documento tipo portocolo: {protocolo.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDProtocolo"
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
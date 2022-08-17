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
    public class DAOGDManual
    {
        public static GDManual GetManual(int idManual) {
            GDManual manual = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GDManual where OidGDManual = @OidGDManual", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDManual", idManual);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    manual = new GDManual
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDManual = Convert.ToInt32(reader["OidGDManual"].ToString()),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrFormatos = reader["Formatos"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProcs = reader["Procs"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"])
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


            return manual;
        }

        public static void SetManual(GDManual manual)
        {
            SqlCommand command;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDManual]" +
                                         "          ([Introduccion],[Objetivos],[MarcoLegal]" +
                                         "          ,[Alcance],[Glosario],[Anexos]" +
                                         "          ,[Formatos],[Desarrollo],[OidGDDocumento], OidGDProceso, Nombre, Equipos" +
                                         "          ,Medicamentos, Procs, RecFin, RecInfo,TalentoHumano)" +
                                         "    VALUES" +
                                         "          (@Introduccion, @Objetivos, @MarcoLegal" +
                                         "          , @Alcance, @Glosario, @Anexos" +
                                         "          , @Formatos, @Desarrollo, @OidGDDocumento, @OidGDProceso, @Nombre, @Equipos" +
                                         "          ,@Medicamentos, @Procs, @RecFin, @RecInfo ,@TalentoHumano)" +
                                         " select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Introduccion", manual.StrIntroduccion);
                command.Parameters.AddWithValue("@Objetivos", manual.StrObjetivos);
                command.Parameters.AddWithValue("@MarcoLegal", manual.StrMarcoLegal);
                command.Parameters.AddWithValue("@Alcance", manual.StrAlcance);
                command.Parameters.AddWithValue("@Glosario", manual.StrGlosario);
                command.Parameters.AddWithValue("@Anexos", manual.StrAnexos);
                command.Parameters.AddWithValue("@Formatos", manual.StrFormatos);
                command.Parameters.AddWithValue("@Desarrollo", manual.StrDesarrollo);
                command.Parameters.AddWithValue("@OidGDDocumento", manual.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidGDProceso", manual.IntOidGDProceso);
                command.Parameters.AddWithValue("@Medicamentos", manual.StrMedicamentos);
                command.Parameters.AddWithValue("@Equipos", manual.StrEquipos);
                command.Parameters.AddWithValue("@Procs", manual.StrProcs);
                command.Parameters.AddWithValue("@RecFin", manual.StrRecFin);
                command.Parameters.AddWithValue("@RecInfo", manual.StrRecInfo);
                command.Parameters.AddWithValue("@TalentoHumano", manual.StrTalentoHumano);
                command.Parameters.AddWithValue("@Nombre", manual.StrNombre);

                int OidInstancia = Convert.ToInt32( command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"se crea el documento tipo manual: {manual.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDManual"
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

        public static void UpdateManual(GDManual manual)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GDManual]" +
                                         "      SET[Introduccion] = @Introduccion" +
                                         "         ,[Objetivos] = @Objetivos" +
                                         "         ,[MarcoLegal] = @MarcoLegal" +
                                         "         ,[Alcance] = @Alcance" +
                                         "         ,[Glosario] = @Glosario" +
                                         "         ,[Anexos] = @Anexos" +
                                         "         ,[Formatos] = @Formatos" +
                                         "         ,[Desarrollo] = @Desarrollo" +
                                         "         ,[OidGDDocumento] = @OidGDDocumento" +
                                         "         ,[OidGDProceso] = @OidGDProceso" +
                                         "         ,[Nombre] = @Nombre" +
                                         "         ,[Medicamentos] = @Medicamentos" +
                                         "         ,[Equipos] = @Equipos" +
                                         "         ,[Procs] = @Procs" +
                                         "         ,[RecFin] = @RecFin" +
                                         "         ,[RecInfo] = @RecInfo" +
                                         "         ,[TalentoHumano] = @TalentoHumano" +
                                         "    WHERE OidGDManual = @OidGDManual", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Introduccion", manual.StrIntroduccion);
                command.Parameters.AddWithValue("@Objetivos", manual.StrObjetivos);
                command.Parameters.AddWithValue("@MarcoLegal", manual.StrMarcoLegal);
                command.Parameters.AddWithValue("@Alcance", manual.StrAlcance);
                command.Parameters.AddWithValue("@Glosario", manual.StrGlosario);
                command.Parameters.AddWithValue("@Anexos", manual.StrAnexos);
                command.Parameters.AddWithValue("@Formatos", manual.StrFormatos);
                command.Parameters.AddWithValue("@Desarrollo", manual.StrDesarrollo);
                command.Parameters.AddWithValue("@OidGDDocumento", manual.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidGDProceso", manual.IntOidGDProceso);
                command.Parameters.AddWithValue("@Medicamentos", manual.StrMedicamentos);
                command.Parameters.AddWithValue("@Equipos", manual.StrEquipos);
                command.Parameters.AddWithValue("@Procs", manual.StrProcs);
                command.Parameters.AddWithValue("@RecFin", manual.StrRecFin);
                command.Parameters.AddWithValue("@RecInfo", manual.StrRecInfo);
                command.Parameters.AddWithValue("@TalentoHumano", manual.StrTalentoHumano);
                command.Parameters.AddWithValue("@Nombre", manual.StrNombre);
                command.Parameters.AddWithValue("@OidGDManual", manual.IntOidGDManual);
                

                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = manual.IntOidGDDocumento,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se Actualiza la informacion del documento {manual.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDManual"
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

        public static GDManual GetManualByIdDoc(int idDocumento)
        {
            GDDocumento documento = DAOGDDocumento.GetDocumento(idDocumento);

            GDManual manual = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GDManual where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDDocumento", idDocumento);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    manual = new GDManual
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDManual = Convert.ToInt32(reader["OidGDManual"].ToString()),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrFormatos = reader["Formatos"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProcs = reader["Procs"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"]),
                        StrVersion = Convert.ToInt32(documento.IntVersion)
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

            return manual;
        }

        public static List<GDManual> GetGDManuales()
        {
            List<GDManual> manuales = new List<GDManual>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GDManual", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GDManual manual = new GDManual
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDManual = Convert.ToInt32(reader["OidGDManual"].ToString()),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrFormatos = reader["Formatos"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProcs = reader["Procs"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"])
                    };
                    manuales.Add(manual);
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


            return manuales;
        }
    }
}
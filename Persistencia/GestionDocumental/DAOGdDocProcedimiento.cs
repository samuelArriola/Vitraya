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
    public class DAOGdDocProcedimiento
    {
        public static void SetDocProcedimiento(GdDocProcedimiento DocProcedimiento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("   INSERT INTO[dbo].[GdDocProcedimiento]([OidGDDocumento],[NomProceso],[NomProcedimiento],[FechaC],"+
                                         "   [Alcance],[Objetivo],[Responsable],[RecursosNecesarios],[Entradas],[Salidas],[OidGNListaArchivo],"+
                                         "   [ProEsperado],[EstCalidad],[RefNormativas],[Definiciones],[Anexos],[DocRelacionados],"+
                                         "   [OidRevisor],[NomRevisor],[FechaRevision],[OidAprobador],[NomAprobador],[FechaAprobacion],"+
                                         "   [Clientes],[Equipos],[Medicamentos],[Proveedores],[RecFin],[RecInfo],[TalentoHumano], [OidGDProceso], [Actividad], [FlujoGrama],[Indicadores],[DocumentosRelacionados]) " +
                                         "   VALUES(@OidGDDocumento, @NomProceso, @NomProcedimiento, @FechaC, @Alcance, @Objetivo,"+
                                         "   @Responsable, @RecursosNecesarios, @Entradas, @Salidas, @OidGNListaArchivo, @ProEsperado,"+
                                         "   @EstCalidad, @RefNormativas, @Definiciones, @Anexos, @DocRelacionados, @OidRevisor,"+
                                         "   @NomRevisor, @FechaRevision, @OidAprobador, @NomAprobador, @FechaAprobacion, @Clientes,"+
                                         "   @Equipos, @Medicamentos, @Proveedores, @RecFin, @RecInfo, @TalentoHumano, @OidGDProceso, @Actividad, @FlujoGrama, @Indicadores, @DocumentosRelacionados)" +
                                         "  select scope_identity()", conexion.OpenConnection());


                command.Parameters.AddWithValue("@OidGDDocumento", DocProcedimiento.IntOidGDDocumento);
                command.Parameters.AddWithValue("@NomProceso", DocProcedimiento.StrNomProceso);
                command.Parameters.AddWithValue("@NomProcedimiento", DocProcedimiento.StrNomProcedimiento);
                command.Parameters.AddWithValue("@FechaC", DocProcedimiento.DtFechaC);
                command.Parameters.AddWithValue("@Alcance", DocProcedimiento.StrAlcance);
                command.Parameters.AddWithValue("@Objetivo", DocProcedimiento.StrObjetivo);
                command.Parameters.AddWithValue("@Responsable", DocProcedimiento.StrResponsable);
                command.Parameters.AddWithValue("@RecursosNecesarios", DocProcedimiento.StrRecursosNecesarios);
                command.Parameters.AddWithValue("@Entradas", DocProcedimiento.StrEntradas);
                command.Parameters.AddWithValue("@Salidas", DocProcedimiento.StrSalidas);
                command.Parameters.AddWithValue("@OidGNListaArchivo", DocProcedimiento.IntOidGNListaArchivo);
                command.Parameters.AddWithValue("@ProEsperado", DocProcedimiento.StrProEsperado);
                command.Parameters.AddWithValue("@EstCalidad", DocProcedimiento.StrEstCalidad);
                command.Parameters.AddWithValue("@RefNormativas", DocProcedimiento.StrRefNormativas);
                command.Parameters.AddWithValue("@Definiciones", DocProcedimiento.StrDefiniciones);
                command.Parameters.AddWithValue("@Anexos", DocProcedimiento.StrAnexos);
                command.Parameters.AddWithValue("@DocRelacionados", DocProcedimiento.StrDocRelacionados);
                command.Parameters.AddWithValue("@OidRevisor", DocProcedimiento.IntOidRevisor);
                command.Parameters.AddWithValue("@NomRevisor", DocProcedimiento.StrNomRevisor);
                command.Parameters.AddWithValue("@FechaRevision", DocProcedimiento.DtFechaRevision);
                command.Parameters.AddWithValue("@OidAprobador", DocProcedimiento.IntOidAprobador);
                command.Parameters.AddWithValue("@NomAprobador", DocProcedimiento.StrNomAprobador);
                command.Parameters.AddWithValue("@FechaAprobacion", DocProcedimiento.DtFechaAprobacion);
                command.Parameters.AddWithValue("@Clientes", DocProcedimiento.StrClientes);
                command.Parameters.AddWithValue("@Equipos", DocProcedimiento.StrEquipos);
                command.Parameters.AddWithValue("@Medicamentos", DocProcedimiento.StrMedicamentos);
                command.Parameters.AddWithValue("@Proveedores", DocProcedimiento.StrProveedores);
                command.Parameters.AddWithValue("@RecFin", DocProcedimiento.StrRecFin);
                command.Parameters.AddWithValue("@RecInfo", DocProcedimiento.StrRecInfo);
                command.Parameters.AddWithValue("@TalentoHumano", DocProcedimiento.StrTalentoHumano);
                command.Parameters.AddWithValue("@OidGDProceso", DocProcedimiento.IntOidGDProceso);
                command.Parameters.AddWithValue("@FlujoGrama", DocProcedimiento.StrFlujoGrama);
                command.Parameters.AddWithValue("@Actividad", DocProcedimiento.StrActividad);
                command.Parameters.AddWithValue("@Indicadores", DocProcedimiento.StrIndicadores);
                command.Parameters.AddWithValue("@DocumentosRelacionados", DocProcedimiento.StrDocumentosRelacionados);



                int OidInstancia =  Convert.ToInt32(command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el documento tipo procedimiento: {DocProcedimiento.StrNomProcedimiento}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GdDocProcedimiento"
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

        public static void SetUpdate(GdDocProcedimiento DocProcedimiento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GdDocProcedimiento]"+
                                         "      SET[OidGDDocumento] = @OidGDDocumento"+
                                         "         ,[NomProceso] = @NomProceso"+
                                         "         ,[NomProcedimiento] = @NomProcedimiento"+
                                         "         ,[FechaC] = @FechaC"+
                                         "         ,[Alcance] = @Alcance"+
                                         "         ,[Objetivo] = @Objetivo"+
                                         "         ,[Responsable] = @Responsable"+
                                         "         ,[RecursosNecesarios] = @RecursosNecesarios"+
                                         "         ,[Entradas] = @Entradas"+
                                         "         ,[Salidas] = @Salidas"+
                                         "         ,[OidGNListaArchivo] = @OidGNListaArchivo"+
                                         "         ,[ProEsperado] = @ProEsperado"+
                                         "         ,[EstCalidad] = @EstCalidad"+
                                         "         ,[RefNormativas] = @RefNormativas"+
                                         "         ,[Definiciones] = @Definiciones"+
                                         "         ,[Anexos] = @Anexos"+
                                         "         ,[DocRelacionados] = @DocRelacionados"+
                                         "         ,[OidRevisor] = @OidRevisor"+
                                         "         ,[NomRevisor] = @NomRevisor"+
                                         "         ,[FechaRevision] = @FechaRevision"+
                                         "         ,[OidAprobador] = @OidAprobador"+
                                         "         ,[NomAprobador] = @NomAprobador"+
                                         "         ,[FechaAprobacion] = @FechaAprobacion"+
                                         "         ,[Clientes] = @Clientes"+
                                         "         ,[Equipos] = @Equipos"+
                                         "         ,[Medicamentos] = @Medicamentos"+
                                         "         ,[Proveedores] = @Proveedores"+
                                         "         ,[RecFin] = @RecFin"+
                                         "         ,[RecInfo] = @RecInfo"+
                                         "         ,[TalentoHumano] = @TalentoHumano"+
                                         "         ,[OidGDProceso] = @OidGDProceso" +
                                         "         ,[Actividad] = @Actividad" +
                                         "         ,[FlujoGrama] = @FlujoGrama" +
                                         "         ,[Indicadores] = @Indicadores" +
                                         "         ,[DocumentosRelacionados] = @DocumentosRelacionados" +
                                         "    WHERE OIdGdDocprocedimiento = @OIdGdDocprocedimiento", conexion.OpenConnection());


                command.Parameters.AddWithValue("@OidGDDocumento", DocProcedimiento.IntOidGDDocumento);
                command.Parameters.AddWithValue("@NomProceso", DocProcedimiento.StrNomProceso);
                command.Parameters.AddWithValue("@NomProcedimiento", DocProcedimiento.StrNomProcedimiento);
                command.Parameters.AddWithValue("@FechaC", DocProcedimiento.DtFechaC);
                command.Parameters.AddWithValue("@Alcance", DocProcedimiento.StrAlcance);
                command.Parameters.AddWithValue("@Objetivo", DocProcedimiento.StrObjetivo);
                command.Parameters.AddWithValue("@Responsable", DocProcedimiento.StrResponsable);
                command.Parameters.AddWithValue("@RecursosNecesarios", DocProcedimiento.StrRecursosNecesarios);
                command.Parameters.AddWithValue("@Entradas", DocProcedimiento.StrEntradas);
                command.Parameters.AddWithValue("@Salidas", DocProcedimiento.StrSalidas);
                command.Parameters.AddWithValue("@OidGNListaArchivo", DocProcedimiento.IntOidGNListaArchivo);
                command.Parameters.AddWithValue("@ProEsperado", DocProcedimiento.StrProEsperado);
                command.Parameters.AddWithValue("@EstCalidad", DocProcedimiento.StrEstCalidad);
                command.Parameters.AddWithValue("@RefNormativas", DocProcedimiento.StrRefNormativas);
                command.Parameters.AddWithValue("@Definiciones", DocProcedimiento.StrDefiniciones);
                command.Parameters.AddWithValue("@Anexos", DocProcedimiento.StrAnexos);
                command.Parameters.AddWithValue("@DocRelacionados", DocProcedimiento.StrDocRelacionados);
                command.Parameters.AddWithValue("@OidRevisor", DocProcedimiento.IntOidRevisor);
                command.Parameters.AddWithValue("@NomRevisor", DocProcedimiento.StrNomRevisor);
                command.Parameters.AddWithValue("@FechaRevision", DocProcedimiento.DtFechaRevision);
                command.Parameters.AddWithValue("@OidAprobador", DocProcedimiento.IntOidAprobador);
                command.Parameters.AddWithValue("@NomAprobador", DocProcedimiento.StrNomAprobador);
                command.Parameters.AddWithValue("@FechaAprobacion", DocProcedimiento.DtFechaAprobacion);
                command.Parameters.AddWithValue("@Clientes", DocProcedimiento.StrClientes);
                command.Parameters.AddWithValue("@Equipos", DocProcedimiento.StrEquipos);
                command.Parameters.AddWithValue("@Medicamentos", DocProcedimiento.StrMedicamentos);
                command.Parameters.AddWithValue("@Proveedores", DocProcedimiento.StrProveedores);
                command.Parameters.AddWithValue("@RecFin", DocProcedimiento.StrRecFin);
                command.Parameters.AddWithValue("@RecInfo", DocProcedimiento.StrRecInfo);
                command.Parameters.AddWithValue("@TalentoHumano", DocProcedimiento.StrTalentoHumano);
                command.Parameters.AddWithValue("@OIdGdDocprocedimiento", DocProcedimiento.IntOIdGdDocprocedimiento);
                command.Parameters.AddWithValue("@OidGDProceso", DocProcedimiento.IntOidGDProceso);
                command.Parameters.AddWithValue("@FlujoGrama", DocProcedimiento.StrFlujoGrama);
                command.Parameters.AddWithValue("@Actividad", DocProcedimiento.StrActividad);
                command.Parameters.AddWithValue("@Indicadores", DocProcedimiento.StrIndicadores);
                command.Parameters.AddWithValue("@DocumentosRelacionados", DocProcedimiento.StrDocumentosRelacionados);



                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = DocProcedimiento.IntOIdGdDocprocedimiento,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se Actualiza la información del documento {DocProcedimiento.StrNomProcedimiento}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GdDocProcedimiento"
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
        /// obtener un procedimiento por id.
        /// </summary>
        /// <param name="OIdGdDocprocedimiento"></param>
        /// <returns></returns>
        public static GdDocProcedimiento getProcedimientoID(string OIdGdDocprocedimiento)
        {
            GdDocProcedimiento Procedimiento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GdDocProcedimiento WHERE  OIdGdDocprocedimiento = @OIdGdDocprocedimiento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdGdDocprocedimiento", OIdGdDocprocedimiento);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Procedimiento = new GdDocProcedimiento
                    {
                        IntOIdGdDocprocedimiento = Convert.ToInt32(reader["OIdGdDocprocedimiento"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        IntOidAprobador = Convert.ToInt32(reader["OidAprobador"]),
                        StrNomProceso = reader["NomProceso"].ToString(),
                        StrNomProcedimiento = reader["NomProcedimiento"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrRecursosNecesarios = reader["RecursosNecesarios"].ToString(),
                        StrEntradas = reader["Entradas"].ToString(),
                        StrSalidas = reader["Salidas"].ToString(),
                        StrProEsperado = reader["ProEsperado"].ToString(),
                        StrEstCalidad = reader["EstCalidad"].ToString(),
                        StrRefNormativas = reader["RefNormativas"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrNomAprobador = reader["NomAprobador"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        DtFechaC = Convert.ToDateTime(reader["FechaC"]),
                        DtFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtFechaAprobacion = Convert.ToDateTime(reader["FechaAprobacion"]),
                        StrClientes = reader["Clientes"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProveedores = reader["Proveedores"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrDocumentosRelacionados = reader["DocumentosRelacionados"].ToString(),
                        StrFlujoGrama = reader["FlujoGrama"].ToString()
                        
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

            return Procedimiento;
        }
        /// <summary>
        /// metodo que retorna una lista de todos los procedimientos que estan en base de datos
        /// </summary>
        /// <returns></returns>
        public static List<GdDocProcedimiento> GetProcedimientos()
        {
            List<GdDocProcedimiento> procedimientos = new List<GdDocProcedimiento>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GdDocProcedimiento", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                     GdDocProcedimiento Procedimiento = new GdDocProcedimiento
                     {
                        IntOIdGdDocprocedimiento = Convert.ToInt32(reader["OIdGdDocprocedimiento"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        IntOidAprobador = Convert.ToInt32(reader["OidAprobador"]),
                        StrNomProceso = reader["NomProceso"].ToString(),
                        StrNomProcedimiento = reader["NomProcedimiento"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrRecursosNecesarios = reader["RecursosNecesarios"].ToString(),
                        StrEntradas = reader["Entradas"].ToString(),
                        StrSalidas = reader["Salidas"].ToString(),
                        StrProEsperado = reader["ProEsperado"].ToString(),
                        StrEstCalidad = reader["EstCalidad"].ToString(),
                        StrRefNormativas = reader["RefNormativas"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrNomAprobador = reader["NomAprobador"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        DtFechaC = Convert.ToDateTime(reader["FechaC"]),
                        DtFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtFechaAprobacion = Convert.ToDateTime(reader["FechaAprobacion"]),
                        StrClientes = reader["Clientes"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProveedores = reader["Proveedores"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                         IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrDocumentosRelacionados = reader["DocumentosRelacionados"].ToString(),
                         StrFlujoGrama = reader["FlujoGrama"].ToString()
                     };
                    procedimientos.Add(Procedimiento);
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

            return procedimientos;
        }


        /// <summary>
        /// obtener un procedimiento por el id del documento al que pertenece.
        /// </summary>
        /// <param name="OidGDDocumento"></param>
        /// <returns></returns>
        public static GdDocProcedimiento getProcedimientoDoc(string OidGDDocumento)
        {
            GdDocProcedimiento Procedimiento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GdDocProcedimiento WHERE  OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", OidGDDocumento);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Procedimiento = new GdDocProcedimiento
                    {
                        IntOIdGdDocprocedimiento = Convert.ToInt32(reader["OIdGdDocprocedimiento"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        IntOidAprobador = Convert.ToInt32(reader["OidAprobador"]),
                        StrNomProceso = reader["NomProceso"].ToString(),
                        StrNomProcedimiento = reader["NomProcedimiento"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrRecursosNecesarios = reader["RecursosNecesarios"].ToString(),
                        StrEntradas = reader["Entradas"].ToString(),
                        StrSalidas = reader["Salidas"].ToString(),
                        StrProEsperado = reader["ProEsperado"].ToString(),
                        StrEstCalidad = reader["EstCalidad"].ToString(),
                        StrRefNormativas = reader["RefNormativas"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrNomAprobador = reader["NomAprobador"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        DtFechaC = Convert.ToDateTime(reader["FechaC"]),
                        DtFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtFechaAprobacion = Convert.ToDateTime(reader["FechaAprobacion"]),
                        StrClientes = reader["Clientes"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProveedores = reader["Proveedores"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrDocumentosRelacionados = reader["DocumentosRelacionados"].ToString(),
                        StrFlujoGrama = reader["FlujoGrama"].ToString()
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

            return Procedimiento;
        }
        public static GdDocProcedimiento GetProcedimientoByIdSol(int idSolicitud)
        {
            GdDocProcedimiento Procedimiento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GdDocProcedimiento  P "+
                                         "   left join GDDocumento D on D.OidGDDocumento = P.OidGDDocumento"+
                                         "   left join GDSolicitud  S on D.OidGDSolicitud = S.OidGDSolicitud  WHERE  S.OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDSolicitud", idSolicitud);

              

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Procedimiento = new GdDocProcedimiento
                    {
                        IntOIdGdDocprocedimiento = Convert.ToInt32(reader["OIdGdDocprocedimiento"]),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"]),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"]),
                        IntOidAprobador = Convert.ToInt32(reader["OidAprobador"]),
                        StrNomProceso = reader["NomProceso"].ToString(),
                        StrNomProcedimiento = reader["NomProcedimiento"].ToString(),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrRecursosNecesarios = reader["RecursosNecesarios"].ToString(),
                        StrEntradas = reader["Entradas"].ToString(),
                        StrSalidas = reader["Salidas"].ToString(),
                        StrProEsperado = reader["ProEsperado"].ToString(),
                        StrEstCalidad = reader["EstCalidad"].ToString(),
                        StrRefNormativas = reader["RefNormativas"].ToString(),
                        StrDefiniciones = reader["Definiciones"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrNomAprobador = reader["NomAprobador"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        DtFechaC = Convert.ToDateTime(reader["FechaC"]),
                        DtFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtFechaAprobacion = Convert.ToDateTime(reader["FechaAprobacion"]),
                        StrClientes = reader["Clientes"].ToString(),
                        StrEquipos = reader["Equipos"].ToString(),
                        StrMedicamentos = reader["Medicamentos"].ToString(),
                        StrProveedores = reader["Proveedores"].ToString(),
                        StrRecFin = reader["RecFin"].ToString(),
                        StrRecInfo = reader["RecInfo"].ToString(),
                        StrTalentoHumano = reader["TalentoHumano"].ToString(),
                        StrIndicadores = reader["Indicadores"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        IntOidGDProceso = reader["OidGDProceso"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidGDProceso"]),
                        StrDocumentosRelacionados = reader["DocumentosRelacionados"].ToString(),
                        StrFlujoGrama = reader["FlujoGrama"].ToString()

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

            return Procedimiento;
        } 
        public static void DeleteProcedimientoByIdDoc(int idDocumento)
        {
            SqlCommand command;
            Conexion conexion= new Conexion();

            try
            {
                command = new SqlCommand("Delete from GdDocProcedimiento where  ", conexion.OpenConnection());
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using Entidades.Generales;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.Procesos;

namespace Persistencia.procesos
{
    public class DAOProceso
    {
        /// <summary>
        /// guardar un proceso a bases de datos
        /// </summary>
        /// <param name="proceso"></param>
        public static void setProceso(PCProceso proceso)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[PCProceso]" +
                                                            " ([NomPro] "+
                                                            " ,[Estado] "+
                                                            " ,[Tipo] " +
                                                            " ,[ProcesoPadre] " +
                                                            " ,[Prefijo] " +
                                                            " ,[Objetivo] " +
                                                            " ,[Alcance] " +
                                                            " ,[LideresProceso] " +
                                                            " ,[RecFinancieros] " +
                                                            " ,[RecHumanos] " +
                                                            " ,[Normas] " +
                                                            " ,[Riesgos] " +
                                                            " ,[OidGNListaArchivo] " +
                                                            " ,[DocRelacionados]" +
                                                            " ,[Version] " +
                                                            " ,[Fecha]" +
                                                            " ,[RecursosTec]" +
                                                            " ,[RecursosInfo]" +
                                                            " ,[RecursosFis]" +
                                                            " ,[OidGDDocumento]" +
                                                            " ,[OidGDSolicitud]" +
                                                            " ,[GnDcDep]" +
                                                            " ,[RecursosMed]" +
                                                            " ,[FlujoGrama]) " +
                                                            " VALUES " +
                                                            " (@NomPro"+
                                                            " ,@Estado" +
                                                            " ,@Tipo" +
                                                            " ,@ProcesoPadre" +
                                                            " ,@Prefijo" +
                                                            " ,@Objetivo" +
                                                            " ,@Alcance" +
                                                            " ,@LideresProceso" +
                                                            " ,@RecFinancieros" +
                                                            " ,@RecHumanos" +
                                                            " ,@Normas" +
                                                            " ,@Riesgos" +
                                                            " ,@OidGNListaArchivo" +
                                                            " ,@DocRelacionados" +
                                                            " ,@Version" +
                                                            " ,@Fecha" +
                                                            " ,@RecursosTec" +
                                                            " ,@RecursosInfo" +
                                                            " ,@RecursosFis" +
                                                            " ,@OidGDDocumento" +
                                                            " ,@OidGDSolicitud" +
                                                            " ,@GnDcDep" +
                                                            " ,@RecursosMed" +
                                                            " ,@FlujoGrama) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@NomPro", proceso.StrNomPro);
                command.Parameters.AddWithValue("@Estado", proceso.StrEstado);
                command.Parameters.AddWithValue("@Tipo", proceso.StrTipo);
                command.Parameters.AddWithValue("@ProcesoPadre", proceso.StrProcesoPadre);
                command.Parameters.AddWithValue("@Prefijo", proceso.StrPrefijo);
                command.Parameters.AddWithValue("@Objetivo", proceso.StrObjetivo);
                command.Parameters.AddWithValue("@Alcance", proceso.StrAlcance);
                command.Parameters.AddWithValue("@LideresProceso", proceso.StrLideresProceso);
                command.Parameters.AddWithValue("@RecFinancieros", proceso.StrRecFinancieros);
                command.Parameters.AddWithValue("@RecHumanos", proceso.StrRecHumanos);
                command.Parameters.AddWithValue("@Normas", proceso.StrNormas);
                command.Parameters.AddWithValue("@Riesgos", proceso.StrRiesgos);
                command.Parameters.AddWithValue("@DocRelacionados", proceso.StrDocRelacionados);
                command.Parameters.AddWithValue("@OidGNListaArchivo", proceso.IntOidGNListaArchivo);
                command.Parameters.AddWithValue("@Version", proceso.IntVersion);
                command.Parameters.AddWithValue("@Fecha", proceso.DtFecha);
                command.Parameters.AddWithValue("@RecursosTec", proceso.StrRecursosTec);
                command.Parameters.AddWithValue("@RecursosInfo", proceso.StrRecursosInfo);
                command.Parameters.AddWithValue("@RecursosFis", proceso.StrRecursosFis);
                command.Parameters.AddWithValue("@OidGDSolicitud", proceso.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@OidGDDocumento", proceso.IntOidGDDocumento);
                command.Parameters.AddWithValue("@GnDcDep", proceso.IntGnDcDep);
                command.Parameters.AddWithValue("@FlujoGrama", proceso.StrFlujoGrama);
                command.Parameters.AddWithValue("@RecursosMed", proceso.StrRecursosMed);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "PCProceso"
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

        public static PCProceso GetProcesoUlt()
        {
            PCProceso proceso = null;

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select top(1) * from PCProceso order by OIdProceso desc", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    proceso = new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()
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

            return proceso;
        }

        /// <summary>
        /// Actualizar un proceso
        /// </summary>
        /// <param name="proceso"></param>
        public static void setUpProceso(PCProceso proceso)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[PCProceso]"+
                                         "      SET[NomPro] = @NomPro"+
                                         "         ,[Estado] = @Estado"+
                                         "         ,[Tipo] = @Tipo"+
                                         "         ,[ProcesoPadre] = @ProcesoPadre"+
                                         "         ,[Prefijo] = @Prefijo"+
                                         "         ,[Objetivo] = @Objetivo"+
                                         "         ,[Alcance] = @Alcance"+
                                         "         ,[LideresProceso] = @LideresProceso"+
                                         "         ,[RecFinancieros] = @RecFinancieros"+
                                         "         ,[RecHumanos] = @RecHumanos"+
                                         "         ,[Normas] = @Normas"+
                                         "         ,[Riesgos] = @Riesgos"+
                                         "         ,[OidGNListaArchivo] = @OidGNListaArchivo"+
                                         "         ,[Version] = @Version"+
                                         "         ,[Fecha] = @Fecha"+
                                         "         ,[RecursosFis] = @RecursosFis" +
                                         "         ,[RecursosInfo] = @RecursosInfo" +
                                         "         ,[RecursosTec] = @RecursosTec" +
                                         "         ,[OidGDDocumento] = @OidGDDocumento" +
                                         "         ,[OidGDSolicitud] = @OidGDSolicitud" +
                                         "         ,[GnDcDep] = @GnDcDep" +
                                         "         ,[FlujoGrama] = @FlujoGrama" +
                                         "         ,[RecursosMed] = @RecursosMed" +
                                         "    WHERE OIdProceso = @OIdProceso", conexion.OpenConnection());




                command.Parameters.AddWithValue("@NomPro", proceso.StrNomPro);
                command.Parameters.AddWithValue("@Estado", proceso.StrEstado);
                command.Parameters.AddWithValue("@Tipo", proceso.StrTipo);
                command.Parameters.AddWithValue("@ProcesoPadre", proceso.StrProcesoPadre);
                command.Parameters.AddWithValue("@Prefijo", proceso.StrPrefijo);
                command.Parameters.AddWithValue("@Objetivo", proceso.StrObjetivo);
                command.Parameters.AddWithValue("@Alcance", proceso.StrAlcance);
                command.Parameters.AddWithValue("@LideresProceso", proceso.StrLideresProceso);
                command.Parameters.AddWithValue("@RecFinancieros", proceso.StrRecFinancieros);
                command.Parameters.AddWithValue("@RecHumanos", proceso.StrRecHumanos);
                command.Parameters.AddWithValue("@Normas", proceso.StrNormas);
                command.Parameters.AddWithValue("@Riesgos", proceso.StrRiesgos);
                command.Parameters.AddWithValue("@OidGNListaArchivo", proceso.IntOidGNListaArchivo);
                command.Parameters.AddWithValue("@Version", proceso.IntVersion);
                command.Parameters.AddWithValue("@Fecha", proceso.DtFecha);
                command.Parameters.AddWithValue("@RecursosFis", proceso.StrRecursosFis);
                command.Parameters.AddWithValue("@RecursosInfo", proceso.StrRecursosInfo);
                command.Parameters.AddWithValue("@RecursosTec", proceso.StrRecursosTec);
                command.Parameters.AddWithValue("@OidGDSolicitud", proceso.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@OidGDDocumento", proceso.IntOidGDDocumento);
                command.Parameters.AddWithValue("@GnDcDep", proceso.IntGnDcDep);
                command.Parameters.AddWithValue("@FlujoGrama", proceso.StrFlujoGrama);
                command.Parameters.AddWithValue("@OIdProceso", proceso.IntOIdProceso);
                command.Parameters.AddWithValue("@RecursosMed", proceso.StrRecursosMed);


                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = proceso.IntOIdProceso,
                    strAccion = "Modificar",
                    strDetalle = $"",
                    strEntidad = "PCProceso"
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
        /// listar todos los procesos
        /// </summary>
        /// <returns></returns>
        public static List<PCProceso> listar()
        {
            List<PCProceso> procesos = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCProceso where Estado = '1'", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                procesos = new List<PCProceso>();
                while (reader.Read())
                {
                    PCProceso proceso = new PCProceso {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()

                    };
                    procesos.Add(proceso);
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
            return procesos;
        }

        /// <summary>
        /// Listar procesos filtrando por el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static List<PCProceso> listarFiltro(string nombre)
        {
            List<PCProceso> procesos = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCProceso where NomPro like '%'+@NomPro+'%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", nombre);

                reader = command.ExecuteReader();

                procesos = new List<PCProceso>();
                while (reader.Read())
                {
                    PCProceso proceso = new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()
                    };
                    procesos.Add(proceso);
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
            return procesos;
        }

        /// <summary>
        /// buscar un procesos por el nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static PCProceso BuscarProceso(string nombre)
        {
            PCProceso proceso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCProceso where NomPro = @NomPro", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", nombre);

                reader = command.ExecuteReader();

                
                if(reader.Read())
                {
                    proceso = new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()

                    }; 
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return proceso;
        }

        public static PCProceso BuscarProceso(int OIdProceso)
        {
            PCProceso proceso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCProceso where OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdProceso", OIdProceso);

                reader = command.ExecuteReader();


                if (reader.Read())
                {
                    proceso = new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()

                    };
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return proceso;
        }

        /// <summary>
        /// Eliminar un proceso tomando como parametro su id!!
        /// </summary>
        /// <param name="OIdProceso"></param>
        /// <returns></returns>
        public static bool DeleteProceso(int OIdProceso)
        {

            bool isDeleted = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM PCProceso WHERE OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdProceso", OIdProceso);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isDeleted;
        }


        public static PCProceso GetProcesoByOidSol(int idSolicitud)
        {
            PCProceso proceso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * From PCProceso where OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDSolicitud", idSolicitud);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    proceso = new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString()
                    };
                }

            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return proceso;
        }

        public static List<PCProceso> GetPCProcesos(string nombreProceso, string prefijo, string tipo, string nomProcesoPadre, string estado)
        {
            List<PCProceso> procesos = new List<PCProceso>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT P.*, "+
                                          "  iif((select NomPro from PCProceso where OIdProceso = P.ProcesoPadre) is null, '', (select NomPro from PCProceso where OIdProceso = P.ProcesoPadre)) nomProPadre"+
                                          "   FROM PCProceso P"+
                                          "  WHERE P.NomPro LIKE '%' + @NomPro + '%' AND P.Prefijo LIKE '%' + @Prefijo + '%'" +
                                          "  AND P.Tipo LIKE '%' + @Tipo + '%'AND P.Estado LIKE  '%' + @Estado + '%'" +
                                          "  AND iif((select NomPro from PCProceso where OIdProceso = P.ProcesoPadre) is null, '',(select NomPro from PCProceso where OIdProceso = P.ProcesoPadre))  LIKE '%' + @nomProPadre + '%' ", conexion.OpenConnection());

                command.Parameters.AddWithValue("NomPro", nombreProceso);
                command.Parameters.AddWithValue("Prefijo", prefijo);
                command.Parameters.AddWithValue("Tipo", tipo);
                command.Parameters.AddWithValue("Estado", estado);
                command.Parameters.AddWithValue("nomProPadre", nomProcesoPadre);


                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    procesos.Add(new PCProceso {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString(),
                        StrNomProPadre = reader["nomProPadre"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return procesos;
        }
        public static List<PCProceso> GetProcesosActivos()
        {
            List<PCProceso> procesos = new List<PCProceso>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM PCProceso where Estado = '1'", conexion.OpenConnection());

                


                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    procesos.Add(new PCProceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        IntOidGNListaArchivo = Convert.ToInt32(reader["OidGNListaArchivo"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrDocRelacionados = reader["DocRelacionados"].ToString(),
                        StrNormas = reader["Normas"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrPrefijo = reader["Prefijo"].ToString(),
                        StrProcesoPadre = reader["ProcesoPadre"].ToString(),
                        StrRecFinancieros = reader["RecFinancieros"].ToString(),
                        StrRecHumanos = reader["RecHumanos"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        StrRiesgos = reader["Riesgos"].ToString(),
                        StrLideresProceso = reader["LideresProceso"].ToString(),
                        DtFecha = Convert.ToDateTime(reader["Fecha"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrRecursosFis = reader["RecursosFis"].ToString(),
                        StrRecursosInfo = reader["RecursosInfo"].ToString(),
                        StrRecursosTec = reader["RecursosTec"].ToString(),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"]),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"]),
                        StrFlujoGrama = reader["FlujoGrama"].ToString(),
                        StrRecursosMed = reader["RecursosMed"].ToString(),
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
            return procesos;
        }
    }
}
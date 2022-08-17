using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia;
using Persistencia.Generales;

namespace Persistencia.proceedings
{
    public class DAOPAPlanAccion : Conexion
    {


        public static List<PAPlanAccion> GetPlanesByUsu(int idUSuario)
        {
            List<PAPlanAccion> planes = new List<PAPlanAccion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                            "    , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomCreador"+
                                          " FROM [dbo].[PAPlanAccion] as c where GNCodUsu = @GNCodUsu and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", idUSuario);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PAPlanAccion planAccion = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        StrNombreUsuarioCreador = reader["NomCreador"].ToString()
                    };
                    planes.Add(planAccion);
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

            return planes;
        }


        /// <summary>
        ///     metodo que consulta y devuelve una instancia de PAPlanAccion por su id
        /// </summary>
        /// <param name="id"> id del ARActasCaompromisos</param>
        /// <returns></returns>
        public static PAPlanAccion Get(int id)
        {
            PAPlanAccion compromiso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * " +
                                            "    , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                          " FROM [dbo].[PAPlanAccion] as c where OidPAPlanAccion = @OidPAPlanAccion and isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidPAPlanAccion", id);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString()

                    };
                }
            }
            catch (Exception ex )
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return compromiso;
        }

        public static List<PAPlanAccion> Listar(int id)
        {
            List<PAPlanAccion> compromisos = new List<PAPlanAccion>();


            SqlCommand consult;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                consult = new SqlCommand("SELECT  * " +
                                            "    , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "    ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                          " FROM [dbo].[PAPlanAccion] as c WHERE c.OidInstancia =  @id and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();
                while (reader.Read())
                {
                    PAPlanAccion compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
                    };
                    compromisos.Add(compromiso);
                }
                conexion.CloseConnection();
                return compromisos;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                conexion.CloseConnection();
                return null;
            }
        }

        public static bool set(PAPlanAccion data)
        {
            SqlCommand consult;
            Conexion conexion = new Conexion();
            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[PAPlanAccion]" +
                                            "   ([OidARActasC]" +
                                            "   ,[GNCodUsu]" +
                                            "    ,[Actividad]" +
                                            "    ,[FecIniActa]" +
                                            "    ,[FecFinalActa]" +
                                            "    ,[EstAct]" +
                                            "    ,[codUsuSegui]" +
                                            "    ,[NomEst]" +
                                            "    ,[Soporte]" +
                                            "    ,[CodUsuApr]" +
                                            "    ,[OidInstancia]" +
                                            "    ,[Cuanto]" +
                                            "    ,[PorQue]" +
                                            "    ,[Donde]" +
                                            "    ,[Como]" +
                                            "    ,[Fuente]" +
                                            "    ,[Descriptcion]" +
                                            "    ,[Contexto]" +
                                            "    ,[Origen]" +
                                            "    ,[Eliminado]" +
                                            "    ,[Proceso])" +
                                        " VALUES" +
                                            "    (@OidARActasC," +
                                            "    @GNCodUsu," +
                                            "    @Actividad, " +
                                            "    @FecIniActa," +
                                            "    @FecFinalActa," +
                                            "    @EstAct," +
                                            "    @codUsuSegui," +
                                            "    @NomEst," +
                                            "    @Soporte," +
                                            "    @CodUsuApr," +
                                            "    @OidInstancia," +
                                            "    @Cuanto," +
                                            "    @PorQue," +
                                            "    @Donde," +
                                            "    @Como," +
                                            "    @Fuente," +
                                            "    @Descriptcion," +
                                            "    @Contexto," +
                                            "    @Origen," +
                                            "    0," +
                                            "    @Proceso) select scope_identity()", conexion.OpenConnection());

                consult.Parameters.AddWithValue("OidARActasC", data.IntOidARActasC);
                consult.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Actividad", data.StrActividad);
                consult.Parameters.AddWithValue("@FecIniActa", data.DtmFecIniActa);
                consult.Parameters.AddWithValue("@FecFinalActa", data.DtmFecFinalActa);
                consult.Parameters.AddWithValue("@EstAct", data.IntEstAct);
                consult.Parameters.AddWithValue("@codUsuSegui", data.IntcodUsuSegui);
                consult.Parameters.AddWithValue("@NomEst", data.StrNomEst);
                consult.Parameters.AddWithValue("@Soporte", data.StrSoporte);
                consult.Parameters.AddWithValue("@CodUsuApr", data.IntCodUsuApr);
                consult.Parameters.AddWithValue("@OidInstancia", data.IntOidInstancia);
                consult.Parameters.AddWithValue("@Cuanto", data.StrCuanto);
                consult.Parameters.AddWithValue("@PorQue", data.StrPorQue);
                consult.Parameters.AddWithValue("@Donde", data.StrDonde);
                consult.Parameters.AddWithValue("@Como", data.StrComo);
                consult.Parameters.AddWithValue("@Proceso", data.StrProceso);
                consult.Parameters.AddWithValue("@Fuente", data.StrFuente);
                consult.Parameters.AddWithValue("@Descriptcion", data.StrDescriptcion);
                consult.Parameters.AddWithValue("@Contexto", data.IntContexto);
                consult.Parameters.AddWithValue("@Origen", data.StrOrigen);
                int OidInstancia = Convert.ToInt32(consult.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el plan de acción asignado al usuario {data.IntGNCodUsu} ",
                    strEntidad = "PAPlanAccion"
                });

                conexion.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                conexion.CloseConnection();
                return false;
            }
        }

        public static List<PAPlanAccion> listar(int idActa, int idTema)
        {
            List<PAPlanAccion> compromisos = new List<PAPlanAccion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * " +
                                            "   , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[dbo].[PAPlanAccion] as c WHERE c.OidARActasC =  @OidARActasC and OidInstancia = @OidInstancia and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidARActasC", idActa);
                command.Parameters.AddWithValue("@OidInstancia", idTema);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PAPlanAccion compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,

                    };
                    compromisos.Add(compromiso);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }



            return compromisos;
        }

        public static List<PAPlanAccion> GetCompromisosActa(int idActa, int idUsuario)
        {
            List<PAPlanAccion> compromisos = new List<PAPlanAccion>();

            SqlCommand commad;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("SELECT  * " +
                                            "   , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[dbo].[PAPlanAccion] as c where c.GNCodUsu = @idUsuario and c.OidARActasC = @idActa and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                commad.Parameters.AddWithValue("@idActa", idActa);
                commad.Parameters.AddWithValue("@idUsuario", idUsuario);

                reader = commad.ExecuteReader();

                if (reader.Read())
                {
                    PAPlanAccion compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,

                    };

                    compromisos.Add(compromiso);
                }
            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                conexion.CloseConnection();
            }
            return compromisos;
        }

        public static List<PAPlanAccion> GetCompromisosActa(int idActa)
        {
            List<PAPlanAccion> compromisos = new List<PAPlanAccion>();

            SqlCommand commad;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("SELECT  * " +
                                            "   , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                            "   ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[dbo].[PAPlanAccion] as c where c.OidARActasC = @idActa and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                commad.Parameters.AddWithValue("@idActa", idActa);

                reader = commad.ExecuteReader();

                while (reader.Read())
                {
                    PAPlanAccion compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,

                    };

                    compromisos.Add(compromiso);
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                conexion.CloseConnection();
            }
            return compromisos;
        }

        public static bool delete(int id)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update [dbo].[PAPlanAccion] set Eliminado = 1  WHERE OidPAPlanAccion = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = id,
                    strAccion = "Eliminar",
                    strDetalle = "",
                    strEntidad = "PAPlanAccion"
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
        public static void UpdateCompromiso(PAPlanAccion compromiso)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[PAPlanAccion]"+
                                         "      SET[OidARActasC] = @OidARActasC"+
                                         "         ,[GNCodUsu] = @GNCodUsu"+
                                         "         ,[Actividad] = @Actividad"+
                                         "         ,[FecIniActa] = @FecIniActa"+
                                         "         ,[FecFinalActa] = @FecFinalActa"+
                                         "         ,[EstAct] = @EstAct"+
                                         "         ,[codUsuSegui] = @codUsuSegui"+
                                         "         ,[NomEst] = @NomEst"+
                                         "         ,[CodUsuApr] = @CodUsuApr"+
                                         "         ,[Soporte] = @Soporte"+
                                         "         ,[OidInstancia] = @OidInstancia"+
                                         "         ,[Cuanto] = @Cuanto"+
                                         "         ,[PorQue] = @PorQue"+
                                         "         ,[Donde] = @Donde"+
                                         "         ,[Como] = @Como"+
                                         "         ,[Descriptcion] = @Descriptcion" +
                                         "         ,[Fuente] = @Fuente" +
                                         "         ,[Contexto] = @Contexto" +
                                         "         ,[Proceso] = @Proceso" +
                                         "    WHERE OidPAPlanAccion = @OidPAPlanAccion", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidARActasC", compromiso.IntOidARActasC);
                command.Parameters.AddWithValue("@GNCodUsu", compromiso.IntGNCodUsu);
                command.Parameters.AddWithValue("@Actividad", compromiso.StrActividad);
                command.Parameters.AddWithValue("@FecIniActa", compromiso.DtmFecIniActa);
                command.Parameters.AddWithValue("@FecFinalActa", compromiso.DtmFecFinalActa);
                command.Parameters.AddWithValue("@EstAct", compromiso.IntEstAct);
                command.Parameters.AddWithValue("@codUsuSegui", compromiso.IntcodUsuSegui);
                command.Parameters.AddWithValue("@NomEst", compromiso.StrNomEst);
                command.Parameters.AddWithValue("@CodUsuApr", compromiso.IntCodUsuApr);
                command.Parameters.AddWithValue("@Soporte", compromiso.StrSoporte);
                command.Parameters.AddWithValue("@OidInstancia", compromiso.IntOidInstancia);
                command.Parameters.AddWithValue("@Cuanto", compromiso.StrCuanto);
                command.Parameters.AddWithValue("@PorQue", compromiso.StrPorQue);
                command.Parameters.AddWithValue("@Donde", compromiso.StrDonde);
                command.Parameters.AddWithValue("@Como", compromiso.StrComo);
                command.Parameters.AddWithValue("@Proceso", compromiso.StrProceso);
                command.Parameters.AddWithValue("@Fuente", compromiso.StrFuente);
                command.Parameters.AddWithValue("@Descriptcion", compromiso.StrDescriptcion);
                command.Parameters.AddWithValue("@Contexto", compromiso.IntContexto);
                command.Parameters.AddWithValue("@OidPAPlanAccion", compromiso.IntOidPAPlanAccion);



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

        public static List<PAPlanAccion> GetPlanesByIdSig(int idUsuSeg, string nombre, string lugar, string fecha )
        {
            List<PAPlanAccion> compromisos = new List<PAPlanAccion>();

            SqlCommand commad;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("Select P.* "+
                                        "    , (select GNNomUsu from Usuario where GNCodUsu = P.CodUsuApr) as NomApr"+
                                        "    ,(select GNNomUsu from Usuario where GNCodUsu = P.codUsuSegui) as NomSeg"+
                                        "    ,(select GNNomUsu from Usuario where GNCodUsu = P.GNCodUsu) as NomResp"+
                                        "    from PAPlanAccion P"+
                                        "    left join PAUsuario U on U.OidPAPlanAccion = P.OidPAPlanAccion "+
                                        "    where U.OidGNUsuario = @codUsuSegui and(U.Rol = '3' or U.Rol = '2')" +
                                        "        and(select GNNomUsu from Usuario where GNCodUsu = P.GNCodUsu) like '%' + @GNCodUsu + '%'"+
                                        "        and P.Donde like '%' + @Donde + '%' and P.FecFinalActa < @FecFinalActa and isnull(Eliminado, 0) = 0" +
                                        "    group by p.[OidPAPlanAccion]"+
                                        "    ,[OidARActasC],[GNCodUsu],[Actividad],[FecIniActa],[FecFinalActa]"+
                                        "    ,[EstAct],[codUsuSegui],[NomEst],[CodUsuApr],[Soporte],[OidInstancia]"+
                                        "    ,[Como],[Donde],[PorQue],[Cuanto],[Proceso],[Fuente]"+
                                        "    ,[Descriptcion],[Contexto],[Origen], P.Eliminado", conexion.OpenConnection());

                commad.Parameters.AddWithValue("GNCodUsu", nombre);
                commad.Parameters.AddWithValue("Donde", lugar);
                commad.Parameters.AddWithValue("FecFinalActa", fecha);
                commad.Parameters.AddWithValue("codUsuSegui", idUsuSeg);

                reader = commad.ExecuteReader();

                while (reader.Read())
                {
                    PAPlanAccion compromiso = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
                    };

                    compromisos.Add(compromiso);
                    
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
            return compromisos;
        }

        public static List<PAPlanAccion> GetPlanAccionByContexto(int idInstancia, int contexto)
        {
            List<PAPlanAccion> planes = new List<PAPlanAccion>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * " +
                                        "        , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                        "        ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                        "        ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                        "        FROM[dbo].[PAPlanAccion] as c where OidInstancia = @OidInstancia and Contexto = @Contexto and EstAct <> 4 and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidInstancia", idInstancia);
                command.Parameters.AddWithValue("Contexto", contexto);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PAPlanAccion plan = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
                    };
                    planes.Add(plan);
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

            return planes;
        }

        public static List<PAPlanAccion> GetPlanesAccion(string nomUsuResp, string nomUsuSeg, string contexto, DateTime fecha1, DateTime fecha2, string proceso, string estado)
        {
            List<PAPlanAccion> planes = new List<PAPlanAccion>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * "+
                                            " , (select GNNomUsu from Usuario where GNCodUsu = P.CodUsuApr) as NomApr"+
                                            " ,(select GNNomUsu from Usuario where GNCodUsu = P.codUsuSegui) as NomSeg"+
                                            " ,(select GNNomUsu from Usuario where GNCodUsu = P.GNCodUsu) as NomResp"+
                                            " FROM PAPlanAccion P"+
                                            " WHERE (select GNNomUsu from Usuario where GNCodUsu = P.GNCodUsu) LIKE  '%' + @GNCodUsu + '%' AND"+
                                            " (select GNNomUsu from Usuario where GNCodUsu = P.codUsuSegui) LIKE  '%' + @codUsuSegui + '%'"+
                                            " AND P.Origen like '%' + @Contexto + '%' AND P.FecFinalActa > @fecha1 AND P.FecFinalActa < @fecha2" +
                                            " AND P.Proceso like '%' + @Proceso + '%' and P.EstAct  like '%' + @EstAct + '%' and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", nomUsuResp);
                command.Parameters.AddWithValue("codUsuSegui", nomUsuSeg);
                command.Parameters.AddWithValue("Contexto", contexto);
                command.Parameters.AddWithValue("fecha1", fecha1);
                command.Parameters.AddWithValue("fecha2", fecha2);
                command.Parameters.AddWithValue("Proceso", proceso);
                command.Parameters.AddWithValue("EstAct", estado);

                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    planes.Add(new PAPlanAccion {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        //Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
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
            return planes;
        }

        public static PAPlanAccion GetPlanAccionUlt()
        {

            PAPlanAccion planAcion = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT top (1) * " +
                                            " , (select GNNomUsu from Usuario where GNCodUsu = P.CodUsuApr) as NomApr" +
                                            " ,(select GNNomUsu from Usuario where GNCodUsu = P.codUsuSegui) as NomSeg" +
                                            " ,(select GNNomUsu from Usuario where GNCodUsu = P.GNCodUsu) as NomResp" +
                                            " FROM PAPlanAccion P where isnull(Eliminado, 0) = 0" +
                                            " order by OidPAPlanAccion desc ", conexion.OpenConnection());


                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    planAcion = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
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
            return planAcion;
        }

        public static List<dynamic> GetEstadisticas(string idResponsable, string proceso, DateTime fecha1, DateTime fecha2)
        {
            List<dynamic> estadisticas = new List<dynamic>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from                                                                                                     "+
                                         "       (                                                                                                          "+
	                                     "           Select P.Proceso, iif(COUNT(*) is null,0,COUNT(*)) Suma, P.EstAct Estado   from PAPlanAccion P         "+
                                         "           where P.GNCodUsu like '%'+  @GNCodUsu +'%' and P.FecFinalActa >=  @fecha1 and p.FecFinalActa <=  @fecha2   " +
	                                     "           and P.Proceso like '%'+  @Proceso +'%' and isnull(Eliminado, 0) = 0                                    "+
	                                     "            group by P.EstAct, P.Proceso                                                                          "+
                                         "       ) as estados                                                                                               "+
                                         "       Pivot(	                                                                                                    "+
	                                     "           sum(Suma)                                                                                              "+
	                                     "           for Estado in ([1],[2],[3],[4])                                                                        "+
                                         "       ) as PVT", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", idResponsable);
                command.Parameters.AddWithValue("fecha1", fecha1);
                command.Parameters.AddWithValue("fecha2", fecha2);
                command.Parameters.AddWithValue("Proceso", proceso);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dynamic estadistica = new System.Dynamic.ExpandoObject();
                    estadistica.Proceso = reader["Proceso"].ToString();
                    estadistica.EstAsignado = reader["1"].ToString() == "" ? 0 : Convert.ToInt32(reader["1"]);
                    estadistica.EstProceso = reader["2"].ToString() == "" ? 0 : Convert.ToInt32(reader["2"]);
                    estadistica.EstEvaluacion = reader["3"].ToString() == "" ? 0 : Convert.ToInt32(reader["3"]);
                    estadistica.EstTerminado = reader["4"].ToString() == "" ? 0 : Convert.ToInt32(reader["4"]);

                    estadisticas.Add(estadistica);
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

            return estadisticas;
        }

        public static dynamic GetEstadisticas(string idResponsable)
        {
            dynamic estadisticas = null;

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select * from                                                                                                      
                                                (                                                                                                           
                                                   select isnull(count(*),0) Suma, P.EstAct Estado   from PAPlanAccion P          
                                                    where P.GNCodUsu like '%' + @GNCodUsu + '%' and isnull(Eliminado, 0) = 0                                                             
                                                     group by P.EstAct, P.Proceso                                                                           
                                                ) as estados                                                                                                
                                                Pivot(	                                                                                                     
                                                    sum(Suma)                                                                                               
                                                    for Estado in ([1],[2],[3],[4])                                                                         
                                                ) as PVT", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", idResponsable);
               

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estadisticas = new { 
                        EstAsignado = reader["1"].ToString() == "" ? 0 : Convert.ToInt32(reader["1"]),
                        EstProceso = reader["2"].ToString() == "" ? 0 : Convert.ToInt32(reader["2"]),
                        EstEvaluacion = reader["3"].ToString() == "" ? 0 : Convert.ToInt32(reader["3"]),
                        EstTerminado = reader["4"].ToString() == "" ? 0 : Convert.ToInt32(reader["4"]),
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

            return estadisticas;
        }

        public PAPlanAccion GetPlanAccion(int idPlanAccion)
        {
            PAPlanAccion planAccion = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM PAPlanAccion OidPAPlanAccion = @OidPAPlanAccion and isnull(Eliminado, 0 ) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidPAPlanAccion", idPlanAccion);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    planAccion = new PAPlanAccion
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidPAPlanAccion = Convert.ToInt32(reader["OidPAPlanAccion"].ToString()),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString()),
                        StrDescriptcion = reader["Descriptcion"].ToString(),
                        StrFuente = reader["Fuente"].ToString(),
                        StrOrigen = reader["Origen"].ToString(),
                        Usuarios = DAOPAUsuario.GetPAUsuarioByIdPlan(Convert.ToInt32(reader["OidPAPlanAccion"])).Data,
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
            return planAccion;
        }
    }
}
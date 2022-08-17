using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.proceedings;
using Persistencia;

namespace Persistencia.proceedings
{
    public class DAOARActasCompromisos : Conexion
    {
        /// <summary>
        /// metodo que consulata y devuelve una instancia de ARActasCompromisos por su id
        /// </summary>
        /// <param name="id"> id del ARActasCaompromisos</param>
        /// <returns></returns>
        public static ARActasCompromisos Get(int id)
        {
            ARActasCompromisos compromiso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * " +
                                              " , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                              " ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                              " ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                          " FROM [dbo].[ARActasCompromisos] as c where OidARActasCompromisos = @OidARActasCompromisos", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidARActasCompromisos", id);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    compromiso = new ARActasCompromisos
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasCompromisos = Convert.ToInt32(reader["OidARActasCompromisos"].ToString()),
                        IntOidARActasTemas = Convert.ToInt32(reader["OidARActasTemas"].ToString()),
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

        public static List<ARActasCompromisos> Listar(int id)
        {
            List<ARActasCompromisos> compromisos = new List<ARActasCompromisos>();


            SqlCommand consult;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                consult = new SqlCommand("SELECT  * " +
                                              " , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                              " ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                              " ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                          " FROM [dbo].[ARActasCompromisos] as c WHERE c.OidARActasTemas =  @id", conexion.OpenConnection());
                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();
                while (reader.Read())
                {
                    ARActasCompromisos compromiso = new ARActasCompromisos
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasCompromisos = Convert.ToInt32(reader["OidARActasCompromisos"].ToString()),
                        IntOidARActasTemas = Convert.ToInt32(reader["OidARActasTemas"].ToString()),
                        StrActividad = reader["Actividad"].ToString(),
                        StrComo = reader["Como"].ToString(),
                        StrCuanto = reader["Cuanto"].ToString(),
                        StrPorQue = reader["PorQue"].ToString(),
                        StrDonde = reader["Donde"].ToString(),
                        StrNombreUsuarioAprueba = reader["NomApr"].ToString(),
                        StrNombreUsuarioResponsable = reader["NomResp"].ToString(),
                        StrNombreUsuarioSeguimiento = reader["NomSeg"].ToString(),
                        StrNomEst = reader["NomEst"].ToString(),
                        StrSoporte   = reader["Soporte"].ToString(),
                        StrProceso = reader["Proceso"].ToString(),
                    };
                    compromisos.Add(compromiso);
                }
                conexion.CloseConnection();
                return compromisos;
            }
            catch (Exception ex)
            {
                conexion.CloseConnection();
                return null;
            }
        }

        public static bool set(ARActasCompromisos data)
        {
            SqlCommand consult;
            Conexion conexion = new Conexion();
            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[ARActasCompromisos]" +
                                              " ([OidARActasC]" +
                                               ",[GNCodUsu]" +
                                               ",[Actividad]" +
                                               ",[FecIniActa]" +
                                               ",[FecFinalActa]" +
                                               ",[EstAct]" +
                                               ",[codUsuSegui]" +
                                               ",[NomEst]" +
                                               ",[Soporte]" +
                                               ",[CodUsuApr]" +
                                               ",[OidARActasTemas]" +
                                               ",[Cuanto]" +
                                               ",[PorQue]" +
                                               ",[Donde]" +
                                               ",[Como]" +
                                               ",[Proceso])" +
                                        " VALUES" +
                                               "(@OidARActasC," +
                                               "@GNCodUsu," +
                                               "@Actividad, " +
                                               "@FecIniActa," +
                                               "@FecFinalActa," +
                                               "@EstAct," +
                                               "@codUsuSegui," +
                                               "@NomEst," +
                                               "@Soporte," +
                                               "@CodUsuApr," +
                                               "@OidARActasTemas," +
                                               "@Cuanto," +
                                               "@PorQue," +
                                               "@Donde," +
                                               "@Como," +
                                               "@Proceso)", conexion.OpenConnection());

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
                consult.Parameters.AddWithValue("@OidARActasTemas", data.IntOidARActasTemas);
                consult.Parameters.AddWithValue("@Cuanto", data.StrCuanto);
                consult.Parameters.AddWithValue("@PorQue", data.StrPorQue);
                consult.Parameters.AddWithValue("@Donde", data.StrDonde);
                consult.Parameters.AddWithValue("@Como", data.StrComo);
                consult.Parameters.AddWithValue("@Proceso", data.StrProceso);
                consult.ExecuteNonQuery();


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

        public static List<ARActasCompromisos> listar(int idActa, int idTema)
        {
            List<ARActasCompromisos> compromisos = new List<ARActasCompromisos>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  * " +
                                             " , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[Vitraya].[dbo].[ARActasCompromisos] as c WHERE c.OidARActasC =  @OidARActasC and OidARActasTemas = @OidARActasTemas", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidARActasC", idActa);
                command.Parameters.AddWithValue("@OidARActasTemas", idTema);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ARActasCompromisos compromiso = new ARActasCompromisos
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasCompromisos = Convert.ToInt32(reader["OidARActasCompromisos"].ToString()),
                        IntOidARActasTemas = Convert.ToInt32(reader["OidARActasTemas"].ToString()),
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

        public static List<ARActasCompromisos> GetCompromisosActa(int idActa, int idUsuario)
        {
            List<ARActasCompromisos> compromisos = new List<ARActasCompromisos>();

            SqlCommand commad;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("SELECT  * " +
                                             " , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[Vitraya].[dbo].[ARActasCompromisos] as c where c.GNCodUsu = @idUsuario and c.OidARActasC = @idActa", conexion.OpenConnection());
                commad.Parameters.AddWithValue("@idActa", idActa);
                commad.Parameters.AddWithValue("@idUsuario", idUsuario);

                reader = commad.ExecuteReader();

                if (reader.Read())
                {
                    ARActasCompromisos compromiso = new ARActasCompromisos
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasCompromisos = Convert.ToInt32(reader["OidARActasCompromisos"].ToString()),
                        IntOidARActasTemas = Convert.ToInt32(reader["OidARActasTemas"].ToString()),
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

        public static List<ARActasCompromisos> GetCompromisosActa(int idActa)
        {
            List<ARActasCompromisos> compromisos = new List<ARActasCompromisos>();

            SqlCommand commad;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                commad = new SqlCommand("SELECT  * " +
                                             " , (select GNNomUsu from Usuario where GNCodUsu = c.CodUsuApr) as NomApr" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.codUsuSegui) as NomSeg" +
                                             " ,(select GNNomUsu from Usuario where GNCodUsu = c.GNCodUsu) as NomResp" +
                                         " FROM[Vitraya].[dbo].[ARActasCompromisos] as c where c.OidARActasC = @idActa", conexion.OpenConnection());
                commad.Parameters.AddWithValue("@idActa", idActa);

                reader = commad.ExecuteReader();

                if (reader.Read())
                {
                    ARActasCompromisos compromiso = new ARActasCompromisos
                    {
                        DtmFecFinalActa = Convert.ToDateTime(reader["FecIniActa"].ToString()),
                        DtmFecIniActa = Convert.ToDateTime(reader["FecFinalActa"].ToString()),
                        IntCodUsuApr = Convert.ToInt32(reader["CodUsuApr"].ToString()),
                        IntcodUsuSegui = Convert.ToInt32(reader["codUsuSegui"].ToString()),
                        IntEstAct = Convert.ToInt32(reader["EstAct"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasCompromisos = Convert.ToInt32(reader["OidARActasCompromisos"].ToString()),
                        IntOidARActasTemas = Convert.ToInt32(reader["OidARActasTemas"].ToString()),
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

        public static void delete(int id)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM [dbo].[ARActasCompromisos]  WHERE OidARActasCompromisos = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }
        }
        public static void UpdateCompromiso(ARActasCompromisos compromiso)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[ARActasCompromisos]"+
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
                                         "         ,[OidARActasTemas] = @OidARActasTemas"+
                                         "         ,[Cuanto] = @Cuanto"+
                                         "         ,[PorQue] = @PorQue"+
                                         "         ,[Donde] = @Donde"+
                                         "         ,[Como] = @Como"+
                                         "         ,[Proceso] = @Proceso"+
                                         "    WHERE OidARActasCompromisos = @OidARActasCompromisos", conexion.OpenConnection());

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
                command.Parameters.AddWithValue("@OidARActasTemas", compromiso.IntOidARActasTemas);
                command.Parameters.AddWithValue("@Cuanto", compromiso.StrCuanto);
                command.Parameters.AddWithValue("@PorQue", compromiso.StrPorQue);
                command.Parameters.AddWithValue("@Donde", compromiso.StrDonde);
                command.Parameters.AddWithValue("@Como", compromiso.StrComo);
                command.Parameters.AddWithValue("@Proceso", compromiso.StrProceso);
                command.Parameters.AddWithValue("@OidARActasCompromisos", compromiso.IntOidARActasCompromisos);



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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Deployment.Internal;
using Entidades.PlanAccion;
using Persistencia.Generales;
using Entidades.Generales;

namespace Persistencia.proceedings
{
    public class DAOARActasC : Conexion, DAOInterfaz<ARActasC>
    {
        public ARActasC Get(int id)
        {
            throw new NotImplementedException();
        }

        public static List<ARActasC> ListarActasProgramadas()
        {
            List<ARActasC> actas = new List<ARActasC>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  [OidARActasC],a.[OidAReunionC],a.[GNCodUsu],[Estado]" +
                                        " ,[Codigo],[LugarReun],[Objetivo], a.[Sigla],[FecInicio],[FecFinal]" +
                                        " ,[FechEditable],[FecSistema], r.NomReunion, Link FROM[dbo].[ARActasC] as a" +
                                        " left join AReunionC as r on r.OidAReunionC = a.OidAReunionC where Estado = 0 and isnull(Eliminado, 0) = 0 order by A.FecInicio", conexion.OpenConnection());
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                     ARActasC acta = new ARActasC
                    {
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrNombre = reader["NomReunion"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        StrLink = reader["Link"].ToString()
                    };
                    actas.Add(acta);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return actas;

        }

        public static List<ARActasC> ListarActasProgramadas(int idCoordinador)
        {
            List<ARActasC> actas = new List<ARActasC>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" DECLARE @fecha DateTime;
                                            SET @fecha =
                                              (SELECT top(1) a.FecInicio
                                               FROM[dbo].[ARActasC] AS a
                                               LEFT JOIN AReunionD AS M ON M.OidAReunionC = A.OidAReunionC
                                               WHERE Estado = 0
                                                 AND isnull(A.Eliminado, 0) = 0
                                                 AND M.GNCodUsu = @GNCodUsu
                                                 AND(M.TpNomUsu = 'Coordinador'
                                                      OR M.TpNomUsu = 'Secretario')
                                                 AND FecInicio > SYSDATETIME()
                                               ORDER BY FecInicio)
                                            SELECT a.*,r.NomReunion
                                            FROM[dbo].[ARActasC] AS a
                                            LEFT JOIN AReunionC AS r ON r.OidAReunionC = a.OidAReunionC
                                            LEFT JOIN AReunionD AS M ON M.OidAReunionC = A.OidAReunionC
                                            WHERE Estado = 0
                                              AND M.GNCodUsu = @GNCodUsu
                                              AND(M.TpNomUsu = 'Coordinador'
                                                   OR M.TpNomUsu = 'Secretario')
                                              AND FecInicio <= (iif(@fecha IS NULL, SYSDATETIME(), @fecha)) and isnull(A.Eliminado, 0) = 0
                                            ORDER BY FecInicio", conexion.OpenConnection());
                command.Parameters.AddWithValue("GNCodUsu", idCoordinador);

                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrNombre = reader["NomReunion"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        StrLink = reader["Link"].ToString()
                    };
                    actas.Add(acta);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return actas;

        }

        public bool set(ARActasC data)
        {
            SqlCommand consult;
           
            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[ARActasC]" +
                                           "([OidAReunionC]" +
                                           ",[LugarReun]" +
                                           ",[FecInicio]" +
                                           ",[FecFinal]" +
                                           ",[Objetivo]" +
                                           ",[FecSistema]" +
                                           ",[FechEditable]" +
                                           ",[GNCodUsu]" +
                                           ",[Estado]" +
                                           ",[Sigla]" +
                                           ",[Codigo]" +
                                           ",[Nombre]" +
                                           ",[Eliminado]" +
                                           ",[Link])" +
                                     "VALUES" +
                                           "(@OidAReunionC" +
                                           ",@LugarReun" +
                                           ",@FecInicio" +
                                           ",@FecFinal" +
                                           ",@Objetivo" +
                                           ",@FecSistema" +
                                           ",@FechEditable" +
                                           ",@GNCodUsu" +
                                           ",@Estado" +
                                           ",@Sigla" +
                                           ",@Codigo" +
                                           ",@nombre" +
                                           ",0" +
                                           ",@Link) select scope_identity()", OpenConnection());
                //consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@OidAReunionC",data.IntOidAReunionC);
                consult.Parameters.AddWithValue("@LugarReun",data.StrLugarReun);
                consult.Parameters.AddWithValue("@FecInicio",data.DtmFecInicio);
                consult.Parameters.AddWithValue("@FecFinal",data.DtmFecFinal);
                consult.Parameters.AddWithValue("@Objetivo",data.StrObjetivo);
                consult.Parameters.AddWithValue("@FecSistema",data.DtmFecSistema);
                consult.Parameters.AddWithValue("@FechEditable",data.DtmFechEditable);
                consult.Parameters.AddWithValue("@GNCodUsu",data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Estado",data.IntEstado);
                consult.Parameters.AddWithValue("@Sigla", data.StrSigla);
                consult.Parameters.AddWithValue("@Codigo", data.IntCodigo);
                consult.Parameters.AddWithValue("@Link", data.StrLink);
                consult.Parameters.AddWithValue("@NOmbre", data.StrNombre);

                int OidInstancia = Convert.ToInt32(consult.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = "",
                    strEntidad = "ARActasC"
                });

                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return false;
            }

        }

        public int set2(ARActasC data)
        {
            SqlCommand consult;

            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[ARActasC]" +
                                           "([OidAReunionC]" +
                                           ",[LugarReun]" +
                                           ",[FecInicio]" +
                                           ",[FecFinal]" +
                                           ",[Objetivo]" +
                                           ",[FecSistema]" +
                                           ",[FechEditable]" +
                                           ",[GNCodUsu]" +
                                           ",[Estado]" +
                                           ",[Sigla]" +
                                           ",[Codigo]" +
                                           ",[Nombre]" +
                                           ",[Eliminado]" +
                                           ",[Link])" +
                                     "VALUES" +
                                           "(@OidAReunionC" +
                                           ",@LugarReun" +
                                           ",@FecInicio" +
                                           ",@FecFinal" +
                                           ",@Objetivo" +
                                           ",@FecSistema" +
                                           ",@FechEditable" +
                                           ",@GNCodUsu" +
                                           ",@Estado" +
                                           ",@Sigla" +
                                           ",@Codigo" +
                                           ",@nombre" +
                                           ",0" +
                                           ",@Link) select scope_identity()", OpenConnection());
                //consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@OidAReunionC", data.IntOidAReunionC);
                consult.Parameters.AddWithValue("@LugarReun", data.StrLugarReun);
                consult.Parameters.AddWithValue("@FecInicio", data.DtmFecInicio);
                consult.Parameters.AddWithValue("@FecFinal", data.DtmFecFinal);
                consult.Parameters.AddWithValue("@Objetivo", data.StrObjetivo);
                consult.Parameters.AddWithValue("@FecSistema", data.DtmFecSistema);
                consult.Parameters.AddWithValue("@FechEditable", data.DtmFechEditable);
                consult.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Estado", data.IntEstado);
                consult.Parameters.AddWithValue("@Sigla", data.StrSigla);
                consult.Parameters.AddWithValue("@Codigo", data.IntCodigo);
                consult.Parameters.AddWithValue("@Link", data.StrLink);
                consult.Parameters.AddWithValue("@NOmbre", data.StrNombre);

                int OidInstancia = Convert.ToInt32(consult.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = "",
                    strEntidad = "ARActasC"
                });

                CloseConnection();
                return OidInstancia;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return 0;
            }

        }

        public bool set(List<ARActasC> datas)
        {
            throw new NotImplementedException();
        }

        public ARActasC setUltimo(int id)
        {
            SqlCommand consult;
            SqlDataReader reader;
            ARActasC aRActasC = null;

            try
            {
                consult = new SqlCommand("select top(1) * from ARActasC where OidAReunionC = @id order by OidARActasC desc", OpenConnection());
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader =  consult.ExecuteReader();

                if (reader.Read())
                {
                    aRActasC = new ARActasC {
                        IntOidARActas = int.Parse(reader["OidARActasC"].ToString()),
                        IntOidAReunionC = int.Parse(reader["OidAReunionC"].ToString()),
                        IntGNCodUsu = int.Parse(reader["GNCodUsu"].ToString()),
                        IntEstado = int.Parse(reader["Estado"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        DtmFecInicio = DateTime.Parse(reader["FecInicio"].ToString()),
                        DtmFecFinal = DateTime.Parse(reader["FecFinal"].ToString()),
                        DtmFechEditable = DateTime.Parse(reader["FechEditable"].ToString()),
                        DtmFecSistema = DateTime.Parse(reader["FecSistema"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        StrSigla = reader["Sigla"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        
                    };
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                CloseConnection();
            }
            return aRActasC;
        }



        public bool update(int id)
        {
            throw new NotImplementedException();
        }

        public static void update(ARActasC data)
        {
            SqlCommand consult;
            Conexion conexion = new Conexion();
            try
            {
                consult = new SqlCommand("UPDATE [dbo].[ARActasC]"+
                                         "      SET[OidAReunionC] = @OidAReunionC"+
                                         "         ,[GNCodUsu] = @GNCodUsu"+
                                         "         ,[Estado] = @Estado"+
                                         "         ,[Codigo] = @Codigo"+
                                         "         ,[LugarReun] = @LugarReun"+
                                         "         ,[Objetivo] = @Objetivo"+
                                         "         ,[Sigla] = @Sigla"+
                                         "         ,[FecInicio] = @FecInicio"+
                                         "         ,[FecFinal] = @FecFinal"+
                                         "         ,[FechEditable] = @FechEditable"+
                                         "         ,[FecSistema] = @FecSistema"+
                                         "         ,[Link] = @Link"+
                                         "         ,[Nombre] = @Nombre" +
                                         "    WHERE OidARActasC = @OidARActasC", conexion.OpenConnection());

                consult.Parameters.AddWithValue("@OidAReunionC", data.IntOidAReunionC);
                consult.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Estado", data.IntEstado);
                consult.Parameters.AddWithValue("@Codigo", data.IntCodigo);
                consult.Parameters.AddWithValue("@LugarReun", data.StrLugarReun);
                consult.Parameters.AddWithValue("@Objetivo", data.StrObjetivo);
                consult.Parameters.AddWithValue("@Sigla", data.StrSigla);
                consult.Parameters.AddWithValue("@FecInicio", data.DtmFecInicio);
                consult.Parameters.AddWithValue("@FecFinal", data.DtmFecFinal);
                consult.Parameters.AddWithValue("@FechEditable", data.DtmFechEditable);
                consult.Parameters.AddWithValue("@FecSistema", data.DtmFecSistema);
                consult.Parameters.AddWithValue("@Link", data.StrLink);
                consult.Parameters.AddWithValue("@OidARActasC", data.IntOidARActas);
                consult.Parameters.AddWithValue("@Nombre", data.StrNombre);


                consult.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = data.IntOidARActas,
                    strAccion = "Modificar",
                    strDetalle = "",
                    strEntidad = "ARActasC"
                });

                conexion.CloseConnection();
            }
            catch (Exception ex)
            {
                conexion.CloseConnection();
            }
        }


        public static void UpdateUsuarioCreador(int idUsuario, int OidARActasC)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update ARActasC set UsuarioCreador = @UsuarioCreador  WHERE OidARActasC = @OidARActasC", conexion.OpenConnection());
                command.Parameters.AddWithValue("UsuarioCreador", idUsuario);
                command.Parameters.AddWithValue("OidARActasC", OidARActasC);


            
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
        /// Metodo que retorna un listado de actas que han sido convocadas por el usuario
        /// </summary>
        /// <param name="id"> id del usuario  logueado</param>
        /// <returns></returns>
        public static List<ARActasC> GetActasConvocadas(int id)
        {
            List<ARActasC> actasConvocadas = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select *   from ARActasC as a left join AReunionC as r "+
                                           " on r.OidAReunionC = a.OidAReunionC where Estado = "+
                                           " 1 and a.OidARActasC in (select a.OidARActasC from ARActasDM as a where a.GNCodUsu = @GNCodUsu and (a.TipoUsuario = 'Coordinador' OR a.TipoUsuario = 'Secretario' ))", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                actasConvocadas = new List<ARActasC>();
                while (reader.Read())
                {
                    ARActasC acta = new ARActasC {
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrLink = reader["Link"].ToString()
                    };
                    actasConvocadas.Add(acta);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return actasConvocadas;
        }

        public static ARActasC get(int id)
        {
            ARActasC acta = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  a.*, r.NomReunion FROM [dbo].[ARActasC] as a" +
                                        " left join AReunionC as r on r.OidAReunionC = a.OidAReunionC"+
                                        " where a.OidARActasC = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();


                if (reader.Read())
                {
                    acta = new ARActasC {
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        StrLink = reader["Link"].ToString(),
                        IntUsuarioCreador = Convert.ToInt32(reader["UsuarioCreador"].ToString() == "" ? 0 : reader["UsuarioCreador"])
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

            return acta;
        }

        public static List<ARActasC> GetARActasTeminadas(int id)
        {
            List<ARActasC> actasTerminadas = new List<ARActasC>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select a.[OidARActasC]"+
                                              ", a.[OidAReunionC]"+
                                              ", d.[GNCodUsu]"+
                                              ",[Estado]"+
                                              ",[Codigo]"+
                                              ",[LugarReun]"+
                                              ",[Objetivo]"+
                                              ", a.[Sigla]"+
                                              ",[FecInicio]"+
                                              ",[FecFinal]"+
                                              ",[FechEditable]"+
                                              ",[FecSistema]"+
                                              ", a.[Link]" +
                                              ", a.Nombre"+
                                              ", r.NomReunion"+
                                       " FROM[dbo].[ARActasC]"+
                                       " as a left join AReunionC as r on r.OidAReunionC = a.OidAReunionC"+
                                       " left join ARActasDM as d on  d.OidARActasC = a.OidARActasC"+
                                       " where a.OidARActasC in (select OidARActasC from ARActasDM as ad"+
                                       "  where ad.EstUsuario = 1  and ad.GNCodUsu = @GNCodUsu)" +
                                       " and Estado = 2 and d.GNCodUsu = @GNCodUsu ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                actasTerminadas = new List<ARActasC>();
                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrLink = reader["Link"].ToString()
                    };
                    actasTerminadas.Add(acta);
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

            return actasTerminadas;
        }

        public static List<ARActasC> getActasConvocadasGenerales(int id)
        {
            List<ARActasC> actas = new List<ARActasC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select a.*, r.NomReunion from ARActasC as a"+
                        " left join AReunionC as r on r.OidAReunionC = a.OidAReunionC"+
                         " where a.Estado = 1 and a.GNCodUsu = @GNCodUsu and a.OidAReunionC = 6017", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                actas = new List<ARActasC>();
                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrLink = reader["Link"].ToString()
                    };
                    actas.Add(acta);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return actas;
        }


        public static List<ARActasC> GetActas(string idReunion, DateTime FecInicio, string nombre)
        {
            List<ARActasC> actas = new List<ARActasC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select R.NomReunion, FecInicio, U.GNNomUsu, A.LugarReun, A.Nombre  from ARActasC A" +
                                         "   left join AReunionC R on A.OidAReunionC = R.OidAReunionC"+
                                         "   left outer join AReunionD AD on AD.OidAReunionC = R.OidAReunionC"+
                                         "   left join Usuario U on U.GNCodUsu = Ad.GNCodUsu"+
                                         "   where A.FecInicio < @FecInicio and R.OidAReunionC like '%' + @OidAReunionC + '%'"+
                                         "   and AD.TpNomUsu = 'Coordinador'  and  U.GNNomUsu like '%' + @GNNomUsu + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNNomUsu", nombre);
                command.Parameters.AddWithValue("@OidAReunionC", idReunion);
                command.Parameters.AddWithValue("@FecInicio", FecInicio);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {

                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        StrNombre = reader["Nombre"].ToString(),
                        StrCoordinador = reader["GNNomUsu"].ToString(),
                        StrLugarReun = reader["LugarReun"].ToString()
                    };
                    actas.Add(acta);
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

            return actas;
        }
        /// <summary>
        /// Metodo que retorna lun listado de todas las actas
        /// </summary>
        /// <returns></returns>
        public static List<ARActasC> GetActas(string nombre, string codigo, DateTime fecha1, DateTime fecha2, string coordinador, string estado)
        {
            List<ARActasC> actas = new List<ARActasC>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select A.[OidARActasC]
	                                            , A.[OidAReunionC]
	                                            , A.[GNCodUsu]
	                                            ,[Estado]
	                                            ,[Codigo]
	                                            ,[LugarReun]
	                                            ,[Objetivo]
	                                            , A.[Sigla] + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) as Sigla
	                                            ,[FecInicio]
	                                            ,[FecFinal]
	                                            ,[FechEditable]
	                                            ,[FecSistema]
	                                            ,[Link]
	                                            , U.GNNomUsu as Coordinador
	                                            , A.Nombre
                                            from ARActasC A
	                                            left join AReunionC R on R.OidAReunionC = A.OidAReunionC
	                                            LEFT JOIN ARActasDM DM ON DM.OidARActasC = A.OidARActasC AND DM.TipoUsuario = 'Coordinador'
	                                            LEFT JOIN Usuario U ON U.GNCodUsu = DM.GNCodUsu
                                            where  A.Nombre like '%' + @NomReunion + '%' and  Estado like '%' + @Estado + '%' and Estado > 0  and
	                                            FechEditable >= @fecha1  and FechEditable <= @fecha2   and(a.[Sigla] + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) like '%' + @Sigla + '%')
	                                            and  ISNULL(U.GNNomUsu,'') like '%' + @GNNomUsu + '%'", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("GNNomUsu", coordinador);               
                command.Parameters.AddWithValue("Sigla", codigo);
                command.Parameters.AddWithValue("fecha1", fecha1);
                command.Parameters.AddWithValue("fecha2", fecha2);
                command.Parameters.AddWithValue("NomReunion", nombre);
                command.Parameters.AddWithValue("Estado", estado);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"]),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"]),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"]),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"]),
                        IntCodigo = Convert.ToInt32(reader["Codigo"]),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"]),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"]),
                        StrCoordinador = reader["Coordinador"].ToString(),
                        StrLink = reader["Link"].ToString(),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                    };
                    actas.Add(acta);
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
            return actas;
        }

        public static List<ARActasC> GetActasBySuario(string codigo, string nombre, DateTime fecha, string lugar, string estado)
        {
            List<ARActasC> actas = new List<ARActasC>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select distinct D.firmado, a.*, A.Sigla + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) as Sig from ARActasC A 
	                                            left join ARActasDM  D on D.OidARActasC = A.OidARActasC
                                            where A.Nombre like '%'+ @Nombre +'%' and D.EstUsuario = 1 
	                                            and A.FechEditable < @fecha and LugarReun like '%' + @LugarReun + '%' and a.Estado = 2
	                                            and A.Sigla + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) like '%' + @Codigo + '%'
                                                and  D.GNCodUsu = @GNCodUsu and  D.Firmado like '%'+ @firmado +'%' order by D.Firmado asc, A.FechEditable desc, A.OidARActasC desc", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("Nombre", nombre);
                command.Parameters.AddWithValue("fecha", fecha);
                command.Parameters.AddWithValue("LugarReun", lugar);
                command.Parameters.AddWithValue("Codigo", codigo);
                command.Parameters.AddWithValue("firmado", estado);
                command.Parameters.AddWithValue("GNCodUsu", Convert.ToInt32(HttpContext.Current.Session["Admin"]));

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    actas.Add(new ARActasC {
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"]),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"]),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"]),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"]),
                        IntCodigo = Convert.ToInt32(reader["Codigo"]),
                        IntEstado = Convert.ToInt32(reader["Estado"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"]),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"]),
                        StrLink = reader["Link"].ToString(),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sig"].ToString(),
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

            return actas;
        }

        public bool update(List<ARActasC> data)
        {
            throw new NotImplementedException();
        }

        ARActasC DAOInterfaz<ARActasC>.Get(int id)
        {
            throw new NotImplementedException();
        }

        List<ARActasC> DAOInterfaz<ARActasC>.Listar(int id)
        {
            throw new NotImplementedException();
        }

        bool DAOInterfaz<ARActasC>.set(ARActasC data)
        {
            throw new NotImplementedException();
        }

        bool DAOInterfaz<ARActasC>.set(List<ARActasC> data)
        {
            throw new NotImplementedException();
        }

        ARActasC DAOInterfaz<ARActasC>.setUltiomo()
        {
            throw new NotImplementedException();
        }

        bool DAOInterfaz<ARActasC>.update(int id)
        {
            throw new NotImplementedException();
        }

        bool DAOInterfaz<ARActasC>.update(List<ARActasC> data)
        {
            throw new NotImplementedException();
        }


        public static void DeleteActa(int idActa)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Update ARActasC set Eliminado = 1 where OidARActasC = @OidARActasC", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidARActasC", idActa);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idActa,
                    strAccion = "Eliminar",
                    strDetalle = "Se elimina  una reunion creada en la agenda ",
                    strEntidad = "ARActasC"
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
        public static int GetCodigo(string sigla)
        {
            int codigo = 1;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  MAX(A.Codigo) + 1 as Codigo FROM[dbo].[ARActasC] AS A where A.Sigla = @Sigla and isnull(A.Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Sigla", sigla);
                reader = command.ExecuteReader();

              

                if (reader.Read())
                {
                    codigo = Convert.ToInt32(reader["Codigo"]); 
                }
              
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conexion.CloseConnection();
            }


            return codigo;
        }

        public static List<ARActasC> GetActasByIdComite(int idComite)
        {
            List<ARActasC> actas = new List<ARActasC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from ARActasC where OidAReunionC = @OidAReunionC and Estado > 0 and isnull(Eliminado, 0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidAReunionC", idComite);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        StrLink = reader["Link"].ToString()
                    };
                    actas.Add(acta);
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

            return actas;
        }
        public static List<ARActasC> GetActasSinFirmaByUsuario(int idUsuario)
        {
            List<ARActasC> actas = new List<ARActasC>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select distinct A.* from ARActasC A
	                                            left join ARActasDM D on D.OidARActasC = A.OidARActasC
                                            where A.Estado > 1 and D.Firmado = 0 and D.EstUsuario = 1 and D.GNCodUsu = @GNCodUsu and isnull(A.Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("GNCodUsu", idUsuario);
                
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ARActasC acta = new ARActasC
                    {
                        IntOidARActas = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntCodigo = Convert.ToInt32(reader["Codigo"].ToString()),
                        IntEstado = Convert.ToInt32(reader["Estado"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString()),
                        StrLugarReun = reader["LugarReun"].ToString(),
                        StrObjetivo = reader["Objetivo"].ToString(),
                        StrSigla = reader["Sigla"].ToString(),
                        DtmFecFinal = Convert.ToDateTime(reader["FecFinal"].ToString()),
                        DtmFechEditable = Convert.ToDateTime(reader["FechEditable"].ToString()),
                        DtmFecInicio = Convert.ToDateTime(reader["FecInicio"].ToString()),
                        DtmFecSistema = Convert.ToDateTime(reader["FecSistema"].ToString()),
                        StrLink = reader["Link"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                    };
                    actas.Add(acta);
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

            return actas;
        }
    }
}
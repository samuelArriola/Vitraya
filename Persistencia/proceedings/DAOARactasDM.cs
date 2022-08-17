using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;

namespace Persistencia.proceedings
{
    public class DAOARactasDM
    {

        public static void set(ARActasDM data)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[ARActasDM]
                                               ([GNCodUsu]
                                               ,[EstUsuario]
                                               ,[OidARActasC]
                                               ,[TipoUsuario]
                                               ,[Firmado] 
                                               ,[Eliminado] 
                                               ,[Nombre])
                                          select  
                                               @GNCodUsu,
                                               @EstUsuario,
                                               @OidARActasC,
                                               @TipoUsuario, 
                                               @Firmado, 
                                               0, 
                                               @Nombre 
                                        where  @GNCodUsu  not in (
                                            select GNCodUsu from ARActasDM where OidARActasC = @OidARActasC
                                        ) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                command.Parameters.AddWithValue("@EstUsuario", data.IntEstUsuario);
                command.Parameters.AddWithValue("@OidARActasC", data.IntOidARActasC);
                command.Parameters.AddWithValue("@TipoUsuario", data.StrTipoUsuario);
                command.Parameters.AddWithValue("@Firmado", data.BlnFirmado);
                command.Parameters.AddWithValue("@Nombre", data.StrNombre);

                int OidInstanacia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = data.IntOidARActasDM,
                    strAccion = "Crear",
                    strDetalle = $"Se agrega el miembro {data.StrNombre} a la reunion con código {data.IntOidARActasC}",
                    strEntidad = "ARActasDM"
                });
            }
            catch (Exception ex)
            {
            }

            finally
            {
                conexion.CloseConnection();
            }
        }
         
        public static List<ARActasDM> getParticipantes(int id)
        {
            List<ARActasDM> participantes = null;
            SqlCommand commamd;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                commamd = new SqlCommand("SELECT  [OidARActasDM]"+
                                              ",[GNCodUsu]"+
                                              ",[EstUsuario]"+
                                              ",[OidARActasC]"+
                                              ",[TipoUsuario]"+
                                              ",[Firmado]" +
                                              ",[Nombre]"+
                                         " FROM[dbo].[ARActasDM] where OidARActasC = @id and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                commamd.Parameters.AddWithValue("@id", id);
                commamd.ExecuteNonQuery();
                reader = commamd.ExecuteReader();

                participantes = new List<ARActasDM>();
                while (reader.Read())
                {
                    ARActasDM participante = new ARActasDM {
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"].ToString()),
                        IntEstUsuario = Convert.ToInt32(reader["EstUsuario"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasDM = Convert.ToInt32(reader["OidARActasDM"].ToString()),
                        StrTipoUsuario = reader["TipoUsuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
                    };


                    participantes.Add(participante);
                }
            }
            catch (Exception ex)
            {
            }
            finally {
                conexion.CloseConnection();
            }

            return participantes;

        }
        public static void update(ARActasDM data)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[ARActasDM]"+
                                              " SET[GNCodUsu] = @GNCodUsu"+
                                                  ",[EstUsuario] = @EstUsuario"+
                                                  ",[OidARActasC] = @OidARActasC"+
                                                  ",[TipoUsuario] = @TipoUsuario"+
                                                  ",[Firmado] = @Firmado"+
                                                  ",[Nombre] = @Nombre"+
                                            " WHERE OidARActasDM = @OidARActasDM", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                command.Parameters.AddWithValue("@EstUsuario", data.IntEstUsuario);
                command.Parameters.AddWithValue("@OidARActasC", data.IntOidARActasC);
                command.Parameters.AddWithValue("@TipoUsuario", data.StrTipoUsuario);
                command.Parameters.AddWithValue("@Firmado", data.BlnFirmado);
                command.Parameters.AddWithValue("@Nombre", data.StrNombre);
                command.Parameters.AddWithValue("@OidARActasDM", data.IntOidARActasDM);
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

        public static ARActasDM get(int idActa, int idMiembro)
        {
            ARActasDM participante = new ARActasDM();
            SqlCommand commamd;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                commamd = new SqlCommand("SELECT  [OidARActasDM]" +
                                              ",[GNCodUsu]" +
                                              ",[EstUsuario]" +
                                              ",[OidARActasC]" +
                                              ",[TipoUsuario]" +
                                              ",[Firmado]" +
                                              ",[Nombre]" +
                                         " FROM[dbo].[ARActasDM] where OidARActasC = @id and GNCodUsu = @GNCodUsu AND isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                commamd.Parameters.AddWithValue("@id", idActa);
                commamd.Parameters.AddWithValue("@GNCodUsu", idMiembro);
                commamd.ExecuteNonQuery();
                reader = commamd.ExecuteReader();

                
                if(reader.Read())
                {
                    participante = new ARActasDM
                    {
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"].ToString()),
                        IntEstUsuario = Convert.ToInt32(reader["EstUsuario"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasDM = Convert.ToInt32(reader["OidARActasDM"].ToString()),
                        StrTipoUsuario = reader["TipoUsuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
                    };
                    participante.BlnFirmado = participante.BlnFirmado;
                    participante.IntOidARActasDM = participante.IntOidARActasDM;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return participante;
        }

        public static ARActasDM Get(int idMiembro)
        {
            ARActasDM miembro = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from ARActasDM where OidARActasDM = @OidARActasDM and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidARActasDM", idMiembro);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    miembro = new ARActasDM {
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"].ToString()),
                        IntEstUsuario = Convert.ToInt32(reader["EstUsuario"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasDM = Convert.ToInt32(reader["OidARActasDM"].ToString()),
                        StrTipoUsuario = reader["TipoUsuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
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

            return miembro;
        }

        public static List<Usuario> GetUsuariosFirmados(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from Usuario as u "+
                                         "  where GNCodUsu in (select GNCodUsu from ARActasDM as u"+
                                         "  where u.EstUsuario = 1 and Firmado = 1 and u.OidARActasC = @id and isnull(Eliminado, 0) = 0)", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Usuario usuario = new Usuario();
                    usuario.GNNomUsu1 = reader["GNNomUsu"].ToString();
                    try
                    {
                        string firma = reader["GNFmUsu"].ToString();
                        usuario.GNFmUsu1 = (byte[])reader["GNFmUsu"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        usuario.GNFmUsu1 = new byte[0];
                    }
                    
                    usuario.GnCargo1 = reader["GNCargo"].ToString();
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return usuarios;
        }

        public static List<Usuario> GetUsuarios(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from Usuario as u " +
                                         "  where GNCodUsu in (select GNCodUsu from ARActasDM as u" +
                                         "  where u.EstUsuario = 1  and u.OidARActasC = @id and isnull(Eliminado, 0) = 0)", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Usuario usuario = new Usuario();
                    usuario.GNNomUsu1 = reader["GNNomUsu"].ToString();
                    try
                    {
                        usuario.GNFmUsu1 = (byte[])reader["GNFmUsu"];
                    }
                    catch (Exception ex)
                    {
                        usuario.GNFmUsu1 = new byte[0];
                    }

                    usuario.GnCargo1 = reader["GNCargo"].ToString();
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return usuarios;
        }

        public static List<ARActasDM> GerAsistentes(int idActa)
        {
            List<ARActasDM> asistentes = new List<ARActasDM>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from ARActasDM where EstUsuario = 1 and OidARActasC = @OidARActasC and Firmado = 1 and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidARActasC", idActa);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    asistentes.Add(new ARActasDM {
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"].ToString()),
                        IntEstUsuario = Convert.ToInt32(reader["EstUsuario"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasDM = Convert.ToInt32(reader["OidARActasDM"].ToString()),
                        StrTipoUsuario = reader["TipoUsuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
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
            return asistentes;
        }

        public static List<ARActasDM> GetCoordSec(int idActa)
        {
            List<ARActasDM> usuarios = new List<ARActasDM>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from ARActasDM where OidARActasC = @OidARActasC and (TipoUsuario = 'Coordinador' or TipoUsuario = 'Secretario') and isnull(Eliminado, 0) = 0",conexion.OpenConnection());
                command.Parameters.AddWithValue("OidARActasC", idActa);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new ARActasDM
                    {
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"].ToString()),
                        IntEstUsuario = Convert.ToInt32(reader["EstUsuario"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidARActasC = Convert.ToInt32(reader["OidARActasC"].ToString()),
                        IntOidARActasDM = Convert.ToInt32(reader["OidARActasDM"].ToString()),
                        StrTipoUsuario = reader["TipoUsuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
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

            return usuarios;
        }

        public static List<dynamic> GetAsistentesSinFirma(string nombreUsuario, string documento, string sigla, string nombreActa)
        {
            List<dynamic> asistentesSinFirma = new List<dynamic>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select u.GNNomUsu, u.GNCodUsu,  A.[Sigla] + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) as Sigla, A.Nombre from Usuario U
	                                            left join ARActasDM D on D.GNCodUsu = U.GNCodUsu 
	                                            left join ARActasC A on A.OidARActasC = D.OidARActasC
                                            where D.EstUsuario = 1 and D.Firmado = 0 and A.Estado > 1 
	                                            and u.GNNomUsu like '%' + @GNNomUsu + '%' and (A.[Sigla] + RIGHT('0000' + Ltrim(Rtrim(Codigo)), 4) like '%'+ @Sigla +'%')
	                                            and u.GNCodUsu like '%' + @GNCodUsu + '%' and  A.Nombre like '%'+@Nombre+'%' and isnull(D.Eliminado, 0) = 0
                                            order by 1", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNNomUsu", nombreUsuario);
                command.Parameters.AddWithValue("GNCodUsu", documento);
                command.Parameters.AddWithValue("Sigla", sigla);
                command.Parameters.AddWithValue("Nombre", nombreActa);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    asistentesSinFirma.Add(new { 
                        NombreUsuario = reader["GNNomUsu"].ToString(),
                        Documento = reader["GNCodUsu"].ToString(),
                        Sigla = reader["Sigla"].ToString(),
                        NombreActa = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["Nombre"].ToString())
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

            return asistentesSinFirma;
        }


        public static dynamic GetEstadisticasActas()
        {
            dynamic estadisticas = null;

            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            Conexion conexion = new Conexion();

            try
            {
                using (var command = new SqlCommand()) {
                    command.Connection = conexion.OpenConnection();
                    command.CommandText = @"
                        select 
	                        (select count(*) from ARActasDM DM2 
		                        inner join ARActasC A on A.OidARActasC = DM2.OidARActasC and A.Estado > 1
	                        where Dm2.GNCodUsu = U.GNCodUsu and EstUsuario = 0 and isnull(DM2.Eliminado, 0) = 0) 'Inasistido',
	                        (select COUNT(*) from ARActasDM DM2 
		                        inner join ARActasC A on A.OidARActasC = DM2.OidARActasC and A.Estado > 1
	                         where Dm2.GNCodUsu = U.GNCodUsu and EstUsuario = 1 and Firmado = 0 and isnull(DM2.Eliminado, 0) = 0) 'No Firmado',
	                        (select COUNT(*) from ARActasDM DM2 
		                        inner join ARActasC A on A.OidARActasC = DM2.OidARActasC and A.Estado > 1
	                         where Dm2.GNCodUsu = U.GNCodUsu and Firmado = 1 and isnull(DM2.Eliminado, 0) = 0) 'Firmado'
                        from Usuario U 
                        where GNCodUsu = @GNCodUsu 
                    ";

                    command.Parameters.AddWithValue("GNCodUsu", idUsuario);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            estadisticas = new {
                                Inasistido = Convert.ToInt32(reader["Inasistido"]),
                                NoFirmado = Convert.ToInt32(reader["No Firmado"]),
                                Firmado = Convert.ToInt32(reader["Firmado"])
                            };
                    }
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
    }
}
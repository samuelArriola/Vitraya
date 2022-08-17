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
    public class DAOCPAsistencia
    {

        public static void SetAsistencia(int idUsuario, DateTime horaIni, DateTime horaFin, int idCapacitacion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[CPAsistencia]"+
                                         "          ([GNCodUsu]"+
                                         "          ,[OidCPMATRICULA]"+
                                         "          ,[NOMUSUARIO]"+
                                         "          ,[UNIDAD]"+
                                         "          ,[CARGO]"+
                                         "          ,[EMAIL]"+
                                         "          ,[TELEFONO]"+
                                         "          ,[FECHAINICIO]"+
                                         "          ,[FECHAFINAL]"+
                                         "          ,[ESTADO]"+
                                         "          ,[FIRMADO])"+
                                         "   SELECT GNCodUsu, (select top(1) OidCPMATRICULA from CPMatricula where GNCodUsu = M.GNCodUsu and OidCPCAPACITACION = @OidCPCapacitacion) ," +
                                         "   NOMUSUARIO, UNIDAD, CARGO, EMAIL, TELEFONO, @FECHAINICIO, @FECHAFINAL,1,0 FROM CPMatricula AS M" +
                                         "   WHERE OidCPMATRICULA NOT IN(SELECT OidCPMATRICULA FROM CPAsistencia) AND OidCPCapacitacion = @OidCPCapacitacion AND GNCodUsu = @idUsuario \n select scope_identity()", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@FECHAINICIO", horaIni);
                command.Parameters.AddWithValue("@FECHAFINAL", horaFin);
                command.Parameters.AddWithValue("@OidCPCapacitacion", idCapacitacion);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "CPAsistencia"
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

        public static List<CPAsistencia> GetAsistencias(int idCapacitacion)
        {
            List<CPAsistencia> asistencias = new List<CPAsistencia>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPAsistencia AS A"+
                                          "  LEFT JOIN CPMatricula AS M ON M.OidCPMATRICULA = A.OidCPMATRICULA"+
                                          "  WHERE M.OidCPCAPACITACION = @OidCPCAPACITACION", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object d = reader["EMAIL"];
                    CPAsistencia asistencia = new CPAsistencia
                    {
                        DtmFECHAFINAL = (reader["FECHAFINAL"].ToString() == "")? DateTime.Now : Convert.ToDateTime(reader["FECHAFINAL"].ToString()),
                        DtmFECHAINICIO = (reader["FECHAINICIO"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(reader["FECHAINICIO"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPAsistencia = Convert.ToInt32(reader["OidCPAsistencia"].ToString()),
                        IntOidCPMATRICULA = Convert.ToInt32(reader["OidCPMATRICULA"].ToString()),
                        IsActivo = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        IsFirmado = Convert.ToBoolean(reader["FIRMADO"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                    };
                    asistencias.Add(asistencia);
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

            return asistencias;
        }

        public static void firmarCapacitacion(int idUsuario, int idCapacitacion)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select OidCPAsistencia from CPAsistencia 
                                            where OidCPMatricula in (
	                                            select OidCPMatricula from CPMatricula as m 
	                                            where m.OidCPCapacitacion = @OidCPCapacitacion and m.GNCodUsu = @GNCodUsu
                                            )
                                            Update CPAsistencia set FIRMADO = 1 
                                            where OidCPMatricula in (
	                                            select OidCPMatricula from CPMatricula as m 
	                                            where m.OidCPCapacitacion = @OidCPCapacitacion and m.GNCodUsu = @GNCodUsu
                                            )", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidCPCapacitacion", idCapacitacion);
                command.Parameters.AddWithValue("@GNCodUsu", idUsuario);
                reader =  command.ExecuteReader();

                reader.Read();

                int OidInstancia = Convert.ToInt32(reader["OidCPAsistencia"]);

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Editar",
                    strDetalle = $"",
                    strEntidad = "CPAsistencia"
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
        public static List<Usuario> GetAsistenciasUsuario(int idCapacitacion)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT [GnIdUsu]"+
                                         "         , U.[GNCodUsu]"+
                                         "         ,[GNNomUsu]"+
                                         "         ,[GNConUsu]"+
                                         "         ,[GnDcDep]"+
                                         "         ,[GNFhUsu]"+
                                         "         ,[GnEtUsu]"+
                                         "         ,[GnCdAra]"+
                                         "         ,[GnDcCgo]"+
                                         "         ,[GNCrusu]"+
                                         "         ,[GNFtUsu]"+
                                         "         ,[GNFmUsu]"+
                                         "         ,[GnTlUsu]"+
                                         "         ,[GnEpsUsu]"+
                                         "         ,[GnFtHull]"+
                                         "         ,[GnUnfun]"+
                                         "         ,[GnCargo]"+
                                         "         ,[codigoR] FROM Usuario AS U"+
                                         "   INNER JOIN CPMatricula AS M ON U.GNCodUsu = M.GNCodUsu"+
                                         "   INNER JOIN CPAsistencia AS A ON A.OidCPMATRICULA = M.OidCPMATRICULA"+
                                         "   WHERE M.OidCPCAPACITACION = @OidCPCAPACITACION AND A.FIRMADO = 1", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GNFmUsu1 = (reader["GNFmUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFmUsu"],
                        GnFtHull1 = (reader["GnFtHull"].ToString() == "") ? new byte[0] : (byte[])reader["GnFtHull"],
                        GNFtUsu1 = (reader["GNFtUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFtUsu"],
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = reader["GNNomUsu"].ToString(),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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
        public static void deleteAsistencia(int id)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select OidCPAsistencia from CPAsistencia where OidCPMatricula = @OidCPMatricula
                                            delete from CPAsistencia where OidCPMatricula = @OidCPMatricula", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPMatricula", id);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        dtmFecha = DateTime.Now,
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = Convert.ToInt32(reader["OidCPAsistencia"]),
                        strAccion = "Elimnar",
                        strDetalle = $"",
                        strEntidad = "CPAsistencia"
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
        }
    }
}
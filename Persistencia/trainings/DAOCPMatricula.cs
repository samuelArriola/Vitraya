using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Persistencia.trainings
{
    public class DAOCPMatricula
    {
        /// <summary>
        /// Metodo para matriculacion de usuarios de pendiendo del tipo de matricula, ya sea por unidad funcional, cargo, todos o uno en particular
        /// </summary>
        /// <param name="dato">El dato por el cual se va consutar</param>
        /// <param name="tipo"> pueder ser; unitario, unidad, cargo; cualquier otro se matricularn todos los usuarios</param>
        /// <param name="idCapacitacion">id de la capacitacion de la matricula</param>
        /// <param name="fecha">fecha en que se realiza la matricula</param>
        public static void matricularUsuarios(string dato, string tipo, int? idCapacitacion, DateTime fecha, int? metodoMatr, int? valorMetodoMatr, string nombreMetodoMatr, int idAgenda)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            string consulta = "";
            switch (tipo)
            {
                case "unitario":
                    {
                        consulta = "AND U.GNCodUsu = @dato";
                        break;
                    }
                case "unidad":
                    {
                        consulta = "AND U.GnDcDep = @dato";
                        break;
                    }
                case "cargo":
                    {
                        consulta = "AND U.GnDcCgo = @dato";
                        break;
                    }
                case "nomCargo":
                    {
                        consulta = "AND U.GnCargo = @dato";
                        break;
                    }
               
            }

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[CPMatricula]
	                                            ([OidCPCAPACITACION]
	                                            ,[OidCPAgenda]
	                                            ,[ESTADO]
	                                            ,[FECHA]
	                                            ,[GNCodUsu]
	                                            ,[NOMUSUARIO]
	                                            ,[UNIDAD]
	                                            ,[CARGO]
	                                            ,[TELEFONO]
	                                            ,[Firmado]
	                                            ,[Asistido]
	                                            ,[NombreMetodoMatr]
	                                            ,[ValorMetodoMatr]
	                                            ,[MetodoMatri]
	                                            ,[Eliminado]
	                                            ,[EMAIL])
                                            SELECT @idcapacitacion,@OidCPAgenda,1, @fecha, GNCodUsu, GNNomUsu, U.GnUnfun, GnCargo, GnTlUsu, 0,0, @NombreMetodoMatr, @ValorMetodoMatr,@MetodoMatri, 0, GNCrusu FROM Usuario AS U
                                            WHERE GNCodUsu NOT IN(SELECT GNCodUsu FROM CPMatricula WHERE CPMatricula.GNCodUsu = U.GNCodUsu AND CPMATRICULA.OidCPAgenda = @OidCPAgenda and isnull(CPMatricula.Eliminado, 0) = 0) and U.GnEtUsu = 'Activo'" + consulta, conexion.OpenConnection());
                if(tipo != "todos")
                {
                    command.Parameters.AddWithValue("@dato", dato);
                }
                command.Parameters.AddWithValue("@idcapacitacion", idCapacitacion);

                if(nombreMetodoMatr == null)
                    command.Parameters.AddWithValue("@NombreMetodoMatr", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@NombreMetodoMatr", nombreMetodoMatr);


                if(valorMetodoMatr == null)
                    command.Parameters.AddWithValue("@ValorMetodoMatr", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ValorMetodoMatr", valorMetodoMatr);


                if(metodoMatr == null)
                    command.Parameters.AddWithValue("@MetodoMatri", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@MetodoMatri", metodoMatr);


                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.ExecuteNonQuery();

                string[] metodos = {"Ninguno", "Usuario", "Cargo", "Unidad funcional" };

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se raliza una matriculacion de masiva para la Agenda con código {idAgenda} con el metodo de matriculacion por {metodos[metodoMatr ?? 0]}",
                    strEntidad = "CPMatricula"
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
        /// Este meto valida si un usuario se encuentra matriculado en otra capacitacion la que se cruse con la fecha de la capacitación a matricular
        /// </summary>
        /// <param name="idUsuario"> id del usuarios a validar</param>
        /// <param name="idCapacitacion">id de la pacitacion de verificación</param>
        /// <returns></returns>
        public static bool validarUsuarioMatriculado(int idUsuario, int idCapacitacion)
        {
            bool repetido = false;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("IF EXISTS("+
                                         "   SELECT OidCPCAPACITACION FROM CPCAPACITACION AS C1 WHERE C1.OidCPCAPACITACION = @OidCPCAPACITACION" +
                                         "       AND C1.OidCPCAPACITACION IN("+
                                         "       IIF(("+
                                         "           SELECT OidCPCAPACITACION FROM CPCAPACITACION AS C2"+
                                         "           WHERE(((C1.HORAINICIAL >= C2.HORAINICIAL) AND(C1.HORAINICIAL < C2.HORAFINAL)) OR((C1.HORAFINAL > C2.HORAINICIAL) AND(C1.HORAFINAL <= C2.HORAFINAL)))"+
                                         "               AND C2.OidCPCAPACITACION IN(SELECT OidCPCAPACITACION FROM CPMATRICULA WHERE GNCodUsu = 45760395)  AND C2.OidCPCAPACITACION <> C1.OidCPCAPACITACION"+
                                         "               AND C1.FECHA = C2.FECHA"+
                                         "           ) IS NULL, '', C1.OidCPCAPACITACION)"+
	                                     "       )"+
                                         "   )"+
                                         "   BEGIN"+
                                         "       SELECT 1 AS EXISTE"+
                                         "   END"+
                                         "   ELSE"+
                                         "       SELECT 0 AS EXISTE", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                command.Parameters.AddWithValue("@GNCodUsu", idUsuario);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    repetido = Convert.ToBoolean(reader["EXISTE"]);
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

            return repetido;
        }

        /// <summary>
        ///  Debuelda un lisatado de todos los usuarios matriculados usando como prametro de consulta el id de la capacitacion
        /// </summary>
        /// <param name="idCapacitacion"></param>
        /// <returns></returns>
        public static List<CPMatricula> GetMatriculasCapacitaciones(int idCapacitacion)
        {
            List<CPMatricula> matriculas = new List<CPMatricula>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPMatricula WHERE OidCPCAPACITACION = @OidCPCAPACITACION and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    CPMatricula matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"]),
                        DtmFECHA = Convert.ToDateTime(reader["FECHA"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"]),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"]),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
                    };
                    matriculas.Add(matricula);
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
            return matriculas;
        }

        public static List<CPMatricula> GetMatriculasByNom(string nombre, int idCapacitacion)
        {
            List<CPMatricula> matriculas = new List<CPMatricula>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPMatricula WHERE OidCPMATRICULA NOT IN (SELECT OidCPMATRICULA FROM CPAsistencia) AND NOMUSUARIO LIKE '%' + @nombre + '%' and OidCPCAPACITACION = @OidCPCAPACITACION and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    CPMatricula matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"]),
                        DtmFECHA = Convert.ToDateTime(reader["FECHA"]),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"]),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"]),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
                    };
                    matriculas.Add(matricula);
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
            return matriculas;
        }

        public static bool eliminarMatricula(int idUsuario, int idCapacitacion)
        {
            bool isDeleted = true;
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@" SELECT OidCPMatricula FROM CPMatricula WHERE GNCodUsu = @GNCodUsu  AND OidCPCAPACITACION = @OidCPCAPACITACION
                                            update CPMatricula set Eliminado = 1 WHERE GNCodUsu = @GNCodUsu  AND OidCPCAPACITACION = @OidCPCAPACITACION", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", idUsuario);
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPMatricula"
                });
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

        public static CPMatricula GetMatricula(int idUsuario, int idAgenda)
        {
            CPMatricula matricula = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CPMATRICULA where OidCPAgenda = @OidCPAgenda and GNCodUsu = @GNCodUsu and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("@GNCodUsu", idUsuario);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD    = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matricula;
        }

        public static CPMatricula GetMatricula(int idMatricula)
        {
            CPMatricula matricula = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from CPMATRICULA where OidCPMatricula = @OidCPMatricula and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPMatricula", idMatricula);
               
               
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matricula;
        }

        public static CPMatricula GetMatriculaUlt()
        {
            CPMatricula matricula = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select top(1) from CPMATRICULA where isnull(Eliminado, 0) = 0 order by OidCPMatricula desc", conexion.OpenConnection());
                
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matricula;
        }


        public static List<Usuario> GetMatriculasByAgenda(int idAgenda, string documento, string nombre,string cargo)
        {
            List<Usuario> matriculas = new List<Usuario>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select U.* from  Usuario U
	                                        inner join CPMATRICULA M on M.GNCodUsu = U.GNCodUsu and isnull(M.Eliminado, 0) = 0
	                                        inner join CPAgenda A on A.OidCPAgenda = M.OidCPAgenda and A.OidCPAgenda = @OidCPAgenda
                                            where U.GNCodUsu like '%' + @documento + '%' and U.GNNomUsu like '%' + @nombre + '%' and U.GnCargo like '%' + @cargo + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("documento", documento);
                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("cargo", cargo);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    matriculas.Add(new Usuario
                    {
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFtUsu1 = (reader["GNFtUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFtUsu"],
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
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
            return matriculas;
        }


        public static List<dynamic> GetMatriculasByIdAgenda(int idAgenda, string nombre)
        {
            List<dynamic> matriculas = new List<dynamic>();


            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select U.GNNomUsu, U.GNFtUsu, M.Asistido, M.OidCPMatricula  from  Usuario U
	                                        inner join CPMATRICULA M on M.GNCodUsu = U.GNCodUsu and isnull(M.Eliminado, 0) = 0
	                                        inner join CPAgenda A on A.OidCPAgenda = M.OidCPAgenda and A.OidCPAgenda = @OidCPAgenda
                                            where U.GNNomUsu like '%' + @nombre + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("nombre", nombre);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    matriculas.Add(new {
                        IdMatricula = Convert.ToInt32(reader["OidCPMatricula"]),
                        Foto = Convert.ToBase64String((reader["GNFtUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFtUsu"]),
                        Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        Asistido = Convert.ToBoolean(reader["Asistido"])
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

            return matriculas;
        }
        public static List<CPMatricula.DatosMatricula> GetDatosMatriculas(int idAgenda)
        {
            List<CPMatricula.DatosMatricula> datos = new List<CPMatricula.DatosMatricula>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select distinct MetodoMatri,ValorMetodoMatr, NombreMetodoMatr from CPMATRICULA 
                                            where MetodoMatri is not null and OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    datos.Add(new CPMatricula.DatosMatricula { 
                        metodoMatr = Convert.ToInt32(reader["MetodoMatri"]),
                        nombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        valorMetodoMetr  = Convert.ToInt32(reader["ValorMetodoMatr"]),
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

            return datos;
        }

        public static void DeleteMatriculaByMethod(int idAgenda, int metodo, int valor)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Update CPMatricula set Eliminado = 1 where 
                                            ValorMetodoMatr = @ValorMetodoMatr and MetodoMatri = @MetodoMatri 
                                            and OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("ValorMetodoMatr", valor);
                command.Parameters.AddWithValue("MetodoMatri", metodo);
                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);

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

        public static void UpdataMatricula(CPMatricula matricula)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"UPDATE [dbo].[CPMATRICULA]
                                               SET [OidCPCAPACITACION] = @OidCPCAPACITACION
                                                  ,[GNCodUsu] = @GNCodUsu
                                                  ,[NOMUSUARIO] = @NOMUSUARIO
                                                  ,[UNIDAD] = @UNIDAD
                                                  ,[CARGO] = @CARGO
                                                  ,[TELEFONO] = @TELEFONO
                                                  ,[EMAIL] = @EMAIL
                                                  ,[FECHA] = @FECHA
                                                  ,[ESTADO] = @ESTADO
                                                  ,[Asistido] = @Asistido
                                                  ,[Firmado] = @Firmado
                                                  ,[OidCPAgenda] = @OidCPAgenda
                                                  ,[MetodoMatri] = @MetodoMatri
                                                  ,[ValorMetodoMatr] = @ValorMetodoMatr
                                                  ,[NombreMetodoMatr] = @NombreMetodoMatr
                                             WHERE  OidCPMATRICULA  =  @OidCPMATRICULA", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPCAPACITACION", matricula.IntOidCPCAPACITACION);
                command.Parameters.AddWithValue("GNCodUsu", matricula.IntGNCodUsu);
                command.Parameters.AddWithValue("NOMUSUARIO", matricula.StrNOMUSUARIO);
                command.Parameters.AddWithValue("UNIDAD", matricula.StrUNIDAD);
                command.Parameters.AddWithValue("CARGO", matricula.StrCARGO);
                command.Parameters.AddWithValue("TELEFONO", matricula.StrTELEFONO);
                command.Parameters.AddWithValue("EMAIL", matricula.StrEMAIL);
                command.Parameters.AddWithValue("FECHA", matricula.DtmFECHA);
                command.Parameters.AddWithValue("ESTADO", matricula.BlnESTADO);
                command.Parameters.AddWithValue("Asistido", matricula.BlnAsistido);
                command.Parameters.AddWithValue("Firmado", matricula.BlnFirmado);
                command.Parameters.AddWithValue("OidCPAgenda", matricula.IntOidCPAgenda);
                command.Parameters.AddWithValue("MetodoMatri", matricula.IntMetodoMatri);
                command.Parameters.AddWithValue("ValorMetodoMatr", matricula.IntValorMetodoMatr);
                command.Parameters.AddWithValue("NombreMetodoMatr", matricula.StrNombreMetodoMatr);
                command.Parameters.AddWithValue("OidCPMATRICULA", matricula.IntOidCPMatricula);

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
        public static CPMatricula GetMatriculaByAgenda(int idAgenda, int idUsuario)
        {
            CPMatricula matricula = null;

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select * from CPMATRICULA where GNCodUsu = @GNCodUsu and OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("GNCodUsu", idUsuario);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    matricula = new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matricula;
        }

        public static void DeleteMatriculaByUsuario(int idUsuario, int idAgenda)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select OidCPMatricula from CPMATRICULA where OidCPAgenda = @OidCPAgenda and GNCodUsu = @GNCodUsu
                                            Delete from CPMATRICULA where OidCPAgenda = @OidCPAgenda and GNCodUsu = @GNCodUsu", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);
                command.Parameters.AddWithValue("GNCodUsu", idUsuario);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPMATRICULA"
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
        public static void SetAsistenciaCapVirtualDoc(int idUsuario)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" UPDATE M SET M.Asistido = 1 from  CPMATRICULA M
	                                            INNER JOIN CPAgenda A ON A.OidCPAgenda = M.OidCPAgenda
	                                            INNER JOIN CPCAPACITACION C ON C.OidCPCAPACITACION = A.OidCPCapacitacion
                                            where A.Modalidad = 'Virtual documental' and M.GNCodUsu = @GNCodUsu", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu",idUsuario);
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

        public static List<CPMatricula>  GetMatriculasFirmadasByAgenda(int idAgenda)
        {
            List<CPMatricula> matriculas = new List<CPMatricula>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT * FROM CPMATRICULA WHERE OidCPAgenda = @OidCPAgenda AND Firmado = 1", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);
                
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    matriculas.Add(new CPMatricula {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matriculas;
        }

        public static List<dynamic> GetInformeMatricula(string tema, string documento, string nomUsuario, string unidad)
        {
            List<dynamic> datos = new List<dynamic>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT DISTINCT  C.TEMA,M.GNCodUsu 'Documento', M.NOMUSUARIO,  D.GnNomDep UNIDAD ,
	                                            IIF(M2.Asistido IS NULL,'No Asistido', 'Asistido') 'Asistencia',
	                                            IIF(ISNULL(M2.Firmado, 0) = 1, 'Firmado','No Firmado' ) 'Firma'
                                            FROM CPMATRICULA M
	                                            INNER JOIN CPCAPACITACION C ON C.OidCPCAPACITACION = M.OidCPCAPACITACION
	                                            LEFT JOIN CPMATRICULA M2 ON M2.OidCPMATRICULA = M.OidCPMATRICULA AND M2.Asistido = 1
	                                            INNER JOIN Usuario U ON U.GNCodUsu = M.GNCodUsu
	                                            INNER JOIN Departamento D ON D.GnDcDep = U.GnDcDep
                                                INNER JOIN CPAgenda A ON A.OidCPCapacitacion = C.OidCPCAPACITACION
                                            WHERE C.TEMA LIKE '%'+ @TEMA +'%' AND M.GNCodUsu LIKE '%'+ @GNCodUsu +'%' AND
	                                            M.NOMUSUARIO LIKE '%'+ @NOMUSUARIO +'%' AND D.GnNomDep LIKE '%'+ @UNIDAD +'%' AND A.ESTADO > 2
                                            ORDER BY c.TEMA, D.GnNomDep, M.NOMUSUARIO ", conexion.OpenConnection());

                command.Parameters.AddWithValue("TEMA", tema);
                command.Parameters.AddWithValue("GNCodUsu", documento);
                command.Parameters.AddWithValue("NOMUSUARIO", nomUsuario);
                command.Parameters.AddWithValue("UNIDAD", unidad);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    datos.Add(new { 
                        Tema = reader["TEMA"].ToString(),
                        Documento = reader["Documento"].ToString(),
                        Nombre = reader["NOMUSUARIO"].ToString(),
                        Unidad = reader["UNIDAD"].ToString(),
                        Asistencia = reader["Asistencia"].ToString(),
                        Firma = reader["Firma"].ToString()
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

            return datos;
        }

        public static List<CPMatricula> GetMatriculasByAgenda(int idAgenda)
        {
            List<CPMatricula> matriculas = new List<CPMatricula>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT * FROM CPMATRICULA WHERE OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCPAgenda", idAgenda);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    matriculas.Add(new CPMatricula
                    {
                        BlnESTADO = Convert.ToBoolean(reader["ESTADO"].ToString()),
                        DtmFECHA = reader["FECHA"].ToString() == "" ? Convert.ToDateTime("1/1/1970") : Convert.ToDateTime(reader["FECHA"].ToString()),
                        IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString()),
                        IntOidCPMatricula = Convert.ToInt32(reader["OidCPMatricula"].ToString()),
                        StrCARGO = reader["CARGO"].ToString(),
                        StrEMAIL = reader["EMAIL"].ToString(),
                        StrNOMUSUARIO = reader["NOMUSUARIO"].ToString(),
                        StrTELEFONO = reader["TELEFONO"].ToString(),
                        StrUNIDAD = reader["UNIDAD"].ToString(),
                        BlnAsistido = Convert.ToBoolean(reader["Asistido"]),
                        BlnFirmado = Convert.ToBoolean(reader["Firmado"]),
                        IntMetodoMatri = reader["MetodoMatri"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MetodoMatri"]),
                        IntValorMetodoMatr = reader["ValorMetodoMatr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ValorMetodoMatr"]),
                        StrNombreMetodoMatr = reader["NombreMetodoMatr"].ToString(),
                        IntOidCPAgenda = Convert.ToInt32(reader["OidCPAgenda"]),
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

            return matriculas;
        }

        public static dynamic GetEstadisticaCapacitaciones()
        {
            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["admin"]);
            dynamic estadisticas = null;

            Conexion conexion = new Conexion();
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = conexion.OpenConnection();
                    command.CommandText = @"SELECT 
	                    (SELECT COUNT(*) FROM CPMATRICULA WHERE Asistido = 0 AND GNCodUsu = U.GNCodUsu) Inasistido,
	                    (select COUNT(*) from CPMATRICULA where Asistido = 1 and Firmado = 0 and GNCodUsu = U.GNCodUsu) 'No firmado',
	                    (select COUNT(*) from CPMATRICULA where Firmado = 1 and GNCodUsu = U.GNCodUsu) 'Firmado'
                    FROM Usuario U
                    WHERE GNCodUsu = @GNCodUsu";

                    command.Parameters.AddWithValue("GNCodUsu", idUsuario);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas = new
                            {
                                Inasistido = reader["Inasistido"],
                                NoFirmado = reader["No firmado"],
                                Firmado = reader["Firmado"]
                            };
                        }
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
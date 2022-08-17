
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
    public class DAOCPCapacitacion
    {

        public static CPCAPACITACION GetCapacitacion(int idCapacitacion)
        {
            CPCAPACITACION capacitacion = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPCAPACITACION where OidCPCAPACITACION = @OidCPCAPACITACION", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPCAPACITACION", idCapacitacion);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
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
            return capacitacion;
        } 


        public static List<CPCAPACITACION> GetCapacitacionesMatriculadas(int idUsuario)
        {
            List<CPCAPACITACION> capacitaciones = new List<CPCAPACITACION>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM CPCAPACITACION  where OidCPCAPACITACION in (select OidCPCAPACITACION from CPMatricula where GNCodUsu = @IDUSUARIO)", conexion.OpenConnection());

                command.Parameters.AddWithValue("@IDUSUARIO", idUsuario);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CPCAPACITACION capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitaciones.Add(capacitacion);
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
            return capacitaciones;
        }

        public static CPCAPACITACION GetCapacitacionUlt()
        {
            CPCAPACITACION capacitacion = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT TOP(1) * FROM CPCAPACITACION ORDER BY OidCPCAPACITACION DESC ", conexion.OpenConnection());


                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());

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
            return capacitacion;
        }

        public static List<CPCAPACITACION> GetCapacitacionesAsistidas(int idUsuario)
        {
            List<CPCAPACITACION> capacitaciones = new List<CPCAPACITACION>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT c.* from CPCAPACITACION as c"+
                                         "   left join CPMatricula as m on m.OidCPCAPACITACION = c.OidCPCAPACITACION"+
                                         "   left join CPAsistencia as a on a.OidCPMATRICULA = m.OidCPMATRICULA"+
                                         "   where a.GNCodUsu = @IDUSUARIO", conexion.OpenConnection());

                command.Parameters.AddWithValue("@IDUSUARIO", idUsuario);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CPCAPACITACION capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO= reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["firma"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitaciones.Add(capacitacion);
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
            return capacitaciones;
        }

        public static int setCPCAPACITACION(CPCAPACITACION Capacitacion)
        {
            int idCapacitacion = 0;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[CPCAPACITACION]
                                                   ([CODIGO]
                                                   ,[FECHA]
                                                   ,[HORAINICIAL]
                                                   ,[HORAFINAL]
                                                   ,[LUGAR]
                                                   ,[UNIDADFUNCIONAL]
                                                   ,[OidCPEJETEMA]
                                                   ,[TEMA]
                                                   ,[ESTADO]
                                                   ,[MODALIDAD]
                                                   ,[RESPONSABLE]
                                                   ,[GNCodUsu]
                                                   ,[LINK]
                                                   ,[TempFirma]
                                                   ,[FechaFirma] 
                                                   ,[OidGDDocumento] 
                                                   ,[OidListArch]) 
                                             VALUES
                                                   ((select CONCAT('ACT-GEC-', ISNULL(max(Convert(int, REPLACE(CODIGO, 'ACT-GEC-', ''))) + 1,1))  AS CODIGO from CPCAPACITACION)
                                                   ,@FECHA
                                                   ,@HORAINICIAL
                                                   ,@HORAFINAL
                                                   ,@LUGAR
                                                   ,@UNIDADFUNCIONAL
                                                   ,@OidCPEJETEMA
                                                   ,@TEMA
                                                   ,@ESTADO
                                                   ,@MODALIDAD
                                                   ,@RESPONSABLE
                                                   ,@GNCodUsu
                                                   ,@LINK
                                                   ,@TempFirma
                                                   ,@FechaFirma 
                                                   ,1 
                                                   ,@OidListArch)
                                            select CAST(SCOPE_IDENTITY() as int)", conexion.OpenConnection());

                command.Parameters.AddWithValue("@FECHA", Capacitacion.DtmFECHA);
                command.Parameters.AddWithValue("@HORAINICIAL", Capacitacion.DtmHORAINICIAL);
                command.Parameters.AddWithValue("@HORAFINAL", Capacitacion.DtmHORAFINAL);
                command.Parameters.AddWithValue("@UNIDADFUNCIONAL", Capacitacion.StrUNIDADFUNCIONAL);
                command.Parameters.AddWithValue("@OidCPEJETEMA", Capacitacion.IntOidCPEJETEMA);
                command.Parameters.AddWithValue("@TEMA", Capacitacion.StrTEMA);
                command.Parameters.AddWithValue("@MODALIDAD", Capacitacion.StrMODALIDAD);
                command.Parameters.AddWithValue("@RESPONSABLE", Capacitacion.StrRESPONSABLE);
                command.Parameters.AddWithValue("@GNCodUsu", Capacitacion.IntGNCodUsu);
                command.Parameters.AddWithValue("@LINK", Capacitacion.StrLINK);
                command.Parameters.AddWithValue("@OidListArch", Capacitacion.IntOidListArch);
                command.Parameters.AddWithValue("@LUGAR", Capacitacion.StrLUGAR);
                command.Parameters.AddWithValue("@ESTADO", Capacitacion.StrESTADO ==  "1" || Capacitacion.StrESTADO == "true");
                command.Parameters.AddWithValue("@TempFirma", Capacitacion.IntTempFirma);
                command.Parameters.AddWithValue("@FechaFirma", Capacitacion.DtmFechaFirma);


                idCapacitacion = (int)command.ExecuteScalar();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idCapacitacion,
                    strAccion = "Crear",
                    strDetalle = $"Se crea la capacitación con tema: {Capacitacion.StrTEMA}",
                    strEntidad = "CPCAPACITACION"
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

            return idCapacitacion;
        }


        public static void SetCapacitacionBySolcitud(int idSolicitud)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("declare @codigo int "+
                                         "       set @codigo = (select max(Convert(int, REPLACE(CODIGO, 'ACT-ENT-', '')))  AS CODIGO from CPCAPACITACION)"+
                                         "       INSERT INTO[dbo].[CPCAPACITACION]"+
                                         "                       ([CODIGO]"+
                                         "                 ,[FECHA]"+
                                         "                 ,[HORAINICIAL]"+
                                         "                 ,[HORAFINAL]"+
                                         "                 ,[LUGAR]"+
                                         "                 ,[UNIDADFUNCIONAL]"+
                                         "                 ,[OidCPEJETEMA]"+
                                         "                 ,[TEMA]"+
                                         "                 ,[ESTADO]"+
                                         "                 ,[MODALIDAD]"+
                                         "                 ,[RESPONSABLE]"+
                                         "                 ,[GNCodUsu]"+
                                         "                 ,[LINK]"+
                                         "                 ,[OidListArch])"+
                                         "            SELECT"+
                                         "               iif(@codigo is null, 'ACT-ENT-1', CONCAT('ACT-ENT-', @codigo + 1)) as codigo"+
                                         "             ,[Fecha]"+
                                         "             ,[HoraInicial]"+
                                         "             ,[HoraFinal]"+
                                         "             ,[Lugar]"+
                                         "             ,[UnidadFuncional]"+
                                         "             ,[OidCPEjeTematico]"+
                                         "             ,[Tema]"+
                                         "             ,1"+
                                         "             ,[Modalidad]"+
                                         "             ,[Responsable]"+
                                         "             ,[GNCodUsu]"+
                                         "             ,[Link]"+
                                         "             ,[OidListaArchivos]"+
                                         "                       FROM[dbo].[CPSolicitud]  where OidCpsolicitud = @OidCpsolicitud"+
                                         "       update CPSubtema set OidCPInstacia = (select top(1) OidCPCAPACITACION from CPCAPACITACION order by OidCPCAPACITACION desc) "+
                                         "       where OidCPInstacia = @OidCpsolicitud and OidGDDocumento = 1  ", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidCpsolicitud", idSolicitud);

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

        public static string getCodCapa()
        {
            string codigo = "";

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select max(Convert(int,REPLACE(CODIGO,'ACT-GEC-',''))) AS CODIGO from CPCAPACITACION", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                    codigo = "ACT-GEC-" + (Convert.ToInt32(reader["CODIGO"].ToString())+1);
                else
                    codigo = "ACT-GEC-1";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return codigo;
        }

        public static List<CPCAPACITACION> GetCapsByResp(int idResponsable)
        {
            List<CPCAPACITACION> capacitaciones = new List<CPCAPACITACION>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from CPCapacitacion where GNCodUsu = @GNCodUsu", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GNCodUsu", idResponsable);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPCAPACITACION capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitaciones.Add(capacitacion);
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
            return capacitaciones;
        }
       

        public static List<CPCAPACITACION> GetCapsByResp(string info)
        {
            List<CPCAPACITACION> capacitaciones = new List<CPCAPACITACION>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"select * from CPCAPACITACION C where CONCAT(C.CODIGO,C.TEMA,C.RESPONSABLE) like '%' + @info + '%' order by OidCPCAPACITACION desc ", conexion.OpenConnection());

                command.Parameters.AddWithValue("info", info);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPCAPACITACION capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitaciones.Add(capacitacion);
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
            return capacitaciones;
        }

        public static List<CPCAPACITACION> GetCapsByResp(string info, int idResponsable)
        {
            List<CPCAPACITACION> capacitaciones = new List<CPCAPACITACION>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"select * from CPCAPACITACION C where CONCAT(C.CODIGO,C.TEMA,C.RESPONSABLE) like '%' + @info + '%' and GNCodUsu = @GNCodUsu order by OidCPCAPACITACION desc ", conexion.OpenConnection());

                command.Parameters.AddWithValue("info", info);
                command.Parameters.AddWithValue("GNCodUsu", idResponsable);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CPCAPACITACION capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["ESTADO"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitaciones.Add(capacitacion);
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
            return capacitaciones;
        }

       
        public static  bool update(CPCAPACITACION Capacitacion)
        {
            bool isUdated = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[CPCAPACITACION]"+
                                         "      SET[CODIGO] = @CODIGO"+
                                         "         ,[FECHA] = @FECHA"+
                                         "         ,[HORAINICIAL] = @HORAINICIAL"+
                                         "         ,[HORAFINAL] = @HORAFINAL"+
                                         "         ,[LUGAR] = @LUGAR"+
                                         "         ,[UNIDADFUNCIONAL] = @UNIDADFUNCIONAL"+
                                         "         ,[OidCPEJETEMA] = @OidCPEJETEMA"+
                                         "         ,[TEMA] = @TEMA"+
                                         "         ,[ESTADO] = @ESTADO"+
                                         "         ,[MODALIDAD] = @MODALIDAD"+
                                         "         ,[RESPONSABLE] = @RESPONSABLE"+
                                         "         ,[GNCodUsu] = @GNCodUsu"+
                                         "         ,[LINK] = @LINK"+
                                         "         ,[TempFirma] = @TempFirma"+
                                         "         ,[OidListArch] = @OidListArch"+
                                         "    WHERE  OidCPCAPACITACION = @OidCPCAPACITACION", conexion.OpenConnection());

                command.Parameters.AddWithValue("@CODIGO", Capacitacion.StrCODIGO);
                command.Parameters.AddWithValue("@FECHA", Capacitacion.DtmFECHA);
                command.Parameters.AddWithValue("@HORAINICIAL", Capacitacion.DtmHORAINICIAL);
                command.Parameters.AddWithValue("@HORAFINAL", Capacitacion.DtmHORAFINAL);
                command.Parameters.AddWithValue("@UNIDADFUNCIONAL", Capacitacion.StrUNIDADFUNCIONAL);
                command.Parameters.AddWithValue("@OidCPEJETEMA", Capacitacion.IntOidCPEJETEMA);
                command.Parameters.AddWithValue("@TEMA", Capacitacion.StrTEMA);
                command.Parameters.AddWithValue("@MODALIDAD", Capacitacion.StrMODALIDAD);
                command.Parameters.AddWithValue("@RESPONSABLE", Capacitacion.StrRESPONSABLE);
                command.Parameters.AddWithValue("@GNCodUsu", Capacitacion.IntGNCodUsu);
                command.Parameters.AddWithValue("@LINK", Capacitacion.StrLINK);
                command.Parameters.AddWithValue("@OidListArch", Capacitacion.IntOidListArch);
                command.Parameters.AddWithValue("@LUGAR", Capacitacion.StrLUGAR);
                command.Parameters.AddWithValue("@ESTADO", Convert.ToBoolean(Capacitacion.StrESTADO));
                command.Parameters.AddWithValue("@OidCPCAPACITACION", Capacitacion.IntOidCPCAPACITACION);
                command.Parameters.AddWithValue("@TempFirma", Capacitacion.IntTempFirma);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Capacitacion.IntOidCPCAPACITACION,
                    strAccion = "Modificar",
                    strDetalle = $"Se edita el contenido en general de la capacitacion con tema {Capacitacion.StrTEMA}",
                    strEntidad = "CPCAPACITACION"
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

            return isUdated;
        }
        /// <summary>
        /// Metodo que crea una capacitacion desde la cracion de un documento en getion documental
        /// </summary>
        /// <param name="idDocumento"></param>
        public static List<int> CreateCapacitacionFromDoc(int idDocumento)
        {
            List<int> ids = new List<int>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"insert into GNListaArchivos (OidGNModulo) values (5)
                                            Declare @idLista int;
                                                            Declare @codigo varchar(20);
                                                            Declare @Tema varchar(250);
                                                            Declare @Responsable varchar(500);
                                                            Declare @idUsuario numeric(18, 0);
                                                            set @idLista = (select top(1) OidGNListaArchivos from GNListaArchivos order by OidGNListaArchivos desc)
                                            set @codigo = (
                                                                select iif(
                                                                    (select max(Convert(int, REPLACE(CODIGO, 'ACT-GEC-', '')))  from CPCAPACITACION) is null, 
                                                                    1,(select max(Convert(int, REPLACE(CODIGO, 'ACT-GEC-', ''))) from CPCAPACITACION)) + 1
                                                                )
                                            set @Tema = (select 'Socialización del documento ' + NomDoc from GDDocumento where OidGDDocumento = @OidGDDocumento)
                                            set @Responsable = (
                                                    select U.GNNomUsu from Usuario U
                                                        left join GDSolicitud S on S.CodUsu = U.GNCodUsu
                                                        left join GDDocumento D on D.OidGDSolicitud = S.OidGDSolicitud
                                                        where D.OidGDDocumento = @OidGDDocumento
                                                    )
                                            set @idUsuario = (
                                                        select S.CodUsu from GDSolicitud S
                                                        left join GDDocumento D on D.OidGDSolicitud = S.OidGDSolicitud
                                                        where D.OidGDDocumento = @OidGDDocumento
                                                    )
                                            INSERT [CPCAPACITACION]
                                                            ([CODIGO]
                                                        ,[FECHA]
                                                        ,[HORAINICIAL]
                                                        ,[HORAFINAL]
                                                        ,[LUGAR]
                                                        ,[UNIDADFUNCIONAL]
                                                        ,[OidCPEJETEMA]
                                                        ,[TEMA]
                                                        ,[ESTADO]
                                                        ,[MODALIDAD]
                                                        ,[RESPONSABLE]
                                                        ,[GNCodUsu]
                                                        ,[LINK]
                                                        ,[OidGDDocumento] 
                                                        ,[TempFirma] 
                                                        ,[FechaFirma] 
                                                        ,[OidListArch])
                                                select
                                                    'ACT-GEC-' + @codigo,
                                                        (GETDATE() +  1),
                                                        '12:00',
                                                        '12:00',
                                                        'Virtual',
                                                        '',
                                                        D.OidCPEJETEMATICO,
                                                        @Tema,
                                                        1,
                                                        'Virtual documental',
                                                        @Responsable,
                                                        @idUsuario,
                                                        '',
                                                        @OidGDDocumento, 
                                                        D.TempFirma, 
                                                        (GETDATE() + D.TempFirma), 
                                                        @idLista 
                                                    from GDDivulgacion D where D.OidGDDocumento = @OidGDDocumento 
                                            Declare @idCapacitacion int
                                            set @idCapacitacion = (select top(1) OidCPCAPACITACION from CPCAPACITACION order by 1 desc) 
                                            Declare @idExamen int = (select OidCPEXAMEN from CPEXAMEN where OidInstancia = @OidGDDocumento and Contexto = 2) 

                                            INSERT INTO [dbo].[CPAgenda]
                                                       ([Fecha]
                                                       ,[FechaFirma]
                                                       ,[HoraInicial]
                                                       ,[HoraFinal]
                                                       ,[OidCPCapacitacion]
                                                       ,[LinkReunion]
                                                       ,[LinkAnexo]
                                                       ,[OidGNListaArchivos]
                                                       ,[TiempoFirma]
                                                       ,[Estado]
                                                       ,[Modalidad]
                                                       ,[Lugar]
                                                       ,[Responsable]
                                                       ,[OidCPExamen]
                                                       ,[OidUsuarioResponsable]
                                                       ,[FechaFinal]
                                                       ,[Facilitador])
                                                 select
                                                        (GETDATE() +  1),
			                                            (GETDATE() + D.TempFirma), 
                                                        '12:00',
                                                        '12:00',
			                                             @idCapacitacion,
                                                        '',
			                                            '',
			                                            @idLista,
			                                            D.TempFirma,
			                                            1,
			                                            'Virtual documental',
			                                            'Virtual',
			                                            @Responsable,
			                                            @idExamen,
			                                            @idUsuario,
			                                            (GETDATE() +  30),
			                                            '' 
                                                    from GDDivulgacion D where D.OidGDDocumento = @OidGDDocumento 
                                            update CPEXAMEN set OidInstancia = @idCapacitacion, Contexto = 1
                                            where OidInstancia = @OidGDDocumento and Contexto = 2
                                            Declare @idAgenda int = (select top (1) OidCPAgenda from CPAgenda order by OidCPAgenda desc)
                                            select @idCapacitacion  OidCPCAPACITACION, @idAgenda as idAgenda", conexion.OpenConnection());


                

                command.Parameters.AddWithValue("@OidGDDocumento", idDocumento);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ids = new List<int> {Convert.ToInt32(reader["OidCPCAPACITACION"]), Convert.ToInt32(reader["idAgenda"]) };
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

            return ids;
        }
        public static CPCAPACITACION GetCapacitacionByAgenda(int idAgenda)
        {
            CPCAPACITACION capacitacion = null;


            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT  C.* FROM CPCAPACITACION C
	                                        INNER JOIN CPAgenda A ON A.OidCPCapacitacion = C.OidCPCAPACITACION AND A.OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);

                reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    capacitacion = new CPCAPACITACION();
                    capacitacion.IntOidListArch = reader["OidListArch"].ToString() == "" ? 0 : Convert.ToInt32(reader["OidListArch"]);
                    capacitacion.IntOidCPEJETEMA = Convert.ToInt32(reader["OidCPEJETEMA"].ToString());
                    capacitacion.StrESTADO = reader["Estado"].ToString();
                    capacitacion.IntOidCPCAPACITACION = Convert.ToInt32(reader["OidCPCAPACITACION"].ToString());
                    capacitacion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    capacitacion.StrLINK = reader["LINK"].ToString();
                    capacitacion.StrLUGAR = reader["LUGAR"].ToString();
                    capacitacion.StrMODALIDAD = reader["MODALIDAD"].ToString();
                    capacitacion.StrRESPONSABLE = reader["RESPONSABLE"].ToString();
                    capacitacion.StrTEMA = reader["TEMA"].ToString();
                    capacitacion.StrUNIDADFUNCIONAL = reader["UNIDADFUNCIONAL"].ToString();
                    capacitacion.DtmFECHA = Convert.ToDateTime(reader["FECHA"].ToString());
                    capacitacion.DtmHORAFINAL = Convert.ToDateTime(reader["HORAFINAL"].ToString());
                    capacitacion.DtmHORAINICIAL = Convert.ToDateTime(reader["HORAINICIAL"].ToString());
                    capacitacion.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    capacitacion.IntTempFirma = Convert.ToInt32(reader["TempFirma"].ToString() == "" ? "0" : reader["TempFirma"].ToString());
                    capacitacion.DtmFechaFirma = Convert.ToDateTime(reader["FechaFirma"].ToString() == "" ? "01/01/1900" : reader["FechaFirma"].ToString());
                    capacitacion.StrCODIGO = reader["CODIGO"].ToString();
                    
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

            return capacitacion;
        }
    }
}
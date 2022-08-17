using Entidades.Facturacion;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Facturacion
{
    public class PCensoDiario
    {

        public static List<ECensoDiario> GetInfoCensoDiario()
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                                      (SELECT COUNT(*)
                                       FROM UsuariosCierreAut
                                       WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                                                    (CASE GENPACIEN.PACTIPDOC
                                                         WHEN '1' THEN 'CC'
                                                         WHEN '2' THEN 'CE'
                                                         WHEN '3' THEN 'TI'
                                                         WHEN '4' THEN 'RC'
                                                         WHEN '5' THEN 'PA'
                                                         WHEN '6' THEN 'ASI'
                                                         WHEN '7' THEN 'MSI'
                                                         WHEN '8' THEN 'CNV'
                                                         WHEN '9' THEN 'CC'
                                                         WHEN '10' THEN 'CNV'
                                                         WHEN '11' THEN 'SC'
                                                         WHEN '12' THEN 'PEP'
                                                         WHEN '13' THEN 'PE'
                                                     END) AS DOC_TIPO,
                                                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE + ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                                                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                                                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                                                    HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                                                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                                                    ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                                    FROM dgempres01..HPNESTANC
	                                    INNER JOIN HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
	                                    INNER JOIN ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
	                                    INNER JOIN GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
	                                    INNER JOIN GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
	                                    INNER JOIN HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
	                                    INNER JOIN GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                                    FULL OUTER JOIN
                                      (SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                                              HCNFOLIO.ADNINGRESO AS OIDINGRESO
                                       FROM HCNDIAPAC
		                                    INNER JOIN HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                                       GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
		                                    INNER JOIN GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
		                                    INNER JOIN GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
		                                    INNER JOIN HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
		                                    INNER JOIN HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
		                                    INNER JOIN GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
		                                    LEFT JOIN NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
		                                    LEFT JOIN BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
		                                    LEFT JOIN HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                                    WHERE ADNINGRESO.AINESTADO IN (0, 3)
                                    ORDER BY HPNGRUPOS.HGRNOMBRE,
                                             HPNSUBGRU.HSUNOMBRE";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

        public static List<ECensoDiario> GetInfoDetaCenso(string id)
        {

            List<ECensoDiario> infoDCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                    (SELECT COUNT(*) FROM RegistroAutorizaciones WHERE Identificacion = GENPACIEN.PACNUMDOC 
                    AND UbicacionPaciente = HPNGRUPOS.HGRNOMBRE AND NomTecnologia LIKE '%'+GENSERIPS.SIPCODCUP+'%' 
                    AND FORMAT(FechaIngreso, 'yyyy-MM-dd') = FORMAT(ADNINGRESO.AINFECING, 'yyyy-MM-dd')) AS 'RESULTADO',
                    GENPACIEN.PACNUMDOC AS DOCUMENTO,
                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                    ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                    (CASE HPNGRUPOS.OID WHEN '1' THEN 'SERVICIO ELECTIVO'
                    WHEN '3' THEN 'SERVICIOS ELECTIVOS' WHEN '3' THEN 'TI'
                    WHEN '4' THEN 'SERVICIOS ELECTIVOS' WHEN '5' THEN 'POSTERIOR URGENCIAS'
                    WHEN '6' THEN 'SERVICIOS ELECTIVOS' END) AS 'TIPO SERVICIO SOLICITADO',
                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                    GENSERIPS.SIPCODCUP AS CUPS,
                    GENSERIPS.SIPNOMBRE AS NOMBRECUPS,
                    HPNDEFCAM.HCACODIGO AS 'CAMA',
                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO'
                    FROM dgempres01..HPNESTANC
                    INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                    INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                    INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                    INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                    INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                    GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                    INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                    FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                    HCNFOLIO.ADNINGRESO AS OIDINGRESO
                    FROM dgempres01..HCNDIAPAC
                    INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                    GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                    INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                    INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                    INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                    INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                    INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                    LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                    LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                    LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                    WHERE GENPACIEN.PACNUMDOC = @idPaciente AND ADNINGRESO.AINESTADO IN (0,3) 
                    GROUP BY GENPACIEN.PACNUMDOC,
                    HPNGRUPOS.HGRNOMBRE,ADNINGRESO.AINFECING, GENSERIPS.SIPCODCUP, HPNESTANC.ADNINGRES,GENDIAGNO.DIACODIGO,
                    GENDIAGNO.DIANOMBRE,GENPACIEN.PACTIPDOC,GENPACIEN.PACPRINOM, GENPACIEN.PACSEGNOM,GENPACIEN.PACPRIAPE,
                    GENPACIEN.PACSEGAPE, HPNGRUPOS.OID,HPNDEFCAM.HCAOBSHOS,
                    ADNINGRESO.AINCONSEC,HPNDEFCAM1.HCACODIGO, GENCONTRA.GECCONTRA, GENCONTRA.GECNOMENT, NOTA19941.CLASIFICACION, BIPGPCLASI.NOMCLASIFICACION,
                    HPNDEFCAM1.HCACODIGO, GENSERIPS.SIPCODCUP, GENSERIPS.SIPDESCUP,GENDIAGNO.DIACODIGO,GENDIAGNO.DIANOMBRE, NOTA19941.CUPS,NOTA19941.DESCRIPCUPS,
                    GENPACIEN.GPAFECNAC, ADNINGRESO.AINFECING, GENPACIEN.GPASEXPAC, GENDETCON.GDECODIGO,GENDETCON.GDENOMBRE, GENSERIPS.SIPNOMBRE, HPNDEFCAM.HCACODIGO
                    ORDER BY HPNGRUPOS.HGRNOMBRE";

                command.Parameters.AddWithValue("idPaciente", id);
                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    ECensoDiario infoDCensoI = new ECensoDiario
                    {
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        TipoServicio = reader["TIPO SERVICIO SOLICITADO"].ToString(),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        Cups = reader["CUPS"].ToString(),
                        NombreCups = reader["NOMBRECUPS"].ToString(),
                        Cama = reader["CAMA"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        Resultado = Convert.ToInt32(reader["RESULTADO"].ToString()),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString()
                    };
                    infoDCenso.Add(infoDCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoDCenso;
        }

        public static void setCierrePaciente(string motivoCierre, string Admision)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string usuarioC = usuario.GNNomUsu1;
            string fechaC = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string estado = "CERRADO";

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"INSERT INTO UsuariosCierreAut (NumIngreso, UsuarioAccion, Estado, Fecha, Motivo) " +
                "VALUES(@Admision, @usuarioC, @estado, @fechaC, @motivoCierre)";

                command.Parameters.AddWithValue("motivoCierre", motivoCierre);
                command.Parameters.AddWithValue("Admision", Admision);
                command.Parameters.AddWithValue("usuarioC", usuarioC);
                command.Parameters.AddWithValue("fechaC", fechaC);
                command.Parameters.AddWithValue("estado", estado);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }
        }

        public static List<ECensoDiario> getfiltroGrupo(string grupo, string subgrupo)
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                    DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                    (SELECT COUNT(*) FROM UsuariosCierreAut WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                    (CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                    WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                    WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                    WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                    WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                    ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                    HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                    ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                    FROM dgempres01..HPNESTANC
                    INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                    INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                    INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                    INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                    INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                    GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                    INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                    FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                    HCNFOLIO.ADNINGRESO AS OIDINGRESO
                    FROM dgempres01..HCNDIAPAC
                    INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                    GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                    INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                    INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                    INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                    INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                    INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                    LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                    LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                    LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                    WHERE ADNINGRESO.AINESTADO IN (0,3) AND HPNGRUPOS.HGRNOMBRE LIKE '%'+@grupo+'%' AND HPNSUBGRU.HSUNOMBRE LIKE '%'+@subgrupo+'%'
                    ORDER BY HPNGRUPOS.HGRNOMBRE, HPNSUBGRU.HSUNOMBRE";

                command.Parameters.AddWithValue("grupo", grupo);
                command.Parameters.AddWithValue("subgrupo", subgrupo);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

        public static List<ECensoDiario> getfiltroNumId(string numID)
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                    DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                    (SELECT COUNT(*) FROM UsuariosCierreAut WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                    (CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                    WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                    WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                    WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                    WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                    ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                    HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                    ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                    FROM dgempres01..HPNESTANC
                    INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                    INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                    INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                    INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                    INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                    GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                    INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                    FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                    HCNFOLIO.ADNINGRESO AS OIDINGRESO
                    FROM dgempres01..HCNDIAPAC
                    INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                    GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                    INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                    INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                    INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                    INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                    INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                    LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                    LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                    LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                    WHERE ADNINGRESO.AINESTADO IN (0,3) AND GENPACIEN.PACNUMDOC LIKE '%'+@numID+'%'
                    ORDER BY HPNGRUPOS.HGRNOMBRE, HPNSUBGRU.HSUNOMBRE";

                command.Parameters.AddWithValue("numID", numID);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

        public static List<ECensoDiario> getfiltroNombre(string nombrePaciente)
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                    DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                    (SELECT COUNT(*) FROM UsuariosCierreAut WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                    (CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                    WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                    WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                    WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                    WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                    ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                    HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                    ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                    FROM dgempres01..HPNESTANC
                    INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                    INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                    INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                    INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                    INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                    GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                    INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                    FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                    HCNFOLIO.ADNINGRESO AS OIDINGRESO
                    FROM dgempres01..HCNDIAPAC
                    INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                    GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                    INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                    INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                    INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                    INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                    INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                    LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                    LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                    LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                    WHERE ADNINGRESO.AINESTADO IN (0,3) AND GENPACIEN.GPANOMCOM LIKE '%'+@nombrePaciente+'%'
                    ORDER BY HPNGRUPOS.HGRNOMBRE, HPNSUBGRU.HSUNOMBRE";

                command.Parameters.AddWithValue("nombrePaciente", nombrePaciente);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

        public static List<ECensoDiario> getfiltroFecha(string filtroFecha)
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                        DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                        (SELECT COUNT(*) FROM UsuariosCierreAut WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                        (CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                        WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                        WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                        WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                        WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                        (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                        ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                        ADNINGRESO.AINFECING AS 'FEC INGRESO',
                        HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                        HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                        ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                        ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                        FROM dgempres01..HPNESTANC
                        INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                        INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                        INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                        INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                        INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                        GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                        INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                        FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                        HCNFOLIO.ADNINGRESO AS OIDINGRESO
                        FROM dgempres01..HCNDIAPAC
                        INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                        GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                        INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                        INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                        INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                        INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                        INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                        LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                        LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                        LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                        WHERE ADNINGRESO.AINESTADO IN (0,3) AND FORMAT(ADNINGRESO.AINFECING, 'yyyy-MM-dd') = @filtroFecha                    
                        ORDER BY HPNGRUPOS.HGRNOMBRE, HPNSUBGRU.HSUNOMBRE";

                command.Parameters.AddWithValue("filtroFecha", filtroFecha);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

        public static List<ECensoDiario> getfiltroIngreso(string numeroIngreso)
        {

            List<ECensoDiario> infoCenso = new List<ECensoDiario>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                    DISTINCT GENPACIEN.PACNUMDOC AS DOCUMENTO,
                    (SELECT COUNT(*) FROM UsuariosCierreAut WHERE NumIngreso = ADNINGRESO.AINCONSEC) AS 'VALIDACION CIERRE',
                    (CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                    WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                    WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                    WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                    WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                    (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                    ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                    ADNINGRESO.AINFECING AS 'FEC INGRESO',
                    HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                    HPNSUBGRU.HSUNOMBRE AS 'SUBGRUPO',
                    ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO',
                    ADNINGRESO.AINFECEGRE AS 'FECHA DE SALIDA'
                    FROM dgempres01..HPNESTANC
                    INNER JOIN dgempres01..HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                    INNER JOIN dgempres01..ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                    INNER JOIN dgempres01..GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                    INNER JOIN dgempres01..GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                    INNER JOIN dgempres01..HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                    GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                    INNER JOIN dgempres01..GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                    FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                    HCNFOLIO.ADNINGRESO AS OIDINGRESO
                    FROM dgempres01..HCNDIAPAC
                    INNER JOIN dgempres01..HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                    GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                    INNER JOIN dgempres01..GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                    INNER JOIN dgempres01..GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                    INNER JOIN dgempres01..HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                    INNER JOIN dgempres01..HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                    INNER JOIN dgempres01..GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                    LEFT JOIN dgempres01..NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                    LEFT JOIN dgempres01..BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                    LEFT JOIN dgempres01..HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                    WHERE ADNINGRESO.AINESTADO IN (0,3) AND ADNINGRESO.AINCONSEC LIKE '%'+@numeroIngreso+'%'                    
                    ORDER BY HPNGRUPOS.HGRNOMBRE, HPNSUBGRU.HSUNOMBRE";

                command.Parameters.AddWithValue("numeroIngreso", numeroIngreso);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ECensoDiario infoCensoI = new ECensoDiario
                    {
                        TipoIdentificacion = reader["DOC_TIPO"].ToString(),
                        NumeroIdentificacion = reader["DOCUMENTO"].ToString(),
                        NombresApellidos = reader["NOMPAC"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        UnidadFuncional = reader["UNIDAD FUNCIONAL"].ToString(),
                        UnidadFuncionalSubgrupo = reader["SUBGRUPO"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        ResultadoCierre = Convert.ToInt32(reader["VALIDACION CIERRE"].ToString()),
                        FechaEgreso = (reader["FECHA DE SALIDA"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA DE SALIDA"].ToString()),
                    };
                    infoCenso.Add(infoCensoI);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoCenso;
        }

    }
}